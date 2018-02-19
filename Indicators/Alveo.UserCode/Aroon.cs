using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Aroon Indicator")]
	[Serializable]
	public class Aroon : IndicatorBase
	{
		private readonly Array<double> upBuffer;

		private readonly Array<double> downBuffer;

		[Category("Settings"), DisplayName("priceber of Periods")]
		public int period
		{
			get;
			set;
		}

		public Aroon()
		{
			base.indicator_buffers = 2;
			base.indicator_chart_window = false;
			base.indicator_minimum = new double?(0.0);
			base.indicator_maximum = new double?((double)100);
			base.indicator_level1 = 30.0;
			base.indicator_level2 = 70.0;
			this.period = 14;
			base.indicator_color1 = Colors.Red;
			base.indicator_color2 = Colors.Green;
			this.upBuffer = new Array<double>();
			this.downBuffer = new Array<double>();
			base.IndicatorShortName(string.Format("Aroon({0})", this.period));
		}

		protected override int Init()
		{
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexBuffer(0, this.upBuffer, false);
			base.SetIndexLabel(0, "UP");
			base.SetIndexDrawBegin(0, this.period);
			base.SetIndexStyle(1, 0, -1, -1, null);
			base.SetIndexBuffer(1, this.downBuffer, false);
			base.SetIndexLabel(1, "DOWN");
			base.SetIndexDrawBegin(1, this.period);
			return 0;
		}

		protected override int Deinit()
		{
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			bool flag = base.Bars <= this.period;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = num < 1;
				int i;
				if (flag2)
				{
					for (i = 1; i <= this.period; i++)
					{
						this.upBuffer[base.Bars - i, true] = 0.0;
						this.downBuffer[base.Bars - i, true] = 0.0;
					}
				}
				i = base.Bars - this.period - 1;
				bool flag3 = num >= this.period;
				if (flag3)
				{
					i = base.Bars - num - 1;
				}
				int num2 = i;
				int num3 = i;
				while (i >= 0)
				{
					double num4 = -100000.0;
					double num5 = 100000.0;
					for (int j = i; j < i + this.period; j++)
					{
						double num6 = base.Close[j, true];
						bool flag4 = num6 > num4;
						if (flag4)
						{
							num4 = num6;
							num2 = j;
						}
						bool flag5 = num6 < num5;
						if (flag5)
						{
							num5 = num6;
							num3 = j;
						}
					}
					this.upBuffer[i, true] = (double)(100 * (this.period - (num2 - i)) / this.period);
					this.downBuffer[i, true] = (double)(100 * (this.period - (num3 - i)) / this.period);
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
							bool flag5 = !(values[2] is int) || (int)values[3] != this.period;
							result = !flag5;
						}
					}
				}
			}
			return result;
		}
	}
}
