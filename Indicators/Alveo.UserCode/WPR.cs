using Alveo.Common.Classes;
using Alveo.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Larry William's percent range Indicator")]
	[Serializable]
	public class WPR : IndicatorBase
	{
		private readonly Array<double> _vals;

		[Category("Settings"), Description("Period of the WPR Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		public WPR()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("WPR({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("WPR({0})", this.IndicatorPeriod));
			this._vals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("WPR({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("WPR({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._vals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			bool flag = i > base.Bars - this.IndicatorPeriod;
			if (flag)
			{
				i = base.Bars - this.IndicatorPeriod;
			}
			Array<Bar> history = base.GetHistory(base.Symbol, base.TimeFrame);
			bool flag2 = history.Count == 0;
			int result;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				while (i >= 0)
				{
					decimal high = history[i, true].High;
					decimal low = history[i, true].Low;
					for (int j = 1; j < this.IndicatorPeriod; j++)
					{
						bool flag3 = history[i + j, true].High > high;
						if (flag3)
						{
							high = history[i + j, true].High;
						}
						bool flag4 = history[i + j, true].Low < low;
						if (flag4)
						{
							low = history[i + j, true].Low;
						}
					}
					bool flag5 = (high - low).Equals(decimal.Zero);
					if (flag5)
					{
						this._vals[i, true] = 0.0;
					}
					else
					{
						this._vals[i, true] = (double)((high - history[i, true].Close) / (high - low)) * -100.0;
					}
					i--;
				}
				result = 0;
			}
			return result;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 3;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = (values[0] != null && base.Symbol == null) || (values[0] == null && base.Symbol != null);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = values[0] != null && (!(values[0] is string) || (string)values[0] != base.Symbol);
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = !(values[1] is int) || (int)values[1] != base.TimeFrame;
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = !(values[2] is int) || (int)values[2] != this.IndicatorPeriod;
							result = !flag5;
						}
					}
				}
			}
			return result;
		}
	}
}
