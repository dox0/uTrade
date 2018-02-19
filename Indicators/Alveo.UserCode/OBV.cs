using Alveo.Common.Classes;
using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("On Balance Volume Indicator")]
	[Serializable]
	public class OBV : IndicatorBase
	{
		private readonly Array<double> _vals;

		[Category("Settings"), Description("Price type on witch OBV will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public OBV()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			base.SetIndexLabel(0, "OBV");
			base.IndicatorShortName("OBV");
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._vals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexBuffer(0, this._vals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			Array<double> price = base.GetPrice(base.GetHistory(base.Symbol, base.TimeFrame), this.PriceType);
			Array<Bar> history = base.GetHistory(base.Symbol, base.TimeFrame);
			bool flag = history.Count == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = i >= base.Bars - 1;
				if (flag2)
				{
					i = base.Bars - 1;
					this._vals[i, true] = (double)history[i, true].Volume;
				}
				while (i >= 0)
				{
					double num = price[i, true];
					double num2 = price[i + 1, true];
					bool flag3 = num.Equals(num2);
					if (flag3)
					{
						this._vals[i, true] = this._vals[i + 1, true];
					}
					else
					{
						bool flag4 = num < num2;
						if (flag4)
						{
							this._vals[i, true] = this._vals[i + 1, true] - (double)history[i, true].Volume;
						}
						else
						{
							this._vals[i, true] = this._vals[i + 1, true] + (double)history[i, true].Volume;
						}
					}
					i--;
				}
				result = 0;
			}
			return result;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 3;
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
							bool flag5 = !(values[2] is PriceConstants) || (PriceConstants)values[2] != this.PriceType;
							result = !flag5;
						}
					}
				}
			}
			return result;
		}
	}
}
