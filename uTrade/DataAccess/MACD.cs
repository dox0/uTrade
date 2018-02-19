using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using uTrade.Controls;
using uTrade.Common;


namespace uTrade.Data
{
    class MACD
    {

        [Category("Settings"), Description("Fast Period of the OsMA Indicator"), DisplayName("Slow Period")]
        public int SlowPeriod
        {
            get;
            set;
        }

        [Category("Settings"), Description("Slow Period of the OsMA Indicator"), DisplayName("Signal Period")]
        public int SignalPeriod
        {
            get;
            set;
        }

        [Category("Settings"), Description("Signal Period of the OsMA Indicator"), DisplayName("Fast Period")]
        public int FastPeriod
        {
            get;
            set;
        }

        [Category("Settings"), Description("Price type on witch OsMA will be calculated"), DisplayName("Price Type")]
        public PriceConstants PriceType
        {
            get;
            set;
        }

        public string IndexLabel
        {
            get;
            set;
        }

        public string IndicatorShortName
        {
            get;
            set;
        }


        public MACD()
        {
            this.FastPeriod = 12;
            this.SlowPeriod = 26;
            this.SignalPeriod = 9;
            this.IndexLabel = string.Format("MACD({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod);
            this.IndicatorShortName = string.Format("MACD({0},{1},{2})", this.FastPeriod, this.SlowPeriod, this.SignalPeriod);
            this.PriceType = PriceConstants.PRICE_CLOSE;
        }


        internal List<DrawObject> GetDrawingObj(PriceInfo pInfo)
        {
            List<DrawObject> lstDrawObj = new List<DrawObject>();


            double[] emaFast = MathUtil.CalcEMA(pInfo.getPrice(PriceConstants.PRICE_CLOSE), FastPeriod);
            double[] emaSlow = MathUtil.CalcEMA(pInfo.getPrice(PriceConstants.PRICE_CLOSE), SlowPeriod);
            double[] emaDiff = MathUtil.CalcDiff(emaFast, emaSlow);
            double[] dea = MathUtil.CalcEMA(emaDiff, SignalPeriod);
            double[] macdDiff = MathUtil.CalcDiff(emaDiff, dea);

            DrawObject obj = new DrawObject()
            {
                Type = DrawObjectType.Line,
                Name = pInfo.Name + "_emaFast",
                Thickness = 1,
                Color = Colors.Blue,
                Vals = emaFast
            };
            lstDrawObj.Add(obj);

            DrawObject obj2 = new DrawObject()
            {
                Type = DrawObjectType.Line,
                Name = pInfo.Name + "_emaSlow",
                Thickness = 1,
                Color = Colors.Red,
                Vals = emaSlow
            };
            lstDrawObj.Add(obj2);

            DrawObject obj3 = new DrawObject()
            {
                Type = DrawObjectType.zVLines,
                Name = pInfo.Name + "_macdDiff",
                Thickness = 1,
                Color = Colors.Red,
                Vals = macdDiff
            };
            lstDrawObj.Add(obj3);

            return lstDrawObj;
        }
    }
}
