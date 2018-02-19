/*************************** Copyright © uTrade 2017 ***************************
* File Name          : GetFundDetailInfo.cs
* Author             : 3abTPa
* Description        : 获取单个基金交易详细信息
* Version            : V2.1.0RC2
* Date               : 08/13/2017
********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace uTrade.Data
{
    public class GetFundDetailInfo
    {
        public class FundDetail
        {
            public int num;
            public string item;
            public string value;
        }

        public List<FundDetail> Datas;

        public List<FundDetail> GetFundDetailList(TradeModel ctrademodel)
        {
            List<FundDetail> cfunddetaillst = new List<FundDetail>();
            FundDetail cfunddetail = new FundDetail();
            int num = 1;

            cfunddetail.num = num++;
            cfunddetail.item = "平均值";
            cfunddetail.value = ctrademodel.avg.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "月末值";
            cfunddetail.value = ctrademodel.later.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "最大值";
            cfunddetail.value = ctrademodel.max.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "最小值";
            cfunddetail.value = ctrademodel.min.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "平均最大值";
            cfunddetail.value = ctrademodel.maxavg.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "平均最小值";
            cfunddetail.value = ctrademodel.minavg.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "跌幅值";
            cfunddetail.value = ctrademodel.diefu.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "波动值";
            cfunddetail.value = ctrademodel.bowave.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "安全期最低值";
            cfunddetail.value = ctrademodel.safelow.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "安全期最高值";
            cfunddetail.value = ctrademodel.safehigh.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "安全期买卖价";
            cfunddetail.value = ctrademodel.safetradecent.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "盈利波动";
            cfunddetail.value = ctrademodel.paywaverate.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "最大盈利值";
            cfunddetail.value = ctrademodel.maxpaycent.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "最大亏损净值";
            cfunddetail.value = ctrademodel.maxlosecent.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "最低买入值";
            cfunddetail.value = ctrademodel.greatbuy.ToString();
            cfunddetaillst.Add(cfunddetail);

            cfunddetail.num = num++;
            cfunddetail.item = "最高卖出值";
            cfunddetail.value = ctrademodel.greatsale.ToString();
            cfunddetaillst.Add(cfunddetail);
            
            return cfunddetaillst;
        }


        #region 交易值计算
        /// <summary>
        /// 计算平均值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private decimal GetAvgNum(List<string> s)
        {
            decimal start = decimal.Parse("0.0000");
            foreach (string num in s)
            {
                string num2 = num.Replace("%", "");
                decimal i = Convert.ToDecimal(num2);
                start = start + i;
            }
            decimal avgold = start / s.Count();
            decimal avg = decimal.Round(avgold, 4);
            return avg;
        }

        /// <summary>
        /// 获取月末值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private decimal GetMonthLater(List<string> s)
        {
            decimal later = decimal.Round(Convert.ToDecimal(s[0]), 4);
            return later;
        }

        /// <summary>
        /// 获取最高值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private decimal GetMax(List<string> s)
        {
            decimal start = decimal.Parse("0.0000");
            foreach (string num in s)
            {
                decimal i = Convert.ToDecimal(num);
                if (i > start)
                {
                    start = i;
                }
            }
            decimal newstart = decimal.Round(start, 4);
            return newstart;
        }
        /// <summary>
        /// 获取最低值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private decimal GetMin(List<string> s)
        {
            decimal start = decimal.Parse(s[0]);
            foreach (string num in s)
            {
                decimal i = Convert.ToDecimal(num);
                if (i < start)
                {
                    start = i;
                }
            }
            decimal newstart = decimal.Round(start, 4);
            return newstart;
        }

        /// <summary>
        /// 获取平均最高价
        /// </summary>
        /// <param name="s"></param>
        /// <param name="avg"></param>
        /// <returns></returns>
        private decimal GetAvgMax(List<string> s, string avg)
        {
            decimal avgd = Convert.ToDecimal(avg);
            List<String> maxlist = new List<string>();
            foreach (string str in s)
            {
                decimal ds = Convert.ToDecimal(str);
                if (ds > avgd)
                {
                    maxlist.Add(ds.ToString());
                }
            }

            decimal avgmax = GetAvgNum(maxlist);
            return avgmax;
        }

        /// <summary>
        /// 获取平均最低价
        /// </summary>
        /// <param name="s"></param>
        /// <param name="avg"></param>
        /// <returns></returns>
        private decimal GetAvgMin(List<string> s, string avg)
        {
            decimal avgd = Convert.ToDecimal(avg);
            List<String> minlist = new List<string>();
            foreach (string str in s)
            {
                decimal ds = Convert.ToDecimal(str);
                if (ds < avgd)
                {
                    minlist.Add(ds.ToString());
                }
            }

            decimal avgmin = GetAvgNum(minlist);
            return avgmin;
        }

        /// <summary>
        /// 跌幅值（平均值-平均最低价）
        /// </summary>
        /// <param name="avg">平均值</param>
        /// <param name="avgmin">平均最低价</param>
        /// <returns></returns>
        private decimal GetLow(string avg, string avgmin)
        {
            return Convert.ToDecimal(avg) - Convert.ToDecimal(avgmin);
        }

        /// <summary>
        /// 涨幅值（平均最高价-平均值）
        /// </summary>
        /// <param name="avg">平均值</param>
        /// <param name="avgmax">平均最高价</param>
        /// <returns></returns>
        private decimal GetHigh(string avg, string avgmax)
        {
            return Convert.ToDecimal(avgmax) - Convert.ToDecimal(avg);
        }

        /// <summary>
        /// 波动值（涨幅值+跌幅值）
        /// </summary>
        /// <param name="lowfu">跌幅值</param>
        /// <param name="highfu">涨幅值</param>
        /// <returns></returns>
        private decimal GetWave(string lowfu, string highfu)
        {
            return Convert.ToDecimal(lowfu) + Convert.ToDecimal(highfu);
        }

        /// <summary>
        /// 安全期最低值（平均最低价-波动值）
        /// </summary>
        /// <param name="a">平均最低价</param>
        /// <param name="b">波动值</param>
        /// <returns></returns>
        private decimal GetSafeLow(string a, string b)
        {
            return Convert.ToDecimal(a) - Convert.ToDecimal(b);
        }
        /// <summary>
        /// 安全期最高值（平均最高价-波动值）
        /// </summary>
        /// <param name="a">平均最高价</param>
        /// <param name="b">波动值</param>
        /// <returns></returns>
        private decimal GetSafeHigh(string a, string b)
        {
            return Convert.ToDecimal(a) - Convert.ToDecimal(b);
        }

        /// <summary>
        /// 安全买卖价（平均价-波动值）
        /// </summary>
        /// <param name="a">平均价</param>
        /// <param name="b">波动值</param>
        /// <returns></returns>
        private decimal GetSafeTradeCent(string a, string b)
        {
            return Convert.ToDecimal(a) - Convert.ToDecimal(b);
        }

        /// <summary>
        /// 盈利波动(波动值/安全买卖价)
        /// </summary>
        /// <param name="a">波动值</param>
        /// <param name="b">安全买卖价</param>
        /// <returns></returns>
        private decimal GetPayWaveRate(string a, string b)
        {
            return decimal.Round(Convert.ToDecimal(a) / Convert.ToDecimal(b), 4);
        }

        /// <summary>
        /// 最大盈利值（安全买卖价+盈利波动）
        /// </summary>
        /// <param name="a">安全买卖价</param>
        /// <param name="b">盈利波动</param>
        /// <returns></returns>
        private decimal GetMaxPayCent(string a, string b)
        {
            return Convert.ToDecimal(a) + Convert.ToDecimal(b);
        }
        /// <summary>
        /// 最大亏损净值（安全买卖价-盈利波动）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private decimal GetMaxLoseCent(string a, string b)
        {
            return Convert.ToDecimal(a) - Convert.ToDecimal(b);
        }

        /// <summary>
        /// 最低买入值
        /// </summary>
        /// <param name="a">平均值</param>
        /// <param name="b">月末值</param>
        /// <param name="c">最小值</param>
        /// <returns></returns>
        private decimal GetGreatBuy(string a, string b, string c)
        {
            decimal avg = Convert.ToDecimal(a);
            decimal later = Convert.ToDecimal(b);
            decimal min = Convert.ToDecimal(c);
            if (later < avg)
            {
                return decimal.Round(min * (decimal)0.98, 4);
            }
            else
            {
                return min;
            }
        }

        /// <summary>
        /// 最高卖出值
        /// </summary>
        /// <param name="a">平均值</param>
        /// <param name="b">月末值</param>
        /// <param name="c">最大值</param>
        /// <returns></returns>
        private decimal GetGreatSale(string a, string b, string c)
        {
            decimal avg = Convert.ToDecimal(a);
            decimal later = Convert.ToDecimal(b);
            decimal max = Convert.ToDecimal(c);
            if (later < avg)
            {
                return max;
            }
            else
            {
                return decimal.Round(max * (decimal)1.02, 4);
            }
        }
#endregion

        public TradeModel GetGuessInfo(List<string> listUnitwork)
        {
            TradeModel model = new TradeModel();
            model.avg = GetAvgNum(listUnitwork).ToString();//平均值
            model.later = GetMonthLater(listUnitwork).ToString();//月末值
            model.max = GetMax(listUnitwork).ToString();//最大值
            model.min = GetMin(listUnitwork).ToString();//最小值
            model.maxavg = GetAvgMax(listUnitwork, model.avg).ToString();//平均最大值
            model.minavg = GetAvgMin(listUnitwork, model.avg).ToString();//平均最小值
            model.diefu = GetLow(model.avg, model.minavg).ToString();//跌幅值
            model.zhangfu = GetHigh(model.avg, model.maxavg).ToString();//涨幅值
            model.bowave = GetWave(model.diefu, model.zhangfu).ToString();//波动值
            model.safelow = GetSafeLow(model.minavg, model.bowave).ToString();//安全期最低值
            model.safehigh = GetSafeHigh(model.maxavg, model.bowave).ToString();//安全期最高值
            model.safetradecent = GetSafeTradeCent(model.avg, model.bowave).ToString();//安全期买卖价
            model.paywaverate = GetPayWaveRate(model.bowave, model.safetradecent).ToString();//盈利波动
            model.maxpaycent = GetMaxPayCent(model.safetradecent, model.paywaverate).ToString();//最大盈利值
            model.maxlosecent = GetMaxLoseCent(model.safetradecent, model.paywaverate).ToString();//最大亏损净值
            model.greatbuy = GetGreatBuy(model.avg, model.later, model.min).ToString();//最低买入值
            model.greatsale = GetGreatSale(model.avg, model.later, model.max).ToString();//最高卖出值

            Datas = GetFundDetailList(model);

            return model;
        }
    }
}
