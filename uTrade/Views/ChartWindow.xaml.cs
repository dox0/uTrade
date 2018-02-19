using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using uTrade.Controls;
using uTrade.Charting;
using uTrade.Data;
using uTrade.Common;
using uTrade.Core;
using System.Reflection;

namespace uTrade.Views
{
    /// <summary>
    /// Interaction logic for TechView.xaml
    /// </summary>
    /// 
    public partial class ChartWindow : UserControl
    {
        ChartPanel oCandleCanvas = new ChartPanel();
        ChartPanel oVolumeCanvas = new ChartPanel();
        ChartPanel oTimeCavas = new ChartPanel();

        PriceInfo oPriceInfo = null;

        //public delegate void PriceChangedEventHandler(DayPrice p);
        //public event PriceChangedEventHandler OnPriceChanged;


        public ChartWindow()
        {
            InitializeComponent();

            this.klinepanel.Children.Add(oCandleCanvas);
            this.volumepanel.Children.Add(oVolumeCanvas);
            this.timepanel.Children.Add(oTimeCavas);

            this.oCandleCanvas.OnPriceChanged += new ChartPanel.PriceChangedEventHandler(canvas_OnPriceChanged);
            this.oCandleCanvas.OnRegionChanged += new ChartPanel.RegionChangedHandler(canvas_OnRegionChanged);

            this.oVolumeCanvas.OnPriceChanged += new ChartPanel.PriceChangedEventHandler(canvas_OnPriceChanged);
            this.oVolumeCanvas.OnRegionChanged += new ChartPanel.RegionChangedHandler(canvas_OnRegionChanged);

            Init();
        }


        public void Init()
        {
            LoadIndicators(Environment.CurrentDirectory + "//uTrade.Core.dll");
        }

        internal void load(PriceInfo pInfo)
        {
            oPriceInfo = pInfo;
            oCandleCanvas.load(oPriceInfo, IndicatorType.Candle);
            oVolumeCanvas.load(oPriceInfo, IndicatorType.CJL);
            oTimeCavas.load(oPriceInfo, IndicatorType.Time);

            InitDrawingObjects();
            InitDrawingRegion();

            if (oPriceInfo.Favorite > 0)
            {
                ImageSource imageUpnew = new BitmapImage(new Uri(@"/Images/favorite.png", UriKind.RelativeOrAbsolute));
                Image imgtemp = new Image();
                imgtemp.Source = imageUpnew;
                imgtemp.Width = 16;
                imgtemp.Height = 16;
                btn_Favorite.Content = imgtemp;
                
            }
            else
            {
                ImageSource imageUpnew = new BitmapImage(new Uri(@"/Images/un-favorite.png", UriKind.RelativeOrAbsolute));
                Image imgtemp = new Image();
                imgtemp.Source = imageUpnew;
                imgtemp.Width = 16;
                imgtemp.Height = 16;
                btn_Favorite.Content = imgtemp;
            }
        }

        void InitDrawingObjects()
        {
            Data.Candle oCandle = new Data.Candle();
            Data.MACD oMacd = new Data.MACD();

            oCandleCanvas.DrawingObjects.Add(oCandle.GetDrawingObj(oPriceInfo));
            oVolumeCanvas.DrawingObjects.Add(oMacd.GetDrawingObj(oPriceInfo));
        }

        void InitDrawingRegion()
        {
            oCandleCanvas.ScaleLineCount = 5;
            oVolumeCanvas.ScaleLineCount = 0;

            oVolumeCanvas.IsShowTime = false;
            oTimeCavas.IsShowTime = false;
        }

        void canvas_OnRegionChanged()
        {

        }


        /// <summary>
        /// 加载指标
        /// </summary>
        /// <param name="file">文件路径</param>
        private void LoadIndicators(string strFile)
        {
            try
            {
                Assembly ass = Assembly.LoadFile(strFile);
                //加载hf_plat报错:增加对hf_plat_core的引用
                foreach (var t in ass.GetTypes())
                {
                    if (t.BaseType.FullName == typeof(uTrade.Core.Indicator).FullName)
                    {
                        this.cmbx_Indicators.Items.Add(t);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        void canvas_OnPriceChanged(Data.DayPrice p)
        {
            //if (OnPriceChanged != null)
            //{
            //    OnPriceChanged(p);
            //}
        }

        internal void reload(string p)
        {
            //canvas.reload(p);
        }
        #region chartPanel Mouse Events
        /// <summary>
        /// 水平移动时隐藏交叉线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        Point mousePos = new Point();

        private void chartPanel_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(this);
            var x = pos.X;
            var y = pos.Y;
            double chartOffset = oCandleCanvas.chartOffset;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!oTimeCavas.InChart())
                {
                    chartOffset += mousePos.X - x;
                }
                else
                {
                    //do our time line select task. infact we just change our chartOffset
                    //calc start index in time space.
                    var startIndex = (int)((x - oCandleCanvas.ChartStartX) / oCandleCanvas.ChartWidth * oCandleCanvas.TotalItemCount);
                    chartOffset = startIndex * (oCandleCanvas.itemWidth + oCandleCanvas.itemSpace) - oCandleCanvas.ChartWidth / 2.0;
                }
            }
            mousePos = pos;

            oCandleCanvas.chartOffset = chartOffset;
            oVolumeCanvas.chartOffset = chartOffset;
            oTimeCavas.chartOffset = chartOffset;

            oCandleCanvas.mouseMove(e);
            oVolumeCanvas.mouseMove(e);
            oTimeCavas.mouseMove(e);
        }

        private void chartPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(this);
            double chartOffset = oCandleCanvas.chartOffset;

            var x = pos.X;
            var y = pos.Y;
            if (!oTimeCavas.InChart())
            {
                return;
            }

            //in timeline area
            var startIndex = (int)((x - oCandleCanvas.ChartStartX) / oCandleCanvas.ChartWidth * oCandleCanvas.TotalItemCount);
            chartOffset = startIndex * (oCandleCanvas.itemWidth + oCandleCanvas.itemSpace) - oCandleCanvas.ChartWidth / 2.0;

            oCandleCanvas.chartOffset = chartOffset;
            oVolumeCanvas.chartOffset = chartOffset;
            oTimeCavas.chartOffset = chartOffset;

            oCandleCanvas.mouseDown(e);
            oCandleCanvas.mouseDown(e);
            oTimeCavas.mouseDown(e);
        }

        private void chartPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //crossLine.Visibility = System.Windows.Visibility.Hidden;
        }

        private void chartPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            //crossLine.Visibility = System.Windows.Visibility.Visible;
        }

        private void chartPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //updateTextblockLayout();
        }
        #endregion

        private void btn_Favorite_Click(object sender, RoutedEventArgs e)
        {
            if(oPriceInfo.Favorite > 0)
            {
                oPriceInfo.Favorite = 0;
                ImageSource imageUpnew = new BitmapImage(new Uri(@"/Images/un-favorite.png", UriKind.RelativeOrAbsolute));
                Image imgtemp = new Image();
                imgtemp.Source = imageUpnew;
                imgtemp.Width = 16;
                imgtemp.Height = 16;
                btn_Favorite.Content = imgtemp;
            }
            else
            {
                oPriceInfo.Favorite = 1;
                ImageSource imageUpnew = new BitmapImage(new Uri(@"/Images/favorite.png", UriKind.RelativeOrAbsolute));
                Image imgtemp = new Image();
                imgtemp.Source = imageUpnew;
                imgtemp.Width = 16;
                imgtemp.Height = 16;
                btn_Favorite.Content = imgtemp;
            }
        }

        private void cmbx_Indicators_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

