using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Chaikin Money Flow")]
	[Serializable]
	public class CMF : IndicatorBase
	{
		private readonly Array<double> cmfBuffer;

		[Category("Settings"), Description("Periods"), DisplayName("Periods")]
		public int periods
		{
			get;
			set;
		}

		public CMF()
		{
			base.indicator_chart_window = false;
			base.indicator_buffers = 1;
			base.indicator_level1 = 0.0;
			base.indicator_levelstyle = 2;
			base.indicator_levelcolor = Colors.Red;
			base.indicator_color1 = Colors.Red;
			this.cmfBuffer = new Array<double>();
			this.periods = 20;
		}

		protected override int Init()
		{
			base.SetIndexBuffer(0, this.cmfBuffer, false);
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexLabel(0, string.Format("CMF({0})", this.periods));
			base.IndicatorShortName(string.Format("CMF({0})", this.periods));
			base.SetIndexDrawBegin(0, this.periods);
			return 0;
		}

		protected override int Start()
		{
			int num = 0;
			int num2 = base.IndicatorCounted();
			bool flag = base.Bars <= this.periods;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = num2 < 1;
				if (flag2)
				{
					for (int i = 1; i <= this.periods; i++)
					{
						this.cmfBuffer[base.Bars - i, true] = 0.0;
					}
				}
				bool flag3 = num2 > 0;
				if (flag3)
				{
					num = base.Bars - num2;
				}
				bool flag4 = num2 == 0;
				if (flag4)
				{
					num = base.Bars - this.periods - 1;
				}
				for (int j = num; j >= 0; j--)
				{
					double num3 = 0.0;
					double num4 = 0.0;
					for (int k = 0; k < this.periods - 1; k++)
					{
						num4 += base.Volume[j + k, true];
						bool flag5 = base.High[j + k, true] - base.Low[j + k, true] > 0.0;
						if (flag5)
						{
							num3 += base.Volume[j + k, true] * (base.Close[j + k, true] - base.Open[j + k, true]) / (base.High[j + k, true] - base.Low[j + k, true]);
						}
					}
					this.cmfBuffer[j, true] = num3 / num4;
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
							bool flag5 = !(values[2] is int) || (int)values[2] != this.periods;
							result = !flag5;
						}
					}
				}
			}
			return result;
		}
	}
}
