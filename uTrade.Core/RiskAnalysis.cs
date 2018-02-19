using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uTrade.Common;

namespace uTrade.Core
{
    class RiskAnalysis
    {
        const int TradeDaysPerYear = 250;      //一年的交易天数
        const double RiskFreeInterestRate = 0.04;   //无风险利率（默认0.04）

        private double m_PropertyStart;        //策略起始价值
        private double m_PropertyEnd;          //策略结束价值
        private double[] m_DailyProfit;        //策略每日收益
        private double[] m_DailyProperty;      //策略的每日价值

        private double m_PropertyBaseStart;    //基准起始价值
        private double m_PropertyBaseEnd;      //基准结束价值
        private double[] m_DailyProfitBase;    //基准每日收益

        private double m_TradeDays;            //交易的天数

        //每日收益率 Rt=ln(Pt/Pt-1）pt是当期的收盘价，pt-1是上一期的收盘价。




        /// <summary>
        /// 策略收益
        /// </summary>
        public double TotalReturns
        {
            get
            {
                return (m_PropertyEnd - m_PropertyStart) / m_PropertyStart;
            }
        }

        /// <summary>
        /// 策略年化收益
        /// </summary>
        public double TotalAnnualizedReturns
        {
            get
            {
                return Math.Pow((TotalReturns + 1), TradeDaysPerYear / m_TradeDays) - 1;
            }
        }

        /// <summary>
        /// 基准收益
        /// </summary>
        public double BenchmarkReturns
        {
            get
            {
                return (m_PropertyBaseEnd - m_PropertyBaseStart) / m_PropertyBaseStart;
            }
        }

        /// <summary>
        /// 基准年化收益
        /// </summary>
        public double BenchmarkAnnualizedReturns
        {
            get
            {
                return Math.Pow((BenchmarkReturns + 1), TradeDaysPerYear / m_TradeDays) - 1;
            }
        }

        /// <summary>
        /// 阿尔法，Alpha是投资者获得与市场波动无关的回报
        /// </summary>
        public double Alpha
        {
            get
            {
                return TotalAnnualizedReturns - (RiskFreeInterestRate + Beta * (BenchmarkAnnualizedReturns - TotalAnnualizedReturns));
            }
        }

        /// <summary>
        /// 贝塔，表示投资的系统性风险，反映了策略对大盘变化的敏感性
        /// </summary>
        public double Beta
        {
            get
            {
                return MathUtil.Covar(m_DailyProfit, m_DailyProfitBase) / MathUtil.Variance(m_DailyProfitBase);
            }
        }

        /// <summary>
        /// 夏普比率，表示每承受一单位总风险，会产生多少的超额报酬
        /// </summary>
        public double Sharpe { set; get; }

        /// <summary>
        /// 索提诺比率，表示每承担一单位的下行风险，将会获得多少超额回报
        /// </summary>
        public double Sortino { set; get; }

        /// <summary>
        /// 信息比率，衡量单位超额风险带来的超额收益
        /// </summary>
        public double InformationRatio
        {
            get
            {
                double[] diff = MathUtil.CalcDiff(m_DailyProfit, m_DailyProfitBase);
                return (TotalAnnualizedReturns - BenchmarkAnnualizedReturns) / MathUtil.Variance(diff);
            }
        }

        /// <summary>
        /// 用来测量策略的风险性，波动越大代表策略风险越高
        /// </summary>
        public double AlgorithmVolatility { set; get; }

        /// <summary>
        /// 基准波动率
        /// </summary>
        public double BenchmarkVolatility { set; get; }

        /// <summary>
        /// 最大回撤
        /// </summary>
        public double MaxDrawdown { set; get; }

        /// <summary>
        /// 下行波动率
        /// </summary>
        public double DownsideRisk { set; get; }

        /// <summary>
        /// 胜率，盈利次数在总交易次数中的占比
        /// </summary>
        public double WinningProbability { set; get; }

        /// <summary>
        /// 日胜率，盈利超过基准的日数在总交易数中的占比
        /// </summary>
        public double WinningRate { set; get; }

        /// <summary>
        /// 盈亏比，周期盈利亏损的比例
        /// </summary>
        public double ProfitLossRatio { set; get; }
    }
}
