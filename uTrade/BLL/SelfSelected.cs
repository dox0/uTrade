using System;
using System.Data;
using uTrade.DAL;
using uTrade.Data;
using uTrade.Models;


namespace uTrade.BLL
{
    class SelfSelected
    {
        public SelfSelected()
        {

        }
        DataSet GetAllSelfSelected()
        {
            return SelfSelectedDAL.Instance.GetList("Symbol is not null");
        }

        int AddSelfSelected(SelfSelectedModel model)
        {
            return SelfSelectedDAL.Instance.Add(model);
        }

        int UpdateSelfSelected(SelfSelectedModel model)
        {
            return SelfSelectedDAL.Instance.Update(model);
        }


        //更新当前的盈利信息
        void UpdateProfit()
        {
            DataTable dsSelecteTbl = new DataTable();
            dsSelecteTbl = GetAllSelfSelected().Tables[0];

            foreach(DataRow drSelect in dsSelecteTbl.Rows)
            {
                SelfSelectedModel oModelSelf = SelfSelectedDAL.Instance.DataRowToModel(drSelect);

                EquityModel buymodel = GetFundEquityInfo.Instance.GetFormatedFundInfo(oModelSelf.Symbol, oModelSelf.BuyDate);
                oModelSelf.BuyPrice = float.Parse(buymodel.unitwork);
                //买入份额
                double dBuyCount = oModelSelf.BuyQuant / oModelSelf.BuyPrice;
                if (oModelSelf.SaleDate == null)
                {
                    oModelSelf.SaleDate = DateTime.Today;
                }

                EquityModel salemodel = GetFundEquityInfo.Instance.GetFormatedFundInfo(oModelSelf.Symbol, oModelSelf.SaleDate);

                oModelSelf.SalePrice = float.Parse(salemodel.unitwork);
                oModelSelf.CurQuant = dBuyCount * oModelSelf.SalePrice;
                oModelSelf.CurProfit = oModelSelf.CurQuant - oModelSelf.BuyQuant;
                SelfSelectedDAL.Instance.Update(oModelSelf);
            }
        }
    }
}
