using System;
using System.Collections.Generic;
using System.Text;

namespace uTrade.Core
{
    public class StrategyConfig
    {
		public string Name { get; set; }
		public string TypeFullName { get; set; }
		public string Interval { get; set; }
		public DateTime BeginDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Params { get; set; }
	}
}
