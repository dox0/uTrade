using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
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

using Newtonsoft.Json;
using uTrade.Core;

namespace uTrade.Views
{

    /// <summary>
    /// BackTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BackTestWindow : UserControl
    {

        string[] SIM_INTERVALS = new string[]{ "Min 1", "Min 3", "Min 5", "Min 10", "Min 15", "Min 30", "Hour 1", "Day 1", "Week 1", "Year 1" };

        //private ConcurrentDictionary<string, Strategy> m_dicStrategies = new ConcurrentDictionary<string, Strategy>();

        private List<StrategyConfig> m_lstStrategies = new List<StrategyConfig>();

        public BackTestWindow()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 窗口动态初始化
        /// </summary>
        public void Init()
        {
            //1.策略
            LoadStrategyFile(Environment.CurrentDirectory + "//uTrade.Strategies.dll");

            //2.时间周期
            foreach( var item in SIM_INTERVALS)
            {
                this.cmbx_Interval.Items.Add(item);
            }

            //3.开始时间，结束时间
            dp_BeginTime.SelectedDate = DateTime.Today.AddYears(-1);
            dp_EndTime.SelectedDate = DateTime.Today;
            dg_Strategies.DataContext = m_lstStrategies;

            //4.策略列表
            LoadStrategy();
        }

        /// <summary>
        /// 加载保存的策略到列表里
        /// </summary>
        void LoadStrategy()
        {
            if (File.Exists("strategies.cfg"))
            {
                var list = JsonConvert.DeserializeObject<List<StrategyConfig>>(File.ReadAllText("strategies.cfg"));
                foreach (var sc in list)
                {
                    Type straType = null;
                    //类型是否存在
                    foreach (var t in this.cmbx_Stratagies.Items)
                    {
                        Type tStra = t as Type;
                        if(null != tStra)
                        {
                            if (tStra.FullName == sc.TypeFullName)
                            {
                                straType = tStra;
                                break;
                            }
                        }
                    }
                    if (straType == null)
                        continue;
                    Strategy stra = (Strategy)Activator.CreateInstance(straType);
                    //参数赋值
                    if (!string.IsNullOrEmpty(sc.Params.Trim('(', ')')))
                        foreach (var v in sc.Params.Trim('(', ')').Split(','))
                        {
                            stra.SetParameterValue(v.Split(':')[0], v.Split(':')[1]);
                        }

                    AddStra(stra, sc.Interval, sc.BeginDate, sc.EndDate, sc.Name);

                }
            }
        }

        // 策略添加到表中,返回添加后的行号
        private StrategyConfig AddStra(Strategy stra,  string pInterval, DateTime pBegin, DateTime? pEnd, string pName = "Invalid")
        {
            if(pName == "Invalid")
            {
                if (m_lstStrategies.Count == 0)
                {
                    pName = "1";
                }
                else
                {
                    pName = (m_lstStrategies.Select(n => int.Parse(n.Name)).Max() + 1).ToString();
                }
            }

            stra.Name = pName;
            StrategyConfig strategy = new StrategyConfig();
            strategy.BeginDate = pBegin;
            strategy.EndDate = pEnd;
            strategy.Interval = pInterval;
            strategy.Name = pName;
            strategy.Params = stra.GetParams();
            strategy.TypeFullName = stra.GetType().FullName;
            m_lstStrategies.Add(strategy);

            return strategy;
        }


        /// <summary>
        /// 加载策略DLL库
        /// </summary>
        /// <param name="file">文件路径</param>
        private void LoadStrategyFile(string file)
        {
            try
            {
                Assembly ass = Assembly.LoadFile(file);
                //加载hf_plat报错:增加对hf_plat_core的引用
                foreach (var t in ass.GetTypes())
                {
                    if (t.BaseType == typeof(Strategy))
                    {
                        this.cmbx_Stratagies.Items.Add(t);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btn_Simulate_Click(object sender, RoutedEventArgs e)
        {
            if (this.cmbx_Stratagies.SelectedIndex < 0) return;
            if (this.cmbx_Interval.SelectedIndex < 0) return;

            Strategy stra = (Strategy)Activator.CreateInstance((Type)this.cmbx_Stratagies.SelectedItem);

            //TODO---参数配置---
            //if (stra.GetParams().Trim('(', ')').Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length >= 1)
            //{
            //    PropertyGrid fp = new PropertyGrid();

            //    //参数配置
            //    fp.DataContext = stra;
            //    fp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //    if (fp.ShowDialog() != true) return;
            //}


            StrategyConfig strategy = AddStra(stra, this.cmbx_Interval.Text, this.dp_BeginTime.DisplayDate, this.dp_EndTime.DisplayDate );

            //数据加载
            LoadDataBar(strategy);
        }


        //加载测试指定行的策略的
        private void LoadDataBar(StrategyConfig config)
        {

            //var inst = row.Cells["Instrument"].Value.ToString();
            //var instOrder = row.Cells["InstrumentOrder"].Value.ToString();
            //var interval = row.Cells["Interval"].Value.ToString();
            //var stra = _dicStrategies[row.Cells["StraName"].Value.ToString()];
            //var begin = ((DateTime)row.Cells["BeginDate"].Value).ToString("yyyyMMdd");
            //var end = row.Cells["EndDate"].Value == null ? DateTime.Today.AddDays(7).ToString("yyyyMMdd") : ((DateTime)row.Cells["EndDate"].Value).AddDays(1).ToString("yyyyMMdd");

            //this.DataGridViewStrategies.Rows[rid].Cells["Loaded"].Value = "加载中...";
                        
        }
    }
}
