using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uTrade.Data
{
    public class FavoriateInfo
    {
        static FavoriateInfo inst = null;
        public static FavoriateInfo Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new FavoriateInfo();
                }
                return inst;
            }
        }

        //public List<string> GetTradeList(TradeType tradeType)
        //{
        //    string strWhere = "Type = " + ((int)tradeType).ToString();

        //    DataSet dtSetFavor = FavoriateInfoManager.Instance.GetList(strWhere);

        //    int cnt = dtSetFavor.Tables[0].
        //    for (int i=0;i<)
        //    foreach (DataRow dtRow in dtSetFavor.Tables[0])
        //    {

        //    }


        //}
    }
}
