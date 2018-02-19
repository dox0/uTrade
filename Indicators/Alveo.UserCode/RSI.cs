using Alveo.Common.Classes;
using Alveo.Interfaces.UserCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Relative Strength Index Indicator")]
	[Serializable]
	public class RSI : IndicatorBase
	{
		private readonly Array<double> _negBuf;

		private readonly Array<double> _posBuf;

		private readonly Array<double> _vals;

		private List<Array<double>> _levels;

		[Category("Settings"), Description("Period of the RSI Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch RSI will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public RSI()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.Levels.Values.Add(new Alveo.Interfaces.UserCode.Double(70.0));
			base.Levels.Values.Add(new Alveo.Interfaces.UserCode.Double(30.0));
			base.SetIndexLabel(0, string.Format("RSI({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("RSI({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._vals = new Array<double>();
			this._posBuf = new Array<double>();
			this._negBuf = new Array<double>();
		}

		protected override int Init()
		{
			for (int i = 1; i < base.indicator_buffers; i++)
			{
				base.SetIndexBuffer(i, null, false);
			}
			base.indicator_buffers = base.Levels.Values.Count + 1;
			base.SetIndexLabel(0, string.Format("RSI({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("RSI({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._vals, false);
			this._levels = new List<Array<double>>();
			int j;
			for (j = 0; j < base.Levels.Values.Count; j++)
			{
				Array<double> array = new Array<double>();
				base.SetIndexLabel(j + 1, string.Format("Level {0}", j + 1));
				base.SetIndexStyle(j + 1, 0, (int)base.Levels.Style, base.Levels.Width, base.Levels.Color);
				base.SetIndexBuffer(j + 1, array, false);
				this._levels.Add(array);
			}
			base.SetIndexBuffer(j + 1, this._posBuf, false);
			base.SetIndexBuffer(j + 2, this._negBuf, false);
			return 0;
		}

		protected override int Start()
		{
			this._vals[this._vals.Count - 1, true] = 100.0;
			this._vals[this._vals.Count - 2, true] = 0.0;
			int num = 0;
			foreach (Array<double> current in this._levels)
			{
				for (int i = 0; i < current.Count; i++)
				{
					current[i, true] = base.Levels.Values[num].Value;
				}
				num++;
			}
			bool flag = base.Bars < this.IndicatorPeriod;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int j = base.Bars - base.IndicatorCounted();
				Array<Bar> history = base.GetHistory(base.Symbol, base.TimeFrame);
				bool flag2 = history.Count == 0;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					bool flag3 = j > base.Bars - this.IndicatorPeriod - 1;
					if (flag3)
					{
						j = base.Bars - this.IndicatorPeriod - 1;
					}
					decimal num2 = 0m;
					decimal num3 = 0m;
					bool flag4 = j == base.Bars - this.IndicatorPeriod - 1;
					if (flag4)
					{
						for (int k = base.Bars - 2; k >= j; k--)
						{
							decimal num4 = history[k, true].Close - history[k + 1, true].Close;
							bool flag5 = num4 > decimal.Zero;
							if (flag5)
							{
								num3 += num4;
							}
							else
							{
								num2 -= num4;
							}
						}
						decimal num5 = num3 / this.IndicatorPeriod;
						decimal num6 = num2 / this.IndicatorPeriod;
						this._posBuf[j, true] = (double)num5;
						this._negBuf[j, true] = (double)num6;
						bool flag6 = num6.Equals(0.0);
						if (flag6)
						{
							this._vals[j, true] = 0.0;
						}
						else
						{
							this._vals[j, true] = (double)(100m - 100m / (decimal.One + num5 / num6));
						}
						j--;
					}
					while (j >= 0)
					{
						num2 = 0m;
						num3 = 0m;
						decimal num4 = history[j, true].Close - history[j + 1, true].Close;
						bool flag7 = num4 > decimal.Zero;
						if (flag7)
						{
							num3 = num4;
						}
						else
						{
							num2 = -num4;
						}
						decimal num5 = ((decimal)this._posBuf[j + 1, true] * (this.IndicatorPeriod - 1) + num3) / this.IndicatorPeriod;
						decimal num6 = ((decimal)this._negBuf[j + 1, true] * (this.IndicatorPeriod - 1) + num2) / this.IndicatorPeriod;
						this._posBuf[j, true] = (double)num5;
						this._negBuf[j, true] = (double)num6;
						bool flag8 = num6.Equals(0.0);
						if (flag8)
						{
							this._vals[j, true] = 0.0;
						}
						else
						{
							this._vals[j, true] = (double)(100m - 100m / (decimal.One + num5 / num6));
						}
						j--;
					}
					result = 0;
				}
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
