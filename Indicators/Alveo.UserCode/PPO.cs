using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Percentage Price Oscillator")]
	[Serializable]
	public class PPO : IndicatorBase
	{
		private readonly Array<double> ppoBuffer;

		private readonly Array<double> signalBuffer;

		[Category("Settings"), Description("Fast EMA"), DisplayName("Fast EMA")]
		public int FastEMA
		{
			get;
			set;
		}

		[Category("Settings"), Description("Slow EMA"), DisplayName("Slow EMA")]
		public int SlowEMA
		{
			get;
			set;
		}

		[Category("Settings"), Description("Signal EMA"), DisplayName("Signal EMA")]
		public int SignalEMA
		{
			get;
			set;
		}

		public PPO()
		{
			base.indicator_chart_window = false;
			base.indicator_buffers = 2;
			base.indicator_color1 = Colors.SkyBlue;
			base.indicator_color2 = Colors.Red;
			this.FastEMA = 12;
			this.SlowEMA = 26;
			this.SignalEMA = 9;
			this.ppoBuffer = new Array<double>();
			this.signalBuffer = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexBuffer(0, this.ppoBuffer, false);
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexLabel(0, "PPO");
			base.SetIndexBuffer(1, this.signalBuffer, false);
			base.SetIndexStyle(1, 0, -1, -1, null);
			base.SetIndexLabel(1, "Signal");
			base.IndicatorDigits(base.Digits + 1);
			base.IndicatorShortName(string.Format("PPO({0},{1},{2})", this.FastEMA, this.SlowEMA, this.SignalEMA));
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
				this.ppoBuffer[i, true] = (base.iMA(null, 0, this.FastEMA, 0, 1, 0, i) - base.iMA(null, 0, this.SlowEMA, 0, 1, 0, i)) / base.iMA(null, 0, this.SlowEMA, 0, 1, 0, i);
			}
			for (int j = 0; j < num2; j++)
			{
				this.signalBuffer[j, true] = base.iMAOnArray(this.ppoBuffer, base.Bars, this.SignalEMA, 0, 1, j);
			}
			return 0;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 5;
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
							bool flag5 = !(values[2] is int) || (int)values[2] != this.FastEMA;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !(values[3] is int) || (int)values[3] != this.SlowEMA;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = !(values[4] is int) || (int)values[4] != this.SignalEMA;
									result = !flag7;
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
