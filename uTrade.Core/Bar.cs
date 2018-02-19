using System;
using System.ComponentModel;

namespace uTrade.Core
{

	/// <summary>
	/// </summary>
	public class Bar
	{
		/// <summary>
		/// 时间
		/// </summary>
		[Category("字段"), ReadOnly(true)]
		public DateTime Time { get; set; }

		/// <summary>
		/// 交易日
		/// </summary>
		public int TradingDay { get; set; }

		/// <summary>
		/// 开盘价
		/// </summary>
		[Description("开盘价"), Category("字段"), ReadOnly(true)]
		public double Open { get; set; }

		/// <summary>
		/// 最高价
		/// </summary>
		[Description("最高价"), Category("字段"), ReadOnly(true)]
		public double High { get; set; }

		/// <summary>
		/// 最低价
		/// </summary>
		[Description("最低价"), Category("字段"), ReadOnly(true)]
		public double Low { get; set; }

		/// <summary>
		/// 收盘价
		/// </summary>
		[Description("收盘价"), Category("字段"), ReadOnly(true)]
		public double Close { get; set; }

		/// <summary>
		/// 成交量
		/// </summary>
		[Description("成交量"), Category("字段"), ReadOnly(true)]
		public double Volume { get; set; }

		/// <summary>
		/// 前Bar的成交量:只用于中间计算
		/// </summary>
		internal double PreVol { get; set; }
	}
}
