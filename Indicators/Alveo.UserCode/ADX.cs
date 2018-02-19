using Alveo.Common.Classes;
using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Average Directional Movement Indicator")]
	[Serializable]
	public class ADX : IndicatorBase
	{
		private readonly Array<double> _adx;

		private readonly Array<double> _minusDi;

		private readonly Array<double> _plusDi;

		[Category("Settings"), Description("Period of the ADX Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch ADX will be calculated"), DisplayName("Price type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public ADX()
		{
			base.indicator_buffers = 3;
			this.IndicatorPeriod = 10;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Blue;
			base.SetIndexLabel(0, string.Format("ADX({0})", this.IndicatorPeriod));
			base.indicator_color2 = Colors.Green;
			base.SetIndexLabel(1, "Plus_Di");
			base.indicator_color3 = Colors.Red;
			base.SetIndexLabel(2, "Minus_Di");
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._adx = new Array<double>();
			this._plusDi = new Array<double>();
			this._minusDi = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("ADX({0})", this.IndicatorPeriod));
			base.SetIndexLabel(1, "Plus_Di");
			base.SetIndexLabel(2, "Minus_Di");
			base.IndicatorShortName(string.Format("ADX({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._adx, false);
			base.SetIndexBuffer(1, this._plusDi, false);
			base.SetIndexBuffer(2, this._minusDi, false);
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
				Array<double> price = base.GetPrice(base.GetHistory(base.Symbol, base.TimeFrame), this.PriceType);
				bool flag2 = i >= base.Bars - 1;
				if (flag2)
				{
					i = base.Bars - 1;
					this._plusDi[i, true] = 0.0;
					this._minusDi[i, true] = 0.0;
					this._adx[i, true] = 0.0;
					i--;
				}
				double num = 2.0 / (double)(this.IndicatorPeriod + 1);
				while (i >= 0)
				{
					bool flag3 = i >= history.Count;
					if (flag3)
					{
						i--;
					}
					else
					{
						double num2 = (double)history[i, true].Low;
						double num3 = (double)history[i, true].High;
						double num4 = num3 - (double)history[i + 1, true].High;
						double num5 = (double)history[i + 1, true].Low - num2;
						bool flag4 = num4 < 0.0;
						if (flag4)
						{
							num4 = 0.0;
						}
						bool flag5 = num5 < 0.0;
						if (flag5)
						{
							num5 = 0.0;
						}
						bool flag6 = num4.Equals(num5);
						if (flag6)
						{
							num4 = 0.0;
							num5 = 0.0;
						}
						else
						{
							bool flag7 = num4 < num5;
							if (flag7)
							{
								num4 = 0.0;
							}
							else
							{
								bool flag8 = num5 < num4;
								if (flag8)
								{
									num5 = 0.0;
								}
							}
						}
						double value = base.MathAbs(num3 - num2);
						double value2 = base.MathAbs(num3 - price[i + 1, true]);
						double value3 = base.MathAbs(num2 - price[i + 1, true]);
						double num6 = base.MathMax(value, value2);
						num6 = base.MathMax(num6, value3);
						bool flag9 = num6.Equals(0.0);
						double num7;
						double num8;
						if (flag9)
						{
							num7 = 0.0;
							num8 = 0.0;
						}
						else
						{
							num7 = 100.0 * num4 / num6;
							num8 = 100.0 * num5 / num6;
						}
						this._plusDi[i, true] = num7 * num + this._plusDi[i + 1, true] * (1.0 - num);
						this._minusDi[i, true] = num8 * num + this._minusDi[i + 1, true] * (1.0 - num);
						double num9 = base.MathAbs(this._plusDi[i, true] + this._minusDi[i, true]);
						bool flag10 = num9.Equals(0.0);
						double num10;
						if (flag10)
						{
							num10 = 0.0;
						}
						else
						{
							num10 = 100.0 * (base.MathAbs(this._plusDi[i, true] - this._minusDi[i, true]) / num9);
						}
						this._adx[i, true] = num10 * num + this._adx[i + 1, true] * (1.0 - num);
						i--;
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
	}
}
