using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;

namespace uTrade.Data
{
    class StockFinanceInfoService
    {
        private static List<int> ALLlistCon = new List<int>();
        private static List<int> OverlistCon = new List<int>();
        private Server m_Server;
        StringBuilder Result = new StringBuilder(1024 * 1024);
        StringBuilder ErrInfo = new StringBuilder(256);
        StockFinanceInfoManager _StockFinanceInfo = new StockFinanceInfoManager();
        StockInfoManager _oStockInfo = new StockInfoManager();

        public StockFinanceInfoService()
        { }

        public StockFinanceInfoService(Server oServer)
        {
            m_Server = oServer;
        }
        public void DataAccess()
        {
            bool bool1 = TdxApi.OpenTdx(ErrInfo);
            int ConnectionID = TdxApi.TdxHq_Multi_Connect(m_Server.IP, m_Server.Port, Result, ErrInfo);
            OverlistCon.Add(ConnectionID);
            int conSZ = OverlistCon[0];
            OverlistCon.RemoveAt(0);

            bool1 = TdxApi.TdxHq_Multi_GetFinanceInfo(ConnectionID, 0, "000002", Result, ErrInfo);
            Console.WriteLine(bool1 ? Result.ToString() : ErrInfo.ToString());

            _StockFinanceInfo.Clear();
            Thread tdStock5MinInfo = new Thread(SyncStocFinanceInfo);
            tdStock5MinInfo.Start();

        }


        void SyncStocFinanceInfo()
        {
            //IsFinanceWork = true;
            int ConnectionID = TdxApi.TdxHq_Multi_Connect("222.73.49.4", 7709, Result, ErrInfo);
            ALLlistCon.Add(ConnectionID);
            OverlistCon.Add(ConnectionID);

            List<StockInfo> stockList = new List<StockInfo>();
            stockList = _oStockInfo.GetStockCodeList("");

            Dictionary<string, string> Message = new Dictionary<string, string>();
            Message.Add("Result", "");
            Message.Add("ErrInfo", "");
            bool bool1;

            #region foreach
            foreach (StockInfo s in stockList)
            {
                //try
                //{
                    bool1 = TdxApi.TdxHq_Multi_GetFinanceInfo(ConnectionID, Convert.ToByte(s.Type), s.stockcode, Result, ErrInfo);
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
                        int IsHave = _StockFinanceInfo.GetRecordCount("Symbol='" + s.stockcode + "' and CWUpdateTime=CONVERT(datetime,'" + strCol[5].Replace("--", "-") + "',102)");
                        if (IsHave > 0)
                        {
                            continue;
                        }
                        StockFinanceInfo stock = new StockFinanceInfo();
                        stock.Type = strCol[0];
                        stock.Code = strCol[1];
                        stock.GBLT = strCol[2];
                        stock.SSSF = strCol[3];
                        stock.SSHY = strCol[4];
                        stock.CWUpdateTime = DateTime.ParseExact(strCol[5], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        stock.ListingDate = DateTime.ParseExact(strCol[6], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        stock.AllGB = strCol[7];
                        stock.GJG = strCol[8];
                        stock.FQRFRG = strCol[9];
                        stock.FRG = strCol[10];
                        stock.BG = strCol[11];
                        stock.HG = strCol[12];
                        stock.ZhGG = strCol[13];
                        stock.AllZC = strCol[14];
                        stock.LDZC = strCol[15];
                        stock.GDZC = strCol[16];
                        stock.WXZC = strCol[17];
                        stock.GDRS = strCol[18];
                        stock.LDFZ = strCol[19];
                        stock.CQFZ = strCol[20];
                        stock.ZBGJJ = strCol[21];
                        stock.JZC = strCol[22];
                        stock.ZYSR = strCol[23];
                        stock.ZYLR = strCol[24];
                        stock.YSZK = strCol[25];
                        stock.YYLR = strCol[26];
                        stock.TZSY = strCol[27];
                        stock.JYXJL = strCol[28];
                        stock.ZXJL = strCol[29];
                        stock.CH = strCol[20];
                        stock.LRZE = strCol[31];
                        stock.SHLR = strCol[32];
                        stock.JLR = strCol[33];
                        stock.WFLR = strCol[34];
                        stock.Unknow1 = strCol[35];
                        stock.unknow2 = strCol[36];
                    int ID = _StockFinanceInfo.Add(stock);
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
