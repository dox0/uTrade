using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTrade.Data
{
    public class StockCwInfo
    {
		/// <summary>
		/// 
		/// </summary>
		public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 报告日期
        /// </summary>
        public DateTime? ReportDate { get; set; }
        /// <summary>
        /// 基本每股收益(元)
        /// </summary>
        public decimal? JBMGSY { get; set; }
        /// <summary>
        /// 每股净资产(元)
        /// </summary>
        public decimal? MGJZC { get; set; }
        /// <summary>
        /// 每股经营活动产生的现金流量净额(元)
        /// </summary>
        public decimal? MGJYHDCSXJLJE { get; set; }
        /// <summary>
        /// 主营业务收入(万元)
        /// </summary>
        public string ZYYWSR { get; set; }
        /// <summary>
        /// 主营业务利润(万元)
        /// </summary>
        public string ZYYWLR { get; set; }
        /// <summary>
        /// 营业利润(万元)
        /// </summary>
        public string YYLR { get; set; }
        /// <summary>
        /// 投资收益(万元)
        /// </summary>
        public string TZSY { get; set; }
        /// <summary>
        /// 营业外收支净额(万元)
        /// </summary>
        public string YYEYSZJE { get; set; }
        /// <summary>
        /// 利润总额(万元)
        /// </summary>
        public string LRZE { get; set; }
        /// <summary>
        /// 净利润(万元)
        /// </summary>
        public string JLR { get; set; }
        /// <summary>
        /// 净利润(扣除非经常性损益后)(万元)
        /// </summary>
        public string JLROUT { get; set; }
        /// <summary>
        /// 经营活动产生的现金流量净额(万元)
        /// </summary>
        public string JYHDCSDXJLJE { get; set; }
        /// <summary>
        /// 现金及现金等价物净增加额(万元)
        /// </summary>
        public string XJJXJDJWJCJE { get; set; }
        /// <summary>
        /// 总资产(万元)
        /// </summary>
        public string ZZC { get; set; }
        /// <summary>
        /// 流动资产(万元)
        /// </summary>
        public string LDZC { get; set; }
        /// <summary>
        /// 总负债(万元)
        /// </summary>
        public string ZFZ { get; set; }
        /// <summary>
        /// 流动负债(万元)
        /// </summary>
        public string LDFZ { get; set; }
        /// <summary>
        /// 股东权益不含少数股东权益(万元)
        /// </summary>
        public string GDQYBHSSGDQY { get; set; }
        /// <summary>
        /// 净资产收益率加权(%)
        /// </summary>
        public decimal? JZCSYLJQ { get; set; }


    }
}
