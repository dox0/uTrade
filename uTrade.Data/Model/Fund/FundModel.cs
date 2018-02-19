using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uTrade.Data
{
    [Serializable]
    public class FundModel
    {
        public string ErrCode{get;set;}
        public string ErrMsg{get;set;}
        public List<Fund> Datas{get;set;}
    }

    public class Fund
    {
        public string _id { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string STOCKMARKET { get; set; }
    }
}