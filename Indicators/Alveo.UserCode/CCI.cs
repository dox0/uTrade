using Alveo.Interfaces.UserCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Commodity Channel Index Indicator")]
	[Serializable]
	public class CCI : IndicatorBase
	{
		private readonly Array<double> _vals;

		private List<Array<double>> _levels;

		[Category("Settings"), Description("Period of the CCI Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch CCI will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public CCI()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("CCI({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("CCI({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._vals = new Array<double>();
		}

		protected override int Init()
		{
			for (int i = 1; i < base.indicator_buffers; i++)
			{
				base.SetIndexBuffer(i, null, false);
			}
			base.indicator_buffers = base.Levels.Values.Count + 1;
			base.SetIndexLabel(0, string.Format("CCI({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("CCI({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._vals, false);
			this._levels = new List<Array<double>>();
			for (int j = 0; j < base.Levels.Values.Count; j++)
			{
				Array<double> array = new Array<double>();
				base.SetIndexLabel(j + 1, string.Format("Level {0}", j + 1));
				base.SetIndexStyle(j + 1, 0, (int)base.Levels.Style, base.Levels.Width, base.Levels.Color);
				base.SetIndexBuffer(j + 1, array, false);
				this._levels.Add(array);
			}
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
			int j = base.Bars - base.IndicatorCounted();
			bool flag = j > base.Bars - this.IndicatorPeriod;
			if (flag)
			{
				j = base.Bars - this.IndicatorPeriod;
			}
			Array<double> price = base.GetPrice(base.GetHistory(base.Symbol, base.TimeFrame), this.PriceType);
			bool flag2 = price.Count == 0;
			int result;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				double num2 = 0.015 / (double)this.IndicatorPeriod;
				while (j >= 0)
				{
					double num3 = 0.0;
					int k = j + this.IndicatorPeriod - 1;
					double num4 = base.iMA(null, 0, this.IndicatorPeriod, 0, 0, (int)this.PriceType, j);
					while (k >= j)
					{
						num3 += base.MathAbs(price[k, true] - num4);
						k--;
					}
					double num5 = num3 * num2;
					double num6 = price[j, true] - num4;
					bool flag3 = num5.Equals(0.0);
					if (flag3)
					{
						this._vals[j, true] = 0.0;
					}
					else
					{
						this._vals[j, true] = num6 / num5;
					}
					j--;
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
