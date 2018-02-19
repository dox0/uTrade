using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Momentum Indicator")]
	[Serializable]
	public class Momentum : IndicatorBase
	{
		private readonly Array<double> _vals;

		[Category("Settings"), Description("Period of the Momentum Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch Momentum will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public Momentum()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("Momentum({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("Momentum({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._vals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("Momentum({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("Momentum({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._vals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			bool flag = i > base.Bars - this.IndicatorPeriod;
			if (flag)
			{
				i = base.Bars - this.IndicatorPeriod - 1;
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
					this._vals[i, true] = price[i, true] * 100.0 / price[i + this.IndicatorPeriod, true];
					i--;
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
