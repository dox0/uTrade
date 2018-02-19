using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using uTrade.Core;

namespace uTrade.Core
{
	/// <summary>
	/// 策略
	/// </summary>
	[DefaultProperty("Name")]
	public abstract partial class Strategy : CustomTypeDescriptor
    {
		private string _name = string.Empty;

		/// <summary>
		/// </summary>
		protected Strategy()
		{
			//处理参数
			DicProperties.Clear();
			foreach (var v in GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
			{
				object[] ps = v.GetCustomAttributes(typeof(ParameterAttribute), false);
				if (ps.Length <= 0)
				{
					continue;
				}
				ParameterAttribute pa = (ParameterAttribute)ps[0];
				Property p = new Property(v.Name, v.GetValue(this)) { Category = pa.Category, Description = pa.Description };
				Add(p);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Indicator Indicator { get; } = new Indicator();

		/// <summary>
		/// 策略所用的数据序列
		/// </summary>
		[Browsable(false)]
		public List<Datas> Datas { get; private set; } = new List<Datas>();

		/// <summary>
		/// 报单操作
		/// </summary>
		[Description("报单操作列表"), Category("交易")]
		[Browsable(false)]
		public List<OrderItem> Operations { get => Datas.Count == 0 ? null : this.Datas[0].Operations; }


		#region 策略状态:对data[0]的引用

		/// <summary>
		/// 当前持仓手数:多
		/// </summary>
		[Description("当前持仓手数:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int Position { get => this.Datas[0].Position; }

		/// <summary>
		/// 当前持仓手数:净
		/// </summary>
		[Description("当前持仓手数:净"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int PositionNet { get => this.Datas[0].PositionNet; }

		/// <summary>
		/// 当前持仓首个建仓时间:多(yyyyMMdd.HHmmss)
		/// </summary>
		[Description("当前持仓首个建仓时间:多(yyyyMMdd.HHmmss)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double EntryDateLong { get => this.Datas[0].EntryDateLong; }

		/// <summary>
		/// 当前持仓最后建仓时间:多(yyyyMMdd.HHmmss)
		/// </summary>
		[Description("当前持仓最后建仓时间:多(yyyyMMdd.HHmmss)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double LastEntryDate { get => this.Datas[0].LastEntryDate; }

		/// <summary>
		/// 当前持仓首个建仓价格:多
		/// </summary>
		[Description("当前持仓首个建仓价格:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double EntryPrice { get => this.Datas[0].EntryPrice; }

		/// <summary>
		/// 当前持仓最后建仓价格:多
		/// </summary>
		[Description("当前持仓最后建仓价格:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double LastEntryPrice { get => this.Datas[0].LastEntryPrice; }

		/// <summary>
		/// 当前持仓平均建仓价格:多
		/// </summary>
		[Description("当前持仓平均建仓价格:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double AvgEntryPrice { get => this.Datas[0].AvgEntryPrice; }

		/// <summary>
		/// 当前持仓首个建仓到当前位置的Bar数:多(从0开始计数)
		/// </summary>
		[Description("当前持仓首个建仓到当前位置的Bar数:多(从0开始计数)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int BarsSinceEntryLong { get => this.Datas[0].BarsSinceEntryLong; }

		/// <summary>
		/// 当前持仓的最后建仓到当前位置的Bar计数:多(从0开始计数)
		/// </summary>
		[Description("当前持仓的最后建仓到当前位置的Bar计数:多(从0开始计数)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int BarsSinceLastEntryLong { get => this.Datas[0].BarsSinceLastEntryLong; }

		/// <summary>
		/// 最近平仓位置到当前位置的Bar计数:多(从0开始计数)
		/// </summary>
		[Description("最近平仓位置到当前位置的Bar计数:多(从0开始计数)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int BarsSinceExitLong { get => this.Datas[0].BarsSinceExitLong; }

		/// <summary>
		/// 最近平仓时间:多(yyyyMMdd.HHmmss)
		/// </summary>
		[Description("平仓时间:多(yyyyMMdd.HHmmss)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double ExitDate { get => this.Datas[0].ExitDate; }

		/// <summary>
		/// 最近平仓价格:多
		/// </summary>
		[Description("平仓价格:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double ExitPrice { get => this.Datas[0].ExitPrice; }

		/// <summary>
		/// 当前持仓浮动盈亏:多
		/// </summary>
		[Description("浮动盈亏:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double PositionProfitLong { get => this.Datas[0].PositionProfitLong; }

		/// <summary>
		/// 当前持仓浮动盈亏:净
		/// </summary>
		[Description("浮动盈亏:净"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double PositionProfit { get => this.Datas[0].PositionProfit; }
		#endregion

		#region ============== 属性 ==================

		/// <summary>
		/// 策略名称
		/// </summary>
		[Description("名称"), Category("设计")]
		[Browsable(false)]
		public string Name
		{
			get
			{
				if (_name == string.Empty)
				{
					string nameLast = DicProperties.Values.Select(p => GetType().GetField(p.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)).Where(fieldInfo => fieldInfo != null).Aggregate(string.Empty, (current, fieldInfo) => current + (fieldInfo.GetValue(this) + ","));
					nameLast = nameLast.TrimEnd(',');
					this._name = GetType().FullName + (string.IsNullOrEmpty(nameLast) ? "" : ("(" + nameLast + ")"));
				}
				return this._name;
			}
			set { this._name = value; }
		}


		/// <summary>
		/// 周期数
		/// </summary>
		[Description("周期数"), Category("设计")]
		[Browsable(false)]
		public int Interval { get => Datas.Count == 0 ? 0 : this.Datas[0].Interval; }

		/// <summary>
		/// 周期类型
		/// </summary>
		[Description("周期类型"), Category("设计")]
		[Browsable(false)]
		public IntervalType IntervalType { get => Datas.Count == 0 ? IntervalType.Min : this.Datas[0].IntervalType; }

		/// <summary>
		/// 是否发送实际委托
		/// </summary>
		public bool EnableOrder { get; set; } = false;

		/// <summary>
		/// 是否接收实时行情
		/// </summary>
		public bool EnableTick { get; set; } = false;

        /// <summary>
        /// 当前K线索引(由左向右从0开始)
        /// </summary>
        [Description("当前K线索引"), Category("数据")]
		[Browsable(false)]
		public int CurrentBar { get => Datas.Count == 0 ? -1 : this.Datas[0].CurrentBar; }

		/// <summary>
		/// 实际行情(无数据时为UpdateTime == null)
		/// </summary>
		[Description("分笔数据"), Category("数据")]
		[Browsable(false)]
		public Tick Tick { get => Datas.Count == 0 ? null : this.Datas[0].Tick; }

		/// <summary>
		/// 时间(yyyyMMdd.HHmmss)
		/// </summary>
		[Browsable(false)]
		public DataSeries Date { get => Datas.Count == 0 ? null : this.Datas[0].Date; }

		/// <summary>
		/// 时间(0.HHmmss)
		/// </summary>
		public DataSeries Time { get => Datas.Count == 0 ? null : this.Datas[0].Time; }

		/// <summary>
		/// 最高价
		/// </summary>
		[Browsable(false)]
		public DataSeries High { get => Datas.Count == 0 ? null : this.Datas[0].High; }

		/// <summary>
		/// 最低价
		/// </summary>
		[Browsable(false)]
		public DataSeries Low { get => Datas.Count == 0 ? null : this.Datas[0].Low; }

		/// <summary>
		/// 开盘价
		/// </summary>
		[Browsable(false)]
		public DataSeries Open { get => Datas.Count == 0 ? null : this.Datas[0].Open; }

		/// <summary>
		/// 收盘价
		/// </summary>
		[Browsable(false)]
		public DataSeries Close { get => Datas.Count == 0 ? null : this.Datas[0].Close; }

		/// <summary>
		/// 成交量
		/// </summary>
		[Browsable(false)]
		public DataSeries Volume { get => Datas.Count == 0 ? null : this.Datas[0].Volume; }

		#endregion

		/// <summary>
		/// </summary>
		/// <returns> </returns>
		public override string ToString()
		{
			string nameLast = DicProperties.Values.Aggregate(string.Empty, (current, p) => current + (p.Value + ",")).TrimEnd(',');
			return GetType().FullName + (string.IsNullOrEmpty(nameLast) ? "" : ("(" + nameLast + ")"));
		}

		/// <summary>
		/// 返回策略的参数列表(name:value),以','分隔
		/// </summary>
		/// <returns></returns>
		public string GetParams()
		{
			return "(" + DicProperties.Values.Aggregate(string.Empty, (current, p) => current + (p.Name + ":" + p.Value + ",")).TrimEnd(',') + ")";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pParameters"></param>
		public void FromString(string pParameters)
		{
			if (string.IsNullOrEmpty(pParameters))
				return;
			for (int i = 0; i < pParameters.Split(',').Length; i++)
			{
				var para = pParameters.Split(',')[i];
				var p = DicProperties.ElementAt(i);
				p.Value.Value = Convert.ChangeType(para, p.Value.Value.GetType());

				var fi = this.GetType().GetField(p.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
				if (fi != null)
					fi.SetValue(this, p.Value.Value);   //配置字段的值
			}
		}

		/// <summary>
		/// StrategyCollection.Add时调用：初始化及测试
		/// </summary>
		/// <param name="pDatas">数据</param>
		public void Init(params Datas[] pDatas)
		{
			//处理Bar相关
			this.Datas.Clear();

			//此时数据为空
			foreach (Datas data in pDatas)
				this.Datas.Add(data);

			this.Initialize(); //调用客户初始化函数


			//发单由plat层控制
			foreach (Datas data in this.Datas)
			{
				data.OnChanged += Data_OnChanged; //少用匿名函数
				data.OnRtnOrder += Data_OnRtnOrder;
			}
		}

		private void Data_OnRtnOrder(OrderItem pOrderItem, Datas pData)
		{
			if (_loading) return;
			_rtnOrder?.Invoke(pOrderItem, pData, this);
		}

		private void Data_OnChanged(int pType, object pNew, object pOld)
		{ //type, new, old

			if (pType >= 0)
				this.OnBarUpdate();    //每个数据有变化都会调用策略
		}

		/// <summary>
		/// 策略报单
		/// </summary>
		/// <param name="pOrderItem">策略信号</param>
		/// <param name="pData">数据序列</param>
		/// <param name="pStrategy">策略</param>
		public delegate void RtnOrder(OrderItem pOrderItem, Datas pData, Strategy pStrategy);

		private RtnOrder _rtnOrder;
		private bool _loading; //历史测试:控制_rtnorder调用

		/// <summary>
		/// 
		/// </summary>
		public event RtnOrder OnRtnOrder
		{
			add
			{
				_rtnOrder += value;
			}
			remove
			{
				_rtnOrder -= value;
			}
		}

		/// <summary>
		/// 支持多个合约数据序列
		/// </summary>
		/// <param name="barss"></param>
		public void LoadHistory(params Tuple<Datas, List<Bar>>[] barss)
		{
            if (barss.Sum(n => n.Item2.Count) == 0)
            {
                return;
            }
			_loading = true;
			var barseries = new List<Bar>();
			foreach (var v in barss)
            {
				barseries.AddRange(v.Item2);
            }
			//按时间排序;对各Data序列赋值
			barseries = barseries.OrderBy(n => n.Time).ToList();
			foreach (var bar in barseries)
			{
				var f = barss.First(n => n.Item2.IndexOf(bar) >= 0);
				//f.Item1.Add(bar);
				f.Item1.OnUpdatePerMin(bar);
			}
			_loading = false;
		}

		/// <summary>
		/// </summary>
		/// <param name="pParamName"> </param>
		/// <param name="pValue"> </param>
		public void SetParameterValue(string pParamName, object pValue)
		{
			FieldInfo fi = GetType().GetField(pParamName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			if (fi == null)
			{
				return;
			}
			fi.SetValue(this, Convert.ChangeType(pValue, fi.FieldType));
			DicProperties[pParamName].Value = Convert.ChangeType(pValue, fi.FieldType);
		}

		/// <summary>
		/// 开多仓：买开
		/// </summary>
		/// <param name="pLots"> 手数 </param>
		/// <param name="pPrice"> 价格 (非PriceTick整数倍会报错,请先行处理.)</param>
		/// <param name="pRemark">注释</param>
		public void Buy(double pLots, double pPrice, string pRemark = "")
		{
			this.Datas[0].Buy((int)pLots, pPrice, pRemark);
		}

		/// <summary>
		/// 平多仓：卖平
		/// </summary>
		/// <param name="pLots"> 手数 </param>
		/// <param name="pPrice"> 价格  (非PriceTick整数倍会报错,请先行处理.)</param>
		/// <param name="pRemark">注释</param>
		public void Sell(double pLots, double pPrice, string pRemark = "")
		{
			this.Datas[0].Sell((int)pLots, pPrice, pRemark);
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public abstract void Initialize();

		/// <summary>
		/// 继承类需完成的策略主体函数
		/// </summary>
		public abstract void OnBarUpdate();
	}


}
