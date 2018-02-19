using Alveo.Common.Classes;
using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Average True Range Indicator")]
	[Serializable]
	public class ATR : IndicatorBase
	{
		private readonly Array<double> _tr;

		private readonly Array<double> _vals;

		[Category("Settings"), Description("Period of the ATR Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		public ATR()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("ATR({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("ATR({0})", this.IndicatorPeriod));
			this._vals = new Array<double>();
			this._tr = new Array<double>();
			base.IndicatorDigits(5);
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("ATR({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("ATR({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._vals, false);
			base.ArraySetAsSeries<double>(this._tr, true);
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			Array<Bar> history = base.GetHistory(base.Symbol, base.TimeFrame);
			bool flag = history.Count == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				while (this._tr.Count < base.Bars)
				{
					this._tr.Add(2147483647.0);
				}
				for (int i = num; i < Math.Min(base.Bars, history.Count); i++)
				{
					bool flag2 = i == 0;
					if (flag2)
					{
						this._tr[i, false] = (double)history[i, false].High - (double)history[i, false].Low;
					}
					else
					{
						this._tr[i, false] = (double)Math.Max(history[i, false].High - history[i, false].Low, Math.Max(Math.Abs(history[i, false].High - history[i - 1, false].Close), Math.Abs(history[i, false].Low - history[i - 1, false].Close)));
					}
					bool flag3 = i == this.IndicatorPeriod - 1;
					if (flag3)
					{
						double num2 = 0.0;
						for (int j = 0; j < this.IndicatorPeriod; j++)
						{
							num2 += this._tr[j, false];
						}
						this._vals[i, false] = num2 / (double)this.IndicatorPeriod;
					}
					else
					{
						bool flag4 = i >= this.IndicatorPeriod;
						if (flag4)
						{
							this._vals[i, false] = this._vals[i - 1, false] + (this._tr[i, false] - this._tr[i - this.IndicatorPeriod, false]) / (double)this.IndicatorPeriod;
						}
					}
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
