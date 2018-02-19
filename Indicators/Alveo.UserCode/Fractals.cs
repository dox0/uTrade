using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Fractals Indicator")]
	[Serializable]
	public class Fractals : IndicatorBase
	{
		private Array<double> _lowVals;

		private Array<double> _upVals;

		public Fractals()
		{
			base.indicator_buffers = 2;
			base.indicator_chart_window = true;
			base.indicator_color1 = Colors.Green;
			base.indicator_color2 = Colors.Red;
			base.SetIndexLabel(0, "Frac_Up");
			base.SetIndexLabel(1, "Frac_Dn");
			base.IndicatorShortName("Fractals");
			this._upVals = new Array<double>();
			this._lowVals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexStyle(0, 3, -1, -1, null);
			base.SetIndexStyle(1, 3, -1, -1, null);
			base.SetIndexArrow(0, 242);
			base.SetIndexArrow(1, 241);
			base.SetIndexBuffer(0, this._upVals, false);
			base.SetIndexBuffer(1, this._lowVals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = 0;
			int num = base.IndicatorCounted();
			bool flag = num <= 2;
			if (flag)
			{
				i = base.Bars - num - 3;
			}
			bool flag2 = num > 2;
			if (flag2)
			{
				num--;
				i = base.Bars - num - 1;
			}
			while (i >= 2)
			{
				double num2 = 2147483647.0;
				double num3 = 2147483647.0;
				bool flag3 = false;
				double num4 = base.High[i, true];
				bool flag4 = num4 > base.High[i + 1, true] && num4 > base.High[i + 2, true] && num4 > base.High[i - 1, true] && num4 > base.High[i - 2, true];
				if (flag4)
				{
					flag3 = true;
					num2 = num4;
				}
				bool flag5 = !flag3 && base.Bars - i - 1 >= 3;
				if (flag5)
				{
					bool flag6 = num4 == base.High[i + 1, true] && num4 > base.High[i + 2, true] && num4 > base.High[i + 3, true] && num4 > base.High[i - 1, true] && num4 > base.High[i - 2, true];
					if (flag6)
					{
						flag3 = true;
						num2 = num4;
					}
				}
				bool flag7 = !flag3 && base.Bars - i - 1 >= 4;
				if (flag7)
				{
					bool flag8 = num4 >= base.High[i + 1, true] && num4 == base.High[i + 2, true] && num4 > base.High[i + 3, true] && num4 > base.High[i + 4, true] && num4 > base.High[i - 1, true] && num4 > base.High[i - 2, true];
					if (flag8)
					{
						flag3 = true;
						num2 = num4;
					}
				}
				bool flag9 = !flag3 && base.Bars - i - 1 >= 5;
				if (flag9)
				{
					bool flag10 = num4 >= base.High[i + 1, true] && num4 == base.High[i + 2, true] && num4 == base.High[i + 3, true] && num4 > base.High[i + 4, true] && num4 > base.High[i + 5, true] && num4 > base.High[i - 1, true] && num4 > base.High[i - 2, true];
					if (flag10)
					{
						flag3 = true;
						num2 = num4;
					}
				}
				bool flag11 = !flag3 && base.Bars - i - 1 >= 6;
				if (flag11)
				{
					bool flag12 = num4 >= base.High[i + 1, true] && num4 == base.High[i + 2, true] && num4 >= base.High[i + 3, true] && num4 == base.High[i + 4, true] && num4 > base.High[i + 5, true] && num4 > base.High[i + 6, true] && num4 > base.High[i - 1, true] && num4 > base.High[i - 2, true];
					if (flag12)
					{
						num2 = num4;
					}
				}
				flag3 = false;
				num4 = base.Low[i, true];
				bool flag13 = num4 < base.Low[i + 1, true] && num4 < base.Low[i + 2, true] && num4 < base.Low[i - 1, true] && num4 < base.Low[i - 2, true];
				if (flag13)
				{
					flag3 = true;
					num3 = num4;
				}
				bool flag14 = !flag3 && base.Bars - i - 1 >= 3;
				if (flag14)
				{
					bool flag15 = num4 == base.Low[i + 1, true] && num4 < base.Low[i + 2, true] && num4 < base.Low[i + 3, true] && num4 < base.Low[i - 1, true] && num4 < base.Low[i - 2, true];
					if (flag15)
					{
						flag3 = true;
						num3 = num4;
					}
				}
				bool flag16 = !flag3 && base.Bars - i - 1 >= 4;
				if (flag16)
				{
					bool flag17 = num4 <= base.Low[i + 1, true] && num4 == base.Low[i + 2, true] && num4 < base.Low[i + 3, true] && num4 < base.Low[i + 4, true] && num4 < base.Low[i - 1, true] && num4 < base.Low[i - 2, true];
					if (flag17)
					{
						flag3 = true;
						num3 = num4;
					}
				}
				bool flag18 = !flag3 && base.Bars - i - 1 >= 5;
				if (flag18)
				{
					bool flag19 = num4 <= base.Low[i + 1, true] && num4 == base.Low[i + 2, true] && num4 == base.Low[i + 3, true] && num4 < base.Low[i + 4, true] && num4 < base.Low[i + 5, true] && num4 < base.Low[i - 1, true] && num4 < base.Low[i - 2, true];
					if (flag19)
					{
						flag3 = true;
						num3 = num4;
					}
				}
				bool flag20 = !flag3 && base.Bars - i - 1 >= 6;
				if (flag20)
				{
					bool flag21 = num4 <= base.Low[i + 1, true] && num4 == base.Low[i + 2, true] && num4 <= base.Low[i + 3, true] && num4 == base.Low[i + 4, true] && num4 < base.Low[i + 5, true] && num4 < base.Low[i + 6, true] && num4 < base.Low[i - 1, true] && num4 < base.Low[i - 2, true];
					if (flag21)
					{
						num3 = num4;
					}
				}
				this._upVals[i, true] = num2;
				this._lowVals[i, true] = num3;
				i--;
			}
			return 0;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 2;
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
						result = !flag4;
					}
				}
			}
			return result;
		}
	}
}
