﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using uTrade.Core;

// This namespace holds indicators in this folder and is required. Do not change it.
namespace uTrade.Core
{
	/// <summary>
	/// The Williams %R is a momentum indicator that is designed to identify overbought and oversold areas in a nontrending market.
	/// </summary>
	public class WilliamsR : Indicator
	{
		private Highest max;
		private Lowest min;

		internal DataSeries High, Low;
		DataSeries Close;

		protected override void Init()
		{
			Close = Input;

			max = Highest(High, Period);
			min = Lowest(Low, Period);
		}

		protected override void OnBarUpdate()
		{
			double max0 = max[0];
			double min0 = min[0];
			Value[0] = -100 * (max0 - Close[0]) / (max0 - min0 == 0 ? 1 : max0 - min0);
		}

		#region Properties
		[Range(1, int.MaxValue)]
		[Parameter("Period")]
		public int Period { get; set; }
		#endregion
	}


	#region generated code. Neither change nor remove.

	public partial class Indicator
	{
		private WilliamsR[] cacheWilliamsR;

		public WilliamsR WilliamsR(DataSeries high, DataSeries low, DataSeries close, int period)
		{
			var cat = cacheWilliamsR;
			if (cacheWilliamsR != null)
				for (int idx = 0; idx < cacheWilliamsR.Length; idx++)
					if (cacheWilliamsR[idx] != null && cacheWilliamsR[idx].Period == period && cat[idx].High == high && cat[idx].Low == low && cacheWilliamsR[idx].EqualsInput(close))
						return cacheWilliamsR[idx];
			return CacheIndicator<WilliamsR>(new WilliamsR() { Period = period, High = high, Low = low, Input = close }, ref cacheWilliamsR);
		}
	}

	public partial class Strategy
	{
		public WilliamsR WilliamsR(int period)
		{
			return WilliamsR(Datas[0], period);
		}

		public WilliamsR WilliamsR(Datas data, int period)
		{
			return Indicator.WilliamsR(data.High, data.Low, data.Close, period);
		}
	}
}

#endregion
