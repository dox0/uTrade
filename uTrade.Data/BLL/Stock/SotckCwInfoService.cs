using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using uTrade.Common;
using SufeiUtil;

namespace uTrade.Data
{
    public partial class StockCwInfoService
    {
        StockInfoManager _oStockInfo = new StockInfoManager();
        StockCwInfoManager _StockCWInfo = new StockCwInfoManager();

        public StockCwInfoService()
		{}
	    
        public void DataAccess()
        {
            List<StockInfo> stocklist = _oStockInfo.GetStockCodeList("");
            foreach (StockInfo s in stocklist)
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem();
                item.URL = "http://quotes.money.163.com/service/zycwzb_" + s.stockcode + ".html?type=report";

                item.Encoding = Encoding.UTF8;
                item.Method = "GET";
                item.Timeout = 100000;
                item.ReadWriteTimeout = 30000;//写入Post数据超时时间，可选项默认为30000

                HttpResult result = http.GetHtml(item);

                string Result = result.Html.Replace("\r\n\t", "").Replace(" ", "");
                //string[] arrTemp = result.Html.Split('\r\n');
                string[] strlist = Result.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (strlist.Length == 1)
                {
                    continue;
                }
                string[] reportDate = strlist[0].Substring(0, strlist[0].Length - 1).Split(',');
                string[] JBMGSY = strlist[1].Substring(0, strlist[1].Length - 1).Split(',');
                string[] MGJZC = strlist[2].Substring(0, strlist[2].Length - 1).Split(',');
                string[] MGJYHDCSXJLJE = strlist[3].Substring(0, strlist[3].Length - 1).Split(',');
                string[] ZYYWSR = strlist[4].Substring(0, strlist[4].Length - 1).Split(',');

                string[] ZYYWLR = strlist[5].Substring(0, strlist[5].Length - 1).Split(',');
                string[] YYLR = strlist[6].Substring(0, strlist[6].Length - 1).Split(',');
                string[] TZSY = strlist[7].Substring(0, strlist[7].Length - 1).Split(',');
                string[] YYEYSZJE = strlist[8].Substring(0, strlist[8].Length - 1).Split(',');
                string[] LRZE = strlist[9].Substring(0, strlist[9].Length - 1).Split(',');

                string[] JLR = strlist[10].Substring(0, strlist[10].Length - 1).Split(',');
                string[] JLROUT = strlist[11].Substring(0, strlist[11].Length - 1).Split(',');
                string[] JYHDCSDXJLJE = strlist[12].Substring(0, strlist[12].Length - 1).Split(',');
                string[] XJJXJDJWJCJE = strlist[13].Substring(0, strlist[13].Length - 1).Split(',');
                string[] ZZC = strlist[14].Substring(0, strlist[14].Length - 1).Split(',');

                string[] LDZC = strlist[15].Substring(0, strlist[15].Length - 1).Split(',');
                string[] ZFZ = strlist[16].Substring(0, strlist[16].Length - 1).Split(',');
                string[] LDFZ = strlist[17].Substring(0, strlist[17].Length - 1).Split(',');
                string[] GDQYBHSSGDQY = strlist[18].Substring(0, strlist[18].Length - 1).Split(',');
                string strlist19 = strlist[19].Replace("\t", "");
                string[] JZCSYLJQ = strlist19.Substring(0, strlist19.Length - 1).Split(',');

                for (int num = 1; num < reportDate.Length; num++)
                {
                    int IsHave = _StockCWInfo.GetRecordCount("Symbol='" + s.stockcode + "' AND ReportDate=CONVERT(datetime,'" + reportDate[num].ToString() + "',102)");
                    if (IsHave != 0)
                    {
                        continue;
                    }
                    StockCwInfo cw = new StockCwInfo();
                    cw.Code = s.stockcode;
                    cw.ReportDate = Convert.ToDateTime(reportDate[num].ToString());
                    cw.JBMGSY = decimal.Parse(PublicTool.IsNumElseToZero(JBMGSY[num].ToString()));
                    cw.MGJZC = decimal.Parse(PublicTool.IsNumElseToZero(MGJZC[num].ToString()));
                    cw.MGJYHDCSXJLJE = decimal.Parse(PublicTool.IsNumElseToZero(MGJYHDCSXJLJE[num].ToString()));
                    cw.ZYYWSR = PublicTool.IsNumElseToZero(ZYYWSR[num].ToString());

                    cw.ZYYWLR = PublicTool.IsNumElseToZero(ZYYWLR[num].ToString());
                    cw.YYLR = PublicTool.IsNumElseToZero(YYLR[num].ToString());
                    cw.TZSY = PublicTool.IsNumElseToZero(TZSY[num].ToString());
                    cw.YYEYSZJE = PublicTool.IsNumElseToZero(YYEYSZJE[num].ToString());
                    cw.LRZE = PublicTool.IsNumElseToZero(LRZE[num].ToString());

                    cw.JLR = PublicTool.IsNumElseToZero(JLR[num].ToString());
                    cw.JLROUT = PublicTool.IsNumElseToZero(JLROUT[num].ToString());
                    cw.JYHDCSDXJLJE = PublicTool.IsNumElseToZero(JYHDCSDXJLJE[num].ToString());
                    cw.XJJXJDJWJCJE = PublicTool.IsNumElseToZero(XJJXJDJWJCJE[num].ToString());
                    cw.ZZC = PublicTool.IsNumElseToZero(ZZC[num].ToString());

                    cw.LDZC = PublicTool.IsNumElseToZero(LDZC[num].ToString());
                    cw.ZFZ = PublicTool.IsNumElseToZero(ZFZ[num].ToString());
                    cw.LDFZ = PublicTool.IsNumElseToZero(LDFZ[num].ToString());
                    cw.GDQYBHSSGDQY = PublicTool.IsNumElseToZero(GDQYBHSSGDQY[num].ToString());
                    cw.JZCSYLJQ = decimal.Parse(PublicTool.IsNumElseToZero(JZCSYLJQ[num].ToString()));

                    if (_StockCWInfo.Add(cw) == 0)
                    {
                        continue;
                    }

                }
            }
        }

    }
}
