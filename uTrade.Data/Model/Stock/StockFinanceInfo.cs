using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTrade.Data
{
    class StockFinanceInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 市场
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 证券代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 流通股本
        /// </summary>
        public string GBLT { get; set; }
        /// <summary>
        /// 所属省份
        /// </summary>
        public string SSSF { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public string SSHY { get; set; }
        /// <summary>
        /// 财务更新日期
        /// </summary>
        public DateTime? CWUpdateTime { get; set; }
        /// <summary>
        /// 上市日期
        /// </summary>
        public DateTime? ListingDate { get; set; }
        /// <summary>
        /// 总股本
        /// </summary>
        public string AllGB { get; set; }
        /// <summary>
        /// 国家股
        /// </summary>
        public string GJG { get; set; }
        /// <summary>
        /// 发起人法人股
        /// </summary>
        public string FQRFRG { get; set; }
        /// <summary>
        /// 法人股
        /// </summary>
        public string FRG { get; set; }
        /// <summary>
        /// B股
        /// </summary>
        public string BG { get; set; }
        /// <summary>
        /// H股
        /// </summary>
        public string HG { get; set; }
        /// <summary>
        /// 职工股
        /// </summary>
        public string ZhGG { get; set; }
        /// <summary>
        /// 总资产
        /// </summary>
        public string AllZC { get; set; }
        /// <summary>
        /// 流动资产
        /// </summary>
        public string LDZC { get; set; }
        /// <summary>
        /// 固定资产
        /// </summary>
        public string GDZC { get; set; }
        /// <summary>
        /// 无形资产
        /// </summary>
        public string WXZC { get; set; }
        /// <summary>
        /// 股东人数
        /// </summary>
        public string GDRS { get; set; }
        /// <summary>
        /// 流动负债
        /// </summary>
        public string LDFZ { get; set; }
        /// <summary>
        /// 长期负债
        /// </summary>
        public string CQFZ { get; set; }
        /// <summary>
        /// 资本公积金
        /// </summary>
        public string ZBGJJ { get; set; }
        /// <summary>
        /// 净资产
        /// </summary>
        public string JZC { get; set; }
        /// <summary>
        /// 主营收入
        /// </summary>
        public string ZYSR { get; set; }
        /// <summary>
        /// 主营利润
        /// </summary>
        public string ZYLR { get; set; }
        /// <summary>
        /// 应收帐款
        /// </summary>
        public string YSZK { get; set; }
        /// <summary>
        /// 营业利润
        /// </summary>
        public string YYLR { get; set; }
        /// <summary>
        /// 投资收益
        /// </summary>
        public string TZSY { get; set; }
        /// <summary>
        /// 经营现金流
        /// </summary>
        public string JYXJL { get; set; }
        /// <summary>
        /// 总现金流
        /// </summary>
        public string ZXJL { get; set; }
        /// <summary>
        /// 存货
        /// </summary>
        public string CH { get; set; }
        /// <summary>
        /// 利润总额
        /// </summary>
        public string LRZE { get; set; }
        /// <summary>
        /// 税后利润
        /// </summary>
        public string SHLR { get; set; }
        /// <summary>
        /// 净利润
        /// </summary>
        public string JLR { get; set; }
        /// <summary>
        /// 未分利润
        /// </summary>
        public string WFLR { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public string Unknow1 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public string unknow2 { get; set; }
    }
}
