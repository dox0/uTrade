using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Detrended Price Oscillator")]
	[Serializable]
	public class DPO : IndicatorBase
	{
		private readonly Array<double> dpoBuffer;

		[Category("Settings"), DisplayName("X Period")]
		public int x_prd
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Number of Bars")]
		public int CountBars
		{
			get;
			set;
		}

		public DPO()
		{
			this.x_prd = 14;
			this.CountBars = 300;
			base.indicator_buffers = 1;
			base.indicator_chart_window = false;
			base.indicator_color1 = Colors.Red;
			this.dpoBuffer = new Array<double>();
		}

		protected override int Init()
		{
			base.IndicatorShortName(string.Format("DPO({0})", this.x_prd));
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexBuffer(0, this.dpoBuffer, false);
			base.SetIndexLabel(0, string.Format("DPO({0})", this.x_prd));
			bool flag = this.CountBars >= base.Bars;
			if (flag)
			{
				this.CountBars = base.Bars;
			}
			int num = this.x_prd + this.x_prd / 2;
			base.SetIndexDrawBegin(0, base.Bars - this.CountBars + num);
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			bool flag = base.Bars <= this.x_prd;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = num < this.x_prd;
				int i;
				if (flag2)
				{
					for (i = 1; i <= this.x_prd; i++)
					{
						this.dpoBuffer[this.CountBars - i, true] = 0.0;
					}
				}
				i = this.CountBars - this.x_prd - 1;
				int maShift = this.x_prd / 2 + 1;
				while (i >= 0)
				{
					this.dpoBuffer[i, true] = base.Close[i, true] - base.iMA(null, 0, this.x_prd, maShift, 0, 0, i);
					i--;
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
							bool flag5 = !(values[2] is int) || !this.x_prd.Equals((int)values[2]);
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !(values[3] is int) || !this.CountBars.Equals((int)values[3]);
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
