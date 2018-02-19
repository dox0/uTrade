using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTrade.Common
{
    public class PublicTool
    {
        public static string ListToString(List<string> list)
        {
            string str = "";
            str += list[0];
            for (int x = 1; x < list.Count; x++)
            {
                str += "," + list[x];
            }
            return str;

        }
        public static string IsNumElseToZero(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                byte tmpStr = Convert.ToByte(str[i]);
                if (((tmpStr < 48) || (tmpStr > 57)) && tmpStr != 46)
                {
                    return "0";
                }
            }
            return str;

        }
        public static bool CanDateTime(string time)
        {
            DateTime date;
            //尝试转换
            if (DateTime.TryParse(time, out date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}


