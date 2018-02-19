using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using uTrade.Common;
using SufeiUtil;


namespace uTrade.Data
{
    public partial class StockZCFZInfoService
    {

        StockInfoManager _oStockInfo = new StockInfoManager();
        StockZCFZInfoManager _StockZCFZInfo = new StockZCFZInfoManager();
        public StockZCFZInfoService()
		{}

        public void DataAccess()
        {
            List<StockInfo> stocklist = _oStockInfo.GetStockCodeList("");
            foreach (StockInfo s in stocklist)
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem();
                item.URL = " http://quotes.money.163.com/service/zcfzb_" + s.stockcode + ".html";
                item.Encoding = Encoding.UTF8;
                item.Method = "GET";
                item.Timeout = 100000;
                item.ReadWriteTimeout = 30000;//写入Post数据超时时间，可选项默认为30000

                HttpResult result = http.GetHtml(item);

                string Result = result.Html.Replace("\r\n\t", "").Replace(" ", "");
                //string[] arrTemp = result.Html.Split('\r\n');
                string[] strlist = Result.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (strlist.Length == 72 || strlist.Length == 1)
                {
                    continue;
                }
                string[] ReportDate = strlist[0].Substring(0, strlist[0].Length - 1).Split(',');
                string[] HBZJ = strlist[1].Substring(0, strlist[1].Length - 1).Split(',');
                string[] JSBFJ = strlist[2].Substring(0, strlist[2].Length - 1).Split(',');
                string[] CCZJ = strlist[3].Substring(0, strlist[3].Length - 1).Split(',');
                string[] JYXJRZC = strlist[4].Substring(0, strlist[4].Length - 1).Split(',');
                string[] YSJRZC = strlist[5].Substring(0, strlist[5].Length - 1).Split(',');
                string[] YSPJ = strlist[6].Substring(0, strlist[6].Length - 1).Split(',');
                string[] YSZK = strlist[7].Substring(0, strlist[7].Length - 1).Split(',');
                string[] YFKX = strlist[8].Substring(0, strlist[8].Length - 1).Split(',');
                string[] YSBF = strlist[9].Substring(0, strlist[9].Length - 1).Split(',');
                string[] YSFBZK = strlist[10].Substring(0, strlist[10].Length - 1).Split(',');
                string[] YSFBHTZBJ = strlist[11].Substring(0, strlist[11].Length - 1).Split(',');
                string[] YSLX = strlist[12].Substring(0, strlist[12].Length - 1).Split(',');
                string[] YSGL = strlist[13].Substring(0, strlist[13].Length - 1).Split(',');
                string[] QTYSK = strlist[14].Substring(0, strlist[14].Length - 1).Split(',');
                string[] YSCKTS = strlist[15].Substring(0, strlist[15].Length - 1).Split(',');
                string[] YSBTK = strlist[16].Substring(0, strlist[16].Length - 1).Split(',');
                string[] YSBZJ = strlist[17].Substring(0, strlist[17].Length - 1).Split(',');
                string[] NBYSK = strlist[18].Substring(0, strlist[18].Length - 1).Split(',');
                string[] MRFSJRZC = strlist[19].Substring(0, strlist[19].Length - 1).Split(',');
                string[] CH = strlist[20].Substring(0, strlist[20].Length - 1).Split(',');
                string[] DTFY = strlist[21].Substring(0, strlist[21].Length - 1).Split(',');
                string[] DCLLDZCSS = strlist[22].Substring(0, strlist[22].Length - 1).Split(',');
                string[] YNNDQDFLDZC = strlist[23].Substring(0, strlist[23].Length - 1).Split(',');
                string[] QTLDZC = strlist[24].Substring(0, strlist[24].Length - 1).Split(',');
                string[] LDZCHJ = strlist[25].Substring(0, strlist[25].Length - 1).Split(',');
                string[] FCDKJDK = strlist[26].Substring(0, strlist[26].Length - 1).Split(',');
                string[] KGCSJRZC = strlist[27].Substring(0, strlist[27].Length - 1).Split(',');
                string[] CYZDQTZ = strlist[28].Substring(0, strlist[28].Length - 1).Split(',');
                string[] CQYSK = strlist[29].Substring(0, strlist[29].Length - 1).Split(',');
                string[] CQGQTZ = strlist[30].Substring(0, strlist[30].Length - 1).Split(',');
                string[] QTCQTZ = strlist[31].Substring(0, strlist[31].Length - 1).Split(',');
                string[] TZXFDC = strlist[32].Substring(0, strlist[32].Length - 1).Split(',');
                string[] GDZCYZ = strlist[33].Substring(0, strlist[33].Length - 1).Split(',');
                string[] LJZJ = strlist[34].Substring(0, strlist[34].Length - 1).Split(',');
                string[] GDZCJZ = strlist[35].Substring(0, strlist[35].Length - 1).Split(',');
                string[] GDZCJZZB = strlist[36].Substring(0, strlist[36].Length - 1).Split(',');
                string[] GDZC = strlist[37].Substring(0, strlist[37].Length - 1).Split(',');
                string[] ZJGC = strlist[38].Substring(0, strlist[38].Length - 1).Split(',');
                string[] GCWZ = strlist[39].Substring(0, strlist[39].Length - 1).Split(',');
                string[] GDZCQL = strlist[40].Substring(0, strlist[40].Length - 1).Split(',');
                string[] SCXSWZC = strlist[41].Substring(0, strlist[41].Length - 1).Split(',');
                string[] GYXSWZC = strlist[42].Substring(0, strlist[42].Length - 1).Split(',');
                string[] QYZC = strlist[43].Substring(0, strlist[43].Length - 1).Split(',');
                string[] WXZC = strlist[44].Substring(0, strlist[44].Length - 1).Split(',');
                string[] KFZC = strlist[45].Substring(0, strlist[45].Length - 1).Split(',');
                string[] SY = strlist[46].Substring(0, strlist[46].Length - 1).Split(',');
                string[] CQDTFY = strlist[47].Substring(0, strlist[47].Length - 1).Split(',');
                string[] GQFZLTQ = strlist[48].Substring(0, strlist[48].Length - 1).Split(',');
                string[] DYSDSZC = strlist[49].Substring(0, strlist[49].Length - 1).Split(',');
                string[] QTFLDZC = strlist[50].Substring(0, strlist[50].Length - 1).Split(',');
                string[] FLDZCHJ = strlist[51].Substring(0, strlist[51].Length - 1).Split(',');
                string[] ZCZJ = strlist[52].Substring(0, strlist[52].Length - 1).Split(',');
                string[] DQJK = strlist[53].Substring(0, strlist[53].Length - 1).Split(',');
                string[] XZYYHJK = strlist[54].Substring(0, strlist[54].Length - 1).Split(',');
                string[] XSCKJTYCF = strlist[55].Substring(0, strlist[55].Length - 1).Split(',');
                string[] CRZJ = strlist[56].Substring(0, strlist[56].Length - 1).Split(',');
                string[] JYXJRFZ = strlist[57].Substring(0, strlist[57].Length - 1).Split(',');
                string[] YSJRFZ = strlist[58].Substring(0, strlist[58].Length - 1).Split(',');
                string[] YFPJ = strlist[59].Substring(0, strlist[59].Length - 1).Split(',');
                string[] YFZK = strlist[60].Substring(0, strlist[60].Length - 1).Split(',');
                string[] YuSZK = strlist[61].Substring(0, strlist[61].Length - 1).Split(',');
                string[] MCHGJRZCK = strlist[62].Substring(0, strlist[62].Length - 1).Split(',');
                string[] YFSXFJYJ = strlist[63].Substring(0, strlist[63].Length - 1).Split(',');
                string[] YFZGXC = strlist[64].Substring(0, strlist[64].Length - 1).Split(',');
                string[] YJSF = strlist[65].Substring(0, strlist[65].Length - 1).Split(',');
                string[] YFLX = strlist[66].Substring(0, strlist[66].Length - 1).Split(',');
                string[] YFGL = strlist[67].Substring(0, strlist[67].Length - 1).Split(',');
                string[] QTYJK = strlist[68].Substring(0, strlist[68].Length - 1).Split(',');
                string[] YFBZJ = strlist[69].Substring(0, strlist[69].Length - 1).Split(',');
                string[] NBYFK = strlist[70].Substring(0, strlist[70].Length - 1).Split(',');
                string[] QTYFK = strlist[71].Substring(0, strlist[71].Length - 1).Split(',');
                string[] YTFY = strlist[72].Substring(0, strlist[72].Length - 1).Split(',');
                string[] YJLDFZ = strlist[73].Substring(0, strlist[73].Length - 1).Split(',');
                string[] YFFBZK = strlist[74].Substring(0, strlist[74].Length - 1).Split(',');
                string[] BXHTZBJ = strlist[75].Substring(0, strlist[75].Length - 1).Split(',');
                string[] DLMMZQK = strlist[76].Substring(0, strlist[76].Length - 1).Split(',');
                string[] DLCXZQK = strlist[77].Substring(0, strlist[77].Length - 1).Split(',');
                string[] GJPZJS = strlist[78].Substring(0, strlist[78].Length - 1).Split(',');
                string[] GNPZJS = strlist[79].Substring(0, strlist[79].Length - 1).Split(',');
                string[] DYSY = strlist[80].Substring(0, strlist[80].Length - 1).Split(',');
                string[] YFDQZQ = strlist[81].Substring(0, strlist[81].Length - 1).Split(',');
                string[] YNDDQDFLDFZ = strlist[82].Substring(0, strlist[82].Length - 1).Split(',');
                string[] QTLDFZ = strlist[83].Substring(0, strlist[83].Length - 1).Split(',');
                string[] LDFZHJ = strlist[84].Substring(0, strlist[84].Length - 1).Split(',');
                string[] CQJQ = strlist[85].Substring(0, strlist[85].Length - 1).Split(',');
                string[] YFZQ = strlist[86].Substring(0, strlist[86].Length - 1).Split(',');
                string[] CQYFZQ = strlist[87].Substring(0, strlist[87].Length - 1).Split(',');
                string[] ZXYFK = strlist[88].Substring(0, strlist[88].Length - 1).Split(',');
                string[] YJFLDFZ = strlist[89].Substring(0, strlist[89].Length - 1).Split(',');
                string[] CQDYSY = strlist[90].Substring(0, strlist[90].Length - 1).Split(',');
                string[] DYSDSFZ = strlist[91].Substring(0, strlist[91].Length - 1).Split(',');
                string[] QTFLDFZ = strlist[92].Substring(0, strlist[92].Length - 1).Split(',');
                string[] FLDFZHJ = strlist[93].Substring(0, strlist[93].Length - 1).Split(',');
                string[] FZHJ = strlist[94].Substring(0, strlist[94].Length - 1).Split(',');
                string[] SSZB = strlist[95].Substring(0, strlist[95].Length - 1).Split(',');
                string[] ZBGJ = strlist[96].Substring(0, strlist[96].Length - 1).Split(',');
                string[] JKCG = strlist[97].Substring(0, strlist[97].Length - 1).Split(',');
                string[] ZXCB = strlist[98].Substring(0, strlist[98].Length - 1).Split(',');
                string[] YYGJ = strlist[99].Substring(0, strlist[99].Length - 1).Split(',');
                string[] YBFXZB = strlist[100].Substring(0, strlist[100].Length - 1).Split(',');
                string[] WQDDTZSS = strlist[101].Substring(0, strlist[101].Length - 1).Split(',');
                string[] WFPLR = strlist[102].Substring(0, strlist[102].Length - 1).Split(',');
                string[] NFPXJGL = strlist[103].Substring(0, strlist[103].Length - 1).Split(',');
                string[] WBBBZSCE = strlist[104].Substring(0, strlist[104].Length - 1).Split(',');
                string[] GSYMGSGDQYHJ = strlist[105].Substring(0, strlist[105].Length - 1).Split(',');
                string[] SSGDQY = strlist[106].Substring(0, strlist[106].Length - 1).Split(',');
                string[] SYZQY = strlist[107].Substring(0, strlist[107].Length - 1).Split(',');
                string strlist109 = strlist[108].Replace("\t", "");
                string[] FZHSYZQY = strlist109.Substring(0, strlist109.Length - 1).Split(',');


                for (int num = 1; num < ReportDate.Length; num++)
                {
                    int IsHave = _StockZCFZInfo.GetRecordCount("Symbol='" + s.stockcode + "' AND ReportDate=CONVERT(datetime,'" + ReportDate[num].ToString() + "',102)");
                    if (IsHave != 0)
                    {
                        continue;
                    }
                    StockZCFZInfo model = new StockZCFZInfo();
                    model.Code = s.stockcode;
                    model.ReportDate = Convert.ToDateTime(ReportDate[num].ToString());
                    model.HBZJ = PublicTool.IsNumElseToZero(HBZJ[num].ToString());
                    model.JSBFJ = PublicTool.IsNumElseToZero(JSBFJ[num].ToString());
                    model.CCZJ = PublicTool.IsNumElseToZero(CCZJ[num].ToString());
                    model.JYXJRZC = PublicTool.IsNumElseToZero(JYXJRZC[num].ToString());
                    model.YSJRZC = PublicTool.IsNumElseToZero(YSJRZC[num].ToString());
                    model.YSPJ = PublicTool.IsNumElseToZero(YSPJ[num].ToString());
                    model.YSZK = PublicTool.IsNumElseToZero(YSZK[num].ToString());
                    model.YFKX = PublicTool.IsNumElseToZero(YFKX[num].ToString());
                    model.YSBF = PublicTool.IsNumElseToZero(YSBF[num].ToString());
                    model.YSFBZK = PublicTool.IsNumElseToZero(YSFBZK[num].ToString());
                    model.YSFBHTZBJ = PublicTool.IsNumElseToZero(YSFBHTZBJ[num].ToString());
                    model.YSLX = PublicTool.IsNumElseToZero(YSLX[num].ToString());
                    model.YSGL = PublicTool.IsNumElseToZero(YSGL[num].ToString());
                    model.QTYSK = PublicTool.IsNumElseToZero(QTYSK[num].ToString());
                    model.YSCKTS = PublicTool.IsNumElseToZero(YSCKTS[num].ToString());
                    model.YSBTK = PublicTool.IsNumElseToZero(YSBTK[num].ToString());
                    model.YSBZJ = PublicTool.IsNumElseToZero(YSBZJ[num].ToString());
                    model.NBYSK = PublicTool.IsNumElseToZero(NBYSK[num].ToString());
                    model.MRFSJRZC = PublicTool.IsNumElseToZero(MRFSJRZC[num].ToString());
                    model.CH = PublicTool.IsNumElseToZero(CH[num].ToString());
                    model.DTFY = PublicTool.IsNumElseToZero(DTFY[num].ToString());
                    model.DCLLDZCSS = PublicTool.IsNumElseToZero(DCLLDZCSS[num].ToString());
                    model.YNNDQDFLDZC = PublicTool.IsNumElseToZero(YNNDQDFLDZC[num].ToString());
                    model.QTLDZC = PublicTool.IsNumElseToZero(QTLDZC[num].ToString());
                    model.LDZCHJ = PublicTool.IsNumElseToZero(LDZCHJ[num].ToString());
                    model.FCDKJDK = PublicTool.IsNumElseToZero(FCDKJDK[num].ToString());
                    model.KGCSJRZC = PublicTool.IsNumElseToZero(KGCSJRZC[num].ToString());
                    model.CYZDQTZ = PublicTool.IsNumElseToZero(CYZDQTZ[num].ToString());
                    model.CQYSK = PublicTool.IsNumElseToZero(CQYSK[num].ToString());
                    model.CQGQTZ = PublicTool.IsNumElseToZero(CQGQTZ[num].ToString());
                    model.QTCQTZ = PublicTool.IsNumElseToZero(QTCQTZ[num].ToString());
                    model.TZXFDC = PublicTool.IsNumElseToZero(TZXFDC[num].ToString());
                    model.GDZCYZ = PublicTool.IsNumElseToZero(GDZCYZ[num].ToString());
                    model.LJZJ = PublicTool.IsNumElseToZero(LJZJ[num].ToString());
                    model.GDZCJZ = PublicTool.IsNumElseToZero(GDZCJZ[num].ToString());
                    model.GDZCJZZB = PublicTool.IsNumElseToZero(GDZCJZZB[num].ToString());
                    model.GDZC = PublicTool.IsNumElseToZero(GDZC[num].ToString());
                    model.ZJGC = PublicTool.IsNumElseToZero(ZJGC[num].ToString());
                    model.GCWZ = PublicTool.IsNumElseToZero(GCWZ[num].ToString());
                    model.GDZCQL = PublicTool.IsNumElseToZero(GDZCQL[num].ToString());
                    model.SCXSWZC = PublicTool.IsNumElseToZero(SCXSWZC[num].ToString());
                    model.GYXSWZC = PublicTool.IsNumElseToZero(GYXSWZC[num].ToString());
                    model.QYZC = PublicTool.IsNumElseToZero(QYZC[num].ToString());
                    model.WXZC = PublicTool.IsNumElseToZero(WXZC[num].ToString());
                    model.KFZC = PublicTool.IsNumElseToZero(KFZC[num].ToString());
                    model.SY = PublicTool.IsNumElseToZero(SY[num].ToString());
                    model.CQDTFY = PublicTool.IsNumElseToZero(CQDTFY[num].ToString());
                    model.GQFZLTQ = PublicTool.IsNumElseToZero(GQFZLTQ[num].ToString());
                    model.DYSDSZC = PublicTool.IsNumElseToZero(DYSDSZC[num].ToString());
                    model.QTFLDZC = PublicTool.IsNumElseToZero(QTFLDZC[num].ToString());
                    model.FLDZCHJ = PublicTool.IsNumElseToZero(FLDZCHJ[num].ToString());
                    model.ZCZJ = PublicTool.IsNumElseToZero(ZCZJ[num].ToString());
                    model.DQJK = PublicTool.IsNumElseToZero(DQJK[num].ToString());
                    model.XZYYHJK = PublicTool.IsNumElseToZero(XZYYHJK[num].ToString());
                    model.XSCKJTYCF = PublicTool.IsNumElseToZero(XSCKJTYCF[num].ToString());
                    model.CRZJ = PublicTool.IsNumElseToZero(CRZJ[num].ToString());
                    model.JYXJRFZ = PublicTool.IsNumElseToZero(JYXJRFZ[num].ToString());
                    model.YSJRFZ = PublicTool.IsNumElseToZero(YSJRFZ[num].ToString());
                    model.YFPJ = PublicTool.IsNumElseToZero(YFPJ[num].ToString());
                    model.YFZK = PublicTool.IsNumElseToZero(YFZK[num].ToString());
                    model.YuSZK = PublicTool.IsNumElseToZero(YuSZK[num].ToString());
                    model.MCHGJRZCK = PublicTool.IsNumElseToZero(MCHGJRZCK[num].ToString());
                    model.YFSXFJYJ = PublicTool.IsNumElseToZero(YFSXFJYJ[num].ToString());
                    model.YFZGXC = PublicTool.IsNumElseToZero(YFZGXC[num].ToString());
                    model.YJSF = PublicTool.IsNumElseToZero(YJSF[num].ToString());
                    model.YFLX = PublicTool.IsNumElseToZero(YFLX[num].ToString());
                    model.YFGL = PublicTool.IsNumElseToZero(YFGL[num].ToString());
                    model.QTYJK = PublicTool.IsNumElseToZero(QTYJK[num].ToString());
                    model.YFBZJ = PublicTool.IsNumElseToZero(YFBZJ[num].ToString());
                    model.NBYFK = PublicTool.IsNumElseToZero(NBYFK[num].ToString());
                    model.QTYFK = PublicTool.IsNumElseToZero(QTYFK[num].ToString());
                    model.YTFY = PublicTool.IsNumElseToZero(YTFY[num].ToString());
                    model.YJLDFZ = PublicTool.IsNumElseToZero(YJLDFZ[num].ToString());
                    model.YFFBZK = PublicTool.IsNumElseToZero(YFFBZK[num].ToString());
                    model.BXHTZBJ = PublicTool.IsNumElseToZero(BXHTZBJ[num].ToString());
                    model.DLMMZQK = PublicTool.IsNumElseToZero(DLMMZQK[num].ToString());
                    model.DLCXZQK = PublicTool.IsNumElseToZero(DLCXZQK[num].ToString());
                    model.GJPZJS = PublicTool.IsNumElseToZero(GJPZJS[num].ToString());
                    model.GNPZJS = PublicTool.IsNumElseToZero(GNPZJS[num].ToString());
                    model.DYSY = PublicTool.IsNumElseToZero(DYSY[num].ToString());
                    model.YFDQZQ = PublicTool.IsNumElseToZero(YFDQZQ[num].ToString());
                    model.YNDDQDFLDFZ = PublicTool.IsNumElseToZero(YNDDQDFLDFZ[num].ToString());
                    model.QTLDFZ = PublicTool.IsNumElseToZero(QTLDFZ[num].ToString());
                    model.LDFZHJ = PublicTool.IsNumElseToZero(LDFZHJ[num].ToString());
                    model.CQJQ = PublicTool.IsNumElseToZero(CQJQ[num].ToString());
                    model.YFZQ = PublicTool.IsNumElseToZero(YFZQ[num].ToString());
                    model.CQYFZQ = PublicTool.IsNumElseToZero(CQYFZQ[num].ToString());
                    model.ZXYFK = PublicTool.IsNumElseToZero(ZXYFK[num].ToString());
                    model.YJFLDFZ = PublicTool.IsNumElseToZero(YJFLDFZ[num].ToString());
                    model.CQDYSY = PublicTool.IsNumElseToZero(CQDYSY[num].ToString());
                    model.DYSDSFZ = PublicTool.IsNumElseToZero(DYSDSFZ[num].ToString());
                    model.QTFLDFZ = PublicTool.IsNumElseToZero(QTFLDFZ[num].ToString());
                    model.FLDFZHJ = PublicTool.IsNumElseToZero(FLDFZHJ[num].ToString());
                    model.FZHJ = PublicTool.IsNumElseToZero(FZHJ[num].ToString());
                    model.SSZB = PublicTool.IsNumElseToZero(SSZB[num].ToString());
                    model.ZBGJ = PublicTool.IsNumElseToZero(ZBGJ[num].ToString());
                    model.JKCG = PublicTool.IsNumElseToZero(JKCG[num].ToString());
                    model.ZXCB = PublicTool.IsNumElseToZero(ZXCB[num].ToString());
                    model.YYGJ = PublicTool.IsNumElseToZero(YYGJ[num].ToString());
                    model.YBFXZB = PublicTool.IsNumElseToZero(YBFXZB[num].ToString());
                    model.WQDDTZSS = PublicTool.IsNumElseToZero(WQDDTZSS[num].ToString());
                    model.WFPLR = PublicTool.IsNumElseToZero(WFPLR[num].ToString());
                    model.NFPXJGL = PublicTool.IsNumElseToZero(NFPXJGL[num].ToString());
                    model.WBBBZSCE = PublicTool.IsNumElseToZero(WBBBZSCE[num].ToString());
                    model.GSYMGSGDQYHJ = PublicTool.IsNumElseToZero(GSYMGSGDQYHJ[num].ToString());
                    model.SSGDQY = PublicTool.IsNumElseToZero(SSGDQY[num].ToString());
                    model.SYZQY = PublicTool.IsNumElseToZero(SYZQY[num].ToString());
                    model.FZHSYZQY = PublicTool.IsNumElseToZero(FZHSYZQY[num].ToString());

                    if (_StockZCFZInfo.Add(model) == 0)
                    {

                        continue;
                    }


                }
            }

        }
    }
   
}
