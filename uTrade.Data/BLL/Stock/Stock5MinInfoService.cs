using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using uTrade.Common;


namespace uTrade.Data
{
    public class Stock5MinInfoService
    {
        private static List<int> ALLlistCon = new List<int>();
        private static List<int> OverlistCon = new List<int>();
        private Server m_Server;
        static StringBuilder Result = new StringBuilder(1024 * 1024);
        static StringBuilder ErrInfo = new StringBuilder(256);
        StockInfoManager _oStockInfo = new StockInfoManager();
        Stock5MinInfoManager _oStock5MinInfo = new Stock5MinInfoManager();

        public void DataAccess()
        { 
            _oStock5MinInfo.Clear();
            Thread tdStock5MinInfo = new Thread(SyncStoc5MinkInfo);
            tdStock5MinInfo.Start();
        }

        public Stock5MinInfoService()
        { }

        public Stock5MinInfoService(Server oServer)
        {
            m_Server = oServer;
        }


        void SyncStoc5MinkInfo()
        {
            bool bool1 = TdxApi.OpenTdx(ErrInfo);
            int ConnectionID = TdxApi.TdxHq_Multi_Connect(m_Server.IP, m_Server.Port, Result, ErrInfo);
            ALLlistCon.Add(ConnectionID);
            OverlistCon.Add(ConnectionID);
            //设置 这个bk 在工作

            List<StockInfo> stockList = _oStockInfo.GetStockCodeList("TYPE=0");
            Dictionary<string, string> Message = new Dictionary<string, string>();
            Message.Add("Result", "");
            Message.Add("ErrInfo", "");
            foreach (StockInfo s in stockList)
            {
                //try
                //{
                short Count = 10;
                bool1 = TdxApi.TdxHq_Multi_GetSecurityBars(ConnectionID, 0, 0, s.stockcode, 0, ref Count, Result, ErrInfo);
                if (Count != 0)
                {
                    string[] strRow = Result.ToString().Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);   //分解行的字符串
                                                                                                                            //string[] strColX = strRow[1].Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                                                                                                            //时间	开盘价	收盘价	最高价	最低价	成交量	成交额	涨家数	跌家数
                    for (int i = 1; i < strRow.Length; i++)
                    {
                        string[] strCol = strRow[i].Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        Stock5MinInfo stock = new Stock5MinInfo();
                        if (!PublicTool.CanDateTime(strCol[0].Replace("--", "-")))
                        {
                            continue;
                        }
                        int IsHave = _oStock5MinInfo.GetRecordCount("Symbol='" + s.stockcode + "' and Time=CONVERT(datetime,'" + strCol[0].Replace("--", "-") + "',102)");
                        if (IsHave > 0)
                        {
                            continue;
                        }

                        stock.StockCode = s.stockcode;
                        stock.Time = Convert.ToDateTime(strCol[0].Replace("--", "-"));
                        stock.open = decimal.Parse(PublicTool.IsNumElseToZero(strCol[1]));
                        stock.Close = decimal.Parse(PublicTool.IsNumElseToZero(strCol[2]));
                        stock.High = decimal.Parse(PublicTool.IsNumElseToZero(strCol[3]));
                        stock.Low = decimal.Parse(PublicTool.IsNumElseToZero(strCol[4]));
                        stock.Volume = strCol[5];
                        stock.Turnover = strCol[6];
                        //stock.UpNum = strCol[7];
                        //stock.DownNum = strCol[8];
                        int ID = _oStock5MinInfo.Add(stock);
                        if (ID > 0)
                        {
                            string message = "Current tiem is: " + Convert.ToDateTime(strCol[0].Replace("--", "-"));
                            //ReportProgress 方法把信息传递给 ProcessChanged 事件处理函数。
                            //第一个参数类型为 int，表示执行进度。
                            //如果有更多的信息需要传递，可以使用 ReportProgress 的第二个参数。
                            //这里我们给第二个参数传进去一条消息。
                            Message["Result"] = Result.ToString();
                            Message["ErrInfo"] = ErrInfo.ToString();
                            Message["Message"] = message.ToString();
                        }
                        else
                        {
                            //记录日志
                            Message["Result"] = Result.ToString();
                            Message["ErrInfo"] = ErrInfo.ToString();
                            Message["Message"] = "";
                            continue;
                        }


                    }
                }
                else
                {
                    Count = 10;
                    bool1 = TdxApi.TdxHq_Multi_GetSecurityBars(ConnectionID, 0, 0, s.stockcode, 0, ref Count, Result, ErrInfo);
                }
       
            }
            ALLlistCon.Remove(ConnectionID);
            OverlistCon.Remove(ConnectionID);
            TdxApi.TdxHq_Multi_Disconnect(ConnectionID);
        }

    }
}
