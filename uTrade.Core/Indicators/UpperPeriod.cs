using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using uTrade.Core;

namespace uTrade.Core
{
	public class UpperPeriod : Indicator
	{
		DataSeries close;
		Bar bar;
		private bool newbar = false;

		protected override void Init()
		{
			close = Input;
		}

		protected override void OnBarUpdate()
		{
			DateTime dtBegin = DateTime.MaxValue;
			switch (IntervalType)
			{
				case IntervalType.Sec:
					dtBegin = MinBar.Time.Date.AddHours(MinBar.Time.Hour).AddMinutes(MinBar.Time.Minute).AddSeconds(MinBar.Time.Second / Interval * Interval);
					break;
				case IntervalType.Min:
					dtBegin = MinBar.Time.Date.AddHours(MinBar.Time.Hour).AddMinutes(MinBar.Time.Minute / Interval * Interval);
					break;
				case IntervalType.Hour:
					dtBegin = MinBar.Time.Date.AddHours(MinBar.Time.Hour / Interval * Interval);
					break;
				case IntervalType.Day:
					dtBegin = DateTime.ParseExact(MinBar.TradingDay.ToString(), "yyyyMMdd", null);
					break;
				case IntervalType.Week:
					dtBegin = DateTime.ParseExact(MinBar.TradingDay.ToString(), "yyyyMMdd", null);
					dtBegin = dtBegin.Date.AddDays(1 - (byte)dtBegin.DayOfWeek);
					break;
				case IntervalType.Month:
					dtBegin = DateTime.ParseExact(MinBar.TradingDay.ToString(), "yyyyMMdd", null);
					dtBegin = new DateTime(dtBegin.Year, dtBegin.Month, 1);
					break;
				case IntervalType.Year:
					dtBegin = DateTime.ParseExact(MinBar.TradingDay.ToString(), "yyyyMMdd", null);
					dtBegin = new DateTime(dtBegin.Year, 1, 1);
					break;
				default:
					throw new Exception("参数错误");
			}
			if (bar == null) //首次调用
			{
				bar = new Bar
				{
                    Time = dtBegin,
					Volume = MinBar.Volume, // kOld.preVol == 0 ? 0 : _tick.Volume - kOld.preVol;
				};
				bar.High = MinBar.High;
				bar.Low = MinBar.Low;
				bar.Open = MinBar.Open;
				bar.Close = MinBar.Close;
				newbar = true;
			}
			else
			{
				if (bar.Time == dtBegin) //在当前K线范围内
				{
					newbar = false;
					bar.High = Math.Max(bar.High, MinBar.High);
					bar.Low = Math.Min(bar.Low, MinBar.Low);
					bar.Close = MinBar.Close;
					bar.Volume = bar.Volume + MinBar.Volume;
				}
				else if (dtBegin > bar.Time)
				{
					newbar = true;
					bar.Time = dtBegin;
					bar.Volume = MinBar.Volume;
					bar.Open = MinBar.Open;
					bar.High = MinBar.High;
					bar.Low = MinBar.Low;
					bar.Close = MinBar.Close;
				}
			}

			if (newbar)
			{
				Date.Add(double.Parse(bar.Time.ToString("yyyyMMdd")));
				Time.Add(double.Parse(bar.Time.ToString("0.HHmmss")));
				Open.Add(bar.Open);
				High.Add(bar.High);
				Low.Add(bar.Low);
				Close.Add(bar.Close);
				Volume.Add(bar.Volume);
			}
			else
			{
				High[0] = bar.High;
				Low[0] = bar.Low;
				Close[0] = bar.Close;
				Volume[0] = bar.Volume;
			}
		}


		public DataSeries Date { get; set; } = new DataSeries();
		public DataSeries Time { get; set; } = new DataSeries();
		public DataSeries Open { get; set; } = new DataSeries();
		public DataSeries High { get; set; } = new DataSeries();
		public DataSeries Low { get; set; } = new DataSeries();
		public DataSeries Close { get; set; } = new DataSeries();
		public DataSeries Volume { get; set; } = new DataSeries();


		#region Properties
		public Bar MinBar { get; set; }

		[Range(1, int.MaxValue)]
		[Parameter("Interval")]
		public int Interval { get; set; } = 5;

		[Range(1, int.MaxValue)]
		[Parameter("IntervalType")]
		public IntervalType IntervalType { get; set; } = IntervalType.Min;
		#endregion
	}


	#region generated code. Neither change nor remove.

	public partial class Indicator
	{
		private UpperPeriod[] cacheUpperPeriod;

		public UpperPeriod UpperPeriod(DataSeries close, Bar curMinBar, int interval, IntervalType type)
		{
			if (cacheUpperPeriod != null)
				for (int idx = 0; idx < cacheUpperPeriod.Length; idx++)
					if (cacheUpperPeriod[idx] != null && cacheUpperPeriod[idx].Interval == interval && cacheUpperPeriod[idx].IntervalType == type && cacheUpperPeriod[idx].EqualsInput(close))
						return cacheUpperPeriod[idx];
			return CacheIndicator(new UpperPeriod() { Interval = interval, IntervalType = type, MinBar = curMinBar, Input = close }, ref cacheUpperPeriod);
		}
	}
	#endregion

	public partial class Strategy
	{
		public UpperPeriod UpperPeriod(int inteval, IntervalType type)
		{
			return UpperPeriod(Datas[0], inteval, type);
		}

		public UpperPeriod UpperPeriod(Datas data, int inteval, IntervalType type)
		{
			return Indicator.UpperPeriod(data.Close, data.CurrentMinBar, inteval, type);
		}
	}
}
