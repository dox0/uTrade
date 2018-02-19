using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

using uTrade.Core;

namespace uTrade.Strategies
{
	class RB_02 : Strategy
	{
		[Parameter("压力线参数", "RB")]
		public int UpLine = 24;
		[Parameter("支持线参数", "RB")]
		public int DnLine = 24;

		[Parameter("手数", "交易参数")]
		public int Lots = 5;
		[Parameter("开始止赢(%)", "交易参数")]
		public double StopProfitStart = 5;
		[Parameter("回落止赢(%)", "交易参数")]
		public double StopProfit = 20;

		[Parameter("止损-多(%)", "交易参数")]
		public double StopLossLong = 3;
		[Parameter("止损-空(%)", "交易参数")]
		public double StopLossShort = 3;

		Highest ht;
		Lowest lt;

		public override void Initialize()
		{
			ht = Highest(High, UpLine);
			lt = Lowest(Low, DnLine);
		}


		public override void OnBarUpdate()
		{
			if (CurrentBar < Max(UpLine, DnLine)) return;
			
			var UpValue = ht[1];
			var DnValue = lt[1];

			var UpBreak = High[0].GreaterEqual(UpValue);
			var DnBreak = Low[0].LessEqual(DnValue);

			if (Position == 0 && UpBreak)
			{
				Buy(Lots, Max(Open[0], UpValue));
			}
			else if (DnBreak)
			{
				if (Position > 0)
					if (BarsSinceEntryLong == 0) //不在开空的Bar上操作(tick测试时重复开平
						return;
					else
						Sell(Lots, Min(Open[0], DnValue));
			}

			double stopLine = 0;
			if (Position > 0 && BarsSinceEntryLong > 0)//BarsSinceEntryLong==0时取不到最高价
			{
				//止盈
				var h1 = High.Highest(1, BarsSinceEntryLong); //替代for之Highest
				var remark = string.Empty;
				if (h1 >= EntryPrice * (1 + StopProfitStart / 100))
				{
					stopLine = EntryPrice + (h1 - EntryPrice) * (1 - StopProfit / 100);
					remark = "回落止赢";
				}
				if (Low[0] <= EntryPrice * (1 - StopLossLong / 100))
				{
					stopLine = EntryPrice * (1 - StopLossLong / 100);
					remark = "止损";
				}
				if (stopLine != 0 && Low[0].LessEqual(stopLine))
					Sell(0, Min(Open[0], stopLine), remark);
			}
		}
	}
}
