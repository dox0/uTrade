using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace uTrade.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Datas : Collection<Bar>
    {
        internal OrderItem LastOrder
        {
            get
            {
                return Operations.Count > 0 ? Operations.Last() : new OrderItem();
            }
        }

        public Datas()
        {
            CurrentMinBar = new Bar();

            Date = new DataSeries();
            Time = new DataSeries();
            Open = new DataSeries();
            Close = new DataSeries();
            High = new DataSeries();
            Low = new DataSeries();
            Volume = new DataSeries();
        }

        private CollectionChange _onChange;
		/// <summary>
		/// 策略变化:加1;减-1;更新0
		/// </summary>
		public event CollectionChange OnChanged
		{
			add
			{
				_onChange += value;
			}
			remove
			{
				_onChange -= value;
			}
		}

        #region 数据序列
  
        /// <summary>
        /// 日期(yyyyMMdd)
        /// </summary>
        public DataSeries Date { get; private set; }

        /// <summary>
        /// 时间(0.HHmmss)
        /// </summary>
        public DataSeries Time { get; private set; }

        /// <summary>
        /// 开盘价
        /// </summary>
        public DataSeries Open { get; private set; }

        /// <summary>
        /// 最高价
        /// </summary>
        public DataSeries High { get; private set; }

        /// <summary>
        /// 最低价
        /// </summary>
        public DataSeries Low { get; private set; }

        /// <summary>
        /// 收盘价
        /// </summary>
        public DataSeries Close { get; private set; }

        /// <summary>
        /// 成交量
        /// </summary>
        public DataSeries Volume { get; private set; }

        #endregion

        #region Properties
        /// <summary>
        /// 实际行情(无数据时为Instrument== null)
        /// </summary>
        [Description("分笔数据"), Category("数据"), Browsable(false)]
		public Tick Tick { get; set; } = new Tick();


		/// <summary>
		/// 周期类型
		/// </summary>
		[Description("周期类型"), Category("配置")]
		public IntervalType IntervalType { get; set; } = IntervalType.Min;

		/// <summary>
		/// 周期数
		/// </summary>
		[Description("周期数"), Category("配置")]
		public int Interval { get; set; } = 5;

		/// <summary>
		/// 当前K线索引(由左向右从0开始)
		/// </summary>
		[Description("当前K线索引"), Category("设计"), Browsable(false)]
		public int CurrentBar { get => Count == 0 ? -1 : (Count - 1); }

		/// <summary>
		/// 当前的1分钟K线
		/// </summary>
		public Bar CurrentMinBar { get; private set; }
        #endregion

        /// <summary>
        /// 被tick行情调用
        /// </summary>
        /// <param name="f"></param>
        /// <exception cref="Exception"></exception>
        public void OnTick(Tick f)
		{

			#region 生成or更新K线
			DateTime dt = DateTime.ParseExact(f.UpdateTime, "yyyyMMdd HH:mm:ss", null);
			DateTime dtBegin = dt.Date;
			switch (IntervalType)
			{
				case IntervalType.Sec:
					dtBegin = dtBegin.Date.AddHours(dt.Hour).AddMinutes(dt.Minute).AddSeconds(dt.Second / Interval * Interval);
					break;
				case IntervalType.Min:
					dtBegin = dtBegin.Date.AddHours(dt.Hour).AddMinutes(dt.Minute / Interval * Interval);
					break;
				case IntervalType.Hour:
					dtBegin = dtBegin.Date.AddHours(dt.Hour / Interval * Interval);
					break;
				case IntervalType.Day:
					dtBegin = DateTime.ParseExact(f.TradingDay.ToString(), "yyyyMMdd", null);
					break;
				case IntervalType.Week:
					dtBegin = DateTime.ParseExact(f.TradingDay.ToString(), "yyyyMMdd", null);
					dtBegin = dtBegin.Date.AddDays(1 - (byte)dtBegin.DayOfWeek);
					break;
				case IntervalType.Month:
					dtBegin = DateTime.ParseExact(f.TradingDay.ToString(), "yyyyMMdd", null);
					dtBegin = new DateTime(dtBegin.Year, dtBegin.Month, 1);
					break;
				case IntervalType.Year:
					dtBegin = DateTime.ParseExact(f.TradingDay.ToString(), "yyyyMMdd", null);
					dtBegin = new DateTime(dtBegin.Year, 1, 1);
					break;
				default:
					throw new Exception("参数错误");
			}
			if (CurrentMinBar == null)
			{
                CurrentMinBar = new Bar
				{
					Time = DateTime.ParseExact(f.UpdateTime.Substring(0, f.UpdateTime.Length - 3), "yyyyMMdd HH:mm", null),
					TradingDay = f.TradingDay,
					PreVol = f.Volume,
					Volume = 0
				};
                CurrentMinBar.High = CurrentMinBar.Low = CurrentMinBar.Open = CurrentMinBar.Close = f.LastPrice;
			}
			else
			{
				if (CurrentMinBar.Time - dt < TimeSpan.FromMinutes(1))
				{
                    CurrentMinBar.High = Math.Max(CurrentMinBar.High, f.LastPrice);
                    CurrentMinBar.Low = Math.Min(CurrentMinBar.Low, f.LastPrice);
                    CurrentMinBar.Close = f.LastPrice;
                    CurrentMinBar.Volume = CurrentMinBar.Volume + f.Volume - CurrentMinBar.PreVol;
                    CurrentMinBar.PreVol = f.Volume;       //逐个tick累加
				}
				else
				{
                    CurrentMinBar.Time = DateTime.ParseExact(f.UpdateTime.Substring(0, f.UpdateTime.Length - 3), "yyyyMMdd HH:mm", null);
                    CurrentMinBar.TradingDay = f.TradingDay;
                    CurrentMinBar.Volume = f.Volume - CurrentMinBar.PreVol;
                    CurrentMinBar.PreVol = f.Volume;
                    CurrentMinBar.High = CurrentMinBar.Low = CurrentMinBar.Open = CurrentMinBar.Close = f.LastPrice;
				}
			}
			if (Count == 0) //无数据
			{
				Bar bar = new Bar
				{
					Time = dtBegin,
					TradingDay = f.TradingDay,
					PreVol = f.Volume,
					Volume = 0 // kOld.preVol == 0 ? 0 : _tick.Volume - kOld.preVol;
				};
				bar.High = bar.Low = bar.Open = bar.Close = f.LastPrice;
				Add(bar);
			}
			else
			{
				Bar bar = this[CurrentBar];
				if (bar.Time == dtBegin) //在当前K线范围内
				{
					bar.High = Math.Max(bar.High, f.LastPrice);
					bar.Low = Math.Min(bar.Low, f.LastPrice);
					bar.Close = f.LastPrice;
					bar.Volume = bar.Volume + f.Volume - bar.PreVol;
					bar.PreVol = f.Volume;      //逐个tick累加
					this[CurrentBar] = bar; //更新会与 _onChange?.Invoke(0, old, item); 连动
				}
				else if (dtBegin > bar.Time)
				{
					Bar di = new Bar
					{
						Time = dtBegin,
						TradingDay = f.TradingDay,
						//V = Math.Abs(bar.PreVol - 0) < 1E-06 ? 0 : f.Volume - bar.PreVol,
						Volume = f.Volume - bar.PreVol,
						PreVol = f.Volume,
						Open = f.LastPrice,
						High = f.LastPrice,
						Low = f.LastPrice,
						Close = f.LastPrice
					};
					Add(di);
				}
			}
			Tick = f; //更新最后的tick
			#endregion
		}

		/// <summary>
		/// 接收分钟测试数据
		/// </summary>
		/// <param name="min"></param>
		internal void OnUpdatePerMin(Bar min)
		{
            CurrentMinBar = min;

            DateTime dtBegin = DateTime.MaxValue;
			switch (IntervalType)
			{
				case IntervalType.Sec:
					dtBegin = min.Time.Date.AddHours(min.Time.Hour).AddMinutes(min.Time.Minute).AddSeconds(min.Time.Second / Interval * Interval);
					break;
				case IntervalType.Min:
					dtBegin = min.Time.Date.AddHours(min.Time.Hour).AddMinutes(min.Time.Minute / Interval * Interval);
					break;
				case IntervalType.Hour:
					dtBegin = min.Time.Date.AddHours(min.Time.Hour / Interval * Interval);
					break;
				case IntervalType.Day:
					dtBegin = DateTime.ParseExact(min.TradingDay.ToString(), "yyyyMMdd", null);
					break;
				case IntervalType.Week:
					dtBegin = DateTime.ParseExact(min.TradingDay.ToString(), "yyyyMMdd", null);
					dtBegin = dtBegin.Date.AddDays(1 - (byte)dtBegin.DayOfWeek);
					break;
				case IntervalType.Month:
					dtBegin = DateTime.ParseExact(min.TradingDay.ToString(), "yyyyMMdd", null);
					dtBegin = new DateTime(dtBegin.Year, dtBegin.Month, 1);
					break;
				case IntervalType.Year:
					dtBegin = DateTime.ParseExact(min.TradingDay.ToString(), "yyyyMMdd", null);
					dtBegin = new DateTime(dtBegin.Year, 1, 1);
					break;
				default:
					throw new Exception("参数错误");
			}
			if (Count == 0) //无数据
			{
				Bar bar = new Bar
				{
                    Time = dtBegin,
					PreVol = min.Volume,
					Volume = min.Volume, // kOld.preVol == 0 ? 0 : _tick.Volume - kOld.preVol;
				};
				bar.High = min.High;
				bar.Low = min.Low;
				bar.Open = min.Open;
				bar.Close = min.Close;
				Add(bar);
			}
			else
			{
				Bar bar = this[CurrentBar];
				if (bar.Time == dtBegin) //在当前K线范围内
				{
					bar.High = Math.Max(bar.High, min.High);
					bar.Low = Math.Min(bar.Low, min.Low);
					bar.Volume = bar.Volume + min.Volume;
					bar.Close = min.Close;

					this[CurrentBar] = bar; //更新会与 _onChange?.Invoke(0, old, item); 连动
				}
				else if (dtBegin > bar.Time)
				{
					Bar di = new Bar
					{
                        Time = dtBegin,
						Volume = min.Volume,
						Open = min.Open,
						High = min.High,
						Low = min.Low,
						Close = min.Close,
					};
					Add(di);
				}
			}
		}

		/// <summary>
		/// 在指定索引处添加参数
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		protected override void InsertItem(int index, Bar item)
		{
			base.InsertItem(index, item);
			Date.Add(double.Parse(item.Time.ToString("yyyyMMdd")));
			Time.Add(double.Parse(item.Time.ToString("0.HHmmss")));

			Open.Add(item.Open);
			High.Add(item.High);
			Low.Add(item.Low);
			Volume.Add(item.Volume);
			Close.Add(item.Close); //最后一项更新:用于触发指标相关执行

			_onChange?.Invoke(1, item, item);
		}

		/// <summary>
		/// 设置指定索引处的参数
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		protected override void SetItem(int index, Bar item)
		{
			base.SetItem(index, item);
			Bar old = this[index];
			High[CurrentBar - index] = item.High;
			Low[CurrentBar - index] = item.Low;
			Volume[CurrentBar - index] = item.Volume;
			Close[CurrentBar - index] = item.Close; //最后一项更新:用于触发指标相关执行

			_onChange?.Invoke(0, old, item);
		}

		/// <summary>
		/// 移除指定索引处的数据
		/// </summary>
		/// <param name="index"></param>
		protected override void RemoveItem(int index)
		{
			Bar old = this[index];
			if (Date.Count == Count)
			{
				Date.RemoveAt(index);
				Time.RemoveAt(index);
				Open.RemoveAt(index);
				High.RemoveAt(index);
				Low.RemoveAt(index);
				Close.RemoveAt(index);
				Volume.RemoveAt(index);
			}
			base.RemoveItem(index);
			_onChange?.Invoke(-1, old, old);
		}

		/// <summary>
		/// 清除所有数据
		/// </summary>
		protected override void ClearItems()
		{
			Date.Clear();
			Time.Clear();
			Open.Clear();
			High.Clear();
			Low.Clear();
			Close.Clear();
			Volume.Clear();
			base.ClearItems();
		}


		/// <summary>
		/// 报单操作
		/// </summary>
		[Description("报单操作列表"), Category("交易")]
		public List<OrderItem> Operations { get; private set; } = new List<OrderItem>();

        /// <summary>
        /// 开多仓：买开,开仓就是建新仓，和平仓相对应。多仓指看好后市，买入后等上涨。空仓指看淡后市，买入后等待下跌赚钱。
        /// </summary>
        /// <param name="pLots"> </param>
        /// <param name="pPrice"> </param>
        /// <param name="pRemark">注释</param>
        public void Buy(int pLots, double pPrice, string pRemark = "")
		{
			this.Order(Direction.Buy, pLots, pPrice, pRemark);
		}

		/// <summary>
		/// 平空仓：卖平
		/// </summary>
		/// <param name="pLots"> </param>
		/// <param name="pPrice"> </param>
		/// <param name="pRemark">注释</param>
		public void Sell(int pLots, double pPrice, string pRemark = "")
		{
			this.Order(Direction.Sell, pLots, pPrice, pRemark);
		}

		/// <summary>
		/// 报单
		/// </summary>
		/// <param name="pDirector">买卖方向（买还是卖）</param>
		/// <param name="pOffset"> </param>
		/// <param name="pLots">交易数量</param>
		/// <param name="pPrice">交易价格</param>
		/// <param name="pRemark">注释</param>
		private void Order(Direction pDirector, int pLots, double pPrice, string pRemark)
		{
			OrderItem order;
			//pPrice = (int)(pPrice / PriceTick) * PriceTick;

			if (this.Operations.Count == 0)
			{
				order = new OrderItem
				{
					Date = this[CurrentBar].Time,
					Dir = pDirector,
					Price = pPrice,
					Lots = pLots,
					Remark = pRemark
				};
			}
			else
			{
				order = new OrderItem
				{
					Date = this[this.CurrentBar].Time,
					Dir = pDirector,
					Price = pPrice,
					Lots = pLots,
					Remark = pRemark,

                    IndexEntry = this.LastOrder.IndexEntry,
					IndexExit = this.LastOrder.IndexExit,
					IndexLastEntry = this.LastOrder.IndexLastEntry,

					AvgEntryPrice = this.LastOrder.AvgEntryPrice,
					Position = this.LastOrder.Position,
					EntryTime = this.LastOrder.EntryTime,
					EntryPrice = this.LastOrder.EntryPrice,
					ExitDate = this.LastOrder.ExitDate,
					ExitPrice = this.LastOrder.ExitPrice,
					LastEntryDate = this.LastOrder.LastEntryDate,
					LastEntryPrice = this.LastOrder.LastEntryPrice,
				};
			}

			switch (string.Format("{0}{1}", pDirector))
			{
				case "BuyOpen":
					order.Position += pLots;

					order.AvgEntryPrice = (this.LastOrder.Position * this.LastOrder.AvgEntryPrice + pLots * pPrice) / order.Position;
					if (this.LastOrder.Position == 0)
					{
						order.IndexEntry = this.CurrentBar;
						order.EntryTime = this.Date[0];
						order.EntryTime = this.Time[0];
						order.EntryPrice = pPrice;
					}
					order.IndexLastEntry = this.CurrentBar;
					order.LastEntryDate = this.Date[0];
					order.LastEntryTime = this.Time[0];
					order.LastEntryPrice = pPrice;
					break;
				case "SellClose":
					if ((pLots = Math.Min(this.Position, pLots)) <= 0)
						return;

					order.Position -= pLots;

					order.IndexExit = this.CurrentBar;
					order.ExitDate = this.Date[0];
					order.ExitTime = this.Time[0];
					order.ExitPrice = pPrice;
					break;
			}
			//this.LastOrder = order;

			this.Operations.Add(order);

			_rtnOrder?.Invoke(order, this);
		}


		private RtnOrder _rtnOrder;

		/// <summary>
		/// 
		/// </summary>
		internal event RtnOrder OnRtnOrder
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

		#region 策略状态信息

		/// <summary>
		/// 当前持仓手数:多
		/// </summary>
		[Description("当前持仓手数:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int Position { get => this.LastOrder.Position; }


		/// <summary>
		/// 当前持仓手数:净
		/// </summary>
		[Description("当前持仓手数:净"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int PositionNet { get => this.LastOrder.Position; }

		/// <summary>
		/// 当前持仓首个建仓时间:多(yyyyMMdd.HHmmss)
		/// </summary>
		[Description("当前持仓首个建仓时间:多(yyyyMMdd.HHmmss)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double EntryDateLong { get => this.LastOrder.EntryTime; }

		/// <summary>
		/// 当前持仓最后建仓时间:多(yyyyMMdd.HHmmss)
		/// </summary>
		[Description("当前持仓最后建仓时间:多(yyyyMMdd.HHmmss)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double LastEntryDate { get => this.LastOrder.LastEntryDate; }

		/// <summary>
		/// 当前持仓首个建仓价格:多
		/// </summary>
		[Description("当前持仓首个建仓价格:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double EntryPrice { get => this.LastOrder.EntryPrice; }

		/// <summary>
		/// 当前持仓最后建仓价格:多
		/// </summary>
		[Description("当前持仓最后建仓价格:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double LastEntryPrice { get => this.LastOrder.LastEntryPrice; }

		/// <summary>
		/// 当前持仓平均建仓价格:多
		/// </summary>
		[Description("当前持仓平均建仓价格:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double AvgEntryPrice { get => this.LastOrder.AvgEntryPrice; }

		/// <summary>
		/// 当前持仓首个建仓到当前位置的Bar数:多(从0开始计数)
		/// </summary>
		[Description("当前持仓首个建仓到当前位置的Bar数:多(从0开始计数)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int BarsSinceEntryLong { get => this.CurrentBar - this.LastOrder.IndexEntry; }

		/// <summary>
		/// 当前持仓的最后建仓到当前位置的Bar计数:多(从0开始计数)
		/// </summary>
		[Description("当前持仓的最后建仓到当前位置的Bar计数:多(从0开始计数)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int BarsSinceLastEntryLong { get => this.CurrentBar - this.LastOrder.IndexLastEntry; }

		/// <summary>
		/// 最近平仓位置到当前位置的Bar计数:多(从0开始计数)
		/// </summary>
		[Description("最近平仓位置到当前位置的Bar计数:多(从0开始计数)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public int BarsSinceExitLong { get => this.CurrentBar - this.LastOrder.IndexExit; }

		/// <summary>
		/// 最近平仓时间:多(yyyyMMdd.HHmmss)
		/// </summary>
		[Description("平仓时间:多(yyyyMMdd.HHmmss)"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double ExitDate { get => this.LastOrder.ExitDate; }

		/// <summary>
		/// 最近平仓价格:多
		/// </summary>
		[Description("平仓价格:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double ExitPrice { get => this.LastOrder.ExitPrice; }

		/// <summary>
		/// 当前持仓浮动盈亏:多
		/// </summary>
		[Description("浮动盈亏:多"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double PositionProfitLong { get => this.Count == 0 ? 0 : ((this.Close[0] - this.LastOrder.AvgEntryPrice) * this.LastOrder.Position); }

		/// <summary>
		/// 当前持仓浮动盈亏:净
		/// </summary>
		[Description("浮动盈亏:净"), Category("状态"), ReadOnly(true), Browsable(false)]
		public double PositionProfit { get => this.PositionProfitLong ; }

		#endregion
	}
}
