using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTrade.Data
{
    public  class GlobalParams
    {
        public static string _server = "";
        public static string _database = "";
        public static string _uid = "";
        public static string _pwd = "";
        public static string _constr = "server=2013-20141203RO;database=TEST_StockWork;uid=sa;pwd=abc123";
        public GlobalParams()
        {
            server = "";//赋值构造
            database = "";
            uid = "";
            pwd = "";
            constr = "server=2013-20141203RO;database=StockWork;uid=sa;pwd=abc123";

        }

        /// <summary>
        /// 
        /// </summary>
        public string server
        {
            set { _server = value; }
            get { return _server; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string database
        {
            set { _database = value; }
            get { return _database; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        public string constr
        {
            set { _constr = value; }
            get { return _constr; }
        }
    }
}
