using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Keltner Channels")]
	[Serializable]
	public class KeltnerChannels : IndicatorBase
	{
		private readonly Array<double> _upper;

		private readonly Array<double> _middle;

		private readonly Array<double> _lower;

		[Category("Settings"), Description("Period"), DisplayName("Period")]
		public int period
		{
			get;
			set;
		}

		public KeltnerChannels()
		{
			base.indicator_buffers = 3;
			base.indicator_chart_window = true;
			base.indicator_color1 = Colors.Red;
			base.indicator_color2 = Colors.DarkBlue;
			base.indicator_color3 = Colors.Red;
			this.period = 10;
			this._upper = new Array<double>();
			this._middle = new Array<double>();
			this._lower = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexBuffer(0, this._upper, false);
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexShift(0, 0);
			base.SetIndexDrawBegin(0, 0);
			base.SetIndexBuffer(1, this._middle, false);
			base.SetIndexStyle(1, 0, 3, -1, null);
			base.SetIndexShift(1, 0);
			base.SetIndexDrawBegin(1, 0);
			base.SetIndexBuffer(2, this._lower, false);
			base.SetIndexStyle(2, 0, -1, -1, null);
			base.SetIndexShift(2, 0);
			base.SetIndexDrawBegin(2, 0);
			base.SetIndexLabel(0, "KChanUp(" + this.period + ")");
			base.SetIndexLabel(1, "KChanMid(" + this.period + ")");
			base.SetIndexLabel(2, "KChanLow(" + this.period + ")");
			base.IndicatorShortName(string.Format("KCH({0})", this.period));
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			bool flag = num < 0;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				bool flag2 = num > 0;
				if (flag2)
				{
					num--;
				}
				int num2 = base.Bars - num;
				bool flag3 = num == 0;
				if (flag3)
				{
					num2 -= 1 + this.period;
				}
				for (int i = 0; i < num2; i++)
				{
					this._middle[i, true] = base.iMA(null, 0, this.period, 0, 0, 5, i);
					double num3 = this.findAvg(this.period, i);
					this._upper[i, true] = this._middle[i, true] + num3;
					this._lower[i, true] = this._middle[i, true] - num3;
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
							bool flag5 = !(values[2] is int) || (int)values[2] != this.period;
							result = !flag5;
						}
					}
				}
			}
			return result;
		}

		private double findAvg(int period, int shift)
		{
			double num = 0.0;
			for (int i = shift; i < shift + period; i++)
			{
				num += base.High[i, true] - base.Low[i, true];
			}
			return num / (double)period;
		}
	}
}
