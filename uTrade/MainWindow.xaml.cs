//* reference:  listview:http://www.cnblogs.com/zhihai/archive/2012/02/03/2337052.html

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

using uTrade.Data;
using uTrade.Common;
using uTrade.Views;


namespace uTrade
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        FundModel fdModle = new FundModel();
        GetFundbyName getuTrade= new GetFundbyName();
        GetFundDetailInfo getMyDetail = new GetFundDetailInfo();
        PriceInfo fdpInfo;
        List<EquityModel> ntWorkModel = new List<EquityModel>();


        public MainWindow()
        {
            InitializeComponent();
            try
            {
                fdpInfo = GetFundEquityInfo.Instance.GetFormatedFundInfo("160631", DateService.ThisTimeLastYear(), DateTime.Now);
                oFundPanel.load(fdpInfo);
            }
            catch
            { }

            LogHelper.Write(LogHelper.LogLevel.Info, "djkdjk");

            //DataAccessFromTdx oTdxAccess = new DataAccessFromTdx();
            //oTdxAccess.DataAccessBackGround();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(tbxSrhContent.Text == "")
            {
                return;
            }
            fdModle = (FundModel)getuTrade.GetFundByName(tbxSrhContent.Text);
            lvFundName.ItemsSource = fdModle.Datas;
        }

        public class ListViewItemStyleSelector : StyleSelector
        {
            public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
            {
                Style st = new Style();
                st.TargetType = typeof(ListViewItem);
                Setter backgroundSetter = new Setter();
                backgroundSetter.Property = ListViewItem.BackgroundProperty;
                ListView listView = ItemsControl.ItemsControlFromItemContainer(container) as ListView;
                int index = listView.ItemContainerGenerator.IndexFromContainer(container);
                if (index % 2 == 0)
                    backgroundSetter.Value = Brushes.LightBlue;
                else
                {
                    backgroundSetter.Value = Brushes.Beige;
                }
                st.Setters.Add(backgroundSetter);
                return st;
            }
        }

        

        private void win_FundDteail_ItemDoubleClick(object sender, ItemkArgs e)
        {

            Rank dRank = e.RankItem;
            string symbol = dRank.Symbol;
            fdpInfo = GetFundEquityInfo.Instance.GetFormatedFundInfo(symbol, DateService.ThisTimeLastYear(), DateTime.Now);

            oFundPanel.load(fdpInfo);
            tbctrl_MainTab.SelectedIndex = 0;
        }

        private void lvFundName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Fund cfund = lvFundName.SelectedItem as Fund;
            ntWorkModel = GetFundEquityInfo.Instance.Info(cfund.CODE , DateService.ThisTimeLastYear(), DateTime.Now);
            lvLatestNet.ItemsSource = ntWorkModel;
            getMyDetail.GetGuessInfo(GetFundEquityInfo.Instance.InfoEqLst(ntWorkModel));
            lvReferInfo.ItemsSource = getMyDetail.Datas;
        }

    }

}
