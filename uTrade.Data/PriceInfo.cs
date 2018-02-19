using System;
using System.Collections.Generic;
using System.ComponentModel;



namespace uTrade.Data
{
    public enum TradeType
    {
        Stock,
        Fund
    }
    public class PriceInfo
    {
        public string Symbol { get; set; }

        public TradeType PriceType { get; set; }


        public string Name
        {
            get
            {
                var value = "";
                if(this.PriceType == TradeType.Fund)
                {
                    Rank oRank = RankDAL.Instance.GetModel(Symbol);
                    if(null != oRank)
                    {
                        value = oRank.Name;
                    }
                }
                else
                {
                    StockInfoManager oStkInfoMg = new StockInfoManager();
                    StockInfo oStkInfo = oStkInfoMg.GetModel(Symbol);
                    if (null != oStkInfo)
                    {
                        value = oStkInfo.Name;
                    }
                }
                return value;
            }
        }

        public int Favorite
        {
            get
            {
                var value = 0;
                if (this.PriceType == TradeType.Fund)
                {
                    Rank oRank = RankDAL.Instance.GetModel(Symbol);
                    if (null != oRank)
                    {
                        value = oRank.Favorite;
                    }
                }
                else
                {
                    StockInfoManager oStkInfoMg = new StockInfoManager();
                    StockInfo oStkInfo = oStkInfoMg.GetModel(Symbol);
                    if (null != oStkInfo)
                    {
                        value = oStkInfo.Favorite;
                    }
                }
                return value;
            }

            set
            {
                if (this.PriceType == TradeType.Fund)
                {
                    Rank oRank = RankDAL.Instance.GetModel(Symbol);
                    if (null != oRank)
                    {
                        oRank.Favorite = value;
                        RankDAL.Instance.Update(oRank);
                    }
                }
                else
                {
                    StockInfoManager oStkInfoMg = new StockInfoManager();
                    StockInfo oStkInfo = oStkInfoMg.GetModel(Symbol);
                    if (null != oStkInfo)
                    {
                        oStkInfo.Favorite = value;
                        oStkInfoMg.Update(oStkInfo);
                    }
                }
            }
        }

        public List<DayPrice> PriceList { get; set; }

        public PriceInfo(string symbol)
        {
            Symbol = symbol;
            PriceList = new List<DayPrice>();
        }

        public DayPrice LastPrice
        {
            get
            {
                return PriceList[PriceList.Count - 1];
            }
        }

        public double[] getPrice(PriceConstants pConstants)
        {
            double[] dPrice = new double[this.PriceList.Count];
            switch (pConstants)
            {
                case PriceConstants.PRICE_CLOSE:
                    for (int i = 0; i < PriceList.Count; i++)
                    {
                        dPrice[i] = PriceList[i].Close;
                    }
                    break;
                case PriceConstants.PRICE_HIGH:
                    for (int i = 0; i < PriceList.Count; i++)
                    {
                        dPrice[i] = PriceList[i].High;
                    }
                    break;
                case PriceConstants.PRICE_LOW:
                    for (int i = 0; i < PriceList.Count; i++)
                    {
                        dPrice[i] = PriceList[i].Low;
                    }
                    break;
                case PriceConstants.PRICE_OPEN:
                    for (int i = 0; i < PriceList.Count; i++)
                    {
                        dPrice[i] = PriceList[i].Open;
                    }
                    break;
                case PriceConstants.PRICE_VOLUME:
                    for (int i = 0; i < PriceList.Count; i++)
                    {
                        dPrice[i] = PriceList[i].Volume;
                    }
                    break;
                case PriceConstants.PRICE_AMOUNT:
                    for (int i = 0; i < PriceList.Count; i++)
                    {
                        dPrice[i] = PriceList[i].Amount;
                    }
                    break;
            }
            return dPrice;
        }


        public DayPrice getHighestPrice(int start, int end)
        {
            if (end > PriceList.Count - 1)
            {
                end = PriceList.Count - 1;
            }

            double[] dHigh = getPrice(PriceConstants.PRICE_HIGH);

            double highest = dHigh[end];
            int index = end;
            //invert for our assum,on world increase.
            for (int i = end; i >= start; --i)
            {
                if (dHigh[i] > highest)
                {
                    highest = dHigh[i];
                    index = i;
                }
            }
            return PriceList[index];
        }

        public DayPrice getLowestPrice(int start, int end)
        {
            if (end > PriceList.Count - 1)
            {
                end = PriceList.Count - 1;
            }
            double lowest = PriceList[start].Low;
            int index = start;
            for (int i = start; i <= end; ++i)
            {
                if (PriceList[i].Low < lowest)
                {
                    lowest = PriceList[i].Low;
                    index = i;
                }
            }
            return PriceList[index];
        }

        public string FormatPrice(double price)
        {
            int priceInt = (int)price;
            double priceFormated = priceInt / 10000.0;
            return priceFormated.ToString();
        }

        public DayPrice getHighestVolume(int start, int end)
        {
            if (end > PriceList.Count - 1)
            {
                end = PriceList.Count - 1;
            }

            double highest = 0;
            int index = 0;
            int i = end;
            //invert for our assum,on world increase.
            for (; i >= start; --i)
            {
                if (i == end)
                {
                    highest = PriceList[i].Volume;
                }
                else
                {
                    if (PriceList[i].Volume > highest)
                    {
                        highest = PriceList[i].Volume;
                        index = i;
                    }
                }
            }
            return PriceList[index];
        }
    }

    /// <summary>
    /// 股票每日价格
    /// </summary>
    public class DayPrice
    {
        [Category("DayPrice"), Description("Date"), DisplayName("Date")]
        public DateTime Date { get; set; }

        [Category("DayPrice"), Description("Open"), DisplayName("Open")]
        public double Open { get; set; }

        [Category("DayPrice"), Description("Close"), DisplayName("Close")]
        public double Close { get; set; }

        [Category("DayPrice"), Description("High"), DisplayName("High")]
        public double High { get; set; }

        [Category("DayPrice"), Description("Low"), DisplayName("Low")]
        public double Low { get; set; }

        [Category("DayPrice"), Description("Amount"), DisplayName("Amount")]
        public double Amount { get; set; }

        [Category("DayPrice"), Description("Volume"), DisplayName("Volume")]
        public double Volume { get; set; }

        public double AmountBillion
        {
            get
            {
                return Amount / 100000000;
            }
        }

        public int DayOfWeek
        {
            get
            {
                return Convert.ToInt32(Date.DayOfWeek.ToString("d"));
            }
        }

        public static string formatPrice(double price)
        {
            int priceInt = (int)price;
            double priceFormated = priceInt / 100.0;
            return priceFormated.ToString();
        }

        public double Change
        {
            get
            {
                double percent = (Close - Open) / Open;
                return percent;
            }
        }

        public double CloseVOpen
        {
            get
            {
                double percent = (Close - Open) / Open * 100;
                return percent;
            }
        }

        public double HighVOpen
        {
            get
            {
                double percent = (High - Open) / Open * 100;
                return percent;
            }
        }

        public double LowVOpen
        {
            get
            {
                double percent = (Low - Open) / Open * 100;
                return percent;
            }
        }

    }
}
