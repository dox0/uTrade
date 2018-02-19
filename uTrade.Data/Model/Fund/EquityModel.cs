using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uTrade.Data
{
    /// <summary>
    /// 基金净值类
    /// </summary>
    public class EquityModel
    {
        public string date { get; set; }//日期
        public string unitwork { get; set; }//单位净值
        public string allwork { get; set; }//累积净值
        public string rate { get; set; }//增长率
    }

}