using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace uTrade.Data
{
    public partial class StockMinInfoService
    {
        private static List<int> ALLlistCon = new List<int>();
        private static List<int> OverlistCon = new List<int>();
        private Server m_Server;
        StringBuilder Result = new StringBuilder(1024 * 1024);
        StringBuilder ErrInfo = new StringBuilder(256);
        StockMinInfoManager _StockMinInfo = new StockMinInfoManager();
        StockInfoManager _oStockInfo = new StockInfoManager();


        public StockMinInfoService()
        { }

        public StockMinInfoService(Server oServer)
        {
            m_Server = oServer;
        }

        public void DataAccess()
        {
            int ConnectionID = TdxApi.TdxHq_Multi_Connect(m_Server.IP, m_Server.Port, Result, ErrInfo);
            OverlistCon.Add(ConnectionID);

            _StockMinInfo.Clear();
            Thread tdStockMinInfo = new Thread(SyncStocMinInfo);
            tdStockMinInfo.Start();

        }


        void SyncStocMinInfo()
        {
            byte[] Market = { 0, 1 };
            string[] Zqdm = { "000001", "600030" };
            short ZqdmCount = 2;
            int ConnectionID = TdxApi.TdxHq_Multi_Connect(m_Server.IP, m_Server.Port, Result, ErrInfo);
            ALLlistCon.Add(ConnectionID);
            OverlistCon.Add(ConnectionID);

            List<StockInfo> stockList = new List<StockInfo>();
            stockList = _oStockInfo.GetStockCodeList("");

            Dictionary<string, string> Message = new Dictionary<string, string>();
            Message.Add("Result", "");
            Message.Add("ErrInfo", "");
            bool bool1;

            bool1 = TdxApi.TdxHq_Multi_GetSecurityQuotes(ConnectionID, Market, Zqdm,ref ZqdmCount, Result, ErrInfo);

            #region foreach
            foreach (StockInfo s in stockList)
            {
                //try
                //{
 
                bool1 = TdxApi.TdxHq_Multi_GetMinuteTimeData(ConnectionID, Convert.ToByte(s.Type), s.stockcode, Result, ErrInfo);
                //bool1 = TdxApi.TdxHq_Multi_GetHistoryMinuteTimeData(ConnectionID, Convert.ToByte(s.Type), s.stockcode, 20180111, Result, ErrInfo);
                //bool1 = TdxApi.TdxHq_Multi_GetTransactionData(ConnectionID, Convert.ToByte(s.Type), s.stockcode, 1, ref sssss, Result, ErrInfo);
                ///出错
                if (!bool1 || Result.ToString() == "")
                {
                    //记录日志
                    continue;
                }
                string[] strRow = Result.ToString().Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);   //分解行的字符串
                string[] strColX = strRow[1].Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //Console.WriteLine(Result.ToString());
                for (int i = 1; i < strRow.Length; i++)
                {
                    string[] strCol = strRow[i].Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (!(strCol[5].Replace("--", "-") != "0" && strCol[6].Replace("--", "-") != "0"))
                    {
                        continue;
                    }
                    int IsHave = _StockMinInfo.GetRecordCount("Symbol='" + s.stockcode + "' and CWUpdateTime=CONVERT(datetime,'" + strCol[5].Replace("--", "-") + "',102)");
                    if (IsHave > 0)
                    {
                        continue;
                    }
                    StockMinInfo stock = new StockMinInfo();
                    stock.Type = strCol[0];
          
                    int ID = _StockMinInfo.Add(stock);
                    if (ID > 0)
                    {
                        string message = "Current tiem is: " + s.stockcode + " type:" + s.Type;
                        //ReportProgress 方法把信息传递给 ProcessChanged 事件处理函数。
                        //第一个参数类型为 int，表示执行进度。
                        //如果有更多的信息需要传递，可以使用 ReportProgress 的第二个参数。
                        //这里我们给第二个参数传进去一条消息。
                        Message["Result"] = Result.ToString();
                        Message["ErrInfo"] = ErrInfo.ToString();
                        Message["Message"] = message.ToString();
                        //bk_FinanceInfo.ReportProgress(i, Message);
                    }
                    else
                    {
                        //记录日志
                        Message["Result"] = Result.ToString();
                        Message["ErrInfo"] = ErrInfo.ToString();
                        Message["Message"] = "";
                        //bk_FinanceInfo.ReportProgress(i, Message);
                        continue;
                    }
                }
                //}
                //catch
                //{
                //    //记录日志

                //    continue;
                //}
                //finally
                //{
                //}
            }
            #endregion

            ALLlistCon.Remove(ConnectionID);
            OverlistCon.Remove(ConnectionID);
            TdxApi.TdxHq_Multi_Disconnect(ConnectionID);
            //IsFinanceWork = false;
        }
    }
}
