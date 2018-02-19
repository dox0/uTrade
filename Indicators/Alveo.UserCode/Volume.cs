using Alveo.Interfaces.UserCode;
using System;
using System.ComponentModel;

namespace Alveo.UserCode
{
	[Description("Volume indicator")]
	[Serializable]
	public class Volume : IndicatorBase
	{
		private readonly Array<double> _volume = new Array<double>();

		public Volume()
		{
			base.indicator_buffers = 1;
			base.indicator_separate_window = true;
			base.SetIndexStyle(0, 2, 0, 4, "#0000FF");
		}

		protected override int Init()
		{
			base.SetIndexBuffer(0, this._volume, false);
			base.SetIndexLabel(0, base.Chart.Symbol + "_Volume");
			return 0;
		}

		protected override int Deinit()
		{
			return 0;
		}

		protected override int Start()
		{
			int num = base.IndicatorCounted();
			for (int i = num; i < base.Bars; i++)
			{
				this._volume[i, false] = base.Volume[i, false];
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
				try
				{
					string value = (string)values[0];
					bool flag2 = !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(base.Symbol);
					if (flag2)
					{
						bool flag3 = !base.Symbol.Equals(value, StringComparison.OrdinalIgnoreCase);
						if (flag3)
						{
							result = false;
							return result;
						}
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(base.Symbol) || !string.IsNullOrEmpty(value);
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					int num = (int)values[1];
					bool flag5 = num != base.TimeFrame;
					if (flag5)
					{
						result = false;
						return result;
					}
				}
				catch (Exception exception)
				{
					this.Logger.ErrorException("IsSameParameters", exception);
				}
				result = true;
			}
			return result;
		}

		[Description("Parameters order Symbol, TimeFrame")]
		public override void SetIndicatorParameters(params object[] values)
		{
			bool flag = values.Length != 2;
			if (!flag)
			{
				try
				{
					base.Symbol = (string)values[0];
					base.TimeFrame = (int)values[1];
				}
				catch (Exception exception)
				{
					this.Logger.ErrorException("SetIndicatorParameters", exception);
				}
			}
		}
	}
}
