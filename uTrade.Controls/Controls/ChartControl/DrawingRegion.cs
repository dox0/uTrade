using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace uTrade.Charting
{
    public class DrawingRegion : FrameworkElement
    {
        public double LeftFixWidth = 50; //左侧固定宽度
        public double RightFixWidth = 50;//右侧固定宽度
        public double TopFixWidth { set; get; }  //顶部固定高度
        public double BottomFixWidth { set; get; }//底部固定高度

        double _totalWidth;
        double _totalHeight;

        public double totalWidth
        {
            get
            {
                return _totalWidth;
            }
        }
        public double totalHeight
        {
            get
            {
                return _totalHeight;
            }
        }
        public double ChartStartX
        {
            get
            {
                return LeftFixWidth;
            }
        }
        public double ChartStartY
        {
            get
            {
                return TopFixWidth;
            }
        }
        public double ChartEndX
        {
            get
            {
                return totalWidth - RightFixWidth;
            }
        }
        public double ChartEndY
        {
            get
            {
                return totalHeight - BottomFixWidth;
            }
        }

        public double ChartWidth
        {
            get
            {
                return ChartEndX - ChartStartX;
            }
        }

        public double ChartHeight
        {
            get
            {
                return ChartEndY - ChartStartY;
            }
        }

        //可视区域的最高最低值。
        public double HighestPrice
        {
            get; set;
        }

        public double LowestPrice
        {
            get; set;
        }

        public double HighestVolume
        {
            get; set;
        }

        public Point MousePos = new Point();


        public DrawingRegion()
        {

        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            this._totalWidth = this.ActualWidth;
            this._totalHeight = this.ActualHeight;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var pos = e.GetPosition(this);
            MousePos = pos;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(this);
            MousePos = pos;
        }

        //判断坐标是否在矩形框内
        public bool InChart()
        {
            if ((MousePos.X >= ChartStartX && MousePos.X <= ChartEndX)
                && (MousePos.Y >= ChartStartY && MousePos.Y <= ChartEndY))
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
