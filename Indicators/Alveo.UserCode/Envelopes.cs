using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Envelopes Indicator")]
	[Serializable]
	public class Envelopes : IndicatorBase
	{
		private readonly Array<double> _minusVals;

		private readonly Array<double> _plusVals;

		[Category("Settings"), Description("Period of the Envelopes Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Type of Moving Avarange the Envelopes Indicator"), DisplayName("MA Type")]
		public MovingAverageType MAType
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch Envelopes will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		[Category("Settings"), Description("Deviation of the Envelopes Indicator")]
		public double Deviation
		{
			get;
			set;
		}

		public Envelopes()
		{
			base.indicator_buffers = 2;
			base.indicator_chart_window = true;
			base.indicator_color1 = Colors.Green;
			base.indicator_color2 = Colors.Red;
			this.IndicatorPeriod = 10;
			this.Deviation = 0.1;
			base.SetIndexLabel(0, string.Format("Env({0})Upper", this.IndicatorPeriod));
			base.SetIndexLabel(1, string.Format("Env({0})Lower", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("Envelopes({0})", this.IndicatorPeriod));
			this.PriceType = PriceConstants.PRICE_CLOSE;
			this._plusVals = new Array<double>();
			this._minusVals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("Env({0})Upper", this.IndicatorPeriod));
			base.SetIndexLabel(1, string.Format("Env({0})Lower", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("Envelopes({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._plusVals, false);
			base.SetIndexBuffer(1, this._minusVals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			bool flag = i > base.Bars - this.IndicatorPeriod;
			if (flag)
			{
				i = base.Bars - this.IndicatorPeriod;
			}
			double num = 1.0 + this.Deviation / 100.0;
			double num2 = 1.0 - this.Deviation / 100.0;
			while (i >= 0)
			{
				double num3 = base.iMA(base.Symbol, base.TimeFrame, this.IndicatorPeriod, 0, 0, (int)this.PriceType, i);
				this._plusVals[i, true] = num * num3;
				this._minusVals[i, true] = num2 * num3;
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
								bool flag6 = !(values[3] is MovingAverageType) || (MovingAverageType)values[3] != this.MAType;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = !(values[4] is PriceConstants) || (PriceConstants)values[4] != this.PriceType;
									result = (!flag7 && values[5] is double && (double)values[5] == this.Deviation);
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
