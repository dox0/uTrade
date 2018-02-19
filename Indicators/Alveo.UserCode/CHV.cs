using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Chaikin Volatility")]
	[Serializable]
	public class CHV : IndicatorBase
	{
		private readonly Array<double> chvBuffer;

		private readonly Array<double> hl;

		[Category("Settings"), Description("Smooth Period"), DisplayName("Smooth Period")]
		public int SmoothPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("ROC Period"), DisplayName("ROC Period")]
		public int ROCPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("0 - SMA, 1 - EMA"), DisplayName("Smooth Type")]
		public int TypeSmooth
		{
			get;
			set;
		}

		public CHV()
		{
			base.indicator_chart_window = false;
			base.indicator_buffers = 1;
			base.indicator_color1 = Colors.Navy;
			this.SmoothPeriod = 10;
			this.ROCPeriod = 10;
			this.TypeSmooth = 1;
			this.chvBuffer = new Array<double>();
			this.hl = new Array<double>();
		}

		protected override int Init()
		{
			bool flag = this.TypeSmooth < 0 || this.TypeSmooth > 1;
			if (flag)
			{
				this.TypeSmooth = 1;
			}
			bool flag2 = this.TypeSmooth == 0;
			string arg;
			if (flag2)
			{
				arg = "SMA";
			}
			else
			{
				arg = "EMA";
			}
			base.IndicatorBuffers(2);
			base.SetIndexBuffer(0, this.chvBuffer, false);
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexEmptyValue(0, 0.0);
			base.SetIndexLabel(0, string.Format("CHV({0},{1})", this.SmoothPeriod, arg));
			base.SetIndexBuffer(1, this.hl, false);
			base.SetIndexEmptyValue(1, 0.0);
			base.IndicatorShortName(string.Format("Chaikin Volatility({0},{1})", this.SmoothPeriod, arg));
			base.IndicatorDigits(1);
			return 0;
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
				bool flag2 = num == 0;
				if (flag2)
				{
					int num2 = base.Bars - 1;
					for (int i = num2; i >= 0; i--)
					{
						this.hl[i, true] = base.High[i, true] - base.Low[i, true];
					}
					for (int i = num2 - 2 * this.SmoothPeriod; i >= 0; i--)
					{
						double num3 = base.iMAOnArray(this.hl, 0, this.SmoothPeriod, 0, this.TypeSmooth, i);
						double num4 = base.iMAOnArray(this.hl, 0, this.SmoothPeriod, 0, this.TypeSmooth, i + this.ROCPeriod);
						this.chvBuffer[i, true] = (num3 - num4) / num4 * 100.0;
					}
				}
				bool flag3 = num > 0;
				if (flag3)
				{
					int num2 = base.Bars - num;
					for (int i = num2; i >= 0; i--)
					{
						this.hl[i, true] = base.High[i, true] - base.Low[i, true];
					}
					for (int i = num2; i >= 0; i--)
					{
						double num3 = base.iMAOnArray(this.hl, 0, this.SmoothPeriod, 0, this.TypeSmooth, i);
						double num4 = base.iMAOnArray(this.hl, 0, this.SmoothPeriod, 0, this.TypeSmooth, i + this.ROCPeriod);
						this.chvBuffer[i, true] = (num3 - num4) / num4 * 100.0;
					}
				}
				result = 0;
			}
			return result;
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
							bool flag5 = !(values[2] is int) || (int)values[2] != this.SmoothPeriod;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !(values[3] is int) || (int)values[3] != this.ROCPeriod;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = !(values[4] is int) || (int)values[4] != this.TypeSmooth;
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
