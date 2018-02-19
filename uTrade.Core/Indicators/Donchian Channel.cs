﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using uTrade.Core;

// This namespace holds indicators in this folder and is required. Do not change it.
namespace uTrade.Core
{
	/// <summary>
	/// 唐奇安通道指标
	/// Donchian Channel. The Donchian Channel indicator was created by Richard Donchian.
	///  It uses the highest high and the lowest low of a period of time to plot the channel.
	/// </summary>
	public class DonchianChannel : Indicator
	{
		private Highest max;
		private Lowest min;
		internal DataSeries High, Low;
		//DataSeries Close;   //只在Close被修改时才会触发指标计算,以避免多个input造成指标计算多次的性能问题.

		protected override void Init()
		{
			max = Highest(High, Period);
			min = Lowest(Low, Period);
		}

		protected override void OnBarUpdate()
		{
			double max0 = max[0];
			double min0 = min[0];

			Value[0] = (max0 + min0) / 2;
			Upper[0] = max0;
			Lower[0] = min0;
		}

		#region Properties
		[Range(1, int.MaxValue)]
		[Parameter("Period")]
		public int Period { get; set; }

		[Browsable(false)]
		public DataSeries Lower { get { return Values[2]; } }

		[Browsable(false)]
		public DataSeries Mean { get { return Values[0]; } }

		[Browsable(false)]
		public DataSeries Upper { get { return Values[1]; } }
		#endregion
	}

	#region generated code. Neither change nor remove.

	public partial class Indicator
	{
		private DonchianChannel[] cacheDonchianChannel;

		public DonchianChannel DonchianChannel(DataSeries high, DataSeries low, DataSeries close, int period)
		{
			var cat = cacheDonchianChannel;
			if (cacheDonchianChannel != null)
				for (int idx = 0; idx < cat.Length; idx++)
					if (cat[idx] != null && cat[idx].Period == period && cat[idx].High == high && cat[idx].Low == low && cat[idx].EqualsInput(close))
						return cat[idx];
			return CacheIndicator(new DonchianChannel() { Period = period, High = high, Low = low, Input = close }, ref cacheDonchianChannel);
		}
	}

	public partial class Strategy
	{
		public DonchianChannel DonchianChannel(int period)
		{
			return DonchianChannel(Datas[0], period);
		}
		public DonchianChannel DonchianChannel(Datas data, int period)
		{
			return Indicator.DonchianChannel(data.High, data.Low, data.Close, period);
		}
	}
}

#endregion
