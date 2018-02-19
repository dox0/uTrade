using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTrade.Data
{
    public class StockInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string stockcode { get; set; }
        /// <summary>
        /// 类型（0：深圳，1：上海）
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 一手股票
        /// </summary>
        public string OneHand { get; set; }
        /// <summary>
        /// 股票名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 价格小数位数
        /// </summary>
        public string PointIndex { get; set; }
        /// <summary>
        /// 昨收
        /// </summary>
        public decimal? YestClose { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Unknow1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Unknow2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Unknow3 { get; set; }

        /// <summary>
        /// 关注
        /// </summary>
        public int Favorite { get; set; }

    }
}
