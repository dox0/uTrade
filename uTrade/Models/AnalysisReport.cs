using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace uTrade.Models
{
    class AnalysisReport
    {
        /// <summary>
        /// 净利润
        /// </summary>
        public double NetProfit { get; set; }

        /// <summary>
        /// 总盈利
        /// </summary>
        public double TotalProfit { get; set; }

        /// <summary>
        /// 总亏损
        /// </summary>
        public double TotalLoss { get; set; }

        /// <summary>
        /// 盈亏比
        /// </summary>
        public double WintoLossRatio
        {
            get
            {
                return TotalProfit / TotalLoss;
            }
        }

        /// <summary>
        /// 交易手数
        /// </summary>
        public double NumberofTrade { get; set; }

        /// <summary>
        /// 盈利比率（盈利手数/总手数）
        /// </summary>
        public double ProfitRatio
        {
            get
            {
                return NumberofWinHands / NumberofTrade;
            }
        }

        /// <summary>
        /// 盈利手数
        /// </summary>
        public double NumberofWinHands { get; set; }

        /// <summary>
        /// 亏损手数
        /// </summary>
        public double NumberofLossHands { get; set; }

        /// <summary>
        /// 持平手数
        /// </summary>
        public double NumberofEqualHands { get; set; }

        /// <summary>
        /// 平均利润（净利润 / 总手数）
        /// </summary>
        public double AverageProfit
        {
            get
            {
                return NetProfit / NumberofTrade;
            }
        }

        /// <summary>
        /// 平均盈利（总盈利/总手数）
        /// </summary>
        public double AverageWin
        {
            get
            {
                return TotalProfit / NumberofTrade;
            }
        }

        /// <summary>
        /// 平均亏损（总亏损/总手数）
        /// </summary>
        public double AverageLoss
        {
            get
            {
                return TotalLoss / NumberofTrade;
            }
        }

        /// <summary>
        /// 最大盈利
        /// </summary>
        public double MaxProfit { get; set; }

        /// <summary>
        /// 最大亏损
        /// </summary>
        public double MaxLoss { get; set; }

        /// <summary>
        /// 最大连续盈利次数
        /// </summary>
        public int MaxContinousProfit { get; set; }

        /// <summary>
        /// 最大连续亏损次数
        /// </summary>
        public int MaxContinousLoss { get; set; }

        /// <summary>
        /// 佣金合计
        /// </summary>
        public double Commission { get; set; }

        /// <summary>
        /// 收益率
        /// </summary>
        public double RateofReturn{ get; set; }

        /// <summary>
        /// 年化收益率
        /// </summary>
        public double AnnualRateofReturn { get; set; }

        /// <summary>
        /// 最大回撤
        /// </summary>
        public double MaxRetracement { get; set; }


    }
}
