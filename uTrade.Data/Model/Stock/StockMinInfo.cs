using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTrade.Data
{
    public class StockMinInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public int? Code { get; set; }
        /// <summary>
        /// 股票代码
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// 股票名称（Unicode转码）
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 开盘价
        /// </summary>
        public decimal? Open { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        public decimal? High { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal? Low { get; set; }
        /// <summary>
        /// 股票状态0有效1无效
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 当前价格
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// 昨日关盘价
        /// </summary>
        public decimal? Yestclose { get; set; }
        /// <summary>
        /// 涨跌幅百分比
        /// </summary>
        public decimal? Percent { get; set; }
        /// <summary>
        /// 涨跌幅度
        /// </summary>
        public decimal? Updown { get; set; }
        /// <summary>
        /// 箭头（Unicode转码）↓
        /// </summary>
        public string Arrow { get; set; }
        /// <summary>
        /// 成交的股票数
        /// </summary>
        public string Volume { get; set; }
        /// <summary>
        /// 成交金额
        /// </summary>
        public decimal? Turnover { get; set; }
        /// <summary>
        /// 卖1
        /// </summary>
        public decimal? Ask1 { get; set; }
        /// <summary>
        /// 卖2
        /// </summary>
        public decimal? Ask2 { get; set; }
        /// <summary>
        /// 卖3
        /// </summary>
        public decimal? Ask3 { get; set; }
        /// <summary>
        /// 卖4
        /// </summary>
        public decimal? Ask4 { get; set; }
        /// <summary>
        /// 卖5
        /// </summary>
        public decimal? Ask5 { get; set; }
        /// <summary>
        /// 卖1量
        /// </summary>
        public string Askvol1 { get; set; }
        /// <summary>
        /// 卖2量
        /// </summary>
        public string Askvol2 { get; set; }
        /// <summary>
        /// 卖3量
        /// </summary>
        public string Askvol3 { get; set; }
        /// <summary>
        /// 卖4量
        /// </summary>
        public string Askvol4 { get; set; }
        /// <summary>
        /// 卖5量
        /// </summary>
        public string Askvol5 { get; set; }
        /// <summary>
        /// 买1价
        /// </summary>
        public decimal? Bid1 { get; set; }
        /// <summary>
        /// 买2价
        /// </summary>
        public decimal? Bid2 { get; set; }
        /// <summary>
        /// 买3价
        /// </summary>
        public decimal? Bid3 { get; set; }
        /// <summary>
        /// 买4价
        /// </summary>
        public decimal? Bid4 { get; set; }
        /// <summary>
        /// 买5价
        /// </summary>
        public decimal? Bid5 { get; set; }
        /// <summary>
        /// 买1量
        /// </summary>
        public string Bidvol1 { get; set; }
        /// <summary>
        /// 买2量
        /// </summary>
        public string Bidvol2 { get; set; }
        /// <summary>
        /// 买3量
        /// </summary>
        public string Bidvol3 { get; set; }
        /// <summary>
        /// 买4量
        /// </summary>
        public string Bidvol4 { get; set; }
        /// <summary>
        /// 买5量
        /// </summary>
        public string Bidvol5 { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string Update { get; set; }
        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime? Time { get; set; }

    }
}
