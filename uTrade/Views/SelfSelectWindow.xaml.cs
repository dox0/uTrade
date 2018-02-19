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
using uTrade.Strategies;
using uTrade.Data;

namespace uTrade.Views
{
    /// <summary>
    /// SelfSelectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelfSelectWindow : UserControl
    {

        SmallTurtle oSmallTT = new SmallTurtle();

        public SelfSelectWindow()
        {
            InitializeComponent();
        }

        private void btn_Favorite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DetailGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dgSender = sender as DataGrid;

        }

        private void btn_RunSelect_Click(object sender, RoutedEventArgs e)
        {
            List<Rank> lstRankSelected = oSmallTT.Process();

            dg_SelfSelect.DataContext = lstRankSelected;
        }
    }
}
