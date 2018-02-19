using Alveo.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Moving Average of Oscillator Indicator")]
	[Serializable]
	public class MACD : IndicatorBase
	{
		private readonly Array<double> signalVals;

		private readonly Array<double> vals;

		[Category("Settings"), Description("Fast Period of the OsMA Indicator"), DisplayName("Slow Period")]
		public int SlowPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Slow Period of the OsMA Indicator"), DisplayName("Signal Period")]
		public int SignalPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Signal Period of the OsMA Indicator"), DisplayName("Fast Period")]
		public int FastPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch OsMA will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public MACD()
		{
			base.indicator_buffers = 2;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			base.indicator_color2 = Colors.Blue;
			this.FastPeriod = 12;
			this.SlowPeriod = 26;
			this.SignalPeriod = 9;
			base.SetIndexLabel(1, "Signal");
			base.SetIndexLabel(0, string.Format("MACD({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod));
			base.IndicatorShortName(string.Format("MACD({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this.vals = new Array<double>();
			this.signalVals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("MACD({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod));
			base.IndicatorShortName(string.Format("MACD({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod));
			base.SetIndexStyle(0, 2, -1, -1, null);
			base.SetIndexStyle(1, 0, -1, -1, null);
			base.SetIndexBuffer(0, this.vals, false);
			base.SetIndexBuffer(1, this.signalVals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			i += this.SignalPeriod;
			bool flag = i >= base.Bars;
			if (flag)
			{
				i = base.Bars - 1;
			}
			double num = 0.0;
			int j = 1;
			while (j < this.SignalPeriod)
			{
				double num2 = base.iMA(base.Symbol, base.TimeFrame, this.FastPeriod, 0, 1, (int)this.PriceType, i);
				double num3 = base.iMA(base.Symbol, base.TimeFrame, this.SlowPeriod, 0, 1, (int)this.PriceType, i);
				num += num2 - num3;
				j++;
				i--;
			}
			while (i >= 0)
			{
				double num2 = base.iMA(base.Symbol, base.TimeFrame, this.FastPeriod, 0, 1, (int)this.PriceType, i);
				double num3 = base.iMA(base.Symbol, base.TimeFrame, this.SlowPeriod, 0, 1, (int)this.PriceType, i);
				num += num2 - num3;
				this.vals[i, true] = num2 - num3;
				this.signalVals[i, true] = num / (double)this.SignalPeriod;
				num2 = base.iMA(base.Symbol, base.TimeFrame, this.FastPeriod, 0, 1, (int)this.PriceType, i + this.SignalPeriod - 1);
				num3 = base.iMA(base.Symbol, base.TimeFrame, this.SlowPeriod, 0, 1, (int)this.PriceType, i + this.SignalPeriod - 1);
				num -= num2 - num3;
				i--;
			}
			return 0;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 6;
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
							bool flag5 = !(values[2] is int) || (int)values[2] != this.FastPeriod;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !(values[3] is int) || (int)values[3] != this.SlowPeriod;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = !(values[4] is int) || (int)values[4] != this.SignalPeriod;
									if (flag7)
									{
										result = false;
									}
									else
									{
										bool flag8 = !(values[5] is PriceConstants) || (PriceConstants)values[5] != this.PriceType;
										result = !flag8;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}
