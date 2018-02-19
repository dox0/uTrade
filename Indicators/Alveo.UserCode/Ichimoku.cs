using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Ichimoku Kinko Hyo Indicator")]
	[Serializable]
	public class Ichimoku : IndicatorBase
	{
		private readonly Array<double> _ChinkouSpan;

		private readonly Array<double> _KijunSen;

		private readonly Array<double> _SenkouSpanA;

		private readonly Array<double> _SenkouSpanA2;

		private readonly Array<double> _SenkouSpanB;

		private readonly Array<double> _SenkouSpanB2;

		private readonly Array<double> _TenkanSen;

		private int a_begin;

		[Category("Settings"), Description("Tenkan Sen averaging period of the Ichimoku Indicator"), DisplayName("Tenkan Sen")]
		public int TenkanSen
		{
			get;
			set;
		}

		[Category("Settings"), Description("Kijun Sen averaging period of the Ichimoku Indicator"), DisplayName("Kijun Sen")]
		public int KijunSen
		{
			get;
			set;
		}

		[Category("Settings"), Description("Senkou SpanB averaging period of the Ichimoku Indicator"), DisplayName("Senkou Span-B")]
		public int SenkouSpanB
		{
			get;
			set;
		}

		public Ichimoku()
		{
			base.indicator_buffers = 7;
			base.indicator_chart_window = true;
			this.TenkanSen = 9;
			this.KijunSen = 26;
			this.SenkouSpanB = 52;
			base.indicator_color1 = Colors.Blue;
			base.indicator_color2 = Colors.Green;
			base.indicator_color3 = Colors.Red;
			base.indicator_color4 = Colors.Indigo;
			base.indicator_color5 = Colors.Orange;
			base.indicator_color6 = Colors.SaddleBrown;
			base.indicator_color7 = Colors.CornflowerBlue;
			base.SetIndexLabel(0, string.Format("Tenkan Sen({0})", this.TenkanSen));
			base.SetIndexLabel(1, string.Format("Kijun Sen({0})", this.KijunSen));
			base.SetIndexLabel(2, "Senkou Span A");
			base.SetIndexLabel(3, string.Format("Senkou Span B({0})", this.SenkouSpanB));
			base.SetIndexLabel(4, "Chinkou Span");
			base.SetIndexLabel(5, "SenkouSpanA2");
			base.SetIndexLabel(6, "SenkouSpanB2");
			base.IndicatorShortName(string.Format("Ichimoku({0},{1},{2})", this.TenkanSen, this.KijunSen, this.SenkouSpanB));
			this._TenkanSen = new Array<double>();
			this._KijunSen = new Array<double>();
			this._SenkouSpanA = new Array<double>();
			this._SenkouSpanB = new Array<double>();
			this._ChinkouSpan = new Array<double>();
			this._SenkouSpanA2 = new Array<double>();
			this._SenkouSpanB2 = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexBuffer(0, this._TenkanSen, false);
			base.SetIndexDrawBegin(0, this.TenkanSen - 1);
			base.SetIndexLabel(0, string.Format("Tenkan Sen({0})", this.TenkanSen));
			base.SetIndexStyle(1, 0, -1, -1, null);
			base.SetIndexBuffer(1, this._KijunSen, false);
			base.SetIndexDrawBegin(1, this.KijunSen - 1);
			base.SetIndexLabel(1, string.Format("Kijun Sen({0})", this.KijunSen));
			this.a_begin = this.KijunSen;
			bool flag = this.a_begin < this.TenkanSen;
			if (flag)
			{
				this.a_begin = this.TenkanSen;
			}
			base.SetIndexStyle(2, 5, 2, -1, null);
			base.SetIndexBuffer(2, this._SenkouSpanA, true);
			base.SetIndexDrawBegin(2, this.KijunSen + this.a_begin - 1);
			base.SetIndexShift(2, this.KijunSen);
			base.SetIndexStyle(5, 0, 2, -1, null);
			base.SetIndexBuffer(5, this._SenkouSpanA2, true);
			base.SetIndexDrawBegin(5, this.KijunSen + this.a_begin - 1);
			base.SetIndexShift(5, this.KijunSen);
			base.SetIndexStyle(3, 5, 2, -1, null);
			base.SetIndexBuffer(3, this._SenkouSpanB, true);
			base.SetIndexDrawBegin(3, this.KijunSen + this.SenkouSpanB - 1);
			base.SetIndexShift(3, this.KijunSen);
			base.SetIndexStyle(6, 0, 2, -1, null);
			base.SetIndexBuffer(6, this._SenkouSpanB2, true);
			base.SetIndexDrawBegin(6, this.KijunSen + this.SenkouSpanB - 1);
			base.SetIndexShift(6, this.KijunSen);
			base.SetIndexLabel(3, string.Format("Senkou Span B({0})", this.SenkouSpanB));
			base.SetIndexStyle(4, 0, -1, -1, null);
			base.SetIndexBuffer(4, this._ChinkouSpan, false);
			base.SetIndexShift(4, -this.KijunSen);
			base.IndicatorShortName(string.Format("Ichimoku({0},{1},{2})", this.TenkanSen, this.KijunSen, this.SenkouSpanB));
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			bool flag = base.Bars <= this.TenkanSen || base.Bars <= this.KijunSen || base.Bars <= this.SenkouSpanB;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = num < 1;
				int i;
				if (flag2)
				{
					for (i = 1; i < this.TenkanSen; i++)
					{
						this._TenkanSen[base.Bars - i, true] = 0.0;
					}
					for (i = 1; i < this.KijunSen; i++)
					{
						this._KijunSen[base.Bars - i, true] = 0.0;
					}
					for (i = 1; i < this.a_begin; i++)
					{
						this._SenkouSpanA[base.Bars - i, true] = 0.0;
						this._SenkouSpanA2[base.Bars + base.Chart.NumForecastBars - i, true] = 0.0;
					}
					for (i = 1; i < this.SenkouSpanB; i++)
					{
						this._SenkouSpanB[base.Bars - i, true] = 0.0;
						this._SenkouSpanB2[base.Bars + base.Chart.NumForecastBars - i, true] = 0.0;
					}
				}
				i = base.Bars - this.TenkanSen;
				bool flag3 = num > this.TenkanSen;
				if (flag3)
				{
					i = base.Bars - num - 1;
				}
				while (i >= 0)
				{
					double num2 = base.High[i, true];
					double num3 = base.Low[i, true];
					for (int j = i + this.TenkanSen - 1; j >= i; j--)
					{
						double num4 = base.High[j, true];
						bool flag4 = num2 < num4;
						if (flag4)
						{
							num2 = num4;
						}
						num4 = base.Low[j, true];
						bool flag5 = num3 > num4;
						if (flag5)
						{
							num3 = num4;
						}
					}
					this._TenkanSen[i, true] = (num2 + num3) / 2.0;
					i--;
				}
				i = base.Bars - this.KijunSen;
				bool flag6 = num > this.KijunSen;
				if (flag6)
				{
					i = base.Bars - num - 1;
				}
				while (i >= 0)
				{
					double num2 = base.High[i, true];
					double num3 = base.Low[i, true];
					for (int j = i + this.KijunSen - 1; j >= i; j--)
					{
						double num4 = base.High[j, true];
						bool flag7 = num2 < num4;
						if (flag7)
						{
							num2 = num4;
						}
						num4 = base.Low[j, true];
						bool flag8 = num3 > num4;
						if (flag8)
						{
							num3 = num4;
						}
					}
					this._KijunSen[i, true] = (num2 + num3) / 2.0;
					i--;
				}
				i = base.Bars - this.a_begin + 1;
				bool flag9 = num > this.a_begin - 1;
				if (flag9)
				{
					i = base.Bars - num - 1;
				}
				while (i >= 0)
				{
					double num4 = (this._KijunSen[i, true] + this._TenkanSen[i, true]) / 2.0;
					this._SenkouSpanA[i + base.Chart.NumForecastBars, true] = num4;
					this._SenkouSpanA2[i + base.Chart.NumForecastBars, true] = num4;
					i--;
				}
				i = base.Bars - this.SenkouSpanB;
				bool flag10 = num > this.SenkouSpanB;
				if (flag10)
				{
					i = base.Bars - num - 1;
				}
				while (i >= 0)
				{
					double num2 = base.High[i, true];
					double num3 = base.Low[i, true];
					double num4;
					for (int j = i - 1 + this.SenkouSpanB; j >= i; j--)
					{
						num4 = base.High[j, true];
						bool flag11 = num2 < num4;
						if (flag11)
						{
							num2 = num4;
						}
						num4 = base.Low[j, true];
						bool flag12 = num3 > num4;
						if (flag12)
						{
							num3 = num4;
						}
					}
					num4 = (num2 + num3) / 2.0;
					this._SenkouSpanB[i + base.Chart.NumForecastBars, true] = num4;
					this._SenkouSpanB2[i + base.Chart.NumForecastBars, true] = num4;
					i--;
				}
				i = base.Bars - 1;
				bool flag13 = num > 1;
				if (flag13)
				{
					i = base.Bars - num - 1;
				}
				while (i >= 0)
				{
					this._ChinkouSpan[i, true] = base.Close[i, true];
					i--;
				}
				result = 0;
			}
			return result;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 5;
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
							bool flag5 = !(values[2] is int) || (int)values[2] != this.TenkanSen;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !(values[3] is int) || (int)values[3] != this.KijunSen;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = !(values[4] is int) || (int)values[4] != this.SenkouSpanB;
									result = !flag7;
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}
