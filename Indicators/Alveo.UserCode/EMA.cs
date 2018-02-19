using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Exponential Moving Avarage Indicator")]
	[Serializable]
	public class EMA : IndicatorBase
	{
		private readonly Array<double> values;

		[Category("Settings"), Description("Period of the EMA Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch EMA will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public EMA()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = true;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("EMA({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("EMA({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
		}

		protected override int Init()
		{
			bool flag = this.IndicatorPeriod <= 0;
			if (flag)
			{
				this.IndicatorPeriod = 1;
			}
			base.SetIndexLabel(0, string.Format("EMA({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("EMA({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this.values, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			bool flag = i >= base.Bars;
			if (flag)
			{
				i = base.Bars - 1;
			}
			double[] price = base.GetPrice(base.GetHistory(base.Symbol, base.TimeFrame), this.PriceType);
			bool flag2 = price.Length == 0;
			int result;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				double num = 2.0 / (double)(this.IndicatorPeriod + 1);
				bool flag3 = i == base.Bars - 1;
				if (flag3)
				{
					this.values[i, true] = price[i, true];
					i--;
				}
				while (i >= 0)
				{
					this.values[i, true] = price[i, true] * num + this.values[i + 1, true] * (1.0 - num);
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
