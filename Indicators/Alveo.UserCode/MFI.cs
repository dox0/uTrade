using Alveo.Common.Classes;
using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Money Flow Index Indicator")]
	[Serializable]
	public class MFI : IndicatorBase
	{
		private readonly Array<double> _vals;

		[Category("Settings"), Description("Period of the MFI Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		public MFI()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("MFI({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("MFI({0})", this.IndicatorPeriod));
			this._vals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("MFI({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("MFI({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._vals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			bool flag = i > base.Bars - this.IndicatorPeriod - 1;
			if (flag)
			{
				i = base.Bars - this.IndicatorPeriod - 1;
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
					decimal d = 0m;
					decimal num = 0m;
					decimal num2 = (history[i, true].High + history[i, true].Low + history[i, true].Close) / 3m;
					for (int j = 0; j < this.IndicatorPeriod; j++)
					{
						decimal num3 = (history[i + j + 1, true].High + history[i + j + 1, true].Low + history[i + j + 1, true].Close) / 3m;
						bool flag3 = num2 > num3;
						if (flag3)
						{
							d += history[i + j, true].Volume * num2;
						}
						else
						{
							bool flag4 = num2 < num3;
							if (flag4)
							{
								num += history[i + j, true].Volume * num2;
							}
						}
						num2 = num3;
					}
					bool flag5 = !num.Equals(0.0);
					if (flag5)
					{
						this._vals[i, true] = 100.0 - 100.0 / (double)(decimal.One + d / num);
					}
					else
					{
						this._vals[i, true] = 100.0;
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
