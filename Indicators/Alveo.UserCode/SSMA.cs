using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Smoothed Moving Avarage Indicator")]
	[Serializable]
	public class SSMA : IndicatorBase
	{
		private readonly Array<double> values;

		[Category("Settings"), Description("Period of the SSMA Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch SSMA will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public SSMA()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = true;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("SSMA({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("SSMA({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this.values = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("SSMA({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("SSMA({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this.values, false);
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
				bool flag2 = i >= base.Bars - this.IndicatorPeriod;
				if (flag2)
				{
					i = base.Bars - this.IndicatorPeriod;
				}
				bool flag3 = i == base.Bars - this.IndicatorPeriod;
				if (flag3)
				{
					i = base.Bars - 1;
					double num = 0.0;
					int j = 0;
					while (j < this.IndicatorPeriod)
					{
						num += price[i, true];
						j++;
						i--;
					}
					this.values[i + 1, true] = num / (double)this.IndicatorPeriod;
				}
				while (i >= 0)
				{
					this.values[i, true] = (this.values[i + 1, true] * (double)(this.IndicatorPeriod - 1) + price[i, true]) / (double)this.IndicatorPeriod;
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
