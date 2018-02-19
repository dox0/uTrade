using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using uTrade.Data;
using uTrade.Controls;
using uTrade.Common;

namespace uTrade.Strategies
{
    class SmallTurtle
    {
        private static int FastPeriod = 12;
        private static int SlowPeriod = 26;
        private static int SignalPeriod = 9;

        List<Rank> lstRankAll = new List<Rank>();
        List<Rank> lstRankSelected = new List<Rank>();
        static PriceInfo fdpInfo;


        public List<Rank> Process()
        {
            lstRankAll = GetFundEquityInfo.Instance.GetAllRankList();
            foreach (Rank oRank in lstRankAll)
            {
                fdpInfo = GetFundEquityInfo.Instance.GetFormatedFundInfo(oRank.Symbol, DateService.ThisTimeLastYear(), DateTime.Now);

                double[] emaFast = MathUtil.CalcEMA(fdpInfo.getPrice(PriceConstants.PRICE_CLOSE), FastPeriod);
                double[] emaSlow = MathUtil.CalcEMA(fdpInfo.getPrice(PriceConstants.PRICE_CLOSE), SlowPeriod);
                double[] emaDiff = MathUtil.CalcDiff(emaFast, emaSlow);
                double[] dea = MathUtil.CalcEMA(emaDiff, SignalPeriod);
                double[] macdDiff = MathUtil.CalcDiff(emaDiff, dea);

                if(macdDiff[macdDiff.Length-1] > 0)
                {
                    lstRankSelected.Add(oRank);
                }

            }
            return lstRankSelected;

        }

    }
}
