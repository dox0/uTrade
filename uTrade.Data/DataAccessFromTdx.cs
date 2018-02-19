using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading.Tasks;

namespace uTrade.Data
{
    public class DataAccessFromTdx
    {
        StringBuilder ErrInfo = new StringBuilder(256);

        TdxServer tdxServer = new TdxServer();

        public DataAccessFromTdx()
        {
            bool bool1 = TdxApi.OpenTdx(ErrInfo);

        }

        ~DataAccessFromTdx()
        {

        }

        public void DataAccessBackGround()
        {
            //StockInfoService _oStockService = new StockInfoService(tdxServer.Available);
            //_oStockService.DataAccess();

            //StockFinanceInfoService _oStockFianceService = new StockFinanceInfoService(tdxServer.Available);
            //_oStockFianceService.DataAccess();

            //Stock5MinInfoService _oStock5MinService = new Stock5MinInfoService(tdxServer.Available);
            //_oStock5MinService.DataAccess();

            //StockZCFZInfoService _oStockZCFZService = new StockZCFZInfoService(tdxServer.Available);
            //_oStockZCFZService.DataAccess();

            StockMinInfoService _oStockMinSerice = new StockMinInfoService(tdxServer.Available);
            _oStockMinSerice.DataAccess();
        }
    }
}
