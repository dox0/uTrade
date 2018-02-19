/*************************** Copyright © uTrade 2017 ***************************
* File Name          : GetFundRankInfo.cs
* Author             : 3abTPa
* Description        : 获取基金排名详细信息
* Version            : V2.1.0RC2
* Date               : 08/13/2017
********************************************************************************/

using System;
using System.Net;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace uTrade.Data
{
    public class GetFundRankInfo
    {
        const string strFundRank = "http://fund.eastmoney.com/data/rankhandler.aspx?op=ph&dt=kf&ft=gp&rs=&gs=0&sc=jzrq&st=desc&sd=btime&ed=etime&qdii=&tabSubtype=,,,,,&pi=pindex&pn=pnum&dx=1&v=0.8868365015098363";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <returns></returns>
        public RankModel GetRankInfo(string pIndex,string pNum )
        {
            RankModel model = new RankModel();
            Rank rankmd = new Rank();

            string etime = DateTime.Now.ToString("yyyy-MM-dd");
            string btime = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");

            string url = strFundRank.Replace("etime", etime).Replace("btime", btime).Replace("pindex", pIndex).Replace("pnum", pNum);
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            Stream resStream = wc.OpenRead(url);
            StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
            string strWebContent = sr.ReadToEnd();
            resStream.Close();
            wc.Dispose();

            Regex regallPages = new Regex("allRecords:[\\d]+");
            Match match2 = regallPages.Match(strWebContent);
            string[] tmp = match2.Groups[0].Value.Split(new string[] { ":"},StringSplitOptions.None);
            model.allRecords = Convert.ToInt16(tmp[1]);

            regallPages = new Regex("pageIndex:[\\d]+");
            match2 = regallPages.Match(strWebContent);
            tmp = match2.Groups[0].Value.Split(new string[] { ":" }, StringSplitOptions.None);
            model.pageIndex = Convert.ToInt16(tmp[1]);

            regallPages = new Regex("pageNum:[\\d]+");
            match2 = regallPages.Match(strWebContent);
            tmp = match2.Groups[0].Value.Split(new string[] { ":" }, StringSplitOptions.None);
            model.pageNum = Convert.ToInt16(tmp[1]);

            regallPages = new Regex("allPages:[\\d]+");
            match2 = regallPages.Match(strWebContent);
            tmp = match2.Groups[0].Value.Split(new string[] { ":" }, StringSplitOptions.None);
            model.allPages = Convert.ToInt16(tmp[1]);

            Regex regHtmlData = new Regex("\"(.*?)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (regHtmlData.IsMatch(strWebContent))
            {
                MatchCollection matchCollection = regHtmlData.Matches(strWebContent);
                foreach (Match match in matchCollection)
                {
                    string value = match.Value;//获取到的
                    value = value.Replace("\"", "");
                    string[] cssstr = value.Split(new string[] { "," }, StringSplitOptions.None);
                    rankmd.Symbol = cssstr[0];
                    rankmd.Name = cssstr[1];
                    rankmd.Shorthand = cssstr[2];
                    rankmd.CurDate = cssstr[3];
                    if (cssstr[4] != "")
                    {
                        rankmd.NetAssetValue = Convert.ToDecimal(cssstr[4]);
                    }
                    if (cssstr[5] != "")
                    {
                        rankmd.AccumulatedNet = Convert.ToDecimal(cssstr[5]);
                    }
                    if (cssstr[6] != "")
                    {
                        rankmd.DailyGrowth = Convert.ToDecimal(cssstr[6]);
                    }
                    if (cssstr[7] != "")
                    {
                        rankmd.LastWeek = Convert.ToDecimal(cssstr[7]);
                    }
                    if (cssstr[8] != "")
                    {
                        rankmd.LastMonth = Convert.ToDecimal(cssstr[8]);
                    }
                    if (cssstr[9] != "")
                    { 
                        rankmd.Last3Month = Convert.ToDecimal(cssstr[9]);
                    }
                    if (cssstr[10] != "")
                    { 
                        rankmd.Last6Month = Convert.ToDecimal(cssstr[10]);
                    }
                    if (cssstr[11] != "")
                    {
                        rankmd.LastYear = Convert.ToDecimal(cssstr[11]);
                    }
                    if (cssstr[12] != "")
                    {
                        rankmd.Last2Year = Convert.ToDecimal(cssstr[12]);
                    }
                    if (cssstr[13] != "")
                    { 
                        rankmd.Last3Year = Convert.ToDecimal(cssstr[13]);
                    }
                    if (cssstr[14] != "")
                    { 
                        rankmd.ThisYear = Convert.ToDecimal(cssstr[14]);
                    }
                    if (cssstr[15] != "")
                    { 
                        rankmd.SinceBuilt = Convert.ToDecimal(cssstr[15]);
                    }
                    rankmd.BuiltDate = cssstr[16];
                    rankmd.Custom = cssstr[17];
                    rankmd.Poundage = cssstr[18];
                    rankmd.PoundageCount = cssstr[19];

                    model.Datas.Add(rankmd);
                    RankDAL.Instance.Add(rankmd);
                }
            }
            return model;
        }



    }
}
