using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uTrade.BackTest
{
    class TransactionCost
    {
        private double m_BuyCost;
        private double m_SellCost;
        private double m_MinCost;

        private void InitCost(DateTime dtTradeTime)
        {
            m_MinCost = 5;
            if(dtTradeTime > DateTime.Parse("2013-01-01"))
            {
                m_BuyCost = 0.0003;
                m_SellCost = 0.0013;
            }
            else if (dtTradeTime > DateTime.Parse("2011-01-01"))
            {
                m_BuyCost = 0.0003;
                m_SellCost = 0.002;
            }
            else if (dtTradeTime > DateTime.Parse("2009-01-01"))
            {
                m_BuyCost = 0.0003;
                m_SellCost = 0.003;
            }
            else
            {
                m_BuyCost = 0.0003;
                m_SellCost = 0.004;
            }
        }
        /// <summary>
        /// 根据交易日期计算费用
        /// </summary>
        /// <param name="dtTradeDate"></param>
        /// <param name="dTrade"></param>
        /// <returns></returns>
        public double GetTranscationCost(DateTime dtTradeDate, double dTrade)
        {
            InitCost(dtTradeDate);
            return (m_BuyCost + m_SellCost) * dTrade > m_MinCost ? (m_BuyCost + m_SellCost) * dTrade : m_MinCost;
        }
    }
}
