using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Linear Weighted Moving Average Indicator")]
	[Serializable]
	public class LWMA : IndicatorBase
	{
		private readonly Array<double> _values;

		[Category("Settings"), Description("Period of the LWMA Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch LWMA will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public LWMA()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = true;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("LWMA({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("LWMA({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._values = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("LWMA({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("LWMA({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._values, false);
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
				i += this.IndicatorPeriod;
				bool flag2 = i >= base.Bars;
				if (flag2)
				{
					i = base.Bars - 1;
				}
				double num = 0.0;
				double num2 = 0.0;
				double num3 = 0.0;
				int j = 1;
				while (j <= this.IndicatorPeriod)
				{
					double num4 = price[i, true];
					num += num4 * (double)j;
					num2 += num4;
					num3 += (double)j;
					j++;
					i--;
				}
				this._values[i + 1, true] = num / num3;
				j = i + this.IndicatorPeriod;
				while (i >= 0)
				{
					double num4 = base.Close[i, true];
					num = num - num2 + num4 * (double)this.IndicatorPeriod;
					num2 -= base.Close[j, true];
					num2 += num4;
					this._values[i, true] = num / num3;
					i--;
					j--;
				}
				result = 0;
			}
			return result;
		}

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
								bool flag6 = !(values[3] is PriceConstants) || (PriceConstants)values[3] != this.PriceType;
								result = !flag6;
							}
						}
					}
				}
			}
			return result;
		}
	}
}
