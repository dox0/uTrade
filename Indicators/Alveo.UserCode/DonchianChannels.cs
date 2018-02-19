using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Donchian Channels")]
	[Serializable]
	public class DonchianChannels : IndicatorBase
	{
		private readonly Array<double> upper;

		private readonly Array<double> middle;

		private readonly Array<double> lower;

		[Category("Settings"), Description("Bars to count"), DisplayName("Bars to count")]
		public int BarsToCount
		{
			get;
			set;
		}

		public DonchianChannels()
		{
			base.indicator_buffers = 3;
			base.indicator_chart_window = true;
			base.indicator_color1 = Colors.Red;
			base.indicator_color2 = Colors.Blue;
			base.indicator_color3 = Colors.Green;
			base.indicator_width1 = 1;
			base.indicator_width2 = 1;
			base.indicator_width3 = 1;
			this.BarsToCount = 20;
			this.upper = new Array<double>();
			this.middle = new Array<double>();
			this.lower = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexBuffer(0, this.upper, true);
			base.SetIndexStyle(0, 0, -1, -1, null);
			base.SetIndexLabel(0, "Upper");
			base.SetIndexBuffer(1, this.middle, false);
			base.SetIndexStyle(1, 0, -1, -1, null);
			base.SetIndexLabel(1, "Middle");
			base.SetIndexBuffer(2, this.lower, false);
			base.SetIndexStyle(2, 0, -1, -1, null);
			base.SetIndexLabel(2, "Lower");
			base.IndicatorShortName(string.Format("DCH({0})", this.BarsToCount));
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			int num2 = base.Bars - num;
			bool flag = num > 0;
			if (flag)
			{
				num2++;
			}
			for (int i = 0; i < num2; i++)
			{
				this.upper[i, true] = base.iHigh(base.Symbol(), base.Period(), base.iHighest(base.Symbol, base.TimeFrame, 2, this.BarsToCount, i));
				this.lower[i, true] = base.iLow(base.Symbol(), base.Period(), base.iLowest(base.Symbol, base.TimeFrame, 1, this.BarsToCount, i));
				this.middle[i, true] = (this.upper[i, true] + this.lower[i, true]) / 2.0;
			}
			return 0;
		}
	}
}
