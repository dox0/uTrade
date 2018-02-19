using Alveo.Interfaces.UserCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	public class TimeCycle : IndicatorBase
	{
		private Array<double> bullishTime;

		private Array<double> bearishTime;

		private Array<double> bothTime;

		private Dictionary<int, double> pivotHighs;

		private Dictionary<int, double> pivotLows;

		private Array<double> bullishBars;

		private Array<double> bearishBars;

		private Array<double> bothBars;

		[Category("Settings"), DisplayName("Time Variation")]
		public int timeDiff
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Pip Variation")]
		public double pipDiff
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Period")]
		public int period
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Bullish")]
		public bool useBullTime
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Bearish")]
		public bool useBearTime
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Combined")]
		public bool useBothTime
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Bullish Color")]
		public Color bullColor
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Bearish Color")]
		public Color bearColor
		{
			get;
			set;
		}

		[Category("Settings"), DisplayName("Combined Color")]
		public Color bothColor
		{
			get;
			set;
		}

		public TimeCycle()
		{
			base.indicator_buffers = 0;
			base.indicator_chart_window = false;
			this.timeDiff = 3;
			this.pipDiff = 0.8;
			this.period = 3;
			this.useBullTime = true;
			this.useBearTime = false;
			this.useBothTime = false;
			this.bullColor = Colors.Green;
			this.bearColor = Colors.Blue;
			this.bothColor = Colors.Red;
			this.pivotHighs = new Dictionary<int, double>();
			this.pivotLows = new Dictionary<int, double>();
		}

		protected override int Init()
		{
			this.bullishBars = new Array<double>();
			this.bearishBars = new Array<double>();
			this.bothBars = new Array<double>();
			for (int i = 0; i < base.indicator_buffers; i++)
			{
				base.SetIndexBuffer(i, null, false);
			}
			base.indicator_buffers = 0;
			bool useBullTime = this.useBullTime;
			if (useBullTime)
			{
				int indicator_buffers = base.indicator_buffers;
				base.indicator_buffers = indicator_buffers + 1;
				this.bullishTime = new Array<double>();
				base.SetIndexStyle(base.indicator_buffers - 1, 1, 0, 1, this.bullColor);
				base.SetIndexBuffer(base.indicator_buffers - 1, this.bullishTime, false);
				base.SetIndexLabel(base.indicator_buffers - 1, "Bull Time Cycle");
			}
			bool useBearTime = this.useBearTime;
			if (useBearTime)
			{
				int indicator_buffers = base.indicator_buffers;
				base.indicator_buffers = indicator_buffers + 1;
				this.bearishTime = new Array<double>();
				base.SetIndexStyle(base.indicator_buffers - 1, 1, 0, 1, this.bearColor);
				base.SetIndexBuffer(base.indicator_buffers - 1, this.bearishTime, false);
				base.SetIndexLabel(base.indicator_buffers - 1, "Bear Time Cycle");
			}
			bool useBothTime = this.useBothTime;
			if (useBothTime)
			{
				int indicator_buffers = base.indicator_buffers;
				base.indicator_buffers = indicator_buffers + 1;
				this.bothTime = new Array<double>();
				base.SetIndexStyle(base.indicator_buffers - 1, 1, 0, 1, this.bothColor);
				base.SetIndexBuffer(base.indicator_buffers - 1, this.bothTime, false);
				base.SetIndexLabel(base.indicator_buffers - 1, "Combined Time Cycle");
			}
			return 0;
		}

		protected override int Deinit()
		{
			return 0;
		}

		protected override int Start()
		{
			int i;
			for (i = base.Bars - 1; i >= 0; i--)
			{
				this.pivotHighs[i] = base.iPivots(base.Symbol, base.TimeFrame, this.pipDiff, this.timeDiff, 1, i);
				this.pivotLows[i] = base.iPivots(base.Symbol, base.TimeFrame, this.pipDiff, this.timeDiff, 2, i);
				bool flag = this.pivotHighs[i] != 2147483647.0 || this.pivotLows[i] != 2147483647.0;
				if (flag)
				{
				}
			}
			this.bullishBars.Clear();
			this.bearishBars.Clear();
			this.bothBars.Clear();
			try
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				for (int j = base.Bars - 1; j >= i; j--)
				{
					if (this.useBothTime)
					{
						bool flag2 = this.bothBars.Count < this.period;
						if (!flag2)
						{
							bool flag3 = this.bothBars.Count % this.period != 0;
							if (!flag3)
							{
								double num4 = 0.0;
								for (int k = this.bothBars.Count - this.period; k < this.bothBars.Count; k++)
								{
									num4 += this.bothBars[k, true];
								}
								double value = num4 / (double)this.period;
								for (int l = num3; l >= j; l--)
								{
									this.bothTime[l, true] = value;
								}
							}
						}
					}
					bool flag4 = !this.pivotLows.ContainsKey(j) || this.pivotLows[j] == 2147483647.0;
					if (!flag4)
					{
						bool flag5 = this.bullishBars.Count % this.period == 0;
						if (flag5)
						{
							num = j;
						}
						int num5 = j;
						while (num5 >= 0 && this.pivotHighs[num5] == 2147483647.0)
						{
							num5--;
						}
						bool flag6 = !this.pivotHighs.ContainsKey(num5) || this.pivotHighs[num5] == 2147483647.0;
						if (!flag6)
						{
							double val = (double)Math.Abs(j - num5);
							this.bullishBars.Add(val);
							bool useBothTime = this.useBothTime;
							if (useBothTime)
							{
								bool flag7 = this.bothBars.Count % this.period == 0;
								if (flag7)
								{
									num3 = j;
								}
								this.bothBars.Add(val);
							}
							bool flag8 = !this.useBullTime;
							if (!flag8)
							{
								bool flag9 = this.bullishBars.Count < this.period;
								if (!flag9)
								{
									bool flag10 = this.bullishBars.Count % this.period != 0;
									if (!flag10)
									{
										double num6 = 0.0;
										for (int m = this.bullishBars.Count - this.period; m < this.bullishBars.Count; m++)
										{
											num6 += this.bullishBars[m, true];
										}
										double value2 = num6 / (double)this.period;
										for (int n = num; n >= num5; n--)
										{
											this.bullishTime[n, true] = value2;
										}
									}
								}
							}
						}
					}
					bool flag11 = !this.pivotHighs.ContainsKey(j) || this.pivotHighs[j] == 2147483647.0;
					if (!flag11)
					{
						bool flag12 = this.bearishBars.Count % this.period == 0;
						if (flag12)
						{
							num2 = j;
						}
						int num7 = j;
						while (num7 >= 0 && this.pivotLows[num7] == 2147483647.0)
						{
							num7--;
						}
						bool flag13 = !this.pivotLows.ContainsKey(num7) || this.pivotLows[num7] == 2147483647.0;
						if (!flag13)
						{
							double val2 = (double)Math.Abs(j - num7);
							this.bearishBars.Add(val2);
							bool useBothTime2 = this.useBothTime;
							if (useBothTime2)
							{
								bool flag14 = this.bothBars.Count % this.period == 0;
								if (flag14)
								{
									num3 = j;
								}
								this.bothBars.Add(val2);
							}
							bool flag15 = !this.useBearTime;
							if (!flag15)
							{
								bool flag16 = this.bearishBars.Count < this.period;
								if (!flag16)
								{
									bool flag17 = this.bearishBars.Count % this.period != 0;
									if (!flag17)
									{
										double num8 = 0.0;
										for (int num9 = this.bearishBars.Count - this.period; num9 < this.bearishBars.Count; num9++)
										{
											num8 += this.bearishBars[num9, true];
										}
										double value3 = num8 / (double)this.period;
										for (int num10 = num2; num10 >= num7; num10--)
										{
											this.bearishTime[num10, true] = value3;
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception var_54_5F2)
			{
			}
			return 0;
		}

		[Description("Parameters order Symbol, TimeFrame")]
		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 2;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !base.CompareString(base.Symbol, (string)values[0], false);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = base.TimeFrame != (int)values[1];
					result = !flag3;
				}
			}
			return result;
		}

		[Description("Parameters order Symbol, TimeFrame")]
		public override void SetIndicatorParameters(params object[] values)
		{
			bool flag = values.Length != 2;
			if (flag)
			{
				throw new ArgumentException("Invalid parameters number");
			}
			base.Symbol = (string)values[0];
			base.TimeFrame = (int)values[1];
		}
	}
}
