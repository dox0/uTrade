/******************** (C) COPYRIGHT 2017 STMicroelectronics ********************
* File Name          : GetFundbyName.cs
* Author             : 3abTPa
* Description        : 交易记录信息
* Version            : V2.1.0RC2
* Date               : 12/13/2017
********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uTrade.Models
{
    public class SelfSelectedModel
    {
        /// <summary>
        /// 股票（基金）代码
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// 股票（基金）名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 买入日期
        /// </summary>
        public DateTime BuyDate { get; set; }
        /// <summary>
        /// 买入价格
        /// </summary>
        public double BuyPrice { get; set; }
        /// <summary>
        /// 买入数量
        /// </summary>
        public double BuyQuant { get; set; }
        /// <summary>
        /// 卖出日期
        /// </summary>
        public DateTime SaleDate { get; set; }
        /// <summary>
        /// 卖出价格
        /// </summary>
        public double SalePrice { get; set; }

        /// <summary>
        /// 当前持有数量，考虑减持
        /// </summary>
        public double CurQuant { get; set; }
        /// <summary>
        /// 当前盈利
        /// </summary>
        public double CurProfit { get; set; }

        public string Rank { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Kind { get; set; }
        /// <summary>
        /// 自定义，保留
        /// </summary>
        public string Custom { get; set; }

    }
}
