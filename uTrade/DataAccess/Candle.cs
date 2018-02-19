using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using uTrade.Controls;
using uTrade.Common;

namespace uTrade.Data
{
    class Candle
    {
        internal List<DrawObject> GetDrawingObj(PriceInfo pInfo)
        {
            List<DrawObject> lstDrawObj = new List<DrawObject>();
            DrawObject obj = new DrawObject()
            {
                Type = DrawObjectType.CandleLine,
                Name = pInfo.Name + "_candle",
            };

            obj.Vals = pInfo.getPrice(PriceConstants.PRICE_OPEN);
            obj.Vals1 = pInfo.getPrice(PriceConstants.PRICE_CLOSE);
            obj.Vals2 = pInfo.getPrice(PriceConstants.PRICE_HIGH);
            obj.Vals3 = pInfo.getPrice(PriceConstants.PRICE_LOW);


            lstDrawObj.Add(obj);

            DrawObject obj2 = new DrawObject()
            {
                Type = DrawObjectType.Line,
                Name = pInfo.Name + "_vol",
                Thickness = 1,
                Color = Colors.Blue
                
            };
            obj2.Vals = pInfo.getPrice(PriceConstants.PRICE_OPEN);

            lstDrawObj.Add(obj2);

            return lstDrawObj;
        }

        void GetTlttleText(PriceInfo pInfo, int iIndex)
        {
            int index = pInfo.PriceList.Count - 1;
            List<TextBlock> lstTxtBlk = new List<TextBlock>();
            string str = pInfo.Symbol + "  ";
            str += pInfo.Name + "  ";
            str += pInfo.PriceList[index].Date.ToString("yyyy-MM-dd") + "  ";
            //str += DateService.FormatDayOfWeek(priceInfo.PriceList[index].Date) + "  ";
            //str += "开" + priceInfo.PriceList[index].Open + "  ";
            //str += "收" + priceInfo.PriceList[index].Close + "  ";
            //str += "高" + priceInfo.PriceList[index].High + "  ";
            //str += "低" + priceInfo.PriceList[index].Low + "  ";
            //txt += "涨" + CommonUtil.formatPricePercent(((price.Close - price.Open) / price.Open)) + "  ";
            //FormattedText txt = new FormattedText(str,
            //    System.Globalization.CultureInfo.CurrentCulture,
            //    FlowDirection.LeftToRight, new Typeface("Verdana"),
            //    12, new SolidColorBrush(Color.FromRgb(64, 64, 64)));
            //dc.DrawText(txt, new Point(ChartStartX, 1));

        }
    }
}
