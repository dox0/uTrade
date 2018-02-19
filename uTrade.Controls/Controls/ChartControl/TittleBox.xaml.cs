using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace uTrade.Controls
{
    /// <summary>
    /// TittleBox.xaml 的交互逻辑
    /// </summary>
    public partial class TittleBox : UserControl
    {
        public TittleBox()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                TextBlock oBlock = new TextBlock()
                {
                    Text = "blo" + i.ToString(),
                    Foreground = new SolidColorBrush(Colors.Black)
                };

                TittlePanel.Children.Add(oBlock);
            }
        }

        public void SetTittle(List<TextBlock> lstTittles)
        {
            for(int i = 0; i<5; i++)
            {
                TextBlock oBlock = new TextBlock()
                {
                    Text = "blo" + i.ToString(),
                    Foreground = new SolidColorBrush(Colors.Black)
                };

                TittlePanel.Children.Add(oBlock);
            }
        }
    }
}
