using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;


namespace uTrade.Data
{
    public partial class StockInfoService
    {
        private static List<int> ALLlistCon = new List<int>();
        private static List<int> OverlistCon = new List<int>();
        private Server m_Server;
        StringBuilder Result = new StringBuilder(1024 * 1024);
        StringBuilder ErrInfo = new StringBuilder(256);
        StockInfoManager _Stockmanager = new StockInfoManager();
        Dictionary<string, string> DicConSH = new Dictionary<string, string>();
        Dictionary<string, string> DicCon = new Dictionary<string, string>();


        public StockInfoService()
		{}
        public StockInfoService(Server oServer)
        {
            m_Server = oServer;
        }

        public void DataAccess()
        {

            #region 连接tdx服务器
            bool bool1 = TdxApi.OpenTdx(ErrInfo);
            int ConnectionID = TdxApi.TdxHq_Multi_Connect(m_Server.IP, m_Server.Port, Result, ErrInfo);
            int ConnectionID2 = TdxApi.TdxHq_Multi_Connect(m_Server.IP, m_Server.Port, Result, ErrInfo);
            ALLlistCon.Add(ConnectionID);
            OverlistCon.Add(ConnectionID);
            #endregion

            int conSZ = OverlistCon[0];
            OverlistCon.RemoveAt(0);
            short Count = 0;
            //SZ 后台查询Count
            bool1 = TdxApi.TdxHq_Multi_GetSecurityCount(conSZ, 0, ref Count, ErrInfo);
            DicCon.Add("Con", conSZ.ToString());
            DicCon.Add("Count", Count.ToString());

            //SZ 后台执行
            ALLlistCon.Add(ConnectionID2);
            OverlistCon.Add(ConnectionID2);

            int conSH = OverlistCon[0];
            OverlistCon.RemoveAt(0);
            //SH 后台查询Count
            bool1 = TdxApi.TdxHq_Multi_GetSecurityCount(conSH, 1, ref Count, ErrInfo);

            DicConSH.Add("Con", conSH.ToString());
            DicConSH.Add("Count", Count.ToString());

            //SZ 后台执行
            _Stockmanager.Clear();
            Thread tdSyncStock_SH = new Thread(SyncStockInfo_SH);
            tdSyncStock_SH.Start();
            Thread tdSyncStock_SZ = new Thread(SyncStockInfo_SZ);
            tdSyncStock_SZ.Start();

        }

        void SyncStockInfo_SH()
        {
            int con = Convert.ToInt32(DicConSH["Con"]);
            short shortResult = (short)Convert.ToInt32(DicConSH["Count"]);

            short Count;
            bool bool1 = TdxApi.TdxHq_Multi_GetSecurityCount(con, 0, ref shortResult, ErrInfo);
            Console.WriteLine(bool1 ? shortResult.ToString() : ErrInfo.ToString());
            int num = shortResult / 1000;
            int sum = 0;
            for (int x = 0; x <= num; x++)
            {
                int start = x * 1000;
                Count = shortResult;
                bool1 = TdxApi.TdxHq_Multi_GetSecurityList(con, 1, (short)start, ref Count, Result, ErrInfo);
                string[] strRow = Result.ToString().Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);   //分解行的字符串
                //string[] strCol=strRow[0].Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> Message = new Dictionary<string, string>();
                Message.Add("Result", "");
                Message.Add("ErrInfo", "");
                for (int i = 1; i < strRow.Length; i++)
                {
                    //分解行的字符串
                    //StockInfo属性 每列数据
                    string[] strCol = strRow[i].Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (strCol[6] == "0" || strCol[2].Contains("指数"))
                    {
                        continue;
                    }
                    StockInfo stock = new StockInfo();
                    stock.stockcode = strCol[0];
                    stock.Type = "1";
                    stock.OneHand = strCol[1];
                    stock.Name = strCol[2];
                    stock.PointIndex = strCol[4];
                    stock.YestClose = decimal.Parse(strCol[5]);
                    stock.Unknow1 = strCol[3];
                    stock.Unknow2 = strCol[6];
                    stock.Unknow3 = strCol[7];
                    int ID = _Stockmanager.Add(stock);
                    if (ID > 0)
                    {
                        sum += 1;
                        string message = "Current sum is: " + start.ToString();
                        Message["Result"] = Result.ToString();
                        Message["ErrInfo"] = ErrInfo.ToString();
                    }
                    else
                    {
                        //记录日志
                        Message["Result"] = Result.ToString();
                        Message["ErrInfo"] = ErrInfo.ToString();
                        continue;
                    }
                }
            }
        }

        void SyncStockInfo_SZ()
        {
            int con = Convert.ToInt32(DicCon["Con"]);
            short shortResult = (short)Convert.ToInt32(DicCon["Count"]);

            short Count;
            bool bool1;
            //bool bool1 = TdxApi.TdxHq_Multi_GetSecurityCount(con, 0, ref shortResult, ErrInfo);
            //Console.WriteLine(bool1 ? shortResult.ToString() : ErrInfo.ToString());
            int num = shortResult / 1000;
            int sum = 0;
            for (int x = 0; x <= num; x++)
            {
                int start = x * 1000;
                Count = shortResult;
                bool1 = TdxApi.TdxHq_Multi_GetSecurityList(con, 0, (short)start, ref Count, Result, ErrInfo);
                string[] strRow = Result.ToString().Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);   //分解行的字符串
                //string[] strCol=strRow[0].Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> Message = new Dictionary<string, string>();
                Message.Add("Result", "");
                Message.Add("ErrInfo", "");
                for (int i = 1; i < strRow.Length; i++)
                {
                    //分解行的字符串
                    //StockInfo属性 每列数据
                    string[] strCol = strRow[i].Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (strCol[6] == "0" || strCol[2].Contains("指数"))
                    {
                        continue;
                    }
                    StockInfo stock = new StockInfo();
                    stock.stockcode = strCol[0];
                    stock.Type = "0";
                    stock.OneHand = strCol[1];
                    stock.Name = strCol[2];
                    stock.PointIndex = strCol[4];
                    stock.YestClose = decimal.Parse(strCol[5]);
                    stock.Unknow1 = strCol[3];
                    stock.Unknow2 = strCol[6];
                    stock.Unknow3 = strCol[7];
                    int ID = _Stockmanager.Add(stock);
                    if (ID > 0)
                    {
                        sum += 1;
                        string message = "Current sum is: " + start.ToString();
                        Message["Result"] = Result.ToString();
                        Message["ErrInfo"] = ErrInfo.ToString();
                    }
                    else
                    {
                        //记录日志
                        Message["Result"] = Result.ToString();
                        Message["ErrInfo"] = ErrInfo.ToString();
                        continue;
                    }
                }
            }
        }


    }
}
