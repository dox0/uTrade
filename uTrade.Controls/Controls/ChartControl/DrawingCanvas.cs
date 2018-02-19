using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using uTrade.Data;
using uTrade.Common;
using uTrade.Charting;
using System.Windows.Controls;

namespace uTrade.Controls
{
    public partial class ChartPanel : DrawingRegion
    {
        public DrawingObject DrawingObjects = new DrawingObject();

        public int ScaleLineCount = 0;
        public bool IsShowTime = true;

        PriceInfo priceInfo = null;
        IndicatorType eIndType;

        //背景的矩形
        Rect rect = new Rect();

        // 数据条数，一只股票从成立到现在的天数
        Pen blackPen = new Pen(new SolidColorBrush(Color.FromRgb(128, 128, 128)), 1);

        Pen LineStyle;

        public int TotalItemCount = 0;
        //K线宽度
        public int itemWidth = 6;
        //K线之间的间隙
        public double itemSpace = 2;
        double maxChartOffset = 0;
        public double chartOffset = 0;
        //可显示区域的第一个在整体中的index
        int drawItemStartIndex = 0;
        //当前可视区域的可显示条目个数
        int drawItemCount = 0;

        public delegate void PriceChangedEventHandler(DayPrice p);
        public event PriceChangedEventHandler OnPriceChanged;

        public delegate void RegionChangedHandler();
        public event RegionChangedHandler OnRegionChanged;

        public ChartPanel()
        {         
            load(priceInfo);//初始化界面，载入数据
        }

        //初始化布局，计算布局坐标
        void InitLayout()
        {
            //Draw a Rectangle
            this.rect.Width = totalWidth;
            this.rect.Height = totalHeight;

            if(IsShowTime)
            {
                this.BottomFixWidth = 20;
            }

            //确定偏移量
            maxChartOffset = GetMaxOffset();
            if (chartOffset > maxChartOffset)
            {
                chartOffset = maxChartOffset;
            }
            if (chartOffset < 0)
            {
                chartOffset = 0;
            }
        }

        public void load( PriceInfo pInfo, IndicatorType eindtype = IndicatorType.Candle)
        {
            eIndType = eindtype;
            Color penColor = Color.FromRgb(0, 0, 0);

            LineStyle = new Pen();
            LineStyle.Brush = new SolidColorBrush(penColor);
            LineStyle.Thickness = 1;
            LineStyle.DashStyle = DashStyles.Dot;

            this.DrawingObjects.Clear();
            priceInfo = pInfo;

            if (this.priceInfo == null)
            {
                return ;
            }
            //记录条数
            this.TotalItemCount = this.priceInfo.PriceList.Count;
      
            chartOffset = GetMaxOffset();
            maxChartOffset = chartOffset;

            this.InvalidateVisual();
        }

        //显示
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            InitLayout();

            if (!InitDrawRegion())
            {
                return;
            }

            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                if(InChart())
                {
                    DrawCrossLine(dc, MousePos);
                }
            }

            //绘制K线Grid
            DrawGrid(dc, ChartStartX, ChartStartY, ChartWidth, ChartHeight, ScaleLineCount);

            //绘制中框和日期
            if (IsShowTime)
            {
                DrawDateTxt(dc, ChartStartX, ChartStartY + ChartHeight, ChartWidth, 16, ScaleLineCount);
            }

            if (eIndType == IndicatorType.Time)
            {
                //绘制时间轴
                DrawTimeLine(dc, ChartStartX, ChartStartY, ChartWidth, ChartHeight);
            }
            else
            {
                //绘制K线图
                DrawObjects(dc);

                DrawValueTxt(dc, ChartStartX, ChartStartY, ChartWidth, ChartHeight, ScaleLineCount);
            }

