using Alveo.Common.Classes;
using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Bill Williams Market Facilitation Index Indicator")]
	[Serializable]
	public class BWMFI : IndicatorBase
	{
		private readonly Array<double> _mfiDownVDown;

		private readonly Array<double> _mfiDownVUp;

		private readonly Array<double> _mfiupVDown;

		private readonly Array<double> _mfiupVUp;

		private readonly Array<double> _vals;

		public BWMFI()
		{
			this._vals = new Array<double>();
			this._mfiupVUp = new Array<double>();
			this._mfiDownVDown = new Array<double>();
			this._mfiupVDown = new Array<double>();
			this._mfiDownVUp = new Array<double>();
			base.indicator_buffers = 5;
			base.indicator_chart_window = false;
			base.SetIndexLabel(0, "BWMFI");
			base.indicator_color1 = Colors.Black;
			base.SetIndexLabel(1, "Up_Up");
			base.indicator_color2 = Colors.Lime;
			base.SetIndexLabel(2, "Down_Down");
			base.indicator_color3 = Colors.SaddleBrown;
			base.SetIndexLabel(3, "Up_Down");
			base.indicator_color4 = Colors.Blue;
			base.SetIndexLabel(4, "Down_Up");
			base.indicator_color5 = Colors.Pink;
			base.indicator_width1 = 0;
			base.indicator_width2 = 2;
			base.indicator_width3 = 2;
			base.indicator_width4 = 2;
			base.indicator_width5 = 2;
			base.IndicatorShortName("BWMFI");
		}

		protected override int Init()
		{
			base.indicator_color1 = Colors.Transparent;
			base.SetIndexBuffer(0, this._vals, false);
			base.SetIndexBuffer(1, this._mfiupVUp, false);
			base.SetIndexBuffer(2, this._mfiDownVDown, false);
			base.SetIndexBuffer(3, this._mfiupVDown, false);
			base.SetIndexBuffer(4, this._mfiDownVUp, false);
			base.SetIndexStyle(0, 12, -1, -1, null);
			base.SetIndexStyle(1, 2, -1, -1, null);
			base.SetIndexStyle(2, 2, -1, -1, null);
			base.SetIndexStyle(3, 2, -1, -1, null);
			base.SetIndexStyle(4, 2, -1, -1, null);
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
				while (i >= 0)
				{
					bool flag3 = history[i, true].Volume.Equals(0L);
					if (flag3)
					{
						bool flag4 = i == base.Bars - 1;
						if (flag4)
						{
							this._vals[i, true] = -1.0;
						}
						else
						{
							this._vals[i, true] = this._vals[i + 1, true];
						}
					}
					else
					{
						this._vals[i, true] = 100000.0 * (double)(history[i, true].High - history[i, true].Low) / (double)history[i, true].Volume;
					}
					i--;
				}
				i = base.Bars - base.IndicatorCounted();
				bool flag5 = i >= base.Bars;
				if (flag5)
				{
					i = base.Bars - 1;
				}
				bool flag6 = true;
				bool flag7 = true;
				bool flag8 = i < base.Bars - 1;
				if (flag8)
				{
					bool flag9 = this._mfiupVUp[i + 1, true].Equals(0.0);
					if (flag9)
					{
						flag6 = true;
						flag7 = true;
					}
					bool flag10 = this._mfiDownVDown[i + 1, true].Equals(0.0);
					if (flag10)
					{
						flag6 = false;
						flag7 = false;
					}
					bool flag11 = this._mfiupVDown[i + 1, true].Equals(0.0);
					if (flag11)
					{
						flag6 = true;
						flag7 = false;
					}
					bool flag12 = this._mfiDownVUp[i + 1, true].Equals(0.0);
					if (flag12)
					{
						flag6 = false;
						flag7 = true;
					}
				}
				for (i--; i >= 0; i--)
				{
					bool flag13 = this._vals[i, true] > this._vals[i + 1, true];
					if (flag13)
					{
						flag6 = true;
					}
					bool flag14 = this._vals[i, true] < this._vals[i + 1, true];
					if (flag14)
					{
						flag6 = false;
					}
					bool flag15 = base.Volume[i, true] > base.Volume[i + 1, true];
					if (flag15)
					{
						flag7 = true;
					}
					bool flag16 = base.Volume[i, true] < base.Volume[i + 1, true];
					if (flag16)
					{
						flag7 = false;
					}
					bool flag17 = flag6 & flag7;
					if (flag17)
					{
						this._mfiupVUp[i, true] = this._vals[i, true];
						this._mfiDownVDown[i, true] = 0.0;
						this._mfiupVDown[i, true] = 0.0;
						this._mfiDownVUp[i, true] = 0.0;
					}
					else
					{
						bool flag18 = !flag6 && !flag7;
						if (flag18)
						{
							this._mfiupVUp[i, true] = 0.0;
							this._mfiDownVDown[i, true] = this._vals[i, true];
							this._mfiupVDown[i, true] = 0.0;
							this._mfiDownVUp[i, true] = 0.0;
						}
						else
						{
							bool flag19 = flag6 && !flag7;
							if (flag19)
							{
								this._mfiupVUp[i, true] = 0.0;
								this._mfiDownVDown[i, true] = 0.0;
								this._mfiupVDown[i, true] = this._vals[i, true];
								this._mfiDownVUp[i, true] = 0.0;
							}
							else
							{
								bool flag20 = !flag6 & flag7;
								if (flag20)
								{
									this._mfiupVUp[i, true] = 0.0;
									this._mfiDownVDown[i, true] = 0.0;
									this._mfiupVDown[i, true] = 0.0;
									this._mfiDownVUp[i, true] = this._vals[i, true];
								}
							}
						}
					}
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
						result = !flag4;
					}
				}
			}
			return result;
		}
	}
}
