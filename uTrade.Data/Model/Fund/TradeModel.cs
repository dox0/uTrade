using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uTrade.Data
{
    public class TradeModel
    {

        public string _avg;//平均值
        public string avg
        {
            get { return _avg; }
            set { _avg = value; }
        }
        public string _later;//月末值
        public string later
        {
            get { return _later; }
            set { _later = value; }
        }
        public string _max;//最大值
        public string max
        {
            get { return _max; }
            set { _max = value; }
        }
        public string _min;//最小值
        public string min
        {
            get { return _min; }
            set { _min =  value; }
        }
        public string _maxavg;//平均最大值
        public string maxavg
        {
            get { return _maxavg; }
            set { _maxavg = value; }
        }
        public string _minavg;//平均最小值
        public string minavg
        {
            get { return _minavg; }
            set { _minavg =  value; }
        }
        public string _diefu;//跌幅值
        public string diefu
        {
            get { return _diefu; }
            set { _diefu = value; }
        }
        public string _zhangfu;//涨幅值
        public string zhangfu
        {
            get { return _zhangfu; }
            set { _zhangfu = value; }
        }
        public string _bowave;//波动值
        public string bowave
        {
            get { return _bowave; }
            set { _bowave = value; }
        }
        public string _safelow;//安全期最低值
        public string safelow
        {
            get { return _safelow; }
            set { _safelow = value; }
        }
        public string _safehigh;//安全期最高值
        public string safehigh
        {
            get { return _safehigh; }
            set { _safehigh =value; }
        }
        public string _safetradecent;//安全期买卖价
        public string safetradecent
        {
            get { return _safetradecent; }
            set { _safetradecent =  value; }
        }
        public string _paywaverate;//盈利波动
        public string paywaverate
        {
            get { return _paywaverate; }
            set { _paywaverate =  value; }
        }
        public string _maxpaycent;//最大盈利值
        public string maxpaycent
        {
            get { return _maxpaycent; }
            set { _maxpaycent =  value; }
        }
        public string _maxlosecent;//最大亏损净值
        public string maxlosecent
        {
            get { return _maxlosecent; }
            set { _maxlosecent =  value; }
        }

        public string _greatbuy;
        public string greatbuy
        {
            get { return _greatbuy; }
            set { _greatbuy =  value; }
        }


        public string _greatsale;
        public string greatsale
        {
            get { return _greatsale; }
            set { _greatsale =  value; }
        }
       
    }
}