            if (OnRegionChanged != null)
            {
                OnRegionChanged();
            }
        }

        /// <summary>
        /// 绘制X轴日期
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="lineCount"></param>
        void DrawDateTxt(DrawingContext dc, double left, double top, double width, double height, int lineCount)
        {
            if (priceInfo == null || TotalItemCount == 0)
            {
                return;
            }
            var startIndex = (int)(chartOffset / (itemWidth + itemSpace));
            var cnt = (int)(width / (itemWidth + itemSpace));
            var itemOffset = (int)(width / (itemWidth + itemSpace) / (lineCount + 1));

            for (var i = 0; i < lineCount + 2; i++)
            {
                if (i * itemOffset + startIndex >= TotalItemCount)
                {
                    break;
                }
                var xoffset = (int)(i * width / (lineCount + 1)) + left + 0.5 - 45;

                DateTime dt = priceInfo.PriceList[i * itemOffset + startIndex].Date;
                var str = dt.ToString("yyyy/MM/dd");
                FormattedText txt = new FormattedText(str,
                  System.Globalization.CultureInfo.CurrentCulture,
                  FlowDirection.LeftToRight, new Typeface("Verdana"),
                  12, new SolidColorBrush(Color.FromRgb(64, 64, 64)));
                dc.DrawText(txt, new Point(xoffset, top));
            }
        }

        /// <summary>
        /// 绘制Y轴的价格列表
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="lineCount"></param>
        void DrawValueTxt(DrawingContext dc, double left, double top, double width, double height, int lineCount)
        {
            for (var i = 0; i < lineCount + 2; i++)
            {
                var yPos = (int)(top) - 13 + i * height / (lineCount + 1);

                double price = HighestPrice - (HighestPrice - LowestPrice) / (lineCount + 1) * i;

                var str = priceInfo.FormatPrice(price);
                FormattedText txt = new FormattedText(str,
                  System.Globalization.CultureInfo.CurrentCulture,
                  FlowDirection.LeftToRight, new Typeface("Verdana"),
                  12, new SolidColorBrush(Color.FromRgb(64, 64, 64)));
                dc.DrawText(txt, new Point(left - 43, yPos));
            }
        }

        void DrawCrossLine(DrawingContext dc, Point point)
        {
            var xoffset = (int)(point.X) + 0.5;
            var yoffset = (int)(point.Y) + 0.5;

            if (yoffset < base.ChartEndY)
            {
                //vertical
                dc.DrawLine(LineStyle, new Point(xoffset, base.ChartStartY), new Point(xoffset, base.ChartEndY));
                //horizontal
                dc.DrawLine(LineStyle, new Point(base.ChartStartX, yoffset), new Point(base.ChartEndX, yoffset));
            }
        }


        //绘制线框，用途在于可视化等分图表，即上部表格，lineCount从0开始
        void DrawGrid(DrawingContext dc, double left, double top, double width, double height, double lineCount)
        {
            //绘制竖线.
            for (double x = 0; x < width + 1; x += width / (lineCount + 1))
            {
                double xPos = (int)(left + x) + 0.5;
                dc.DrawLine(blackPen, new Point(xPos, top), new Point(xPos, height + top));
            }

            //绘制横线.
            for (double y = 0; y < height + 1; y += height / (lineCount + 1))
            {
                var yPos = (int)(top + y) + 0.5;
                dc.DrawLine(blackPen, new Point(left, yPos), new Point(width + left, yPos));
            }
        }

        /// <summary>
        /// 绘制下方时间轴
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void DrawTimeLine(DrawingContext dc, double left, double top, double width, double height)
        {
            //每条记录的宽度
            var itemOffset = this.ChartWidth / TotalItemCount;

            //draw time line
            var yearLast = 0;
            for (var i = 0; i < TotalItemCount; i++)
            {
                var xoffset = (int)(i * itemOffset) + left + 0.5;
                var year = priceInfo.PriceList[i].Date.Year;//(int)(priceInfo.PriceList[i].Date / 10000);
                if (year > yearLast)
                {
                    //格式文本，指定位置
                    FormattedText txt = new FormattedText(year.ToString(),
                     System.Globalization.CultureInfo.CurrentCulture,
                     FlowDirection.LeftToRight, new Typeface("Verdana"),
                     12, new SolidColorBrush(Color.FromRgb(64, 64, 64)));
                    //绘制文本
                    dc.DrawText(txt, new Point(xoffset, top + height - 16));
                }
                yearLast = year;
            }

            //draw curve line
            var highest = priceInfo.getHighestPrice(0, TotalItemCount).High;
            var lowest = priceInfo.getLowestPrice(0, TotalItemCount).Low;

            var pixelcount = 3;
            var inc = (int)(TotalItemCount * pixelcount / this.ChartWidth);
            if (inc == 0)
            {
                inc = 1;
            }

            //var color = "#30b8f3";
            var pen = getPen(48, 184, 243, 1);
            //draw line curve.
            PathFigure pf = new PathFigure();
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);

            for (var i = 0; i < TotalItemCount; i += inc)
            {
                var xoffset = (int)(i * itemOffset) + 0.5 + left;
                var yClose = (int)((highest - priceInfo.PriceList[i].Close) / (highest - lowest) * (height - 12)) + 0.5 + top;

                if (i == 0)
                {
                    pf.StartPoint = new Point(xoffset, yClose);
                }
                else
                {
                    pf.Segments.Add(new LineSegment(new Point(xoffset, yClose), true));
                }
            }

            dc.DrawGeometry(Brushes.Transparent, pen, pg);

            //draw item time region button
            double x = (int)(drawItemStartIndex * itemOffset) + left + 0.5;
            var xwidth = (int)(drawItemCount * itemOffset);

            var brush = getBrush(224, 224, 255, 128);
            pen = getPen(224, 224, 255, 1, 128);
            dc.DrawRectangle(brush, pen, new Rect(x, top, xwidth, height));
        }

        //可显示区域的前面还有多长
        double GetMaxOffset()
        {
            //可显示的K线的数量
            var cnt = this.ChartWidth / (itemWidth + itemSpace);

            //整个中间部分的宽度（不可见部分）即显示的第一个记录在总记录中的位置（换算成长度）
            var offset = (TotalItemCount - cnt) * (itemWidth + itemSpace);
            if (offset < 0)   //总记录数不满一屏
            {
                offset = 0;
            }
            return offset + 6;//末尾不贴到边界
        }

        //计算可视区域的始末
        bool InitDrawRegion()
        {
            //显示的记录个数和开始序号
            drawItemCount = (int)(ChartWidth / (itemWidth + itemSpace));
            drawItemStartIndex = (int)(chartOffset / (itemWidth + itemSpace));
            if (drawItemStartIndex >= TotalItemCount)
            {
                return false;
            }
            //微调显示的个数，确保能显示完全
            if (drawItemStartIndex + drawItemCount > TotalItemCount)
            {
                drawItemCount = TotalItemCount - drawItemStartIndex - 1;
            }
            //确定纵坐标范围
            HighestPrice = this.priceInfo.getHighestPrice(drawItemStartIndex, drawItemStartIndex + drawItemCount).High;
            LowestPrice = this.priceInfo.getLowestPrice(drawItemStartIndex, drawItemStartIndex + drawItemCount).Low;
            HighestVolume = priceInfo.getHighestVolume(drawItemStartIndex, drawItemStartIndex + drawItemCount).Volume;
            return true;
        }

        //定义笔
        public Pen getPen(byte r, byte g, byte b, double thickness, byte a = 255)
        {
            Pen pen = new Pen();
            Color penColor = Color.FromArgb(a, r, g, b);
            pen.Brush = new SolidColorBrush(penColor);
            pen.Thickness = thickness;
            return pen;
        }

        public Brush getBrush(byte r, byte g, byte b, byte a = 255)
        {
            var brush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            return brush;
        }


        /// <summary>
        /// 鼠标按住移动事件，移动水平位置，计算chartOffset
        /// </summary>
        /// <param name="e"></param>
        public void mouseMove(MouseEventArgs e)
        {
            var pos = e.GetPosition(this);

            MousePos = pos;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (OnRegionChanged != null)
                {
                    OnRegionChanged();
                }
                this.InvalidateVisual();
            }

            if (chartOffset < 0)
            {
                chartOffset = 0;
            }
            maxChartOffset = GetMaxOffset();
            if (chartOffset > maxChartOffset)
            {
                chartOffset = maxChartOffset;
            }

            //在时间区里，直接返回
            if (eIndType == IndicatorType.Time)
            {
                return;
            }

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                var itemIndex = (int)((chartOffset + MousePos.X) / (itemWidth + itemSpace));
                if (itemIndex < TotalItemCount)
                {
                    var price = priceInfo.PriceList[itemIndex];//priceInfo.getPrice(itemIndex);
                    //tell listener that price item changed.
                    if (OnPriceChanged != null)
                    {
                        OnPriceChanged(price);
                    }
                }
            }
        }


        public void mouseDown(MouseButtonEventArgs e)
        {
            maxChartOffset = GetMaxOffset();
            if (chartOffset > maxChartOffset)
            {
                chartOffset = maxChartOffset;
            }
            if (chartOffset < 0)
            {
                chartOffset = 0;
            }
            this.InvalidateVisual();
        }

        //准备废弃
        //internal double getPriceYPercent(MouseEventArgs e)
        //{
            //double width = this.ActualWidth;
            //double height = this.ActualHeight;
            //double y = e.GetPosition(this).Y;
            //double price = base.HighestPrice - y / this.ChartHeight * (base.HighestPrice - base.LowestPrice);
            //double avg = (base.HighestPrice + base.LowestPrice) / 2.0;
            //double percent = (price - avg) / avg;
            //return percent;
        //}

        //绘制指标
        private void DrawObjects(DrawingContext dc)
        {
            //均线
            DrawObjectGroup(dc, DrawObjectType.Line);
            //MADC曲线
            DrawObjectGroup(dc, DrawObjectType.zVLines);
            //蜡烛图
            DrawObjectGroup(dc, DrawObjectType.CandleLine);
            //成交量垂线
            DrawObjectGroup(dc, DrawObjectType.vLines);
        }

        private void DrawObjectGroup(DrawingContext dc, DrawObjectType type)
        {
            double highest = 0;
            double lowest = double.MaxValue;

            foreach (var i in DrawingObjects.All())
            {
                var obj = i.Value;
                if (obj.Type != type)
                {
                    continue;
                }
                double h = 0;
                double l = 0;
                //candle line.
                if (obj.Type == DrawObjectType.CandleLine)
                {
                    h = MathUtil.GetHighest(obj.Vals2, drawItemStartIndex, drawItemStartIndex + drawItemCount);
                    if (h > highest)
                    {
                        highest = h;
                    }
                    l = MathUtil.GetLowest(obj.Vals3, drawItemStartIndex, drawItemStartIndex + drawItemCount);
                    if (l < lowest)
                    {
                        lowest = l;
                    }
                    continue;
                }

                h = MathUtil.GetHighest(obj.Vals, drawItemStartIndex, drawItemStartIndex + drawItemCount);
                if (h > highest)
                {
                    highest = h;
                }
                l = MathUtil.GetLowest(obj.Vals, drawItemStartIndex, drawItemStartIndex + drawItemCount);
                if (l < lowest)
                {
                    lowest = l;
                }

                //special for zero bars.
                if (obj.Type == DrawObjectType.zVLines)
                {
                    if (highest < -lowest)
                    {
                        highest = -lowest;
                    }
                }
            }

            foreach (var i in DrawingObjects.All())
            {
                var obj = i.Value;
                if (obj.Type == type)
                {
                    DrawObj(dc, highest, lowest, obj);
                }
            }
        }

        private void DrawObj(DrawingContext dc,double highest,double lowest,DrawObject obj)
        {
            switch (obj.Type)
            {
                case DrawObjectType.Line:
                    drawLine(dc, highest, lowest, obj);
                    break;
                case DrawObjectType.zVLines:
                    DrawZeroVerticalLines(dc, highest, lowest, obj);
                    break;
                case DrawObjectType.CandleLine:
                    DrawCandleLine(dc, highest, lowest, obj);
                    break;
                case DrawObjectType.vLines:
                    DrawVLines(dc, highest, lowest, obj);
                    break;
            }
        }

        #region DrawObjects Line,VLines,Candleline,ZeroVerticalLines
        //绘制垂线
        private void DrawVLines(DrawingContext dc, double highest, double lowest, DrawObject obj)
        {
            if (obj.Type != DrawObjectType.vLines)
            {
                return;
            }
            var vals = obj.Vals;
            for (var i = 0; i < drawItemCount; ++i)
            {
                int itemIndex = drawItemStartIndex + i;
                double xoffset = (int)(i * (itemWidth + itemSpace)) + 0.5 + ChartStartX;
                double yoffset = (1.0 - vals[itemIndex] / highest) * ChartHeight + 0.5 + ChartStartY;

                Color color = obj.Color;

                var pen = getPen(color.R, color.G, color.B, 1);
                if (priceInfo.PriceList[itemIndex].Open > priceInfo.PriceList[itemIndex].Close)
                {
                    pen = getPen(255, 0, 0, 1);
                }
                else
                {
                    pen = getPen(0, 128, 0, 1);
                }
                dc.DrawLine(pen, new Point(xoffset + itemWidth / 2, yoffset), new Point(xoffset + itemWidth / 2, ChartStartY + ChartHeight));
            }
        }

        //绘制Candle线
        private void DrawCandleLine(DrawingContext dc, double highest, double lowest, DrawObject obj)
        {
            var opens = obj.Vals;
            var closes = obj.Vals1;
            var highs = obj.Vals2;
            var lows = obj.Vals3;
            if (opens == null || closes == null || highs == null || lows == null 
                || opens.Length==0
                || opens.Length != closes.Length || closes.Length != highs.Length || highs.Length != lows.Length)
            {
                return;
            }

            //绘制可视区域的K线
            for (int i = 0; i < drawItemCount; ++i)
            {
                int itemIndex = drawItemStartIndex + i;
                double xoffset = (int)(i * (itemWidth + itemSpace)) + 0.5 + ChartStartX;
                double yTop = (int)((highest - highs[itemIndex]) / (highest - lowest) * ChartHeight) + 0.5 + ChartStartY;

                double yBottom = (int)((highest - lows[itemIndex]) / (highest - lowest) * ChartHeight) + 0.5 + ChartStartY;
                double yOpen = (int)((highest - opens[itemIndex]) / (highest - lowest) * ChartHeight) + 0.5 + ChartStartY;
                double yClose = (int)((highest - closes[itemIndex]) / (highest - lowest) * ChartHeight) + 0.5 + ChartStartY;
                double bodyBottom = yOpen;
                double bodyTop = yClose;

                Color bodyColor = new Color();
                bodyColor.R = 255;
                bodyColor.G = 0;
                bodyColor.B = 0;
                if (opens[itemIndex] > closes[itemIndex])
                {
                    bodyTop = yOpen;
                    bodyBottom = yClose;
                    bodyColor.R = 0;
                    bodyColor.G = 128;
                    bodyColor.B = 0;
                }

                var pen = getPen(0, 0, 0, 1);
                var brush = getBrush(bodyColor.R, bodyColor.G, bodyColor.B);

                //draw top vertical line
                dc.DrawLine(pen, new Point(xoffset + itemWidth / 2, yTop), new Point(xoffset + itemWidth / 2, bodyTop));

                //draw kline body
                double bodyHeight = bodyBottom - bodyTop;
                dc.DrawRectangle(brush, pen, new Rect(xoffset, bodyTop, itemWidth, bodyHeight));
                //draw bottom line.
                dc.DrawLine(pen, new Point(xoffset + itemWidth / 2, bodyBottom), new Point(xoffset + itemWidth / 2, yBottom));
            }
        }

        //绘制以0为基准的垂线，类似MACD线
        private void DrawZeroVerticalLines(DrawingContext dc, double highest, double lowest, DrawObject obj)
        {
            if (obj.Type != DrawObjectType.zVLines)
            {
                return;
            }

            for (var i = 0; i < drawItemCount; ++i)
            {
                var itemIndex = drawItemStartIndex + i;
                var xoffset = (int)(i * (itemWidth + itemSpace)) + 0.5 + ChartStartX;
                var yoffset = ChartHeight - (int)((obj.Vals[itemIndex] + highest) / (2 * highest) * ChartHeight) + 0.5 + ChartStartY;
                if (itemIndex > 0)
                {
                    if (obj.Vals[itemIndex] * obj.Vals[itemIndex - 1] < 0) //正负交汇处画粗线
                    {
                        if (obj.Vals[itemIndex - 1] < 0)
                        {
                            Pen gpen = getPen(255, 0, 0, 1); //绿色
                            gpen.Thickness = 2.5;
                            dc.DrawLine(gpen, new Point(xoffset, ChartStartY + ChartHeight / 2 - 4), new Point(xoffset, ChartStartY + ChartHeight / 2 + 4));
                        }
                        if (obj.Vals[itemIndex - 1] > 0)
                        {
                            Pen rpen = getPen(0, 128, 0, 1); //红色
                            rpen.Thickness = 2.5;
                            dc.DrawLine(rpen, new Point(xoffset, ChartStartY + ChartHeight / 2 - 4), new Point(xoffset, ChartStartY + ChartHeight / 2 + 4));
                        }
                        continue;
                    }
                }
                if(obj.Vals[itemIndex] > 0)
                {
                    Pen gpen = getPen(255, 0, 0, 1); //绿色
                    gpen.Thickness = 1;
                    dc.DrawLine(gpen, new Point(xoffset, ChartStartY + ChartHeight / 2 + 0.5), new Point(xoffset, yoffset));
                }
                else
                {
                    Pen rpen = getPen(0, 128, 0, 1); //红色
                    rpen.Thickness = 1;
                    dc.DrawLine(rpen, new Point(xoffset, ChartStartY + ChartHeight / 2 + 0.5), new Point(xoffset, yoffset));
                }
            }
        }

        //绘制连续的曲线
        private void drawLine(DrawingContext dc, double highest, double lowest, DrawObject obj)
        {
            var pen = getPen(obj.Color.R, obj.Color.G, obj.Color.B, obj.Thickness);

            PathFigure pfFast = new PathFigure();
            PathGeometry pgFast = new PathGeometry();
            pgFast.Figures.Add(pfFast);

            for (var i = 0; i < drawItemCount; ++i)
            {
                var itemIndex = drawItemStartIndex + i;
                var xoffset = (int)(i * (itemWidth + itemSpace)) + 0.5 + ChartStartX;
                var yoffset = (int)((highest - obj.Vals[itemIndex]) / (highest - lowest) * ChartHeight) + 0.5 + ChartStartY;

                if (i == 0)
                {
                    pfFast.StartPoint = new Point(xoffset, yoffset);
                }
                else
                {
                    pfFast.Segments.Add(new LineSegment(new Point(xoffset, yoffset), true));
                }
            }
            dc.DrawGeometry(Brushes.Transparent, pen, pgFast);
        }

        #endregion
    }

    public enum IndicatorType
    {
        Time,    //时间
        Candle,  //K线
        MACD,    //MACD
        KDJ,     //KDJ
        CJL,     //成交量
        JXZC     //均线之差
    }

}