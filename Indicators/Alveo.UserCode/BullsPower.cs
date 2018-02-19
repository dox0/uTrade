using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Bulls Power Indicator")]
	[Serializable]
	public class BullsPower : IndicatorBase
	{
		private readonly Array<double> BullsBuffer = new Array<double>();

		private readonly Array<double> TempBuffer = new Array<double>();

		[Category("Settings"), Description("Period of the Bulls Power Indicator")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch Bulls Power will be calculated")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public BullsPower()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("BullPower({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("BullPower({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("BullPower({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("BullPower({0})", this.IndicatorPeriod));
			base.IndicatorBuffers(2);
			base.IndicatorDigits(base.Digits);
			base.SetIndexStyle(0, 2, -1, -1, null);
			base.SetIndexBuffer(0, this.BullsBuffer, false);
			base.SetIndexBuffer(1, this.TempBuffer, false);
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			bool flag = base.Bars <= this.IndicatorPeriod;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num2 = base.Bars - num;
				bool flag2 = num > 0;
				if (flag2)
				{
					num2++;
				}
				for (int i = 0; i < num2; i++)
				{
					this.TempBuffer[i, true] = base.iMA(null, 0, this.IndicatorPeriod, 0, 1, 0, i);
				}
				for (int i = base.Bars - num - 1; i >= 0; i--)
				{
					this.BullsBuffer[i, true] = base.High[i, true] - this.TempBuffer[i, true];
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
