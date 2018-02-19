using Alveo.Common.Classes;
using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Parabolic Stop and Reverse Indicator")]
	[Serializable]
	public class SAR : IndicatorBase
	{
		private readonly Array<double> _vals;

		private bool _first = true;

		private bool save_dirlong;

		private double save_ep;

		private double save_last_high;

		private double save_last_low;

		private int save_lastreverse;

		private double save_sar;

		private double save_start;

		[Category("Settings"), Description("Increment of SAR indicator")]
		public double Step
		{
			get;
			set;
		}

		[Category("Settings"), Description("Maximum value of SAR indicator")]
		public double Maximum
		{
			get;
			set;
		}

		public SAR()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = true;
			base.indicator_color1 = Colors.Red;
			this.Step = 0.02;
			this.Maximum = 0.2;
			base.SetIndexLabel(0, string.Format("SAR({0},{1})", this.Step, this.Maximum));
			base.IndicatorShortName(string.Format("SAR({0},{1})", this.Step, this.Maximum));
			this._vals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexStyle(0, 3, -1, -1, null);
			base.SetIndexArrow(0, 159);
			base.SetIndexLabel(0, string.Format("SAR({0},{1})", this.Step, this.Maximum));
			base.IndicatorShortName(string.Format("SAR({0},{1})", this.Step, this.Maximum));
			base.SetIndexBuffer(0, this._vals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			Array<Bar> history = base.GetHistory(base.Symbol, base.TimeFrame);
			bool flag = history.Count == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = i >= base.Bars - 1 || this._first;
				bool flag3;
				double num;
				double num2;
				double num3;
				double num4;
				double num7;
				if (flag2)
				{
					i = base.Bars - 2;
					this._first = false;
					flag3 = true;
					num = this.Step;
					num2 = -10000000.0;
					num3 = 10000000.0;
					this.save_lastreverse = 0;
					num4 = 0.0;
					while (i > 0)
					{
						this.save_lastreverse = i;
						double num5 = (double)history[i, true].Low;
						double num6 = (double)history[i, true].High;
						bool flag4 = num3 > num5;
						if (flag4)
						{
							num3 = num5;
						}
						bool flag5 = num2 < num6;
						if (flag5)
						{
							num2 = num6;
						}
						bool flag6 = num6 > (double)history[i + 1, true].High && num5 > (double)history[i + 1, true].Low;
						if (flag6)
						{
							break;
						}
						bool flag7 = num6 < (double)history[i + 1, true].High && num5 < (double)history[i + 1, true].Low;
						if (flag7)
						{
							flag3 = false;
							break;
						}
						i--;
					}
					for (int j = i; j < base.Bars; j++)
					{
						this._vals[j, true] = 0.0;
					}
					bool flag8 = flag3;
					if (flag8)
					{
						this._vals[i, true] = (double)history[i + 1, true].Low;
						num7 = (double)history[i, true].High;
					}
					else
					{
						this._vals[i, true] = (double)history[i + 1, true].High;
						num7 = (double)history[i, true].Low;
					}
					i--;
				}
				else
				{
					i = this.save_lastreverse;
					num = this.save_start;
					flag3 = this.save_dirlong;
					num2 = this.save_last_high;
					num3 = this.save_last_low;
					num7 = this.save_ep;
					num4 = this.save_sar;
				}
				while (i >= 0)
				{
					double num5 = (double)history[i, true].Low;
					double num6 = (double)history[i, true].High;
					double num8 = this._vals[i + 1, true];
					bool flag9 = flag3 && num5 < num8;
					if (flag9)
					{
						this.SaveLastReverse(i + 1, true, num, num5, num2, num7, num4);
						num = this.Step;
						flag3 = false;
						num7 = num5;
						num3 = num5;
						this._vals[i, true] = num2;
						i--;
					}
					else
					{
						bool flag10 = !flag3 && num6 > num8;
						if (flag10)
						{
							this.SaveLastReverse(i + 1, false, num, num3, num6, num7, num4);
							num = this.Step;
							flag3 = true;
							num7 = num6;
							num2 = num6;
							this._vals[i, true] = num3;
							i--;
						}
						else
						{
							num4 = num8 + num * (num7 - num8);
							bool flag11 = flag3;
							if (flag11)
							{
								bool flag12 = num7 < num6 && num + this.Step <= this.Maximum;
								if (flag12)
								{
									num += this.Step;
								}
								bool flag13 = num6 < (double)history[i + 1, true].High && i == base.Bars - 2;
								if (flag13)
								{
									num4 = num8;
								}
								bool flag14 = num4 > (double)history[i + 1, true].Low;
								if (flag14)
								{
									num4 = (double)history[i + 1, true].Low;
								}
								bool flag15 = num4 > (double)history[i + 2, true].Low;
								if (flag15)
								{
									num4 = (double)history[i + 2, true].Low;
								}
								bool flag16 = num4 > num5;
								if (flag16)
								{
									this.SaveLastReverse(i + 1, true, num, num5, num2, num7, num4);
									num = this.Step;
									flag3 = false;
									num7 = num5;
									num3 = num5;
									this._vals[i, true] = num2;
									i--;
									continue;
								}
								bool flag17 = num7 < num6;
								if (flag17)
								{
									num2 = num6;
									num7 = num6;
								}
							}
							else
							{
								bool flag18 = num7 > num5 && num + this.Step <= this.Maximum;
								if (flag18)
								{
									num += this.Step;
								}
								bool flag19 = num5 < (double)history[i + 1, true].Low && i == base.Bars - 2;
								if (flag19)
								{
									num4 = num8;
								}
								bool flag20 = num4 < (double)history[i + 1, true].High;
								if (flag20)
								{
									num4 = (double)history[i + 1, true].High;
								}
								bool flag21 = num4 < (double)history[i + 2, true].High;
								if (flag21)
								{
									num4 = (double)history[i + 2, true].High;
								}
								bool flag22 = num4 < num6;
								if (flag22)
								{
									this.SaveLastReverse(i + 1, false, num, num3, num6, num7, num4);
									num = this.Step;
									flag3 = true;
									num7 = num6;
									num2 = num6;
									this._vals[i, true] = num3;
									i--;
									continue;
								}
								bool flag23 = num7 > num5;
								if (flag23)
								{
									num3 = num5;
									num7 = num5;
								}
							}
							this._vals[i, true] = num4;
							i--;
						}
					}
				}
				result = 0;
			}
			return result;
		}

		private void SaveLastReverse(int last, bool dir, double start, double low, double high, double ep, double sar)
		{
			this.save_lastreverse = last;
			this.save_dirlong = dir;
			this.save_start = start;
			this.save_last_low = low;
			this.save_last_high = high;
			this.save_ep = ep;
			this.save_sar = sar;
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
							bool flag5 = !(values[2] is double) || !this.Step.Equals((double)values[2]);
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !(values[3] is double) || !this.Maximum.Equals((double)values[3]);
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
