using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTrade.Data
{
    class Stock5MinInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StockCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Time { get; set; }
        /// <summary>
        /// 开盘价
        /// </summary>
        public decimal? open { get; set; }
        /// <summary>
        /// 收盘价
        /// </summary>
        public decimal? Close { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        public decimal? High { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal? Low { get; set; }
        /// <summary>
        /// 成交量
        /// </summary>
        public string Volume { get; set; }
        /// <summary>
        /// 成交额
        /// </summary>
        public string Turnover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UpNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DownNum { get; set; }

    }
}
