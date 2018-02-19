using Alveo.Common.Classes;
using Alveo.Interfaces.UserCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Alveo.UserCode
{
	[Description("Stochastic oscillator Indicator")]
	[Serializable]
	public class Stochastic : IndicatorBase
	{
		private readonly Array<double> _highesBuffer = new Array<double>();

		private readonly Array<double> _lowesBuffer = new Array<double>();

		private readonly Array<double> _mainBuffer = new Array<double>();

		private readonly Array<double> _signalBuffer = new Array<double>();

		private List<Array<double>> _levels;

		private int draw_begin1;

		private int draw_begin2;

		[Category("Settings"), Description("%K Period of the Stochastic Indicator"), DisplayName("K-Period")]
		public int KPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("%D Period of the Stochastic Indicator"), DisplayName("D-Period")]
		public int DPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Slowing value of the Stochastic Indicator")]
		public int Slowing
		{
			get;
			set;
		}

		[Category("Settings"), Description("Moving Average type on witch Stochastic will be calculated"), DisplayName("MA Type")]
		public MovingAverageType MAType
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch Stochastic will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public Stochastic()
		{
			base.indicator_buffers = 2;
			base.indicator_chart_window = false;
			this.KPeriod = 5;
			this.DPeriod = 3;
			this.Slowing = 3;
			this.PriceType = PriceConstants.PRICE_CLOSE;
			base.indicator_color1 = CodeBase.LightSeaGreen;
			base.indicator_color2 = CodeBase.Red;
			base.Levels.Values.Add(new Alveo.Interfaces.UserCode.Double(70.0));
			base.Levels.Values.Add(new Alveo.Interfaces.UserCode.Double(30.0));
			string text = string.Concat(new object[]
			{
				"Sto(",
				this.KPeriod,
				",",
				this.DPeriod,
				",",
				this.Slowing,
				")"
			});
			base.IndicatorShortName(text);
			base.SetIndexLabel(0, text);
			base.SetIndexLabel(1, "Signal");
		}

		protected override int Init()
		{
			for (int i = 2; i < base.indicator_buffers; i++)
			{
				base.SetIndexBuffer(i, null, false);
			}
			base.indicator_buffers = base.Levels.Values.Count + 2;
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexBuffer(0, this._mainBuffer, false);
			base.SetIndexStyle(1, 0, -1, -1, null);
			base.SetIndexBuffer(1, this._signalBuffer, false);
			string text = string.Concat(new object[]
			{
				"Sto(",
				this.KPeriod,
				",",
				this.DPeriod,
				",",
				this.Slowing,
				")"
			});
			base.IndicatorShortName(text);
			base.SetIndexLabel(0, text);
			base.SetIndexLabel(1, "Signal");
			this.draw_begin1 = this.KPeriod + this.Slowing;
			this.draw_begin2 = this.draw_begin1 + this.DPeriod;
			base.SetIndexDrawBegin(0, this.draw_begin1);
			base.SetIndexDrawBegin(1, this.draw_begin2);
			this._levels = new List<Array<double>>();
			int j;
			for (j = 0; j < base.Levels.Values.Count; j++)
			{
				Array<double> array = new Array<double>();
				base.SetIndexLabel(j + 2, string.Format("Level {0}", j + 1));
				base.SetIndexStyle(j + 2, 0, (int)base.Levels.Style, base.Levels.Width, base.Levels.Color);
				base.SetIndexBuffer(j + 2, array, false);
				this._levels.Add(array);
			}
			base.SetIndexBuffer(j + 2, this._highesBuffer, false);
			base.SetIndexBuffer(j + 3, this._lowesBuffer, false);
			return 0;
		}

		protected override int Start()
		{
			int num = 0;
			foreach (Array<double> current in this._levels)
			{
				for (int i = 0; i < current.Count; i++)
				{
					current[i, true] = base.Levels.Values[num].Value;
				}
				num++;
			}
			int num2 = base.IndicatorCounted();
			bool flag = base.Bars <= this.draw_begin2;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				Array<Bar> history = base.GetHistory(base.Symbol, base.TimeFrame);
				bool flag2 = history.Count == 0;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					bool flag3 = num2 < 1;
					int j;
					if (flag3)
					{
						for (j = 1; j <= this.draw_begin1; j++)
						{
							this._mainBuffer[base.Bars - j, true] = 0.0;
						}
						for (j = 1; j <= this.draw_begin2; j++)
						{
							this._signalBuffer[base.Bars - j, true] = 0.0;
						}
					}
					j = base.Bars - this.KPeriod;
					bool flag4 = num2 > this.KPeriod;
					if (flag4)
					{
						j = base.Bars - num2 - 1;
					}
					while (j >= 0)
					{
						double num3 = 1000000.0;
						for (int k = j + this.KPeriod - 1; k >= j; k--)
						{
							double num4 = (double)history[k, true].Low;
							bool flag5 = num3 > num4;
							if (flag5)
							{
								num3 = num4;
							}
						}
						this._lowesBuffer[j, true] = num3;
						j--;
					}
					j = base.Bars - this.KPeriod;
					bool flag6 = num2 > this.KPeriod;
					if (flag6)
					{
						j = base.Bars - num2 - 1;
					}
					while (j >= 0)
					{
						double num5 = -1000000.0;
						for (int k = j + this.KPeriod - 1; k >= j; k--)
						{
							double num4 = (double)history[k, true].High;
							bool flag7 = num5 < num4;
							if (flag7)
							{
								num5 = num4;
							}
						}
						this._highesBuffer[j, true] = num5;
						j--;
					}
					j = base.Bars - this.draw_begin1;
					bool flag8 = num2 > this.draw_begin1;
					if (flag8)
					{
						j = base.Bars - num2 - 1;
					}
					while (j >= 0)
					{
						double num6 = 0.0;
						double num7 = 0.0;
						for (int k = j + this.Slowing - 1; k >= j; k--)
						{
							num6 += (double)history[k, true].Close - this._lowesBuffer[k, true];
							num7 += this._highesBuffer[k, true] - this._lowesBuffer[k, true];
						}
						bool flag9 = num7 == 0.0;
						if (flag9)
						{
							this._mainBuffer[j, true] = 100.0;
						}
						else
						{
							this._mainBuffer[j, true] = num6 / num7 * 100.0;
						}
						j--;
					}
					bool flag10 = num2 > 0;
					if (flag10)
					{
						num2--;
					}
					int num8 = base.Bars - num2;
					for (j = 0; j < num8; j++)
					{
						this._signalBuffer[j, true] = base.iMAOnArray(this._mainBuffer, base.Bars, this.DPeriod, 0, 0, j);
					}
					result = 0;
				}
			}
			return result;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 7;
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
							bool flag5 = !(values[2] is int) || (int)values[2] != this.KPeriod;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !(values[3] is int) || (int)values[3] != this.DPeriod;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = !(values[4] is int) || (int)values[4] != this.Slowing;
									if (flag7)
									{
										result = false;
									}
									else
									{
										bool flag8 = !(values[5] is MovingAverageType) || (MovingAverageType)values[5] != this.MAType;
										if (flag8)
										{
											result = false;
										}
										else
										{
											bool flag9 = !(values[6] is PriceConstants) || (PriceConstants)values[6] != this.PriceType;
											result = !flag9;
										}
									}
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
