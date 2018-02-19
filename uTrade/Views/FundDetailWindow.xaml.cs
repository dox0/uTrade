using System;
using System.Collections.Generic;
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

using uTrade.Data;
using uTrade.Common;
using System.Threading;

namespace uTrade.Views
{

    //4.定义事件参数类
    public class ItemkArgs : EventArgs
    {
        public readonly Rank RankItem;

        public ItemkArgs(Rank oItem)
        {
            RankItem = oItem;
        }
    }

    /// <summary>
    /// FundDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FundDetailWindow : UserControl
    {
        List<Rank> lstRankAll;
        PriceInfo fdpInfo;
        bool IsShowFilteredItems = false;
        List<EquityModel> ntWorkModel = new List<EquityModel>();
        RankModel rkModel = new RankModel();
        GetFundRankInfo getMyRank = new GetFundRankInfo();

        //1.定义delegate

        public delegate void DoubleClickEventHandler(object sender, ItemkArgs e);
        //2.用event 关键字声明事件对象

        public event DoubleClickEventHandler ItemDoubleClick;

        //3.事件触发方法
        protected virtual void OnItemDoubleClick(ItemkArgs e)
        {
            if (ItemDoubleClick != null)
            {
                ItemDoubleClick(this, e);
            }
        }


        public FundDetailWindow()
        {
            InitializeComponent();

            lstRankAll = GetFundEquityInfo.Instance.GetAllRankList();
            DetailGrid.ItemsSource = lstRankAll;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DetailGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dgSender = sender as DataGrid;
            Rank dRank = dgSender.SelectedItem as Rank;
            string symbol = dRank.Symbol;
            fdpInfo = GetFundEquityInfo.Instance.GetFormatedFundInfo(symbol, DateService.ThisTimeLastYear(), DateTime.Now);

            ItemkArgs itemkArgs = new ItemkArgs(dRank);
            //引发事件
            OnItemDoubleClick(itemkArgs);

        }

        /// <summary>
        /// 根据基金代码获取基金详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetFundDetailByID_Click(object sender, RoutedEventArgs e)
        {
            ntWorkModel = GetFundEquityInfo.Instance.Info(tbxSrhDetailFundID.Text, DateService.ThisTimeLastYear(), DateTime.Now);
        }

        private void btnSyncFromRemote_Click(object sender, RoutedEventArgs e)
        {
            Thread tdUpdateRank = new Thread(SyncRankDataFromRemote);
            tdUpdateRank.Start();
            pgbSync.Value = 0;
        }

        /// <summary>
        /// 从远程更新本地的数据库
        /// </summary>
        void SyncRankDataFromRemote()
        {
            RankDAL.Clear();
            for (int i = 0; i < rkModel.allPages; i++)
            {
                rkModel = getMyRank.GetRankInfo((i + 1).ToString(), "50");
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    pgbSync.Value = (i + 1) * 100 / rkModel.allPages;
                }));
            }
            Dispatcher.BeginInvoke(new Action(delegate
            {
                pgbSync.Value = 0;
                lstRankAll = GetFundEquityInfo.Instance.GetAllRankList();
                DetailGrid.ItemsSource = lstRankAll;
            }));
        }

        private void btn_Favorite_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_FavoriteFilter_Click(object sender, RoutedEventArgs e)
        {
            if (IsShowFilteredItems)
            {
                IsShowFilteredItems = false;
                ImageSource imageUpnew = new BitmapImage(new Uri(@"/Images/favorite.png", UriKind.RelativeOrAbsolute));
                Image imgtemp = new Image();
                imgtemp.Source = imageUpnew;
                imgtemp.Width = 16;
                imgtemp.Height = 16;
                btn_FavoriteFilter.Content = imgtemp;
                lstRankAll = GetFundEquityInfo.Instance.GetFavoriteRankList();
                DetailGrid.ItemsSource = lstRankAll;
            }
            else
            {
                IsShowFilteredItems = true;
                ImageSource imageUpnew = new BitmapImage(new Uri(@"/Images/un-favorite.png", UriKind.RelativeOrAbsolute));
                Image imgtemp = new Image();
                imgtemp.Source = imageUpnew;
                imgtemp.Width = 16;
                imgtemp.Height = 16;
                btn_FavoriteFilter.Content = imgtemp;
                lstRankAll = GetFundEquityInfo.Instance.GetAllRankList();
                DetailGrid.ItemsSource = lstRankAll;
            }
        }


    }
}
