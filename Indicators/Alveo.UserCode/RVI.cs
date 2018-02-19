using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Relative Vigor index Indicator")]
	[Serializable]
	public class RVI : IndicatorBase
	{
		private readonly Array<double> _signalVals;

		private readonly Array<double> _vals;

		[Category("Settings"), Description("Period of the RVI Indicator"), DisplayName("Period")]
		public int IndicatorPeriod
		{
			get;
			set;
		}

		public RVI()
		{
			base.indicator_buffers = 2;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			base.indicator_color2 = Colors.Blue;
			this.IndicatorPeriod = 10;
			base.SetIndexLabel(0, string.Format("RVI({0})", this.IndicatorPeriod));
			base.SetIndexLabel(1, "Signal");
			base.IndicatorShortName(string.Format("RVI({0})", this.IndicatorPeriod));
			this._vals = new Array<double>();
			this._signalVals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("RVI({0})", this.IndicatorPeriod));
			base.IndicatorShortName(string.Format("RVI({0})", this.IndicatorPeriod));
			base.SetIndexBuffer(0, this._vals, false);
			base.SetIndexBuffer(1, this._signalVals, false);
			return 0;
		}

		protected override int Start()
		{
			bool flag = base.Bars < this.IndicatorPeriod;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int i = base.Bars - base.IndicatorCounted();
				bool flag2 = i > base.Bars - this.IndicatorPeriod - 3;
				if (flag2)
				{
					i = base.Bars - this.IndicatorPeriod - 3;
				}
				while (i >= 0)
				{
					double num = 0.0;
					double num2 = 0.0;
					for (int j = 0; j < this.IndicatorPeriod; j++)
					{
						int num3 = i + j;
						double num4 = (base.Close[num3, true] - base.Open[num3, true] + 2.0 * (base.Close[num3 + 1, true] - base.Open[num3 + 1, true]) + 2.0 * (base.Close[num3 + 2, true] - base.Open[num3 + 2, true]) + (base.Close[num3 + 3, true] - base.Open[num3 + 3, true])) / 6.0;
						double num5 = (base.High[num3, true] - base.Low[num3, true] + 2.0 * (base.High[num3 + 1, true] - base.Low[num3 + 1, true]) + 2.0 * (base.High[num3 + 2, true] - base.Low[num3 + 2, true]) + (base.High[num3 + 3, true] - base.Low[num3 + 3, true])) / 6.0;
						num += num4;
						num2 += num5;
					}
					bool flag3 = !num2.Equals(0.0);
					if (flag3)
					{
						this._vals[i, true] = num / num2;
					}
					else
					{
						this._vals[i, true] = num;
					}
					i--;
				}
				i = base.Bars - base.IndicatorCounted();
				bool flag4 = i > base.Bars - this.IndicatorPeriod - 6;
				if (flag4)
				{
					i = base.Bars - this.IndicatorPeriod - 6;
				}
				while (i >= 0)
				{
					this._signalVals[i, true] = (this._vals[i, true] + 2.0 * this._vals[i + 1, true] + 2.0 * this._vals[i + 2, true] + this._vals[i + 3, true]) / 6.0;
					i--;
				}
				result = 0;
			}
			return result;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 3;
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
							result = !flag5;
						}
					}
				}
			}
			return result;
		}
	}
}
