using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uTrade.Data
{

    public class RankModel
    {
        public int allRecords = 1000;
        public int pageIndex { get; set; }
        public int pageNum = 50;
        public int allPages = 50;

        public List<Rank> Datas = new List<Rank>();
    }
    public class Rank
    {
        /// <summary>
        /// 基金代码
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// 基金名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 基金简写
        /// </summary>
        public string Shorthand { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string CurDate { get; set; }
        /// <summary>
        /// 单位净值
        /// </summary>
        public decimal NetAssetValue { get; set; }
        /// <summary>
        /// 累计净值
        /// </summary>
        public decimal AccumulatedNet { get; set; }
        /// <summary>
        /// 日增长率
        /// </summary>
        public decimal DailyGrowth { get; set; }
        /// <summary>
        /// 近一周
        /// </summary>
        public decimal LastWeek { get; set; }
        /// <summary>
        /// 近一月
        /// </summary>
        public decimal LastMonth { get; set; }
        /// <summary>
        /// 近三月
        /// </summary>
        public decimal Last3Month { get; set; }
        /// <summary>
        /// 近六月
        /// </summary>
        public decimal Last6Month { get; set; }
        /// <summary>
        /// 近一年
        /// </summary>
        public decimal LastYear { get; set; }
        /// <summary>
        /// 近两年
        /// </summary>
        public decimal Last2Year { get; set; }
        /// <summary>
        /// 近三年
        /// </summary>
        public decimal Last3Year { get; set; }
        /// <summary>
        /// 今年以来
        /// </summary>
        public decimal ThisYear { get; set; }
        /// <summary>
        /// 成立以来
        /// </summary>
        public decimal SinceBuilt { get; set; }
        /// <summary>
        /// 成立时间
        /// </summary>
        public string BuiltDate { get; set; }
        /// <summary>
        /// 自定义
        /// </summary>
        public string Custom { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public string Poundage { get; set; }
        /// <summary>
        /// 折后手续费
        /// </summary>
        public string PoundageCount { get; set; }
        /// <summary>
        /// 关注
        /// </summary>
        public int Favorite { get; set; }
    }
}
