using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Bollinger Bands Indicator")]
	[Serializable]
	public class Bands : IndicatorBase
	{
		private readonly Array<double> _lowVals;

		private readonly Array<double> _upVals;

		private readonly Array<double> _vals;

		[Category("Settings"), Description("Averaging period to calculate the main line"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Deviation from the main line")]
		public int Deviation
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch Bollinger Bands will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public Bands()
		{
			base.indicator_buffers = 3;
			base.indicator_chart_window = true;
			this.IndicatorPeriod = 10;
			this.Deviation = 2;
			base.indicator_color1 = Colors.Blue;
			base.SetIndexLabel(0, string.Format("Bands({0},{1})", this.IndicatorPeriod, this.Deviation));
			base.indicator_color2 = Colors.Green;
			base.SetIndexLabel(1, "Bands_High");
			base.indicator_color3 = Colors.Red;
			base.SetIndexLabel(2, "Bands_Low");
			base.IndicatorShortName(string.Format("Bands({0},{1})", this.IndicatorPeriod, this.Deviation));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._vals = new Array<double>();
			this._upVals = new Array<double>();
			this._lowVals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("Bands({0},{1})", this.IndicatorPeriod, this.Deviation));
			base.IndicatorShortName(string.Format("Bands({0},{1})", this.IndicatorPeriod, this.Deviation));
			base.SetIndexBuffer(0, this._vals, false);
			base.SetIndexBuffer(1, this._upVals, false);
			base.SetIndexBuffer(2, this._lowVals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			bool flag = i > base.Bars - this.IndicatorPeriod;
			if (flag)
			{
				i = base.Bars - this.IndicatorPeriod;
			}
			Array<double> price = base.GetPrice(base.GetHistory(base.Symbol, base.TimeFrame), this.PriceType);
			bool flag2 = price.Count == 0;
			int result;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				while (i >= 0)
				{
					double num = base.iMA(base.Symbol, base.TimeFrame, this.IndicatorPeriod, 0, 0, (int)this.PriceType, i);
					double num2 = 0.0;
					for (int j = 0; j < this.IndicatorPeriod; j++)
					{
						double num3 = price[i + j, true] - num;
						num2 += num3 * num3;
					}
					double num4 = (double)this.Deviation * base.MathSqrt(num2 / (double)this.IndicatorPeriod);
					this._vals[i, true] = num;
					this._upVals[i, true] = num + num4;
					this._lowVals[i, true] = num - num4;
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
								bool flag6 = !(values[3] is int) || (int)values[3] != this.Deviation;
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
