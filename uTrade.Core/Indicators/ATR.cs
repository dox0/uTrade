﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using uTrade.Core;

// This namespace holds indicators in this folder and is required. Do not change it.
namespace uTrade.Core
{
	/// <summary>
	/// The Average True Range (ATR) is a measure of volatility. It was introduced by Welles Wilder 
	/// in his book 'New Concepts in Technical Trading Systems' and has since been used as a component 
	/// of many indicators and trading systems.
	/// </summary>
	public class ATR : Indicator
	{
		internal DataSeries High, Low;
		DataSeries Close;   //只在Close被修改时才会触发指标计算,以避免多个input造成指标计算多次的性能问题.

		/// <summary>
		/// 
		/// </summary>
		protected override void Init()
		{
			Close = Input;
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnBarUpdate()
		{
			double high0 = High[0];
			double low0 = Low[0];

			if (CurrentBar == 0)
				Value[0] = high0 - low0;
			else
			{
				double close1 = Close[1];
				double trueRange = Math.Max(Math.Abs(low0 - close1), Math.Max(high0 - low0, Math.Abs(high0 - close1)));
				Value[0] = ((Math.Min(CurrentBar + 1, Period) - 1) * Value[1] + trueRange) / Math.Min(CurrentBar + 1, Period);
			}
		}

		#region Properties
		/// <summary>
		/// 参数
		/// </summary>
		[Range(1, int.MaxValue)]
		[Parameter("Period", "NinjaScriptParameters")]
		public int Period { get; set; }
		#endregion
	}

	#region generated code. Neither change nor remove.

	public partial class Indicator
	{
		private ATR[] cacheATR;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="high"></param>
		/// <param name="low"></param>
		/// <param name="close"></param>
		/// <param name="period"></param>
		/// <returns></returns>
		public ATR ATR(DataSeries high, DataSeries low, DataSeries close, int period)
		{
			var cat = cacheATR;
			if (cacheATR != null)
				for (int idx = 0; idx < cacheATR.Length; idx++)
					if (cacheATR[idx] != null && cacheATR[idx].Period == period && cat[idx].High == high && cat[idx].Low == low && cacheATR[idx].EqualsInput(close))
						return cacheATR[idx];
			return CacheIndicator<ATR>(new ATR() { Period = period, High = high, Low = low, Input = close }, ref cacheATR);
		}
	}
	public partial class Strategy
	{
		public ATR ATR(int period)
		{
			return ATR(Datas[0], period);
		}
		public ATR ATR(Datas data, int period)
		{
			return Indicator.ATR(data.High, data.Low, data.Close, period);
		}
	}
}

#endregion
