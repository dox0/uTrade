using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using uTrade.Core;

namespace uTrade.Strategies
{
	class MACD_Cross : Strategy
	{
		private DataSeries diff;

		public override void Initialize()
		{
			diff = MACD(this.Close, 12, 26, 9).Diff;
		}

		public override void OnBarUpdate()
		{
			if (diff[1] > 0 && diff[2] <= 0) //cross up
			{
				Buy(1, Open[0]);
			}
			else if (diff[1] < 0 && diff[2] >= 0) //cross dn
			{
				if (this.Position > 0)
					Sell(Position, Open[0]);
			}
		}
	}
}
