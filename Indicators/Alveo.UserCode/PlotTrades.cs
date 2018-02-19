using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Plot historical trades that match the symbol for the current chart")]
	[Serializable]
	public class PlotTrades : ScriptBase
	{
		[Description("This is the first order number in a range of trades you want to plot"), DisplayName("Order ID Begin")]
		public int oidB
		{
			get;
			set;
		}

		[Description("This is the last order number in a range of trades you want to plot"), DisplayName("Order ID End")]
		public int oidE
		{
			get;
			set;
		}

		[Description("Select this to plot all trades in your history"), DisplayName("Plot all trades?")]
		public bool allHistory
		{
			get;
			set;
		}

		[DisplayName("Trend Line Width")]
		public int tLineWidth
		{
			get;
			set;
		}

		[Description("This is the width of the trade's description label"), DisplayName("Label Width")]
		public int tLabelWidth
		{
			get;
			set;
		}

		public PlotTrades()
		{
			this.oidB = 0;
			this.oidE = 0;
			this.tLineWidth = 4;
			this.tLabelWidth = 12;
			base.copyright = "Apiary Investment Fund, LLC";
			base.link = "";
		}

		protected override int Start()
		{
			bool allHistory = this.allHistory;
			if (allHistory)
			{
				this.oidB = 0;
				this.oidE = base.OrdersHistoryTotal();
			}
			else
			{
				bool flag = this.oidE == 0;
				if (flag)
				{
					this.oidE = this.oidB;
				}
			}
			for (int i = this.oidB; i <= this.oidE; i++)
			{
				this.plotTrade(i);
			}
			this.oidB = 0;
			this.oidE = 0;
			this.allHistory = false;
			return 0;
		}

		private void plotTrade(int x)
		{
			base.OrderSelect(x, 1, 0);
			bool flag = base.OrderSymbol() == base.Symbol();
			if (flag)
			{
				base.Print(new object[]
				{
					"Trade Plotter: Trade " + x + " was drawn on the chart"
				});
				base.Chart.DrawTrendLine("Trade " + x + " trend line", 0, base.OrderOpenTime(), base.OrderOpenPrice(), base.OrderCloseTime(), base.OrderClosePrice());
				bool flag2 = base.OrderProfit() > 0.0;
				Color color;
				Color color2;
				if (flag2)
				{
					color = Colors.Green;
					color2 = Colors.LightGreen;
				}
				else
				{
					color = Colors.Orange;
					color2 = Colors.Cornsilk;
				}
				base.Chart.SetObjectColor("Trade " + x + " trend line", color);
				base.Chart.SetObjectWidth("Trade " + x + " trend line", this.tLineWidth);
				bool flag3 = base.OrderType() == 0;
				if (flag3)
				{
					base.Chart.DrawText("Trade " + x + " open", 0, base.OrderOpenTime(), base.OrderOpenPrice(), "BUY; OID " + x);
				}
				bool flag4 = base.OrderType() == 1;
				if (flag4)
				{
					base.Chart.DrawText("Trade " + x + " open", 0, base.OrderOpenTime(), base.OrderOpenPrice(), "SELL; OID " + x);
				}
				base.Chart.SetObjectColor("Trade " + x + " open", color2);
				base.Chart.SetObjectWidth("Trade " + x + " open", this.tLabelWidth);
			}
		}
	}
}
