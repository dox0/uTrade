using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Moving Average of Oscillator Indicator")]
	[Serializable]
	public class OsMA : IndicatorBase
	{
		private readonly Array<double> MacdBuffer = new Array<double>();

		private readonly Array<double> OsmaBuffer = new Array<double>();

		private readonly Array<double> SignalBuffer = new Array<double>();

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

		public OsMA()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Silver;
			base.indicator_width1 = 3;
			this.FastPeriod = 12;
			this.SlowPeriod = 26;
			this.SignalPeriod = 9;
			this.PriceType = PriceConstants.PRICE_CLOSE;
			base.IndicatorShortName(string.Format("OsMA({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod));
			base.SetIndexLabel(0, string.Format("OsMA({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod));
		}

		protected override int Init()
		{
			base.IndicatorBuffers(3);
			base.SetIndexStyle(0, 2, -1, -1, null);
			base.SetIndexDrawBegin(0, this.SignalPeriod);
			base.IndicatorDigits(base.Digits + 2);
			base.SetIndexBuffer(0, this.OsmaBuffer, false);
			base.SetIndexBuffer(1, this.MacdBuffer, false);
			base.SetIndexBuffer(2, this.SignalBuffer, false);
			base.IndicatorShortName(string.Format("OsMA({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod));
			base.SetIndexLabel(0, string.Format("OsMA({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod));
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			bool flag = num > 0;
			if (flag)
			{
				num--;
			}
			int num2 = base.Bars - num;
			for (int i = 0; i < num2; i++)
			{
				this.MacdBuffer[i, true] = base.iMA(null, 0, this.FastPeriod, 0, 1, 0, i) - base.iMA(null, 0, this.SlowPeriod, 0, 1, 0, i);
			}
			for (int i = 0; i < num2; i++)
			{
				this.SignalBuffer[i, true] = base.iMAOnArray(this.MacdBuffer, base.Bars, this.SignalPeriod, 0, 0, i);
			}
			for (int i = 0; i < num2; i++)
			{
				this.OsmaBuffer[i, true] = this.MacdBuffer[i, true] - this.SignalBuffer[i, true];
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
