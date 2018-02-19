using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("")]
	[Serializable]
	public class ApiaryPivots : IndicatorBase
	{
		private Array<double> pivotHighs;

		private Array<double> pivotLows;

		private Array<double> majorPivotHighs;

		private Array<double> majorPivotLows;

		[Category("Settings"), DisplayName("Vertical Variation")]
		public double pipDiff
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Horizontal Variation")]
		public int posDiff
		{
			get;
			set;
		}

		[Category("Settings (Appearance)"), DisplayName("Pivot WingDing")]
		public int pivotIcon
		{
			get;
			set;
		}

		public ApiaryPivots()
		{
			base.indicator_buffers = 4;
			base.indicator_chart_window = true;
			this.pipDiff = 0.08;
			this.posDiff = 3;
			this.pivotIcon = 117;
			base.indicator_color1 = Colors.Orange;
			base.indicator_color2 = Colors.Orange;
			base.indicator_color3 = Colors.Red;
			base.indicator_color4 = Colors.Red;
			this.pivotHighs = new Array<double>();
			this.pivotLows = new Array<double>();
			this.majorPivotHighs = new Array<double>();
			this.majorPivotLows = new Array<double>();
			base.copyright = "Apiary Fund, LLC";
			base.link = "https://apiaryfund.com";
		}

		protected override int Init()
		{
			base.SetIndexStyle(0, 3, 2, -1, null);
			base.SetIndexArrow(0, this.pivotIcon);
			base.SetIndexBuffer(0, this.pivotHighs, false);
			base.SetIndexLabel(0, "Pivot High");
			base.SetIndexStyle(1, 3, 2, -1, null);
			base.SetIndexArrow(1, this.pivotIcon);
			base.SetIndexBuffer(1, this.pivotLows, false);
			base.SetIndexLabel(1, "Pivot Low");
			base.SetIndexStyle(2, 3, 2, -1, null);
			base.SetIndexArrow(2, this.pivotIcon);
			base.SetIndexBuffer(2, this.majorPivotHighs, false);
			base.SetIndexLabel(2, "Major Pivot High");
			base.SetIndexStyle(3, 3, 2, -1, null);
			base.SetIndexArrow(3, this.pivotIcon);
			base.SetIndexBuffer(3, this.majorPivotLows, false);
			base.SetIndexLabel(3, "Major Pivot Low");
			return 0;
		}

		protected override int Deinit()
		{
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars;
			double num = 0.0;
			double num2 = 0.0;
			int num3 = 0;
			int num4 = 0;
			bool flag = false;
			bool flag2 = false;
			while (i >= 0)
			{
				double num5 = base.iFractals(base.Symbol, base.TimeFrame, 1, i);
				bool flag3 = num5 != 2147483647.0 && (base.MathAbs((double)(num4 - i)) >= (double)this.posDiff || base.MathAbs(num5 - num2) / ((num5 + num2) / 2.0) * 100.0 >= this.pipDiff);
				if (flag3)
				{
					bool flag4 = !flag;
					if (flag4)
					{
						bool flag5 = this.isMajorPivot(i, true);
						if (flag5)
						{
							this.majorPivotHighs[i, true] = base.High[i, true];
							flag2 = true;
						}
						else
						{
							this.pivotHighs[i, true] = base.High[i, true];
							flag2 = false;
						}
						num = base.High[i, true];
						num3 = i;
						flag = true;
					}
					else
					{
						bool flag6 = num5 > num;
						if (flag6)
						{
							bool flag7 = flag2;
							if (flag7)
							{
								this.majorPivotHighs[num3, true] = 2147483647.0;
							}
							else
							{
								this.pivotHighs[num3, true] = 2147483647.0;
							}
							bool flag8 = this.isMajorPivot(i, true);
							if (flag8)
							{
								this.majorPivotHighs[i, true] = base.High[i, true];
								flag2 = true;
							}
							else
							{
								this.pivotHighs[i, true] = base.High[i, true];
								flag2 = false;
							}
							num = base.High[i, true];
							num3 = i;
							flag = true;
						}
					}
				}
				double num6 = base.iFractals(base.Symbol, base.TimeFrame, 2, i);
				bool flag9 = num6 != 2147483647.0 && (base.MathAbs((double)(i - num3)) >= (double)this.posDiff || base.MathAbs(num - num6) / ((num + num6) / 2.0) * 100.0 >= this.pipDiff);
				if (flag9)
				{
					bool flag10 = flag;
					if (flag10)
					{
						bool flag11 = this.isMajorPivot(i, false);
						if (flag11)
						{
							this.majorPivotLows[i, true] = base.Low[i, true];
							flag2 = true;
						}
						else
						{
							this.pivotLows[i, true] = base.Low[i, true];
							flag2 = false;
						}
						num2 = base.Low[i, true];
						num4 = i;
						flag = false;
					}
					else
					{
						bool flag12 = num6 < num2;
						if (flag12)
						{
							bool flag13 = flag2;
							if (flag13)
							{
								this.majorPivotLows[num4, true] = 2147483647.0;
							}
							else
							{
								this.pivotLows[num4, true] = 2147483647.0;
							}
							bool flag14 = this.isMajorPivot(i, false);
							if (flag14)
							{
								this.majorPivotLows[i, true] = base.Low[i, true];
								flag2 = true;
							}
							else
							{
								this.pivotLows[i, true] = base.Low[i, true];
								flag2 = false;
							}
							num2 = base.Low[i, true];
							num4 = i;
							flag = false;
						}
					}
				}
				bool flag15 = num4 == num3;
				if (flag15)
				{
					this.pivotHighs[num3, true] = 2147483647.0;
					this.pivotLows[num4, true] = 2147483647.0;
				}
				i--;
			}
			return 0;
		}

		private bool isMajorPivot(int pos, bool high)
		{
			bool result;
			if (high)
			{
				int num = pos + 11;
				bool flag = num > base.Bars;
				if (flag)
				{
					result = false;
				}
				else
				{
					for (int i = pos; i < num; i++)
					{
						bool flag2 = base.High[i, true] > base.High[pos, true];
						if (flag2)
						{
							result = false;
							return result;
						}
					}
					num = pos - 10;
					bool flag3 = num < 0;
					if (flag3)
					{
						result = false;
					}
					else
					{
						for (int j = pos; j > num; j--)
						{
							bool flag4 = base.High[j, true] > base.High[pos, true];
							if (flag4)
							{
								result = false;
								return result;
							}
						}
						result = true;
					}
				}
			}
			else
			{
				int num2 = pos + 11;
				bool flag5 = num2 > base.Bars;
				if (flag5)
				{
					result = false;
				}
				else
				{
					for (int k = pos; k < num2; k++)
					{
						bool flag6 = base.Low[k, true] < base.Low[pos, true];
						if (flag6)
						{
							result = false;
							return result;
						}
					}
					num2 = pos - 11;
					bool flag7 = num2 < 0;
					if (flag7)
					{
						result = false;
					}
					else
					{
						for (int l = pos; l > num2; l--)
						{
							bool flag8 = base.Low[l, true] < base.Low[pos, true];
							if (flag8)
							{
								result = false;
								return result;
							}
						}
						result = true;
					}
				}
			}
			return result;
		}

		[Description("Parameters order Symbol, TimeFrame")]
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
				bool flag2 = !base.CompareString(base.Symbol, (string)values[0], false);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = base.TimeFrame != (int)values[1];
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = this.pipDiff != (double)values[2];
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = this.posDiff != (int)values[3];
							result = !flag5;
						}
					}
				}
			}
			return result;
		}

		[Description("Parameters order Symbol, TimeFrame")]
		public override void SetIndicatorParameters(params object[] values)
		{
			bool flag = values.Length != 2;
			if (flag)
			{
				throw new ArgumentException("Invalid parameters number");
			}
			base.Symbol = (string)values[0];
			base.TimeFrame = (int)values[1];
			this.pipDiff = (double)values[2];
			this.posDiff = (int)values[3];
		}
	}
}
