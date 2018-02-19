using Alveo.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Collections.Generic;

namespace Alveo.UserCode
{
	[Description("Bill Williams' Accelerator/Decelerator Indicator")]
	[Serializable]
	public class AC : IndicatorBase
	{
		private readonly Array<double> _vals;

		[Category("Settings"), Description("Fast MA Period"), DisplayName("Fast MA")]
		public int Period1
		{
			get;
			set;
		}

		[Category("Settings"), Description("Slow MA Period"), DisplayName("Slow MA")]
		public int Period2
		{
			get;
			set;
		}

		[Category("Settings"), Description("Forming MA Period"), DisplayName("Forming MA")]
		public int Period3
		{
			get;
			set;
		}

		[Category("Settings"), Description("Smoothing method"), DisplayName("Smoothing method")]
		public MovingAverageType Smoothing
		{
			get;
			set;
		}

		[Category("Settings"), Description("Base price"), DisplayName("Base price")]
		public PriceConstants PriceBase
		{
			get;
			set;
		}

		public AC()
		{
			base.indicator_buffers = 1;
			this.Period1 = 5;
			this.Period2 = 34;
			this.Period3 = 5;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this._vals = new Array<double>();
			base.SetIndexLabel(0, string.Format("AC({0},{1},{2})", this.Period1, this.Period2, this.Period3));
			base.IndicatorShortName(string.Format("AC({0},{1},{2})", this.Period1, this.Period2, this.Period3));
			this.Smoothing = MovingAverageType.MODE_SMA;
			this.PriceBase = PriceConstants.PRICE_MEDIAN;
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("AC({0},{1},{2})", this.Period1, this.Period2, this.Period3));
			base.IndicatorShortName(string.Format("AC({0},{1},{2})", this.Period1, this.Period2, this.Period3));
			base.SetIndexBuffer(0, this._vals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			i += this.Period3;
			bool flag = i > base.Bars - this.Period2;
			if (flag)
			{
				i = base.Bars - this.Period2;
			}
			double num = 0.0;
			int j = 1;
			while (j < this.Period3)
			{
				double num2 = base.iMA(base.Symbol, base.TimeFrame, this.Period1, 0, (int)this.Smoothing, (int)this.PriceBase, i);
				double num3 = base.iMA(base.Symbol, base.TimeFrame, this.Period2, 0, (int)this.Smoothing, (int)this.PriceBase, i);
				double num4 = num2 - num3;
				num += num4;
				j++;
				i--;
			}
			while (i >= 0)
			{
				double num2 = base.iMA(base.Symbol, base.TimeFrame, this.Period1, 0, (int)this.Smoothing, (int)this.PriceBase, i);
				double num3 = base.iMA(base.Symbol, base.TimeFrame, this.Period2, 0, (int)this.Smoothing, (int)this.PriceBase, i);
				double num4 = num2 - num3;
				num += num4;
				double num5 = num / (double)this.Period3;
				this._vals[i, true] = num4 - num5;
				num2 = base.iMA(base.Symbol, base.TimeFrame, this.Period1, 0, (int)this.Smoothing, (int)this.PriceBase, i + this.Period3 - 1);
				num3 = base.iMA(base.Symbol, base.TimeFrame, this.Period2, 0, (int)this.Smoothing, (int)this.PriceBase, i + this.Period3 - 1);
				num4 = num2 - num3;
				num -= num4;
				i--;
			}
			return 0;
		}

		[Description("Parameters order Symbol, TimeFrame, Period1, Period2, Period3, Smoothing, PriceBase")]
		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 7;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					string value = (string)values[0];
					bool flag2 = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(base.Symbol);
					if (flag2)
					{
						bool flag3 = !base.Symbol.Equals(value, StringComparison.OrdinalIgnoreCase);
						if (flag3)
						{
							result = false;
							return result;
						}
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(base.Symbol) || !string.IsNullOrEmpty(value);
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					bool flag5 = (int)values[1] != base.TimeFrame;
					if (flag5)
					{
						result = false;
						return result;
					}
					bool flag6 = (int)values[2] != this.Period1;
					if (flag6)
					{
						result = false;
						return result;
					}
					bool flag7 = (int)values[3] != this.Period2;
					if (flag7)
					{
						result = false;
						return result;
					}
					bool flag8 = (int)values[4] != this.Period3;
					if (flag8)
					{
						result = false;
						return result;
					}
					bool flag9 = (MovingAverageType)values[5] != this.Smoothing;
					if (flag9)
					{
						result = false;
						return result;
					}
					bool flag10 = (PriceConstants)values[6] != this.PriceBase;
					if (flag10)
					{
						result = false;
						return result;
					}
				}
				catch (Exception exception)
				{
					this.Logger.ErrorException("IsSameParameters", exception);
				}
				result = true;
			}
			return result;
		}
	}
}
