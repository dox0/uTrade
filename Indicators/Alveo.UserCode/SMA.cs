using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Simple Moving Avarage Indicator")]
	[Serializable]
	public class SMA : IndicatorBase
	{
		private readonly Array<double> _values;

		[Category("Settings"), Description("Period of the SMA Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch SMA will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public SMA()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = true;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("SMA({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("SMA({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._values = new Array<double>();
		}

		protected override int Init()
		{
			bool flag = this.IndicatorPeriod <= 0;
			if (flag)
			{
				this.IndicatorPeriod = 1;
			}
			base.SetIndexLabel(0, string.Format("SMA({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("SMA({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._values, false);
			return 0;
		}

		protected override int Start()
		{
			Array<double> price = base.GetPrice(base.GetHistory(base.Symbol, base.TimeFrame), this.PriceType);
			bool flag = price.Count == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				for (int i = base.IndicatorCounted(); i < Math.Min(price.Count, base.Bars); i++)
				{
					bool flag2 = i < this.IndicatorPeriod - 1;
					if (flag2)
					{
						this._values[i, false] = 2147483647.0;
					}
					else
					{
						bool flag3 = i == this.IndicatorPeriod - 1;
						if (flag3)
						{
							double num = 0.0;
							for (int j = 0; j < this.IndicatorPeriod; j++)
							{
								num += price[j, false];
							}
							this._values[i, false] = num / (double)this.IndicatorPeriod;
						}
						else
						{
							this._values[i, false] = this._values[i - 1, false] + (price[i, false] - price[i - this.IndicatorPeriod, false]) / (double)this.IndicatorPeriod;
						}
					}
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

		[Description("Parameters order Symbol, TimeFrame, maPeriod_1, maMethod_1, maAppPrice_1, maPeriod_2, maMethod_2, maPeriod_3 maMethod_3")]
		public override void SetIndicatorParameters(params object[] values)
		{
			bool flag = values.Length != 4;
			if (!flag)
			{
				try
				{
					base.Symbol = (string)values[0];
					base.TimeFrame = (int)values[1];
					this.IndicatorPeriod = (int)values[2];
					this.PriceType = (PriceConstants)values[3];
				}
				catch (Exception exception)
				{
					this.Logger.ErrorException("SetIndicatorParameters", exception);
				}
			}
		}
	}
}
