/*************************** Copyright © uTrade 2017 ***************************
* File Name          : GetFundbyName.cs
* Author             : 3abTPa
* Description        : 通过关键字获取相关的基金列表
* Version            : V2.1.0RC2
* Date               : 08/13/2017
********************************************************************************/
using System;
using System.IO;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;

namespace uTrade.Data
{
    public class GetFundbyName
    {
        string m_strUrl;

        public GetFundbyName()
        {
            m_strUrl = ConfigurationManager.AppSettings["GetFundByName"].ToString();
        }
        /// <summary>
        /// 获取包含某字段的基金
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public FundModel GetFundByName(string strName)
        {
            if (String.IsNullOrEmpty(strName))
            {
                return null;
            }
            string url = m_strUrl.Replace("xkey", strName.Trim());// ConfigurationManager.AppSettings["GetFundByName"].ToString().Replace("xkey", strName.Trim());
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            Stream resStream = wc.OpenRead(url);
            StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
            string SourceCode = sr.ReadToEnd();
            resStream.Close();
            wc.Dispose();
            FundModel model = new FundModel();
            model = JsonConvert.DeserializeObject<FundModel>(SourceCode);
            return model;
        }
    }
}