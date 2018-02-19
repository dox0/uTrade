//计算协方差：http://blog.csdn.net/luckisok/article/details/1605202


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uTrade.Common
{
    //
    public class MathUtil
    {
        public static double GetHighest(double[] items, int start, int end)
        {
            if (start < 0)
            {
                start = 0;
            }
            if (end > items.Length - 1)
            {
                end = items.Length - 1;
            }

            if (end < start)
            {
                return 0;
            }

            double highest = 0;
            int index = 0;
            //invert for our assum,on world increase.
            for (int i = end; i >= start; --i)
            {
                if (i == end)
                {
                    highest = items[i];
                }
                else
                {
                    if (items[i] > highest)
                    {
                        highest = items[i];
                        index = i;
                    }
                }

            }
            return highest;
        }


        public static double GetLowest(double[] items, int start, int end)
        {

            if (start < 0)
            {
                start = 0;
            }
            if (end > items.Length - 1)
            {
                end = items.Length - 1;
            }
            if (end < start)
            {
                return 0;
            }

            double lowest = 0;
            for (int i = start; i <= end; ++i)
            {
                if (i == start)
                {
                    lowest = items[i];
                }
                else
                {
                    if (items[i] < lowest)
                    {
                        lowest = items[i];
                    }
                }
            }
            return lowest;
        }

        //求协方差
        public static double Covar(double[] datax, double[] datay)
        {
            if(datax.Length != datay.Length)
            {
                return double.NaN;
            }
            double averx = datax.Average();
            double avery = datay.Average();
            double sum = 0;

            for (int i = 0; i < datax.Length; i++)
            {
                sum += (datax[i] - averx) * (datay[i] - avery);
            }

            return sum / (datax.Length - 1);
        }

        /// <summary>
        /// fangcha
        /// </summary>
        /// <param name="a"></param>
        public static double Variance(double[] data)
        {
            double sum = 0;
            double avrg = data.Average();
            foreach (float x in data)
            {
                sum += (x - avrg) * (x - avrg);
            }
            return sum / data.Length;
        }

        public static double[] CalcDiff(double[] inputs0, double[] inputs1)
        {
            if (inputs0 == null || inputs1 == null)
            {
                return null;
            }

            var len = inputs0.Length;
            var outputs = new double[inputs0.Length];

            for (var i = 0; i < len; i++)
            {
                outputs[i] = inputs0[i] - inputs1[i];
            }

            return outputs;
        }

        //values are day price.
        public static double[] CalcEMA(double[] values, double days)
        {
            if (values == null)
            {
                return null;
            }

            var len = values.Length;
            var outputs = new double[len];
            var lastValue = values[0];

            for (var i = 0; i < len; ++i)
            {
                double K = 2.0 / (days + 1);
                outputs[i] = values[i] * K + lastValue * (1 - K);
                lastValue = outputs[i];
            }
            return outputs;
        }


       
        public static double[] CalcRSV(int days, double[] close, double[] high, double[] low)
        {
            if (close == null)
            {
                return null;
            }

            int cnt = close.Length;
            var rsvOut = new double[cnt];

            for (int i = 0; i < cnt; i++)
            {
                double highest = GetHighest(high, i, i + days);
                double lowest = GetLowest(low, i, i + days);
                rsvOut[i] = (close[i] - lowest) / (highest - lowest) * 100;
            }

            //RSV(n)=(Close(n)-Lowest(n))/((Highest(n)-Lowest(n))*100;
            return rsvOut;
        }


        internal static void CalcKDJ(int days, double[] closes, double[] highs, double[] lows, out double[] kArray, out double[] dArray, out double[] jArray)
        {
            if (closes == null)
            {
                kArray = null;
                dArray = null;
                jArray = null;
                return;
            }

            var rsvArray = CalcRSV(days, closes, highs, lows);
            // calcKDJ_K
            //K(n) = K(n-1)*2/3 + RSV(n)/3;
            //D(n)=D(n-1)*2/3+K(n)/3;
            //J(n)=K(n)*3-D(n)*2;
            //k=d=50(n=0)
            int cnt = rsvArray.Length;

            if (cnt == 0)
            {
                kArray = null;
                dArray = null;
                jArray = null;
                return;
            }

            kArray = new double[cnt];
            dArray = new double[cnt];
            jArray = new double[cnt];

            kArray[0] = 50;
            dArray[0] = 50;
            jArray[0] = 50;
            for (int i = 1; i < cnt; i++)
            {
                kArray[i] = kArray[i - 1] * 2.0 / 3.0 + rsvArray[i] / 3.0;
                dArray[i] = dArray[i - 1] * 2.0 / 3.0 + kArray[i] / 3.0;
                jArray[i] = kArray[i] * 3.0 - dArray[i] * 2;
            }

        }
    }
}
