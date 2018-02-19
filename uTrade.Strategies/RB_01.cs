using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

using uTrade.Core;

namespace uTrade.Strategies
{
	class RB_01 : Strategy
	{
		[Parameter("压力线参数", "RB")]
		public int UpLine = 24;
		[Parameter("支持线参数", "RB")]
		public int DnLine = 24;

		[Parameter("手数", "交易参数")]
		public int Lots = 1;


		Highest ht;
		Lowest lt;

		public override void Initialize()
		{
			ht = Highest(High, UpLine);
			lt = Lowest(Low, DnLine);
		}

		/* 这是一个
		  多行注释的
		  例子	 */
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
					Sell(Lots, Min(Open[0], DnValue));
			}
		}
	}
}
