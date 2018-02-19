using Alveo.CollectionEditor;
using Alveo.Interfaces.UserCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Alveo.UserCode
{
	[Serializable]
	public abstract class IndicatorBase : CodeBase, IIndicator, ICode, ICloneable, IManageableObject
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class c
		{
			public static readonly IndicatorBase.c __9 = new IndicatorBase.c();

			public static Action<IndicatorBase> __9__201_0;

			public static Func<IndicatorBase, int> __9__201_1;

			internal void <BaseStart>b__201_0(IndicatorBase i)
			{
				i.BaseStart();
			}

			internal int <BaseStart>b__201_1(IndicatorBase i)
			{
				return i.IndicatorCounted();
			}
		}

		private readonly List<Array<double>> _fakeSeriesArrays = new List<Array<double>>();

		private readonly List<string> _ignoreUpdateProperties = new List<string>(new string[]
		{
			"CopySeries",
			"Series",
			"ShortName"
		});

		private int _indicatorBuffers;

		private int _indicatorCounted;

		private List<IndicatorSeries> _indicatorSeries = new List<IndicatorSeries>();

		private IndicatorLevels _levelsSettings = new IndicatorLevels();

		protected bool indicator_separate_window
		{
			get
			{
				return !this.IsOverlay;
			}
			set
			{
				this.IsOverlay = !value;
			}
		}

		protected bool indicator_chart_window
		{
			get
			{
				return this.IsOverlay;
			}
			set
			{
				this.IsOverlay = value;
			}
		}

		[Browsable(false), XmlIgnore]
		public int indicator_buffers
		{
			get
			{
				return this._indicatorBuffers;
			}
			set
			{
				this._indicatorBuffers = value;
				this.IndicatorBuffers(value);
			}
		}

		[Browsable(false)]
		public double? indicator_minimum
		{
			get;
			set;
		}

		[Browsable(false)]
		public double? indicator_maximum
		{
			get;
			set;
		}

		[Browsable(false)]
		public bool use_minimum
		{
			get;
			set;
		}

		[Browsable(false)]
		public bool use_maximum
		{
			get;
			set;
		}

		[DisplayName("Indicator Level Color")]
		protected color indicator_levelcolor
		{
			get
			{
				return this._levelsSettings.Color;
			}
			set
			{
				this._levelsSettings.Color = value;
			}
		}

		[DisplayName("Indicator Level Width")]
		protected int indicator_levelwidth
		{
			get
			{
				return this._levelsSettings.Width;
			}
			set
			{
				this._levelsSettings.Width = value;
			}
		}

		[DisplayName("Indicator Level Style")]
		protected int indicator_levelstyle
		{
			get
			{
				return (int)this._levelsSettings.Style;
			}
			set
			{
				this._levelsSettings.Style = (MqlDashStyle)value;
			}
		}

		protected bool show_confirm
		{
			get;
			set;
		}

		protected bool show_inputs
		{
			get;
			set;
		}

		protected color indicator_color1
		{
			get
			{
				bool flag = this._indicatorBuffers >= 1;
				color result;
				if (flag)
				{
					result = this.Series[0].Color;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 1;
				if (flag)
				{
					this.Series[0].Color = value;
				}
			}
		}

		protected color indicator_color2
		{
			get
			{
				bool flag = this._indicatorBuffers >= 2;
				color result;
				if (flag)
				{
					result = this.Series[1].Color;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 2;
				if (flag)
				{
					this.Series[1].Color = value;
				}
			}
		}

		protected color indicator_color3
		{
			get
			{
				bool flag = this._indicatorBuffers >= 3;
				color result;
				if (flag)
				{
					result = this.Series[2].Color;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 3;
				if (flag)
				{
					this.Series[2].Color = value;
				}
			}
		}

		protected color indicator_color4
		{
			get
			{
				bool flag = this._indicatorBuffers >= 4;
				color result;
				if (flag)
				{
					result = this.Series[3].Color;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 4;
				if (flag)
				{
					this.Series[3].Color = value;
				}
			}
		}

		protected color indicator_color5
		{
			get
			{
				bool flag = this._indicatorBuffers >= 5;
				color result;
				if (flag)
				{
					result = this.Series[4].Color;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 5;
				if (flag)
				{
					this.Series[4].Color = value;
				}
			}
		}

		protected color indicator_color6
		{
			get
			{
				bool flag = this._indicatorBuffers >= 6;
				color result;
				if (flag)
				{
					result = this.Series[5].Color;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 6;
				if (flag)
				{
					this.Series[5].Color = value;
				}
			}
		}

		protected color indicator_color7
		{
			get
			{
				bool flag = this._indicatorBuffers >= 7;
				color result;
				if (flag)
				{
					result = this.Series[6].Color;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 7;
				if (flag)
				{
					this.Series[6].Color = value;
				}
			}
		}

		protected color indicator_color8
		{
			get
			{
				bool flag = this._indicatorBuffers >= 8;
				color result;
				if (flag)
				{
					result = this.Series[7].Color;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 8;
				if (flag)
				{
					this.Series[7].Color = value;
				}
			}
		}

		protected int indicator_width1
		{
			get
			{
				bool flag = this._indicatorBuffers >= 1;
				int result;
				if (flag)
				{
					result = this.Series[0].Width;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 1;
				if (flag)
				{
					this.Series[0].Width = value;
				}
			}
		}

		protected int indicator_width2
		{
			get
			{
				bool flag = this._indicatorBuffers >= 2;
				int result;
				if (flag)
				{
					result = this.Series[1].Width;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 2;
				if (flag)
				{
					this.Series[1].Width = value;
				}
			}
		}

		protected int indicator_width3
		{
			get
			{
				bool flag = this._indicatorBuffers >= 3;
				int result;
				if (flag)
				{
					result = this.Series[2].Width;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 3;
				if (flag)
				{
					this.Series[2].Width = value;
				}
			}
		}

		protected int indicator_width4
		{
			get
			{
				bool flag = this._indicatorBuffers >= 4;
				int result;
				if (flag)
				{
					result = this.Series[3].Width;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 4;
				if (flag)
				{
					this.Series[3].Width = value;
				}
			}
		}

		protected int indicator_width5
		{
			get
			{
				bool flag = this._indicatorBuffers >= 5;
				int result;
				if (flag)
				{
					result = this.Series[4].Width;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 5;
				if (flag)
				{
					this.Series[4].Width = value;
				}
			}
		}

		protected int indicator_width6
		{
			get
			{
				bool flag = this._indicatorBuffers >= 6;
				int result;
				if (flag)
				{
					result = this.Series[5].Width;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 6;
				if (flag)
				{
					this.Series[5].Width = value;
				}
			}
		}

		protected int indicator_width7
		{
			get
			{
				bool flag = this._indicatorBuffers >= 7;
				int result;
				if (flag)
				{
					result = this.Series[6].Width;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 7;
				if (flag)
				{
					this.Series[6].Width = value;
				}
			}
		}

		protected int indicator_width8
		{
			get
			{
				bool flag = this._indicatorBuffers >= 8;
				int result;
				if (flag)
				{
					result = this.Series[7].Width;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 8;
				if (flag)
				{
					this.Series[7].Width = value;
				}
			}
		}

		protected int indicator_style1
		{
			get
			{
				bool flag = this._indicatorBuffers >= 1;
				int result;
				if (flag)
				{
					result = this.Series[0].Style;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 1;
				if (flag)
				{
					this.Series[0].Style = value;
				}
			}
		}

		protected int indicator_style2
		{
			get
			{
				bool flag = this._indicatorBuffers >= 2;
				int result;
				if (flag)
				{
					result = this.Series[1].Style;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 2;
				if (flag)
				{
					this.Series[1].Style = value;
				}
			}
		}

		protected int indicator_style3
		{
			get
			{
				bool flag = this._indicatorBuffers >= 3;
				int result;
				if (flag)
				{
					result = this.Series[2].Style;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 3;
				if (flag)
				{
					this.Series[2].Style = value;
				}
			}
		}

		protected int indicator_style4
		{
			get
			{
				bool flag = this._indicatorBuffers >= 4;
				int result;
				if (flag)
				{
					result = this.Series[3].Style;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 4;
				if (flag)
				{
					this.Series[3].Style = value;
				}
			}
		}

		protected int indicator_style5
		{
			get
			{
				bool flag = this._indicatorBuffers >= 5;
				int result;
				if (flag)
				{
					result = this.Series[4].Style;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 5;
				if (flag)
				{
					this.Series[4].Style = value;
				}
			}
		}

		protected int indicator_style6
		{
			get
			{
				bool flag = this._indicatorBuffers >= 6;
				int result;
				if (flag)
				{
					result = this.Series[5].Style;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 6;
				if (flag)
				{
					this.Series[5].Style = value;
				}
			}
		}

		protected int indicator_style7
		{
			get
			{
				bool flag = this._indicatorBuffers >= 7;
				int result;
				if (flag)
				{
					result = this.Series[6].Style;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 7;
				if (flag)
				{
					this.Series[6].Style = value;
				}
			}
		}

		protected int indicator_style8
		{
			get
			{
				bool flag = this._indicatorBuffers >= 8;
				int result;
				if (flag)
				{
					result = this.Series[7].Style;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				bool flag = this._indicatorBuffers >= 8;
				if (flag)
				{
					this.Series[7].Style = value;
				}
			}
		}

		protected double indicator_level1
		{
			get
			{
				bool flag = this._levelsSettings.Values.Count > 0;
				double result;
				if (flag)
				{
					result = this._levelsSettings.Values[0].Value;
				}
				else
				{
					result = 2147483647.0;
				}
				return result;
			}
			set
			{
				this.SetLevelValue(0, value);
			}
		}

		protected double indicator_level2
		{
			get
			{
				bool flag = this._levelsSettings.Values.Count > 1;
				double result;
				if (flag)
				{
					result = this._levelsSettings.Values[1].Value;
				}
				else
				{
					result = 2147483647.0;
				}
				return result;
			}
			set
			{
				this.SetLevelValue(1, value);
			}
		}

		protected double indicator_level3
		{
			get
			{
				bool flag = this._levelsSettings.Values.Count > 2;
				double result;
				if (flag)
				{
					result = this._levelsSettings.Values[2].Value;
				}
				else
				{
					result = 2147483647.0;
				}
				return result;
			}
			set
			{
				this.SetLevelValue(2, value);
			}
		}

		protected double indicator_level4
		{
			get
			{
				bool flag = this._levelsSettings.Values.Count > 3;
				double result;
				if (flag)
				{
					result = this._levelsSettings.Values[3].Value;
				}
				else
				{
					result = 2147483647.0;
				}
				return result;
			}
			set
			{
				this.SetLevelValue(3, value);
			}
		}

		protected double indicator_level5
		{
			get
			{
				bool flag = this._levelsSettings.Values.Count > 4;
				double result;
				if (flag)
				{
					result = this._levelsSettings.Values[4].Value;
				}
				else
				{
					result = 2147483647.0;
				}
				return result;
			}
			set
			{
				this.SetLevelValue(4, value);
			}
		}

		protected double indicator_level6
		{
			get
			{
				bool flag = this._levelsSettings.Values.Count > 5;
				double result;
				if (flag)
				{
					result = this._levelsSettings.Values[5].Value;
				}
				else
				{
					result = 2147483647.0;
				}
				return result;
			}
			set
			{
				this.SetLevelValue(5, value);
			}
		}

		protected double indicator_level7
		{
			get
			{
				bool flag = this._levelsSettings.Values.Count > 6;
				double result;
				if (flag)
				{
					result = this._levelsSettings.Values[6].Value;
				}
				else
				{
					result = 2147483647.0;
				}
				return result;
			}
			set
			{
				this.SetLevelValue(6, value);
			}
		}

		protected double indicator_level8
		{
			get
			{
				bool flag = this._levelsSettings.Values.Count > 7;
				double result;
				if (flag)
				{
					result = this._levelsSettings.Values[7].Value;
				}
				else
				{
					result = 2147483647.0;
				}
				return result;
			}
			set
			{
				this.SetLevelValue(7, value);
			}
		}

		[Browsable(false)]
		public new string Symbol
		{
			get;
			set;
		}

		[Browsable(false)]
		public int TimeFrame
		{
			get;
			set;
		}

		[Browsable(false), XmlElement]
		public IndicatorSeries[] CopySeries
		{
			get
			{
				return this._indicatorSeries.ToArray();
			}
			set
			{
				bool flag = value == null;
				if (!flag)
				{
					for (int i = 0; i < Math.Min(this._indicatorSeries.Count, value.Length); i++)
					{
						this._indicatorSeries[i].ArrowType = value[i].ArrowType;
						this._indicatorSeries[i].Color = value[i].Color;
						this._indicatorSeries[i].EmptyValue = value[i].EmptyValue;
						this._indicatorSeries[i].Name = value[i].Name;
						this._indicatorSeries[i].Shift = value[i].Shift;
						this._indicatorSeries[i].StartIndexDraw = value[i].StartIndexDraw;
						this._indicatorSeries[i].Style = value[i].Style;
						this._indicatorSeries[i].Type = value[i].Type;
						this._indicatorSeries[i].Width = value[i].Width;
					}
				}
			}
		}

		[Browsable(false), XmlElement]
		public string ShortName
		{
			get;
			set;
		}

		[Browsable(false), Category("Advanced"), XmlIgnore]
		public List<IndicatorSeries> Series
		{
			get
			{
				return this._indicatorSeries;
			}
			set
			{
				this._indicatorSeries = value;
			}
		}

		[Category("Advanced"), DisplayName("Series"), XmlIgnore]
		public IndicatorSeries[] SeriesArray
		{
			get
			{
				return this._indicatorSeries.ToArray();
			}
		}

		[Category("Advanced"), DisplayName("Digits"), XmlElement]
		public int IndDigits
		{
			get;
			set;
		}

		[Browsable(false), XmlElement]
		public int ChartPanel
		{
			get;
			set;
		}

		[Browsable(false), XmlElement]
		public bool IsOverlay
		{
			get;
			set;
		}

		[Browsable(false), XmlElement(typeof(IndicatorLevels))]
		public IndicatorLevels Levels
		{
			get
			{
				return this._levelsSettings;
			}
			set
			{
				bool flag = value != null;
				if (flag)
				{
					this._levelsSettings = value;
				}
			}
		}

		[Category("Advanced"), DisplayName("Level Color"), XmlIgnore]
		public Color LevelColor
		{
			get
			{
				return this._levelsSettings.Color.Color;
			}
			set
			{
				this._levelsSettings.Color.Color = value;
			}
		}

		[Category("Advanced"), Description("Not all indicators use levels."), DisplayName("Levels"), XmlIgnore]
		public List<Alveo.Interfaces.UserCode.Double> LevelValues
		{
			get
			{
				return this._levelsSettings.Values;
			}
		}

		protected void IndicatorBuffers(int count)
		{
			bool flag = this._indicatorSeries.Count < this._indicatorBuffers;
			if (flag)
			{
				while (this._indicatorSeries.Count != this._indicatorBuffers)
				{
					IndicatorSeries item = new IndicatorSeries(this._indicatorSeries.Count.ToString(CultureInfo.InvariantCulture));
					this._indicatorSeries.Add(item);
				}
			}
		}

		protected int IndicatorCounted()
		{
			return this._indicatorCounted;
		}

		protected void IndicatorDigits(object digits)
		{
			int num;
			try
			{
				num = (int)digits;
			}
			catch
			{
				base.LastError = 4051;
				return;
			}
			bool flag = num < 0 || num > 10;
			if (flag)
			{
				base.LastError = 4051;
			}
			else
			{
				this.IndDigits = num;
			}
		}

		protected void IndicatorShortName(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				base.LastError = 4062;
			}
			else
			{
				this.ShortName = name;
			}
		}

		protected void SetIndexArrow(int index, int code)
		{
			bool flag = !this.IsValidIndicatorIndex(index) || code < 33 || code > 255;
			if (flag)
			{
				base.LastError = 4051;
			}
			this._indicatorSeries[index].ArrowType = code;
		}

		protected bool SetIndexBuffer(int index, Array<double> data, bool isForcast = false)
		{
			bool flag = data == null;
			bool result;
			if (flag)
			{
				bool flag2 = index < this._indicatorSeries.Count;
				if (flag2)
				{
					this._indicatorSeries.RemoveAt(index);
				}
				result = true;
			}
			else
			{
				data.IsSeries = true;
				int num = base.Bars;
				if (isForcast)
				{
					this._indicatorSeries[index].IsForcast = true;
					num += base.Chart.NumForecastBars;
				}
				while (data.Count < num)
				{
					data.Add(2147483647.0);
				}
				bool flag3 = !this.IsValidIndicatorIndex(index);
				if (flag3)
				{
					this._fakeSeriesArrays.Add(data);
					base.LastError = 4051;
					result = false;
				}
				else
				{
					this._indicatorSeries[index].Values = data;
					result = true;
				}
			}
			return result;
		}

		protected void SetIndexDrawBegin(int index, int begin)
		{
			bool flag = !this.IsValidIndicatorIndex(index) || begin < 0;
			if (flag)
			{
				base.LastError = 4051;
			}
			else
			{
				this._indicatorSeries[index].StartIndexDraw = begin;
			}
		}

		protected void SetIndexEmptyValue(int index, double value)
		{
			bool flag = !this.IsValidIndicatorIndex(index);
			if (flag)
			{
				base.LastError = 4051;
			}
			else
			{
				bool flag2 = index >= 0 && index < this._indicatorSeries.Count;
				if (flag2)
				{
					this._indicatorSeries[index].EmptyValue = value;
				}
			}
		}

		protected void SetIndexLabel(int index, string text)
		{
			bool flag = this.IsValidIndicatorIndex(index) && !string.IsNullOrEmpty(text);
			if (flag)
			{
				this._indicatorSeries[index].Name = text;
			}
			else
			{
				base.LastError = 4051;
			}
		}

		protected void SetIndexShift(int index, int shift)
		{
			bool flag = this.IsValidIndicatorIndex(index);
			if (flag)
			{
				this._indicatorSeries[index].Shift = shift;
			}
			else
			{
				base.LastError = 4051;
			}
		}

		protected void SetIndexStyle(int index, int type, int style = -1, int width = -1, color clr = null)
		{
			bool flag = this.IsValidIndicatorIndex(index);
			if (flag)
			{
				try
				{
					bool flag2 = width > 0;
					if (flag2)
					{
						this._indicatorSeries[index].Width = width;
					}
					bool flag3 = clr != null;
					if (flag3)
					{
						this._indicatorSeries[index].Color = clr;
					}
					bool flag4 = type != -1;
					if (flag4)
					{
						this._indicatorSeries[index].Type = type;
					}
					bool flag5 = style != -1;
					if (flag5)
					{
						this._indicatorSeries[index].Style = style;
					}
				}
				catch
				{
					base.LastError = 4051;
				}
			}
			else
			{
				base.LastError = 4051;
			}
		}

		protected void SetLevelStyle(int drawStyle, int lineWidth, color clr)
		{
			this._levelsSettings.Width = lineWidth;
			this._levelsSettings.Color = clr;
			this._levelsSettings.Style = (MqlDashStyle)drawStyle;
		}

		protected void SetLevelValue(int level, double value)
		{
			bool flag = level > 31;
			if (flag)
			{
				base.LastError = 4051;
			}
			else
			{
				bool flag2 = this._levelsSettings.Values.Count <= level;
				if (flag2)
				{
					while (this._levelsSettings.Values.Count - 1 != level)
					{
						this._levelsSettings.Values.Add(new Alveo.Interfaces.UserCode.Double(2147483647.0));
					}
				}
				this._levelsSettings.Values[level] = new Alveo.Interfaces.UserCode.Double(value);
				this._levelsSettings.IsUpdateRequired = true;
			}
		}

		static IndicatorBase()
		{
			CollectionEditor collectionEditor = new CollectionEditor();
		}

		protected IndicatorBase()
		{
			base.ID = Guid.NewGuid().ToString();
			this.ShortName = base.GetType().Name;
			this.ChartPanel = -1;
			this.IsOverlay = true;
			this.IndDigits = 5;
		}

		public void AddEmptyValueToAllSeries()
		{
			foreach (IndicatorSeries current in this._indicatorSeries)
			{
				current.Values.Add(current.EmptyValue);
			}
			foreach (Array<double> current2 in this._fakeSeriesArrays)
			{
				current2.Add(2147483647.0);
			}
			foreach (IndicatorBase current3 in this.IndicatorCache)
			{
				current3.AddEmptyValueToAllSeries();
			}
		}

		public override void BaseInit()
		{
			this._indicatorCounted = 0;
			foreach (IndicatorSeries current in this.Series)
			{
				current.Values.Clear();
			}
			foreach (IndicatorBase current2 in this.IndicatorCache)
			{
				current2.BaseInit();
			}
			base.BaseInit();
			this.IndicatorDigits(this.IndDigits);
		}

		public override void BaseStart()
		{
			List<IndicatorBase> arg_26_0 = this.IndicatorCache;
			Action<IndicatorBase> arg_26_1;
			if ((arg_26_1 = IndicatorBase.c.__9__201_0) == null)
			{
				arg_26_1 = (IndicatorBase.c.__9__201_0 = new Action<IndicatorBase>(IndicatorBase.c.__9.<BaseStart>b__201_0));
			}
			arg_26_0.ForEach(arg_26_1);
			base.BaseStart();
			int num = Math.Max(base.iBars(this.Symbol, this.TimeFrame) - 1, 0);
			int arg_90_1;
			if (this.IndicatorCache.Count <= 0)
			{
				arg_90_1 = num;
			}
			else
			{
				int arg_8B_0 = num;
				IEnumerable<IndicatorBase> arg_86_0 = this.IndicatorCache;
				Func<IndicatorBase, int> arg_86_1;
				if ((arg_86_1 = IndicatorBase.c.__9__201_1) == null)
				{
					arg_86_1 = (IndicatorBase.c.__9__201_1 = new Func<IndicatorBase, int>(IndicatorBase.c.__9.<BaseStart>b__201_1));
				}
				arg_90_1 = Math.Min(arg_8B_0, arg_86_0.Min(arg_86_1));
			}
			this._indicatorCounted = arg_90_1;
		}

		public override object Clone()
		{
			Type type = base.GetType();
			object result;
			try
			{
				IndicatorBase indicatorBase = Activator.CreateInstance(type) as IndicatorBase;
				bool flag = indicatorBase == null;
				if (flag)
				{
					result = this;
				}
				else
				{
					PropertyInfo[] properties = type.GetProperties();
					for (int i = 0; i < properties.Length; i++)
					{
						PropertyInfo propertyInfo = properties[i];
						object value = propertyInfo.GetValue(this, null);
						bool flag2 = value != null && !propertyInfo.Name.Contains("Series") && propertyInfo.GetSetMethod() != null;
						if (flag2)
						{
							bool flag3 = value is ValueType;
							if (flag3)
							{
								propertyInfo.SetValue(indicatorBase, value, null);
							}
							else
							{
								bool flag4 = value is ICloneable;
								if (flag4)
								{
									propertyInfo.SetValue(indicatorBase, ((ICloneable)value).Clone(), null);
								}
							}
						}
					}
					indicatorBase._indicatorSeries = new List<IndicatorSeries>();
					foreach (IndicatorSeries current in this._indicatorSeries)
					{
						indicatorBase._indicatorSeries.Add((IndicatorSeries)current.Clone());
					}
					result = indicatorBase;
				}
			}
			catch (Exception exception)
			{
				this.Logger.ErrorException("Clone", exception);
				result = null;
			}
			return result;
		}

		public override void UpdateProperties(ICode code)
		{
			IndicatorBase indicatorBase = code as IndicatorBase;
			bool flag = indicatorBase == null;
			if (!flag)
			{
				Type type = base.GetType();
				Type type2 = code.GetType();
				try
				{
					PropertyInfo[] properties = type.GetProperties();
					for (int i = 0; i < properties.Length; i++)
					{
						PropertyInfo prop = properties[i];
						bool flag2 = this._ignoreUpdateProperties.Any((string p) => p.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
						if (!flag2)
						{
							object value = type2.GetProperty(prop.Name).GetValue(code, null);
							bool flag3 = value != null && prop.GetSetMethod() != null;
							if (flag3)
							{
								bool flag4 = value is ValueType;
								if (flag4)
								{
									prop.SetValue(this, value, null);
								}
								else
								{
									bool flag5 = value is ICloneable;
									if (flag5)
									{
										prop.SetValue(this, ((ICloneable)value).Clone(), null);
									}
								}
							}
						}
					}
					for (int j = 0; j < Math.Min(indicatorBase.Series.Count, this.Series.Count); j++)
					{
						this.Series[j].UpdateProperties(indicatorBase.Series[j]);
					}
				}
				catch (Exception exception)
				{
					this.Logger.ErrorException("UpdateProperties", exception);
				}
			}
		}

		protected int WindowFind(string name)
		{
			return base.Chart.FindWindowByName(name);
		}

		public virtual bool IsSameParameters(params object[] values)
		{
			return false;
		}

		public virtual void SetIndicatorParameters(params object[] values)
		{
		}

		private bool IsValidIndicatorIndex(int index)
		{
			return index >= 0 && index < this._indicatorSeries.Count;
		}
	}
}
