using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace uTrade.Controls.Charting
{
    public partial class DrawingCanvas
    {

        #region ChartControls Properties

        private string border = "Black";
        public string Border
        {
            get
            {
                return border;
            }
            set
            {
                border = value;
                ReportPropertyChanged("Border");
            }
        }

        private const string whiteColor = "White";
        private const string blackColor = "Black";
        private const string grayColor = "Gray";

        private string background = whiteColor;
        public string Background
        {
            get
            {
                return background;
            }
            set
            {
                background = value;
                ReportPropertyChanged("Background");
            }
        }

        private int yScaleCount = 5;

        /// <summary>
        /// Y轴grid数
        /// </summary>
        public int YScaleCount
        {
            get
            {
                return yScaleCount;
            }
            set
            {
                yScaleCount = value;
                ReportPropertyChanged("YScaleCount");
            }
        }
        private int xScaleCount = 15;

        /// <summary>
        /// X轴grid数
        /// </summary>
        public int XScaleCount
        {
            get
            {
                return xScaleCount;
            }
            set
            {
                xScaleCount = value;
                ReportPropertyChanged("XScaleCount");
            }
        }

        /// <summary>
        /// 光标线dash
        /// </summary>
        private DoubleCollection cursorLinesDashes = null;
        public DoubleCollection CursorLinesDashes
        {
            get { return cursorLinesDashes; }
            set
            {
                cursorLinesDashes = value;
                ReportPropertyChanged("CursorLinesDashes");
            }
        }

        private string scaleLineColor = grayColor;
        public string ScaleLineColor
        {
            get
            {
                return scaleLineColor;
            }
            set
            {
                scaleLineColor = value;
                ReportPropertyChanged("ScaleLineColor");
            }
        }

        private int scaleLineThickness = 1;
        public int ScaleLineThickness
        {
            get
            {
                return scaleLineThickness;
            }
            set
            {
                scaleLineThickness = value;
                ReportPropertyChanged("ScaleLineThickness");

            }
        }

        private DoubleCollection scaleLineDashes = null;
        public DoubleCollection ScaleLineDashes
        {
            get { return scaleLineDashes; }
            set
            {
                scaleLineDashes = value;
                ReportPropertyChanged("ScaleLineDashes");
            }
        }

        private string fontFamily = "Arial";
        public string FontFamily
        {
            get
            {
                return fontFamily;
            }
            set
            {
                fontFamily = value;
                ReportPropertyChanged("FontFamily");
            }
        }

        private int fontSize = 10;
        public int FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                ReportPropertyChanged("FontSize");
            }
        }

        private bool isScalesOptimized = true;
        public bool IsScalesOptimized
        {
            get { return isScalesOptimized; }
            set
            {
                isScalesOptimized = value;
                ReportPropertyChanged("IsScalesOptimized");
            }

        }

        private int yColumnCount = 4;
        public int YColumnCount
        {
            get { return yColumnCount; }
            set
            {
                yColumnCount = value;
                ReportPropertyChanged("YColumnCount");
            }
        }

        private int xColumnCount = 4;
        public int XColumnCount
        {
            get { return xColumnCount; }
            set
            {
                xColumnCount = value;
                ReportPropertyChanged("XColumnCount");
            }

        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        private void ReportPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void SetValue<T>(int i, string namePrefix, T value)
        {
            string name = namePrefix + i;
            var property = GetType().GetProperties().FirstOrDefault(p => p.Name == name);
            if (property != null)
            {
                var oldValue = (T)property.GetValue(this, null);
                if (oldValue == null || !oldValue.Equals(value))
                {
                    property.SetValue(this, value, null);
                    ReportPropertyChanged(name);
                }

            }
        }

        public T GetValue<T>(int i, string namePrefix)
        {
            string priceName = namePrefix + i;
            var property = GetType().GetProperties().FirstOrDefault(p => p.Name == priceName);
            if (property != null)
            {
                return (T)property.GetValue(this, null);
            }
            else
            {
                return default(T);
            }
        }




    }
}
