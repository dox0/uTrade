using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("")]
	[Serializable]
	public class HMA : IndicatorBase
	{
		private Array<double> UpTrend;

		private Array<double> DownTrend;

		private Array<double> ExtMapBuffer;

		private int price = 0;

		[Category("Settings"), DisplayName("MA Period")]
		public int period
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Method")]
		public int method
		{
			get;
			set;
		}

		public HMA()
		{
			base.indicator_buffers = 2;
			base.indicator_chart_window = true;
			this.period = 21;
			this.method = 3;
			base.indicator_width1 = 2;
			base.indicator_width2 = 2;
			base.indicator_color1 = Colors.Blue;
			base.indicator_color2 = Colors.Red;
			this.UpTrend = new Array<double>();
			this.DownTrend = new Array<double>();
			this.ExtMapBuffer = new Array<double>();
			base.copyright = "";
			base.link = "";
		}

		protected override int Init()
		{
			base.IndicatorBuffers(2);
			base.SetIndexBuffer(0, this.UpTrend, false);
			base.SetIndexArrow(0, 159);
			base.SetIndexBuffer(1, this.DownTrend, false);
			base.SetIndexArrow(1, 159);
			base.SetIndexStyle(0, 0, 0, -1, null);
			base.SetIndexLabel(0, "HMA(" + this.period + ").Bull");
			base.SetIndexStyle(1, 0, 0, -1, null);
			base.SetIndexLabel(1, "HMA(" + this.period + ").Bear");
			base.IndicatorShortName("Hull Moving Average(" + this.period + ")");
			return 0;
		}

		protected override int Deinit()
		{
			return 0;
		}

		protected double WMA(int x, int p)
		{
			return base.iMA(null, 0, p, 0, this.method, this.price, x);
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			bool flag = num < 0;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				double num2 = base.MathSqrt((double)this.period);
				int num3 = base.Bars - num + this.period + 1;
				Array<double> array = new Array<double>();
				Array<double> array2 = new Array<double>();
				bool flag2 = num3 > base.Bars;
				if (flag2)
				{
					num3 = base.Bars;
				}
				base.ArrayResize<double>(array, num3);
				base.ArraySetAsSeries<double>(array, true);
				base.ArrayResize<double>(array2, num3);
				base.ArraySetAsSeries<double>(array2, true);
				base.ArrayResize<double>(this.ExtMapBuffer, num3);
				base.ArraySetAsSeries<double>(this.ExtMapBuffer, true);
				for (int i = num3; i >= 0; i--)
				{
					array[i, true] = 2.0 * this.WMA(i, this.period / 2) - this.WMA(i, this.period);
				}
				for (int i = 0; i < num3 - this.period; i++)
				{
					this.ExtMapBuffer[i, true] = base.iMAOnArray(array, 0, (int)num2, 0, this.method, i);
				}
				for (int i = num3 - this.period; i >= 0; i--)
				{
					array2[i, true] = array2[i + 1, true];
					bool flag3 = this.ExtMapBuffer[i, true] > this.ExtMapBuffer[i + 1, true];
					if (flag3)
					{
						array2[i, true] = 1.0;
					}
					bool flag4 = this.ExtMapBuffer[i, true] < this.ExtMapBuffer[i + 1, true];
					if (flag4)
					{
						array2[i, true] = -1.0;
					}
					bool flag5 = array2[i, true] > 0.0;
					if (flag5)
					{
						this.UpTrend[i, true] = this.ExtMapBuffer[i, true];
						bool flag6 = array2[i + 1, true] < 0.0;
						if (flag6)
						{
							this.UpTrend[i + 1, true] = this.ExtMapBuffer[i + 1, true];
						}
						this.DownTrend[i, true] = 2147483647.0;
					}
					else
					{
						bool flag7 = array2[i, true] < 0.0;
						if (flag7)
						{
							this.DownTrend[i, true] = this.ExtMapBuffer[i, true];
							bool flag8 = array2[i + 1, true] > 0.0;
							if (flag8)
							{
								this.DownTrend[i + 1, true] = this.ExtMapBuffer[i + 1, true];
							}
							this.UpTrend[i, true] = 2147483647.0;
						}
					}
				}
				result = 0;
			}
			return result;
		}

		[Description("Parameters order Symbol, TimeFrame")]
		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 4;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !base.CompareString(base.Symbol, (string)values[0], false);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = base.TimeFrame != (int)values[1];
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = this.period != (int)values[2];
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = this.method != (int)values[3];
							result = !flag5;
						}
					}
				}
			}
			return result;
		}

		[Description("Parameters order Symbol, TimeFrame")]
		public override void SetIndicatorParameters(params object[] values)
		{
			bool flag = values.Length != 2;
			if (flag)
			{
				throw new ArgumentException("Invalid parameters number");
			}
			base.Symbol = (string)values[0];
			base.TimeFrame = (int)values[1];
			this.period = (int)values[2];
			this.method = (int)values[3];
		}
	}
}
