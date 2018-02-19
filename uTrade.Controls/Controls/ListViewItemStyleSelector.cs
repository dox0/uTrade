using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Media.Animation;

namespace uTrade.Controls
{
    public class ListViewItemStyleSelector:StyleSelector
    {

        private Dictionary<ListViewItem, List<Storyboard>> storyboards = new Dictionary<ListViewItem, List<Storyboard>>();
        
        /// <summary>
        /// 下面的示例演示如何定义一个为行定义 Style 的 StyleSelector。
        /// 此示例依据行索引定义 Background 颜色，为每行定义ListViewItem的动画板（Storyboard）。
        ///ListView控件在初始化的时候，每初始化一行ListViewItem的时候都会进入该函数
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            Style st = new Style();
            st.TargetType=typeof(ListViewItem);
            Setter backGroundSetter = new Setter();
            backGroundSetter.Property = ListViewItem.BackgroundProperty;
            ListView listview =
                ItemsControl.ItemsControlFromItemContainer(container)
                as ListView;//获得当前ListView
            int index =
                listview.ItemContainerGenerator.IndexFromContainer(container);//行索引
            if (index % 2 == 0)
            {
                backGroundSetter.Value = Brushes.AliceBlue;
            }
            else
            {
                backGroundSetter.Value = Brushes.Transparent;
            }
            st.Setters.Add(backGroundSetter);

            //获得当前ListViewItem
            ListViewItem iteml = (ListViewItem)listview.ItemContainerGenerator.ContainerFromIndex(index);
            
            //故事板列表，用来存放1.鼠标进入故事板2.鼠标离开故事板
            List<Storyboard> sbl = new List<Storyboard>();
            //声明故事板
            Storyboard storyboard = new Storyboard();

            //1.鼠标进入故事板
            //声明动画
            DoubleAnimation fontEnterAnimation = new DoubleAnimation();
            //动画的目标值
            fontEnterAnimation.To = 16;
            //开始之前的等待时间，设置0.5s的等待时间是为了模拟“悬停时间”
            fontEnterAnimation.BeginTime = TimeSpan.FromSeconds(0.5);
            //动画持续时间
            fontEnterAnimation.Duration = TimeSpan.FromSeconds(1);
            //动画缓动，可要可不要
            fontEnterAnimation.EasingFunction = new ElasticEase() { EasingMode=EasingMode.EaseOut};
            //绑定动画目标控件
            Storyboard.SetTarget(fontEnterAnimation, iteml);
            //绑定动画目标属性
            Storyboard.SetTargetProperty(fontEnterAnimation, new PropertyPath("FontSize"));
            //将动画板添加到故事板中
            storyboard.Children.Add(fontEnterAnimation);
            sbl.Add(storyboard);

            //2.鼠标离开故事板
            storyboard = new Storyboard();
            DoubleAnimation fontLeaveAnimation = new DoubleAnimation();
            fontLeaveAnimation.BeginTime = TimeSpan.FromSeconds(0);
            fontLeaveAnimation.Duration = TimeSpan.FromSeconds(0.5);

            Storyboard.SetTarget(fontLeaveAnimation, iteml);
            Storyboard.SetTargetProperty(fontLeaveAnimation, new PropertyPath("FontSize"));
            storyboard.Children.Add(fontLeaveAnimation);
            sbl.Add(storyboard);


            storyboards.Add(iteml, sbl);
            //绑定鼠标进入事件
            iteml.MouseEnter += new System.Windows.Input.MouseEventHandler(iteml_MouseEnter);
            //绑定鼠标离开事件
            iteml.MouseLeave += new System.Windows.Input.MouseEventHandler(iteml_MouseLeave);
            return st;
        }

        /// <summary>
        /// 鼠标进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iteml_MouseEnter(object sender, RoutedEventArgs e)
        {
            ListViewItem item=(ListViewItem)sender;
            List<Storyboard> storyboard = storyboards[item];
            storyboard[0].Begin();
        }


        private void iteml_MouseLeave(object sender, RoutedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            List<Storyboard> storyboard = storyboards[item];
            storyboard[1].Begin();
        }
    }
}
