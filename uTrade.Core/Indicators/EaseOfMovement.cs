﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using uTrade.Core;

// This namespace holds indicators in this folder and is required. Do not change it.
namespace uTrade.Core
{
	/// <summary>
	/// 简易波动指标
	/// The Ease of Movement (EMV) indicator emphasizes days in which the stock is moving
	///  easily and minimizes the days in which the stock is finding it difficult to move. 
	/// A buy signal is generated when the EMV crosses above zero, a sell signal when it
	///  crosses below zero. When the EMV hovers around zero, then there are small price 
	/// movements and/or high volume, which is to say, the price is not moving easily.
	/// </summary>
	public class EaseOfMovement : Indicator
	{
		private EMA ema;
		private DataSeries emv, Median;
		internal DataSeries High, Low, Volume;
		//DataSeries Close;   //只在Close被修改时才会触发指标计算,以避免多个input造成指标计算多次的性能问题.

		protected override void Init()
		{
			Median = new DataSeries(Input);

			emv = new DataSeries(Input);
			ema = EMA(emv, Smoothing);
		}

		protected override void OnBarUpdate()
		{
			Median[0] = (High[0] + Low[0]) / 2;
			if (CurrentBar == 0)
				return;

			double midPoint = Median[0] - Median[1];
			double boxRatio = (Volume[0] / VolumeDivisor) / (High[0] - Low[0]);

			emv[0] = boxRatio.ApproxCompare(0) == 0 ? 0 : midPoint / boxRatio;
			Value[0] = ema[0];
		}

		#region Properties
		[Range(1, int.MaxValue)]
		[Parameter("Smoothing")]
		public int Smoothing { get; set; }

		[Range(1, int.MaxValue)]
		[Parameter("VolumeDivisor")]
		public int VolumeDivisor { get; set; }
		#endregion
	}

	#region generated code. Neither change nor remove.

	public partial class Indicator
	{
		private EaseOfMovement[] cacheEaseOfMovement;

		public EaseOfMovement EaseOfMovement(DataSeries high, DataSeries low, DataSeries volume, DataSeries close, int smoothing, int volumeDivisor)
		{
			if (cacheEaseOfMovement != null)
			{
				var cat = cacheEaseOfMovement;
				for (int idx = 0; idx < cacheEaseOfMovement.Length; idx++)
					if (cacheEaseOfMovement[idx] != null && cacheEaseOfMovement[idx].Smoothing == smoothing && cacheEaseOfMovement[idx].VolumeDivisor == volumeDivisor && cat[idx].High == high && cat[idx].Low == low && cat[idx].Volume == volume && cacheEaseOfMovement[idx].EqualsInput(close))
						return cacheEaseOfMovement[idx];
			}
			return CacheIndicator<EaseOfMovement>(new EaseOfMovement() { Smoothing = smoothing, VolumeDivisor = volumeDivisor, High = high, Low = low, Volume = volume, Input = close }, ref cacheEaseOfMovement);
		}
	}

	public partial class Strategy
	{
		public EaseOfMovement EaseOfMovement(int smoothing, int volumeDivisor)
		{
			return EaseOfMovement(Datas[0], smoothing, volumeDivisor);
		}
		public EaseOfMovement EaseOfMovement(Datas data, int smoothing, int volumeDivisor)
		{
			return Indicator.EaseOfMovement(data.High, data.Low, data.Volume, data.Close, smoothing, volumeDivisor);
		}
	}
}

#endregion
