/*************************** Copyright © uTrade 2017 ***************************
 * File Name          : GetFundInfo.cs
 * Author             : 3abTPa
 * Description        : 获取基金净值相关信息
 * Version            : V2.1.0RC2
 * Date               : 08/13/2017
 * Reference          : 
 *                     (1)解析HTML table: https://stackoverflow.com/questions/10513529/getting-data-from-html-table-into-a-datatable
 *                     (2)解析table http://www.cnblogs.com/guwei4037/p/4720182.html
********************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;
using HtmlAgilityPack;
using uTrade.Data;


namespace uTrade.Data
{
    public class GetFundEquityInfo
    {

        static GetFundEquityInfo inst = null;
        public static GetFundEquityInfo Instance
        {
            get
            {
                if (inst == null)
                {
                    inst = new GetFundEquityInfo();
                }
                return inst;
            }
        }

        static string m_strUrl;

        private GetFundEquityInfo()
        {
            m_strUrl = ConfigurationManager.AppSettings["GetEquityUrl"].ToString();
        }

        /// <summary>
        /// 获取上月净值相关源数据
        /// </summary>
        /// <param name="symbol"></param>
        /// time格式 yyyy-MM-dd
        /// <returns></returns>
        public string GetEquityInfo(string symbol, string btime, string etime)
        {
            string url = m_strUrl.Replace("xsymbol", symbol.Trim()).Replace("btime", btime.Trim()).Replace("etime", etime.Trim());//ConfigurationManager.AppSettings["GetEquityUrl"].ToString().Replace("xsymbol", symbol.Trim()).Replace("btime", btime.Trim()).Replace("etime", etime.Trim());
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            Stream resStream = wc.OpenRead(url);
            StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
            string SourceCode = sr.ReadToEnd();
            resStream.Close();
            wc.Dispose();
            return SourceCode;
        }


        /// <summary>
        /// 通过HtmlAgilityPack解析获取到的净值信息
        /// </summary>
        /// <param name="strWebContent"></param>
        /// <returns></returns>
        public List<EquityModel> DealEquityInfo(string strWebContent)
        {
            List<EquityModel> modellist = new List<EquityModel>();//定义1个列表用于保存结果
            string strHtmlData = strWebContent;

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(strHtmlData);//加载HTML字符串，如果是文件可以用htmlDocument.Load方法加载

            HtmlNodeCollection collection2 = htmlDocument.DocumentNode.SelectSingleNode("//table/tbody").ChildNodes;//跟Xpath一样，轻松的定位到相应节点下
            foreach (HtmlNode table in collection2)
            {
                List<string> listNode= new List<string>();
                foreach (HtmlAgilityPack.HtmlNode td in table.ChildNodes)
                {
                    listNode.Add(td.InnerText);
                }
                EquityModel netWork = new EquityModel();
                netWork.date = listNode[0];
                netWork.unitwork = listNode[1];
                netWork.allwork = listNode[2];
                netWork.rate = listNode[3];
                modellist.Add(netWork);
            }
            return modellist;
        }

        public List<EquityModel> Info(string symbol, DateTime tmBegin, DateTime tmEnd)
        {
            //string btime = date.FirstDayOfPreviousMonth(DateTime.Now).ToString("yyyy-MM-dd");
            string btime = tmBegin.ToString("yyyy-MM-dd");
            string etime = tmEnd.ToString("yyyy-MM-dd");
            List<EquityModel> modellist = new List<EquityModel>();
            string s = GetEquityInfo(symbol, btime, etime);
            modellist = DealEquityInfo(s);
            return modellist;
        }

        public EquityModel GetFormatedFundInfo(string symbol, DateTime date)
        {
            string btime = date.ToString("yyyy-MM-dd");
            List<EquityModel> modellist = new List<EquityModel>();
            string s = GetEquityInfo(symbol, btime, btime);
            modellist = DealEquityInfo(s);

            if(modellist != null)
            {
                return modellist[0];
            }
            else
            {
                return null;
            }
        }

        public PriceInfo GetFormatedFundInfo(string symbol, DateTime tmBegin, DateTime tmEnd)
        {
            List<EquityModel> modellist = new List<EquityModel>();
            PriceInfo pInfo = new PriceInfo(symbol);
            pInfo.PriceType = TradeType.Fund;
            modellist = Info(symbol, tmBegin, tmEnd);
            foreach(EquityModel eModel in modellist)
            {
                DayPrice dPrice = new DayPrice();
                dPrice.Close = Convert.ToDouble(eModel.unitwork) * 10000;
                dPrice.Open = Convert.ToDouble(eModel.unitwork) * 10000;
                dPrice.High = Convert.ToDouble(eModel.unitwork) * 10000;
                dPrice.Low = Convert.ToDouble(eModel.unitwork) * 10000;
                dPrice.Date= DateTime.ParseExact(eModel.date, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                pInfo.PriceList.Add(dPrice);
            }
            pInfo.PriceList.Reverse();
            return pInfo;
        }

        public List<string> InfoEqLst(List<EquityModel> lEqLst)
        {
            List<string> lInfoEq = new List<string>();
            foreach(EquityModel eq in lEqLst)
            {
                lInfoEq.Add(eq.allwork);
            }
            return lInfoEq;
        }

        public List<Rank> GetFavoriteRankList()
        {
            DataSet dsRank = RankDAL.Instance.GetFavoriateList();

            return RankDAL.Instance.DataTableToList(dsRank.Tables[0]);
        }

        public List<Rank> GetAllRankList()
        {
            DataSet dsRank = RankDAL.Instance.GetList("");

            return RankDAL.Instance.DataTableToList(dsRank.Tables[0]);
        }
    }
}