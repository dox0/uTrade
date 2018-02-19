using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Bill Williams' Awesome Indicator")]
	[Serializable]
	public class AO : IndicatorBase
	{
		public static int DefaultPeriod1 = 5;

		public static int DefaultPeriod2 = 34;

		public static MovingAverageType DefaultSmoothing = MovingAverageType.MODE_SMA;

		public static PriceConstants DefaultPriceConstants = PriceConstants.PRICE_MEDIAN;

		private readonly Array<double> _vals;

		[Category("Settings"), Description("Fast MA Period"), DisplayName("Fast MA")]
		public int Period1
		{
			get;
			set;
		}

		[Category("Settings"), Description("Slow MA Period"), DisplayName("Slow MA")]
		public int Period2
		{
			get;
			set;
		}

		[Category("Settings"), Description("Smoothing method"), DisplayName("Smoothing method")]
		public MovingAverageType Smoothing
		{
			get;
			set;
		}

		[Category("Settings"), Description("Base price"), DisplayName("Base price")]
		public PriceConstants PriceBase
		{
			get;
			set;
		}

		public AO()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this._vals = new Array<double>();
			this.Period1 = AO.DefaultPeriod1;
			this.Period2 = AO.DefaultPeriod2;
			base.SetIndexLabel(0, string.Format("AO({0},{1})", this.Period1, this.Period2));
			base.IndicatorShortName(string.Format("AO({0},{1})", this.Period1, this.Period2));
			this.Smoothing = AO.DefaultSmoothing;
			this.PriceBase = AO.DefaultPriceConstants;
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("AO({0},{1})", this.Period1, this.Period2));
			base.IndicatorShortName(string.Format("AO({0},{1})", this.Period1, this.Period2));
			base.SetIndexStyle(0, 2, -1, -1, null);
			base.SetIndexBuffer(0, this._vals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			bool flag = i > base.Bars - this.Period2;
			if (flag)
			{
				i = base.Bars - this.Period2;
			}
			while (i >= 0)
			{
				double num = base.iMA(base.Symbol, base.TimeFrame, this.Period1, 0, (int)this.Smoothing, (int)this.PriceBase, i);
				double num2 = base.iMA(base.Symbol, base.TimeFrame, this.Period2, 0, (int)this.Smoothing, (int)this.PriceBase, i);
				double value = num - num2;
				this._vals[i, true] = value;
				i--;
			}
			return 0;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 6;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					string value = (string)values[0];
					bool flag2 = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(base.Symbol);
					if (flag2)
					{
						bool flag3 = !base.Symbol.Equals(value, StringComparison.OrdinalIgnoreCase);
						if (flag3)
						{
							result = false;
							return result;
						}
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(base.Symbol) || !string.IsNullOrEmpty(value);
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					bool flag5 = (int)values[1] != base.TimeFrame;
					if (flag5)
					{
						result = false;
						return result;
					}
					bool flag6 = (int)values[2] != this.Period1;
					if (flag6)
					{
						result = false;
						return result;
					}
					bool flag7 = (int)values[3] != this.Period2;
					if (flag7)
					{
						result = false;
						return result;
					}
					bool flag8 = (MovingAverageType)values[4] != this.Smoothing;
					if (flag8)
					{
						result = false;
						return result;
					}
					bool flag9 = (PriceConstants)values[5] != this.PriceBase;
					if (flag9)
					{
						result = false;
						return result;
					}
				}
				catch (Exception exception)
				{
					this.Logger.ErrorException("IsSameParameters", exception);
				}
				result = true;
			}
			return result;
		}
	}
}
