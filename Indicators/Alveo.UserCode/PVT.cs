using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Price Volume Trend")]
	[Serializable]
	public class PVT : IndicatorBase
	{
		private readonly Array<double> pvtBuffer;

		[Category("Settings"), Description("PVT Applied Price"), DisplayName("PVT Applied Price")]
		public int appliedPrice
		{
			get;
			set;
		}

		public PVT()
		{
			base.indicator_chart_window = false;
			base.indicator_buffers = 1;
			base.indicator_color1 = Colors.DodgerBlue;
			this.appliedPrice = 0;
			this.pvtBuffer = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexBuffer(0, this.pvtBuffer, false);
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexLabel(0, "PVT");
			base.IndicatorDigits(0);
			base.IndicatorShortName("PVT");
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			bool flag = num > 0;
			if (flag)
			{
				num--;
			}
			int num2 = base.Bars - num - 1;
			for (int i = num2; i >= 0; i--)
			{
				bool flag2 = i == base.Bars - 1;
				if (flag2)
				{
					this.pvtBuffer[i, true] = base.Volume[i, true];
				}
				else
				{
					double appliedPrice = this.getAppliedPrice(this.appliedPrice, i);
					double appliedPrice2 = this.getAppliedPrice(this.appliedPrice, i + 1);
					this.pvtBuffer[i, true] = this.pvtBuffer[i + 1, true] + base.Volume[i, true] * (appliedPrice - appliedPrice2) / appliedPrice2;
				}
			}
			return 0;
		}

		private double getAppliedPrice(int appliedPrice, int index)
		{
			double result;
			switch (appliedPrice)
			{
			case 0:
				result = base.Close[index, true];
				break;
			case 1:
				result = base.Open[index, true];
				break;
			case 2:
				result = base.High[index, true];
				break;
			case 3:
				result = base.Low[index, true];
				break;
			case 4:
				result = (base.High[index, true] + base.Low[index, true]) / 2.0;
				break;
			case 5:
				result = (base.High[index, true] + base.Low[index, true] + base.Close[index, true]) / 3.0;
				break;
			case 6:
				result = (base.High[index, true] + base.Low[index, true] + 2.0 * base.Close[index, true]) / 4.0;
				break;
			default:
				result = 0.0;
				break;
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
							bool flag5 = !(values[2] is int) || (int)values[2] != this.appliedPrice;
							result = !flag5;
						}
					}
				}
			}
			return result;
		}
	}
}
