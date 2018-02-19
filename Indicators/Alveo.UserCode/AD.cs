using Alveo.Common.Classes;
using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Accumulation/Distribution Indicator")]
	[Serializable]
	public class AD : IndicatorBase
	{
		private readonly Array<double> _vals;

		public AD()
		{
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			base.SetIndexLabel(0, "AD");
			base.IndicatorShortName("AD");
			this._vals = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, "AD");
			base.IndicatorShortName("AD");
			base.SetIndexBuffer(0, this._vals, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			bool flag = i >= base.Bars;
			if (flag)
			{
				i = base.Bars - 1;
			}
			Array<Bar> history = base.GetHistory(base.Symbol, base.TimeFrame);
			bool flag2 = history.Count == 0;
			int result;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				double num = 0.0;
				bool flag3 = i < base.Bars - 1;
				if (flag3)
				{
					num = this._vals[i + 1, true];
				}
				while (i >= 0)
				{
					Bar bar = history[i, true];
					double num2 = (double)(bar.Close - bar.Low) - (double)(bar.High - bar.Close);
					bool flag4 = !num2.Equals(0.0);
					if (flag4)
					{
						double num3 = (double)(bar.High - bar.Low);
						bool flag5 = num3.Equals(0.0);
						if (flag5)
						{
							num2 = 0.0;
						}
						else
						{
							num2 /= num3;
							num2 *= (double)bar.Volume;
						}
					}
					num2 += num;
					this._vals[i, true] = num2;
					num = num2;
					i--;
				}
				result = 0;
			}
			return result;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 2;
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
