using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Standard Deviation Indicator")]
	[Serializable]
	public class StdDev : IndicatorBase
	{
		private readonly Array<double> _vals;

		[Category("Settings"), Description("Period of the StdDev Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Type of StdDev Indicator"), DisplayName("MA Type")]
		public MovingAverageType MAType
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch StdDev will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public StdDev()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("StdDev({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("StdDev({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._vals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("StdDev({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("StdDev({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._vals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			Array<double> price = base.GetPrice(base.GetHistory(base.Symbol, base.TimeFrame), this.PriceType);
			bool flag = price.Count == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = i > base.Bars - this.IndicatorPeriod;
				if (flag2)
				{
					i = base.Bars - this.IndicatorPeriod;
				}
				while (i >= 0)
				{
					double num = 0.0;
					double num2 = base.iMA(base.Symbol, base.TimeFrame, this.IndicatorPeriod, 0, (int)this.MAType, (int)this.PriceType, i);
					for (int j = 0; j < this.IndicatorPeriod; j++)
					{
						double num3 = price[i + j, true];
						num += (num3 - num2) * (num3 - num2);
					}
					this._vals[i, true] = base.MathSqrt(num / (double)this.IndicatorPeriod);
					i--;
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
							bool flag5 = !(values[2] is int) || (int)values[2] != this.IndicatorPeriod;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !(values[3] is MovingAverageType) || (MovingAverageType)values[3] != this.MAType;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = !(values[4] is PriceConstants) || (PriceConstants)values[4] != this.PriceType;
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
