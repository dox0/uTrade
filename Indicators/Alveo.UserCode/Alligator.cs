using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Alveo.UserCode
{
	[Description("Bill Williams' Alligator Indicator")]
	[Serializable]
	public class Alligator : IndicatorBase
	{
		private readonly Array<double> _jawBuff;

		private readonly Array<double> _lipsBuff;

		private readonly Array<double> _teethBuff;

		[Category("Settings"), Description("Jaw Period of the Alligator Indicator"), DisplayName("Jaw Period")]
		public int JawPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Jaw Shift of the Alligator Indicator"), DisplayName("Jaw Shift")]
		public int JawShift
		{
			get;
			set;
		}

		[Category("Settings"), Description("Teeth Period of the Alligator Indicator"), DisplayName("Teeth Period")]
		public int TeethPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Teeth Shift of the Alligator Indicator"), DisplayName("Teeth Shift")]
		public int TeethShift
		{
			get;
			set;
		}

		[Category("Settings"), Description("Lips Period of the Alligator Indicator"), DisplayName("Lips Period")]
		public int LipsPeriod
		{
			get;
			set;
		}

		[Category("Settings"), Description("Lips Shift of the Alligator Indicator"), DisplayName("Lips Shift")]
		public int LipsShift
		{
			get;
			set;
		}

		[Category("Settings"), Description("Moving Average type on witch Alligator will be calculated"), DisplayName("MA Type")]
		public MovingAverageType MAType
		{
			get;
			set;
		}

		[Category("Settings"), Description("Price type on witch Alligator will be calculated"), DisplayName("Price Type")]
		public PriceConstants PriceType
		{
			get;
			set;
		}

		public Alligator()
		{
			base.indicator_buffers = 3;
			base.indicator_chart_window = true;
			this.JawPeriod = 13;
			this.JawShift = 8;
			this.TeethPeriod = 8;
			this.TeethShift = 5;
			this.LipsPeriod = 5;
			this.LipsShift = 3;
			this.MAType = MovingAverageType.MODE_SMA;
			this.PriceType = PriceConstants.PRICE_MEDIAN;
			base.SetIndexLabel(0, string.Format("Jaw({0},{1})", this.JawPeriod, this.JawShift));
			base.indicator_color1 = Colors.Blue;
			base.SetIndexLabel(1, string.Format("Teeth({0},{1})", this.TeethPeriod, this.TeethShift));
			base.indicator_color2 = Colors.Red;
			base.SetIndexLabel(2, string.Format("Lips({0},{1})", this.LipsPeriod, this.LipsShift));
			base.indicator_color3 = Colors.Lime;
			base.IndicatorShortName("Alligator");
			this._jawBuff = new Array<double>();
			this._teethBuff = new Array<double>();
			this._lipsBuff = new Array<double>();
		}

		protected override int Init()
		{
			base.SetIndexLabel(0, string.Format("Jaw({0},{1})", this.JawPeriod, this.JawShift));
			base.SetIndexLabel(1, string.Format("Teeth({0},{1})", this.TeethPeriod, this.TeethShift));
			base.SetIndexLabel(2, string.Format("Lips({0},{1})", this.LipsPeriod, this.LipsShift));
			base.IndicatorShortName("Alligator");
			base.SetIndexBuffer(0, this._jawBuff, false);
			base.SetIndexBuffer(1, this._teethBuff, false);
			base.SetIndexBuffer(2, this._lipsBuff, false);
			return 0;
		}

		protected override int Start()
		{
			int i = base.Bars - base.IndicatorCounted();
			bool flag = i >= base.Bars;
			if (flag)
			{
				i = base.Bars - 1;
			}
			while (i >= 0)
			{
				this._jawBuff[i, true] = base.iMA(base.Symbol, base.TimeFrame, this.JawPeriod, this.JawShift, (int)this.MAType, (int)this.PriceType, i);
				this._teethBuff[i, true] = base.iMA(base.Symbol, base.TimeFrame, this.TeethPeriod, this.TeethShift, (int)this.MAType, (int)this.PriceType, i);
				this._lipsBuff[i, true] = base.iMA(base.Symbol, base.TimeFrame, this.LipsPeriod, this.LipsShift, (int)this.MAType, (int)this.PriceType, i);
				i--;
			}
			return 0;
		}

		public override bool IsSameParameters(params object[] values)
		{
			bool flag = values.Length != 10;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = (values[0] != null && base.Symbol == null) || (values[0] == null && base.Symbol != null);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = values[0] != null && (!(values[0] is string) || (string)values[0] != base.Symbol);
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = !(values[1] is int) || (int)values[1] != base.TimeFrame;
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = !(values[2] is int) || (int)values[2] != this.JawPeriod;
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !(values[3] is int) || (int)values[3] != this.JawShift;
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = !(values[4] is int) || (int)values[4] != this.TeethPeriod;
									if (flag7)
									{
										result = false;
									}
									else
									{
										bool flag8 = !(values[5] is int) || (int)values[5] != this.TeethShift;
										if (flag8)
										{
											result = false;
										}
										else
										{
											bool flag9 = !(values[6] is int) || (int)values[6] != this.LipsPeriod;
											if (flag9)
											{
												result = false;
											}
											else
											{
												bool flag10 = !(values[7] is int) || (int)values[7] != this.LipsShift;
												if (flag10)
												{
													result = false;
												}
												else
												{
													bool flag11 = !(values[8] is MovingAverageType) || (MovingAverageType)values[8] != this.MAType;
													if (flag11)
													{
														result = false;
													}
													else
													{
														bool flag12 = !(values[9] is PriceConstants) || (PriceConstants)values[9] != this.PriceType;
														result = !flag12;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}
