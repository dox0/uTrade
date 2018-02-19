using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uTrade.Data
{

    public class FavoriateModel
    {
        /// <summary>
        /// 股票代码
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// 类型 1：股票 2：基金
        /// </summary>
        public TradeType Type
        { get; set; }

        /// <summary>
        /// 增加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 保留
        /// </summary>
        public string Reserve { get; set; }



    }
}
