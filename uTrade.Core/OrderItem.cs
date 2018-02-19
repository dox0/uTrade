using System;
using System.ComponentModel;

namespace uTrade.Core
{

	/// <summary>
	/// 交易报单
	/// </summary>
	public class OrderItem
	{
		//在策略中被赋值
		internal int IndexEntry;
		internal int IndexLastEntry;
		internal int IndexExit;

		/// <summary>
		/// 时间
		/// </summary>
		[Description("时间:yyyyMMdd.HHmmss"), Category("字段"), ReadOnly(true)]
		public DateTime Date { get; set; }

		/// <summary>
		/// 买卖
		/// </summary>
		[Description("买卖"), Category("字段"), ReadOnly(true)]
		public Direction Dir { get; set; }

		/// <summary>
		/// 价格
		/// </summary>
		[Description("价格"), Category("字段"), ReadOnly(true)]
		public double Price { get; set; }

		/// <summary>
		/// 手数
		/// </summary>
		[Description("手数"), Category("字段"), ReadOnly(true)]
		public int Lots { get; set; }

		/// <summary>
		/// 注释
		/// </summary>
		[Description("说明"), Category("字段"), ReadOnly(true)]



		public string Remark { get; set; }

		internal double ExitTime { get; set; }

		internal double LastEntryTime { get; set; }

		internal double AvgEntryPrice { get; set; }
		
		internal int Position { get; set; }

		internal double EntryTime { get; set; }

		internal double EntryPrice { get; set; }

		internal double ExitDate { get; set; }

		internal double ExitPrice { get; set; }

		internal double LastEntryDate { get; set; }

		internal double LastEntryPrice { get; set; }

	}
}