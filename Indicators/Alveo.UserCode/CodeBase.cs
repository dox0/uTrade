using Alveo.Common;
using Alveo.Common.Classes;
using Alveo.Common.Classes.Notifications;
using Alveo.Common.Enums;
using Alveo.Interfaces.Broker;
using Alveo.Interfaces.UserCode;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Alveo.UserCode
{
	public abstract class CodeBase : ICode, ICloneable, IManageableObject
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class __c
		{
			public static readonly CodeBase.__c __9 = new CodeBase.__c();

			public static Func<object, string> __9__647_1;

			public static Func<Bar, double> __9__722_0;

			public static Func<Bar, double> __9__722_1;

			public static Func<Bar, double> __9__722_2;

			public static Func<Bar, double> __9__722_3;

			public static Func<Bar, double> __9__722_4;

			public static Func<Bar, datetime> __9__722_5;

			public static Func<Bar, double> __9__837_0;

			public static Func<Bar, double> __9__837_1;

			public static Func<Bar, double> __9__837_2;

			public static Func<Bar, double> __9__837_3;

			public static Func<Bar, double> __9__837_4;

			public static Func<Bar, double> __9__837_5;

			public static Func<Bar, double> __9__837_6;

			internal string <Comment>b__647_1(object o)
			{
				return o.ToString();
			}

			internal double <RefreshRates>b__722_0(Bar b)
			{
				return (double)b.Open;
			}

			internal double <RefreshRates>b__722_1(Bar b)
			{
				return (double)b.High;
			}

			internal double <RefreshRates>b__722_2(Bar b)
			{
				return (double)b.Low;
			}

			internal double <RefreshRates>b__722_3(Bar b)
			{
				return (double)b.Close;
			}

			internal double <RefreshRates>b__722_4(Bar b)
			{
				return (double)b.Volume;
			}

			internal datetime <RefreshRates>b__722_5(Bar b)
			{
				return new datetime(b.BarTime);
			}

			internal double <GetPrice>b__837_0(Bar p)
			{
				return (double)p.Close;
			}

			internal double <GetPrice>b__837_1(Bar p)
			{
				return (double)p.Open;
			}

			internal double <GetPrice>b__837_2(Bar p)
			{
				return (double)p.High;
			}

			internal double <GetPrice>b__837_3(Bar p)
			{
				return (double)p.Low;
			}

			internal double <GetPrice>b__837_4(Bar p)
			{
				return (double)(p.High + p.Low) / 2.0;
			}

			internal double <GetPrice>b__837_5(Bar p)
			{
				return (double)(p.High + p.Low + p.Close) / 3.0;
			}

			internal double <GetPrice>b__837_6(Bar p)
			{
				return (double)(p.High + p.Low + p.Close + p.Open) / 4.0;
			}
		}

		private Order _selectedOrder;

		internal const int ErrorResult = -1;

		protected const double Epsilon = 1E-06;

		protected readonly IndicatorCache IndicatorCache;

		protected readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private readonly MqlFileHelper fileHelper;

		protected Array<Bar> ChartBars = new Array<Bar>();

		protected UninitializeReason UninitializedReason;

		private Random random = new Random();

		private const int NotSupportedReturnValue = -1;

		public const int MODE_OPEN = 0;

		public const int MODE_LOW = 1;

		public const int MODE_HIGH = 2;

		public const int MODE_CLOSE = 3;

		public const int MODE_VOLUME = 4;

		public const int MODE_TIME = 5;

		public const int PERIOD_M1 = 1;

		public const int PERIOD_M5 = 5;

		public const int PERIOD_M15 = 15;

		public const int PERIOD_M30 = 30;

		public const int PERIOD_H1 = 60;

		public const int PERIOD_H4 = 240;

		public const int PERIOD_D1 = 1440;

		public const int PERIOD_W1 = 10080;

		public const int PERIOD_MN1 = 43200;

		public const int OP_BUY = 0;

		public const int OP_SELL = 1;

		public const int OP_BUYLIMIT = 2;

		public const int OP_SELLLIMIT = 3;

		public const int OP_BUYSTOP = 4;

		public const int OP_SELLSTOP = 5;

		public const int SELECT_BY_POS = 0;

		public const int SELECT_BY_TICKET = 1;

		public const int MODE_TRADES = 0;

		public const int MODE_HISTORY = 1;

		public const int PRICE_CLOSE = 0;

		public const int PRICE_OPEN = 1;

		public const int PRICE_HIGH = 2;

		public const int PRICE_LOW = 3;

		public const int PRICE_MEDIAN = 4;

		public const int PRICE_TYPICAL = 5;

		public const int PRICE_WEIGHTED = 6;

		public const int MODE_BID = 9;

		public const int MODE_ASK = 10;

		public const int MODE_POINT = 11;

		public const int MODE_DIGITS = 12;

		public const int MODE_SPREAD = 13;

		public const int MODE_STOPLEVEL = 14;

		public const int MODE_LOTSIZE = 15;

		public const int MODE_TICKVALUE = 16;

		public const int MODE_TICKSIZE = 17;

		public const int MODE_SWAPLONG = 18;

		public const int MODE_SWAPSHORT = 19;

		public const int MODE_STARTING = 20;

		public const int MODE_EXPIRATION = 21;

		public const int MODE_TRADEALLOWED = 22;

		public const int MODE_MINLOT = 23;

		public const int MODE_LOTSTEP = 24;

		public const int MODE_MAXLOT = 25;

		public const int MODE_SWAPTYPE = 26;

		public const int MODE_PROFITCALCMODE = 27;

		public const int MODE_MARGINCALCMODE = 28;

		public const int MODE_MARGININIT = 29;

		public const int MODE_MARGINMAINTENANCE = 30;

		public const int MODE_MARGINHEDGED = 31;

		public const int MODE_MARGINREQUIRED = 32;

		public const int MODE_FREEZELEVEL = 33;

		public const int DRAW_LINE = 0;

		public const int DRAW_SECTION = 1;

		public const int DRAW_HISTOGRAM = 2;

		public const int DRAW_ARROW = 3;

		public const int DRAW_ZIGZAG = 4;

		public const int DRAW_LINKED_HISTOGRAM = 5;

		public const int DRAW_NONE = 12;

		public const int STYLE_SOLID = 0;

		public const int STYLE_DASH = 1;

		public const int STYLE_DOT = 2;

		public const int STYLE_DASHDOT = 3;

		public const int STYLE_DASHDOTDOT = 4;

		public const int SYMBOL_THUMBSUP = 67;

		public const int SYMBOL_THUMBSDOWN = 68;

		public const int SYMBOL_ARROWUP = 241;

		public const int SYMBOL_ARROWDOWN = 242;

		public const int SYMBOL_STOPSIGN = 251;

		public const int SYMBOL_CHECKSIGN = 252;

		public const int SYMBOL_LEFTPRICE = 5;

		public const int SYMBOL_RIGHTPRICE = 6;

		public static readonly color Black = Colors.Black;

		public static readonly color DarkGreen = Colors.DarkGreen;

		public static readonly color DarkSlateGray = Colors.DarkSlateGray;

		public static readonly color Olive = Colors.Olive;

		public static readonly color Green = Colors.Green;

		public static readonly color Teal = Colors.Teal;

		public static readonly color Navy = Colors.Navy;

		public static readonly color Purple = Colors.Purple;

		public static readonly color Maroon = Colors.Maroon;

		public static readonly color Indigo = Colors.Indigo;

		public static readonly color MidnightBlue = Colors.MidnightBlue;

		public static readonly color DarkBlue = Colors.DarkBlue;

		public static readonly color DarkOliveGreen = Colors.DarkOliveGreen;

		public static readonly color SaddleBrown = Colors.SaddleBrown;

		public static readonly color ForestGreen = Colors.ForestGreen;

		public static readonly color OliveDrab = Colors.OliveDrab;

		public static readonly color DimGray = Colors.DimGray;

		public static readonly color DarkTurquoise = Colors.DarkTurquoise;

		public static readonly color Brown = Colors.Brown;

		public static readonly color MediumBlue = Colors.MediumBlue;

		public static readonly color Sienna = Colors.Sienna;

		public static readonly color DarkSlateBlue = Colors.DarkSlateBlue;

		public static readonly color DarkGoldenrod = Colors.DarkGoldenrod;

		public static readonly color SeaGreen = Colors.SeaGreen;

		public static readonly color LightSeaGreen = Colors.LightSeaGreen;

		public static readonly color DarkViolet = Colors.DarkViolet;

		public static readonly color FireBrick = 11674146;

		public static readonly color MediumVioletRed = Colors.MediumVioletRed;

		public static readonly color MediumSeaGreen = Colors.MediumSeaGreen;

		public static readonly color Chocolate = Colors.Chocolate;

		public static readonly color Crimson = Colors.Crimson;

		public static readonly color SteelBlue = Colors.SteelBlue;

		public static readonly color OrangeRed = Colors.OrangeRed;

		public static readonly color LimeGreen = Colors.LimeGreen;

		public static readonly color YellowGreen = Colors.YellowGreen;

		public static readonly color DarkOrchid = Colors.DarkOrchid;

		public static readonly color CadetBlue = Colors.CadetBlue;

		public static readonly color LawnGreen = Colors.LawnGreen;

		public static readonly color MediumSpringGreen = Colors.MediumSpringGreen;

		public static readonly color Goldenrod = Colors.Goldenrod;

		public static readonly color DarkOrange = Colors.DarkOrange;

		public static readonly color Orange = Colors.Orange;

		public static readonly color Gold = Colors.Gold;

		public static readonly color Yellow = Colors.Yellow;

		public static readonly color Chartreuse = Colors.Chartreuse;

		public static readonly color Lime = Colors.Lime;

		public static readonly color SpringGreen = Colors.SpringGreen;

		public static readonly color Aqua = Colors.Aqua;

		public static readonly color DeepSkyBlue = Colors.DeepSkyBlue;

		public static readonly color Blue = Colors.Blue;

		public static readonly color Magenta = Colors.Magenta;

		public static readonly color Red = Colors.Red;

		public static readonly color Gray = Colors.Gray;

		public static readonly color SlateGray = Colors.SlateGray;

		public static readonly color Peru = Colors.Peru;

		public static readonly color BlueViolet = Colors.BlueViolet;

		public static readonly color DarkKhaki = Colors.DarkKhaki;

		public static readonly color SlateBlue = Colors.SlateBlue;

		public static readonly color RoyalBlue = Colors.RoyalBlue;

		public static readonly color Turquoise = Colors.Turquoise;

		public static readonly color DodgerBlue = Colors.DodgerBlue;

		public static readonly color MediumTurquoise = Colors.MediumTurquoise;

		public static readonly color DeepPink = Colors.DeepPink;

		public static readonly color LightSlateGray = Colors.LightSlateGray;

		public static readonly color IndianRed = Colors.IndianRed;

		public static readonly color MediumOrchid = Colors.MediumOrchid;

		public static readonly color GreenYellow = Colors.GreenYellow;

		public static readonly color MediumAquamarine = Colors.MediumSeaGreen;

		public static readonly color DarkSeaGreen = Colors.DarkSeaGreen;

		public static readonly color Tomato = Colors.Tomato;

		public static readonly color RosyBrown = Colors.RosyBrown;

		public static readonly color Orchid = Colors.Orchid;

		public static readonly color Tan = Colors.Tan;

		public static readonly color MediumSlateBlue = Colors.MediumSlateBlue;

		public static readonly color SandyBrown = Colors.SandyBrown;

		public static readonly color DarkGray = Colors.DarkGray;

		public static readonly color CornflowerBlue = Colors.CornflowerBlue;

		public static readonly color Coral = Colors.Coral;

		public static readonly color PaleVioletRed = Colors.PaleVioletRed;

		public static readonly color MediumPurple = Colors.MediumPurple;

		public static readonly color DarkSalmon = Colors.DarkSalmon;

		public static readonly color BurlyWood = Colors.BurlyWood;

		public static readonly color HotPink = Colors.HotPink;

		public static readonly color Salmon = Colors.Salmon;

		public static readonly color Violet = Colors.Violet;

		public static readonly color LightCoral = Colors.LightCoral;

		public static readonly color SkyBlue = Colors.SkyBlue;

		public static readonly color LightSalmon = Colors.LightSalmon;

		public static readonly color LightBlue = Colors.LightBlue;

		public static readonly color LightSteelBlue = Colors.LightSteelBlue;

		public static readonly color LightSkyBlue = Colors.LightSkyBlue;

		public static readonly color Silver = Colors.Silver;

		public static readonly color Aquamarine = Colors.Aquamarine;

		public static readonly color LightGreen = Colors.LightGreen;

		public static readonly color Khaki = Colors.Khaki;

		public static readonly color Plum = Colors.Plum;

		public static readonly color PaleGreen = Colors.PaleGreen;

		public static readonly color Thistle = Colors.Thistle;

		public static readonly color PowderBlue = Colors.PowderBlue;

		public static readonly color PaleGoldenrod = Colors.PaleGoldenrod;

		public static readonly color PaleTurquoise = Colors.PaleTurquoise;

		public static readonly color LightGray = Colors.LightGray;

		public static readonly color Wheat = Colors.Wheat;

		public static readonly color NavajoWhite = Colors.NavajoWhite;

		public static readonly color BlanchedAlmond = Colors.BlanchedAlmond;

		public static readonly color LightGoldenrod = 16448210;

		public static readonly color Bisque = Colors.Bisque;

		public static readonly color Pink = Colors.Pink;

		public static readonly color PeachPuff = Colors.PeachPuff;

		public static readonly color Gainsboro = Colors.Gainsboro;

		public static readonly color LightPink = Colors.LightPink;

		public static readonly color Moccasin = Colors.Moccasin;

		public static readonly color LemonChiffon = Colors.LemonChiffon;

		public static readonly color Beige = Colors.Beige;

		public static readonly color AntiqueWhite = Colors.AntiqueWhite;

		public static readonly color PapayaWhip = Colors.PapayaWhip;

		public static readonly color Cornsilk = Colors.Cornsilk;

		public static readonly color LightYellow = Colors.LightYellow;

		public static readonly color LightCyan = Colors.LightCyan;

		public static readonly color Linen = Colors.Linen;

		public static readonly color AliceBlue = Colors.AliceBlue;

		public static readonly color Honeydew = Colors.Honeydew;

		public static readonly color Ivory = Colors.Ivory;

		public static readonly color WhiteSmoke = Colors.WhiteSmoke;

		public static readonly color OldLace = Colors.OldLace;

		public static readonly color MistyRose = Colors.MistyRose;

		public static readonly color Lavender = Colors.Lavender;

		public static readonly color LavenderBlush = Colors.LavenderBlush;

		public static readonly color MintCream = Colors.MintCream;

		public static readonly color Snow = Colors.Snow;

		public static readonly color White = Colors.White;

		public const int MODE_MAIN = 0;

		public const int MODE_SIGNAL = 1;

		public const int MODE_PLUSDI = 1;

		public const int MODE_MINUSDI = 2;

		public const int MODE_UPPER = 1;

		public const int MODE_LOWER = 2;

		public const int MODE_TENKANSEN = 1;

		public const int MODE_KIJUNSEN = 2;

		public const int MODE_SENKOUSPANA = 3;

		public const int MODE_SENKOUSPANB = 4;

		public const int MODE_CHINKOUSPAN = 5;

		public const int MODE_SMA = 0;

		public const int MODE_EMA = 1;

		public const int MODE_SMMA = 2;

		public const int MODE_LWMA = 3;

		public const int MODE_BULL = 0;

		public const int MODE_BEAR = 1;

		public const int IDOK = 1;

		public const int IDCANCEL = 2;

		public const int IDABORT = 3;

		public const int IDRETRY = 4;

		public const int IDIGNORE = 5;

		public const int IDYES = 6;

		public const int IDNO = 7;

		public const int IDTRYAGAIN = 10;

		public const int IDCONTINUE = 11;

		public const int MB_OK = 0;

		public const int MB_OKCANCEL = 1;

		public const int MB_ABORTRETRYIGNORE = 2;

		public const int MB_YESNOCANCEL = 3;

		public const int MB_YESNO = 4;

		public const int MB_RETRYCANCEL = 5;

		public const int MB_CANCELTRYCONTINUE = 6;

		public const int MB_ICONSTOP = 16;

		public const int MB_ICONERROR = 16;

		public const int MB_ICONHAND = 16;

		public const int MB_ICONQUESTION = 32;

		public const int MB_ICONEXCLAMATION = 48;

		public const int MB_ICONWARNING = 48;

		public const int MB_ICONINFORMATION = 64;

		public const int MB_ICONASTERISK = 64;

		public const int MB_DEFBUTTON1 = 0;

		public const int MB_DEFBUTTON2 = 256;

		public const int MB_DEFBUTTON3 = 512;

		public const int MB_DEFBUTTON4 = 768;

		public const int OBJ_VLINE = 0;

		public const int OBJ_HLINE = 1;

		public const int OBJ_TREND = 2;

		public const int OBJ_TRENDBYANGLE = 3;

		public const int OBJ_REGRESSION = 4;

		public const int OBJ_CHANNEL = 5;

		public const int OBJ_STDDEVCHANNEL = 6;

		public const int OBJ_GANNLINE = 7;

		public const int OBJ_GANNFAN = 8;

		public const int OBJ_GANNGRID = 9;

		public const int OBJ_FIBO = 10;

		public const int OBJ_FIBOTIMES = 11;

		public const int OBJ_FIBOFAN = 12;

		public const int OBJ_FIBOARC = 13;

		public const int OBJ_EXPANSION = 14;

		public const int OBJ_FIBOCHANNEL = 15;

		public const int OBJ_RECTANGLE = 16;

		public const int OBJ_TRIANGLE = 17;

		public const int OBJ_ELLIPSE = 18;

		public const int OBJ_PITCHFORK = 19;

		public const int OBJ_CYCLES = 20;

		public const int OBJ_TEXT = 21;

		public const int OBJ_ARROW = 22;

		public const int OBJ_LABEL = 23;

		public const int OBJPROP_TIME1 = 0;

		public const int OBJPROP_PRICE1 = 1;

		public const int OBJPROP_TIME2 = 2;

		public const int OBJPROP_PRICE2 = 3;

		public const int OBJPROP_TIME3 = 4;

		public const int OBJPROP_PRICE3 = 5;

		public const int OBJPROP_COLOR = 6;

		public const int OBJPROP_STYLE = 7;

		public const int OBJPROP_WIDTH = 8;

		public const int OBJPROP_BACK = 9;

		public const int OBJPROP_RAY = 10;

		public const int OBJPROP_ELLIPSE = 11;

		public const int OBJPROP_SCALE = 12;

		public const int OBJPROP_ANGLE = 13;

		public const int OBJPROP_ARROWCODE = 14;

		public const int OBJPROP_TIMEFRAMES = 15;

		public const int OBJPROP_DEVIATION = 16;

		public const int OBJPROP_FONTSIZE = 100;

		public const int OBJPROP_CORNER = 101;

		public const int OBJPROP_XDISTANCE = 102;

		public const int OBJPROP_YDISTANCE = 103;

		public const int OBJPROP_FIBOLEVELS = 200;

		public const int OBJPROP_LEVELCOLOR = 201;

		public const int OBJPROP_LEVELSTYLE = 202;

		public const int OBJPROP_LEVELWIDTH = 203;

		public const int OBJPROP_FIRSTLEVEL = 210;

		public const int OBJ_PERIOD_M1 = 1;

		public const int OBJ_PERIOD_M5 = 2;

		public const int OBJ_PERIOD_M15 = 4;

		public const int OBJ_PERIOD_M30 = 8;

		public const int OBJ_PERIOD_H1 = 16;

		public const int OBJ_PERIOD_H4 = 32;

		public const int OBJ_PERIOD_D1 = 64;

		public const int OBJ_PERIOD_W1 = 128;

		public const int OBJ_PERIOD_MN1 = 256;

		public const int OBJ_ALL_PERIODS = 255;

		public const int EMPTY = -1;

		public const int REASON_REMOVE = 1;

		public const int REASON_RECOMPILE = 2;

		public const int REASON_CHARTCHANGE = 3;

		public const int REASON_CHARTCLOSE = 4;

		public const int REASON_PARAMETERS = 5;

		public const int REASON_ACCOUNT = 6;

		public const int EMPTY_VALUE = 2147483647;

		public const uint CLR_NONE = 4294967295u;

		public const int WHOLE_ARRAY = 0;

		public const int ERR_NO_ERROR = 0;

		public const int ERR_NO_RESULT = 1;

		public const int ERR_COMMON_ERROR = 2;

		public const int ERR_INVALID_TRADE_PARAMETERS = 3;

		public const int ERR_SERVER_BUSY = 4;

		public const int ERR_OLD_VERSION = 5;

		public const int ERR_NO_CONNECTION = 6;

		public const int ERR_NOT_ENOUGH_RIGHTS = 7;

		public const int ERR_TOO_FREQUENT_REQUESTS = 8;

		public const int ERR_MALFUNCTIONAL_TRADE = 9;

		public const int ERR_ACCOUNT_DISABLED = 64;

		public const int ERR_INVALID_ACCOUNT = 65;

		public const int ERR_TRADE_TIMEOUT = 128;

		public const int ERR_INVALID_PRICE = 129;

		public const int ERR_INVALID_STOPS = 130;

		public const int ERR_INVALID_TRADE_VOLUME = 131;

		public const int ERR_MARKET_CLOSED = 132;

		public const int ERR_TRADE_DISABLED = 133;

		public const int ERR_NOT_ENOUGH_MONEY = 134;

		public const int ERR_PRICE_CHANGED = 135;

		public const int ERR_OFF_QUOTES = 136;

		public const int ERR_BROKER_BUSY = 137;

		public const int ERR_REQUOTE = 138;

		public const int ERR_ORDER_LOCKED = 139;

		public const int ERR_LONG_POSITIONS_ONLY_ALLOWED = 140;

		public const int ERR_TOO_MANY_REQUESTS = 141;

		public const int ERR_TRADE_MODIFY_DENIED = 145;

		public const int ERR_TRADE_CONTEXT_BUSY = 146;

		public const int ERR_TRADE_EXPIRATION_DENIED = 147;

		public const int ERR_TRADE_TOO_MANY_ORDERS = 148;

		public const int ERR_TRADE_HEDGE_PROHIBITED = 149;

		public const int ERR_TRADE_PROHIBITED_BY_FIFO = 150;

		public const int ERR_NO_MQLERROR = 4000;

		public const int ERR_WRONG_FUNCTION_POINTER = 4001;

		public const int ERR_ARRAY_INDEX_OUT_OF_RANGE = 4002;

		public const int ERR_NO_MEMORY_FOR_CALL_STACK = 4003;

		public const int ERR_RECURSIVE_STACK_OVERFLOW = 4004;

		public const int ERR_NOT_ENOUGH_STACK_FOR_PARAM = 4005;

		public const int ERR_NO_MEMORY_FOR_PARAM_STRING = 4006;

		public const int ERR_NO_MEMORY_FOR_TEMP_STRING = 4007;

		public const int ERR_NOT_INITIALIZED_STRING = 4008;

		public const int ERR_NOT_INITIALIZED_ARRAYSTRING = 4009;

		public const int ERR_NO_MEMORY_FOR_ARRAYSTRING = 4010;

		public const int ERR_TOO_LONG_STRING = 4011;

		public const int ERR_REMAINDER_FROM_ZERO_DIVIDE = 4012;

		public const int ERR_ZERO_DIVIDE = 4013;

		public const int ERR_UNKNOWN_COMMAND = 4014;

		public const int ERR_WRONG_JUMP = 4015;

		public const int ERR_NOT_INITIALIZED_ARRAY = 4016;

		public const int ERR_DLL_CALLS_NOT_ALLOWED = 4017;

		public const int ERR_CANNOT_LOAD_LIBRARY = 4018;

		public const int ERR_CANNOT_CALL_FUNCTION = 4019;

		public const int ERR_EXTERNAL_CALLS_NOT_ALLOWED = 4020;

		public const int ERR_NO_MEMORY_FOR_RETURNED_STR = 4021;

		public const int ERR_SYSTEM_BUSY = 4022;

		public const int ERR_INVALID_FUNCTION_PARAMSCNT = 4050;

		public const int ERR_INVALID_FUNCTION_PARAMVALUE = 4051;

		public const int ERR_STRING_FUNCTION_INTERNAL = 4052;

		public const int ERR_SOME_ARRAY_ERROR = 4053;

		public const int ERR_INCORRECT_SERIESARRAY_USING = 4054;

		public const int ERR_CUSTOM_INDICATOR_ERROR = 4055;

		public const int ERR_INCOMPATIBLE_ARRAYS = 4056;

		public const int ERR_GLOBAL_VARIABLES_PROCESSING = 4057;

		public const int ERR_GLOBAL_VARIABLE_NOT_FOUND = 4058;

		public const int ERR_FUNC_NOT_ALLOWED_IN_TESTING = 4059;

		public const int ERR_FUNCTION_NOT_CONFIRMED = 4060;

		public const int ERR_SEND_MAIL_ERROR = 4061;

		public const int ERR_STRING_PARAMETER_EXPECTED = 4062;

		public const int ERR_INTEGER_PARAMETER_EXPECTED = 4063;

		public const int ERR_DOUBLE_PARAMETER_EXPECTED = 4064;

		public const int ERR_ARRAY_AS_PARAMETER_EXPECTED = 4065;

		public const int ERR_HISTORY_WILL_UPDATED = 4066;

		public const int ERR_TRADE_ERROR = 4067;

		public const int ERR_END_OF_FILE = 4099;

		public const int ERR_SOME_FILE_ERROR = 4100;

		public const int ERR_WRONG_FILE_NAME = 4101;

		public const int ERR_TOO_MANY_OPENED_FILES = 4102;

		public const int ERR_CANNOT_OPEN_FILE = 4103;

		public const int ERR_INCOMPATIBLE_FILEACCESS = 4104;

		public const int ERR_NO_ORDER_SELECTED = 4105;

		public const int ERR_UNKNOWN_SYMBOL = 4106;

		public const int ERR_INVALID_PRICE_PARAM = 4107;

		public const int ERR_INVALID_TICKET = 4108;

		public const int ERR_TRADE_NOT_ALLOWED = 4109;

		public const int ERR_LONGS_NOT_ALLOWED = 4110;

		public const int ERR_SHORTS_NOT_ALLOWED = 4111;

		public const int ERR_OBJECT_ALREADY_EXISTS = 4200;

		public const int ERR_UNKNOWN_OBJECT_PROPERTY = 4201;

		public const int ERR_OBJECT_DOES_NOT_EXIST = 4202;

		public const int ERR_UNKNOWN_OBJECT_TYPE = 4203;

		public const int ERR_NO_OBJECT_NAME = 4204;

		public const int ERR_OBJECT_COORDINATES_ERROR = 4205;

		public const int ERR_NO_SPECIFIED_SUBWINDOW = 4206;

		public const int ERR_SOME_OBJECT_ERROR = 4207;

		public const int MODE_ASCEND = 1;

		public const int MODE_DESCEND = 2;

		public const int MODE_GATORJAW = 1;

		public const int MODE_GATORTEETH = 2;

		public const int MODE_GATORLIPS = 3;

		public const int TIME_DATE = 2;

		public const int TIME_MINUTES = 4;

		public const int TIME_SECONDS = 8;

		public const int FILE_BIN = 2;

		public const int FILE_CSV = 4;

		public const int FILE_READ = 8;

		public const int FILE_WRITE = 16;

		public const int DOUBLE_VALUE = 8;

		public const int FLOAT_VALUE = 4;

		public const int CHAR_VALUE = 1;

		public const int SHORT_VALUE = 2;

		public const int LONG_VALUE = 4;

		public const int SEEK_SET = 0;

		public const int SEEK_CUR = 1;

		public const int SEEK_END = 2;

		[Browsable(false), XmlElement]
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		[Browsable(false), XmlElement]
		public string ID
		{
			get;
			set;
		}

		[Browsable(false), XmlIgnore]
		public int Number
		{
			get;
			set;
		}

		private ICore Core
		{
			get;
			set;
		}

		protected IChart Chart
		{
			get;
			private set;
		}

		protected Instrument Instrument
		{
			get;
			private set;
		}

		protected double Ask
		{
			get;
			private set;
		}

		protected int Bars
		{
			get
			{
				return this.ChartBars.Count;
			}
		}

		protected double Bid
		{
			get;
			private set;
		}

		protected int Digits
		{
			get;
			private set;
		}

		protected double Point
		{
			get;
			private set;
		}

		protected Array<double> Close
		{
			get;
			private set;
		}

		protected Array<double> High
		{
			get;
			private set;
		}

		protected Array<double> Low
		{
			get;
			private set;
		}

		protected Array<double> Open
		{
			get;
			private set;
		}

		protected Array<datetime> Time
		{
			get;
			private set;
		}

		protected Array<double> Volume
		{
			get;
			private set;
		}

		protected int LastError
		{
			get;
			set;
		}

		protected bool isStopped
		{
			get;
			set;
		}

		protected string copyright
		{
			get;
			set;
		}

		protected string link
		{
			get;
			set;
		}

		protected void OrderClose(int ticket, double lots, double price, int slippage, color Color = null)
		{
			bool flag = this is IndicatorBase;
			if (flag)
			{
				this.LastError = 4055;
			}
			else
			{
				this.Core.Broker.CloseOrder((long)ticket, CloseType.Unknown, "");
				this.LastError = 0;
			}
		}

		protected bool OrderCloseBy(int ticket, int opposite, color Color = null)
		{
			throw new NotSupportedException();
		}

		protected double OrderClosePrice()
		{
			bool flag = this._selectedOrder == null;
			double result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0.0;
			}
			else
			{
				result = (double)this._selectedOrder.ClosePrice;
			}
			return result;
		}

		protected datetime OrderCloseTime()
		{
			bool flag = this._selectedOrder == null;
			datetime result;
			if (flag)
			{
				this.LastError = 4105;
				result = DateTime.MinValue;
			}
			else
			{
				result = this._selectedOrder.CloseDate;
			}
			return result;
		}

		protected string OrderComment()
		{
			bool flag = this._selectedOrder == null;
			string result;
			if (flag)
			{
				this.LastError = 4105;
				result = string.Empty;
			}
			else
			{
				result = this._selectedOrder.Comment;
			}
			return result;
		}

		protected double OrderCommission()
		{
			bool flag = this._selectedOrder == null;
			double result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0.0;
			}
			else
			{
				result = (double)this._selectedOrder.TradeCommission;
			}
			return result;
		}

		protected bool OrderDelete(int ticket, color Color = null)
		{
			bool flag = this is IndicatorBase;
			bool result;
			if (flag)
			{
				this.LastError = 4055;
				result = false;
			}
			else
			{
				TradeResult tradeResult = this.Core.Broker.DeleteOrder((long)ticket, CloseType.Unknown, "");
				bool isSuccess = tradeResult.IsSuccess;
				if (isSuccess)
				{
					this.LastError = 0;
					result = true;
				}
				else
				{
					this.LastError = 3;
					result = false;
				}
			}
			return result;
		}

		protected datetime OrderExpiration()
		{
			bool flag = this._selectedOrder == null;
			datetime result;
			if (flag)
			{
				this.LastError = 4105;
				result = DateTime.MinValue;
			}
			else
			{
				DateTime? expirationDate = this._selectedOrder.ExpirationDate;
				result = (expirationDate.HasValue ? expirationDate.GetValueOrDefault() : null);
			}
			return result;
		}

		protected double OrderLots()
		{
			bool flag = this._selectedOrder == null;
			double result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0.0;
			}
			else
			{
				result = (double)this._selectedOrder.Quantity;
			}
			return result;
		}

		protected int OrderMagicNumber()
		{
			bool flag = this._selectedOrder == null;
			int result;
			if (flag)
			{
				this.LastError = 4105;
				result = -1;
			}
			else
			{
				result = (int)this._selectedOrder.Id;
			}
			return result;
		}

		protected bool OrderModify(int ticket, double price, double stoploss, double takeprofit, datetime expiration, color arrow_color = null)
		{
			bool flag = this is IndicatorBase;
			bool result;
			if (flag)
			{
				this.LastError = 4055;
				result = false;
			}
			else
			{
				Order order = this.Core.Broker.Orders.SingleOrDefault((Order o) => o.Id == (long)ticket);
				bool flag2 = order == null;
				if (flag2)
				{
					this.LastError = 4108;
					result = false;
				}
				else
				{
					Order order2 = new Order();
					order2.Copy(order);
					order2.Price = (decimal)price;
					order2.StopLoss = (decimal)stoploss;
					order2.TakeProfit = (decimal)takeprofit;
					bool flag3 = expiration == null;
					if (flag3)
					{
						order2.TimeInForce = TimeInForce.Day;
						order2.ExpirationDate = null;
					}
					else
					{
						order2.TimeInForce = TimeInForce.GTD;
						order2.ExpirationDate = new DateTime?(expiration);
					}
					TradeResult tradeResult = this.Core.Broker.ModifyOrder(order2);
					bool isSuccess = tradeResult.IsSuccess;
					if (isSuccess)
					{
						this.LastError = 0;
						result = true;
					}
					else
					{
						this.LastError = 3;
						result = false;
					}
				}
			}
			return result;
		}

		protected double OrderOpenPrice()
		{
			bool flag = this._selectedOrder == null;
			double result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0.0;
			}
			else
			{
				result = (double)this._selectedOrder.OpenPrice;
			}
			return result;
		}

		protected double OrderPendingPrice()
		{
			bool flag = this._selectedOrder == null;
			double result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0.0;
			}
			else
			{
				result = (double)this._selectedOrder.Price;
			}
			return result;
		}

		protected datetime OrderOpenTime()
		{
			bool flag = this._selectedOrder == null;
			datetime result;
			if (flag)
			{
				this.LastError = 4105;
				result = DateTime.MinValue;
			}
			else
			{
				result = this._selectedOrder.OpenDate;
			}
			return result;
		}

		protected datetime OrderFillTime()
		{
			bool flag = this._selectedOrder == null;
			datetime result;
			if (flag)
			{
				this.LastError = 4105;
				result = DateTime.MinValue;
			}
			else
			{
				bool hasValue = this._selectedOrder.FillDate.HasValue;
				if (hasValue)
				{
					result = this._selectedOrder.FillDate.Value;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		protected void OrderPrint()
		{
			bool flag = this._selectedOrder == null;
			if (flag)
			{
				this.LastError = 4105;
			}
			else
			{
				object[] values = new object[]
				{
					this._selectedOrder.Id,
					this._selectedOrder.OpenDate,
					CodeBase.ToMql4Operation(this._selectedOrder.Type, this._selectedOrder.Side),
					this._selectedOrder.Quantity,
					this._selectedOrder.OpenPrice,
					this._selectedOrder.StopLoss,
					this._selectedOrder.TakeProfit,
					this._selectedOrder.CloseDate,
					this._selectedOrder.ClosePrice,
					this._selectedOrder.TradeCommission,
					this._selectedOrder.Swap,
					this._selectedOrder.Profit,
					this._selectedOrder.Comment,
					this._selectedOrder.ExpirationDate
				};
				this.Print(new object[]
				{
					string.Join("; ", values)
				});
			}
		}

		protected double OrderProfit()
		{
			bool flag = this._selectedOrder == null;
			double result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0.0;
			}
			else
			{
				result = (double)this._selectedOrder.Profit;
			}
			return result;
		}

		protected bool OrderSelect(int index, int select, int pool = 0)
		{
			this._selectedOrder = null;
			bool flag = select == 1;
			if (flag)
			{
				this._selectedOrder = this.Core.Broker.Orders.Union(this.Core.Broker.HistoryOrders).SingleOrDefault((Order order) => order.Id == (long)index);
			}
			else
			{
				bool flag2 = select == 0;
				if (flag2)
				{
					bool flag3 = pool == 0;
					if (flag3)
					{
						this._selectedOrder = this.Core.Broker.Orders.ElementAtOrDefault(index);
					}
					else
					{
						bool flag4 = pool == 1;
						if (flag4)
						{
							this._selectedOrder = this.Core.Broker.HistoryOrders.ElementAtOrDefault(index);
						}
					}
				}
			}
			return this._selectedOrder != null;
		}

		protected int OrderSend(string symbol, int cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment = null, int magic = 0, datetime expiration = null, color arrow_color = null)
		{
			bool flag = this is IndicatorBase;
			int result;
			if (flag)
			{
				this.LastError = 4055;
				result = -1;
			}
			else
			{
				int lastError = 0;
				TradeType type;
				TradeSide side;
				bool flag2 = !CodeBase.FromMql4Operation(cmd, out type, out side, ref lastError);
				if (flag2)
				{
					this.LastError = lastError;
					result = -1;
				}
				else
				{
					Instrument instrumentBySymbol = this.Core.DataProvider.GetInstrumentBySymbol(symbol);
					bool flag3 = instrumentBySymbol == null;
					if (flag3)
					{
						this.LastError = 4106;
						result = -1;
					}
					else
					{
						Order order = new Order
						{
							Symbol = instrumentBySymbol.Symbol,
							Type = type,
							Side = side,
							Quantity = (decimal)volume,
							Price = (decimal)price,
							StopLoss = (decimal)stoploss,
							TakeProfit = (decimal)takeprofit,
							Comment = comment,
							CustomId = string.Empty,
							TimeInForce = ((expiration == null) ? TimeInForce.Day : TimeInForce.GTD),
							ExpirationDate = ((expiration == null) ? null : new DateTime?(expiration)),
							Sender = Sender.CodeBase
						};
						TradeResult<long> tradeResult = this.Core.Broker.PlaceOrder(order);
						bool isSuccess = tradeResult.IsSuccess;
						if (isSuccess)
						{
							this.LastError = 0;
							result = (int)tradeResult.Value;
						}
						else
						{
							this.Logger.Info(tradeResult.Error);
							this.LastError = 3;
							result = -1;
						}
					}
				}
			}
			return result;
		}

		protected int OrdersStatus()
		{
			return (int)this._selectedOrder.Status;
		}

		protected int OrdersHistoryTotal()
		{
			return this.Core.Broker.HistoryOrders.Count;
		}

		protected double OrderStopLoss()
		{
			bool flag = this._selectedOrder == null;
			double result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0.0;
			}
			else
			{
				result = (double)this._selectedOrder.StopLoss;
			}
			return result;
		}

		protected int OrdersTotal()
		{
			return this.Core.Broker.Orders.Count;
		}

		protected double OrderSwap()
		{
			bool flag = this._selectedOrder == null;
			double result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0.0;
			}
			else
			{
				result = (double)this._selectedOrder.Swap;
			}
			return result;
		}

		protected string OrderSymbol()
		{
			bool flag = this._selectedOrder == null;
			string result;
			if (flag)
			{
				this.LastError = 4105;
				result = string.Empty;
			}
			else
			{
				result = this._selectedOrder.Symbol;
			}
			return result;
		}

		protected double OrderTakeProfit()
		{
			bool flag = this._selectedOrder == null;
			double result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0.0;
			}
			else
			{
				result = (double)this._selectedOrder.TakeProfit;
			}
			return result;
		}

		protected int OrderTicket()
		{
			bool flag = this._selectedOrder == null;
			int result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0;
			}
			else
			{
				result = (int)this._selectedOrder.Id;
			}
			return result;
		}

		protected int OrderEmotion()
		{
			bool flag = this._selectedOrder == null;
			int result;
			if (flag)
			{
				this.LastError = 4105;
				result = 0;
			}
			else
			{
				result = (int)this._selectedOrder.Emoticon;
			}
			return result;
		}

		protected void OrderEmotion(int emotion)
		{
			bool flag = this._selectedOrder == null;
			if (flag)
			{
				this.LastError = 4105;
			}
			else
			{
				try
				{
					this._selectedOrder.Emoticon = (Emotion)emotion;
				}
				catch (Exception exception)
				{
					this.Logger.ErrorException("Unable to set order emotion.", exception);
				}
			}
		}

		protected int OrderType()
		{
			bool flag = this._selectedOrder == null;
			int result;
			if (flag)
			{
				this.LastError = 4105;
				result = -1;
			}
			else
			{
				bool flag2 = this._selectedOrder.Side == TradeSide.Buy;
				if (flag2)
				{
					switch (this._selectedOrder.Type)
					{
					case TradeType.Market:
						result = 0;
						return result;
					case TradeType.Limit:
						result = 2;
						return result;
					case TradeType.Stop:
						result = 4;
						return result;
					}
				}
				else
				{
					bool flag3 = this._selectedOrder.Side == TradeSide.Sell;
					if (flag3)
					{
						switch (this._selectedOrder.Type)
						{
						case TradeType.Market:
							result = 1;
							return result;
						case TradeType.Limit:
							result = 3;
							return result;
						case TradeType.Stop:
							result = 5;
							return result;
						}
					}
				}
				this.LastError = 4105;
				result = -1;
			}
			return result;
		}

		private static int? TryConvertServerErrorCodeToLastError(ServerErrorCodes code)
		{
			int? result;
			if (code <= ServerErrorCodes.ERR_TRADE_DISABLED)
			{
				if (code == ServerErrorCodes.ERR_INVALID_PRICE)
				{
					result = new int?(4107);
					return result;
				}
				if (code == ServerErrorCodes.ERR_TRADE_DISABLED)
				{
					result = new int?(4109);
					return result;
				}
			}
			else
			{
				if (code == ServerErrorCodes.ERR_LONG_POSITIONS_ONLY_ALLOWED)
				{
					result = new int?(4111);
					return result;
				}
				if (code == ServerErrorCodes.ERR_TRADE_CONTEXT_BUSY)
				{
					result = new int?(146);
					return result;
				}
			}
			result = null;
			return result;
		}

		private static bool FromMql4Operation(int operation, out TradeType type, out TradeSide side, ref int error)
		{
			error = 0;
			bool result;
			switch (operation)
			{
			case 0:
				type = TradeType.Market;
				side = TradeSide.Buy;
				result = true;
				break;
			case 1:
				type = TradeType.Market;
				side = TradeSide.Sell;
				result = true;
				break;
			case 2:
				type = TradeType.Limit;
				side = TradeSide.Buy;
				result = true;
				break;
			case 3:
				type = TradeType.Limit;
				side = TradeSide.Sell;
				result = true;
				break;
			case 4:
				type = TradeType.Stop;
				side = TradeSide.Buy;
				result = true;
				break;
			case 5:
				type = TradeType.Stop;
				side = TradeSide.Sell;
				result = true;
				break;
			default:
				type = TradeType.Unknown;
				side = TradeSide.Unknown;
				error = 3;
				result = false;
				break;
			}
			return result;
		}

		private static string ToMql4Operation(TradeType type, TradeSide side)
		{
			bool flag = side == TradeSide.Buy;
			if (flag)
			{
				switch (type)
				{
				case TradeType.Market:
				{
					string result = "OP_BUY";
					return result;
				}
				case TradeType.Limit:
				{
					string result = "OP_BUYLIMIT";
					return result;
				}
				case TradeType.Stop:
				{
					string result = "OP_BUYSTOP";
					return result;
				}
				}
			}
			else
			{
				bool flag2 = side == TradeSide.Sell;
				if (flag2)
				{
					switch (type)
					{
					case TradeType.Market:
					{
						string result = "OP_SELL";
						return result;
					}
					case TradeType.Limit:
					{
						string result = "OP_SELLLIMIT";
						return result;
					}
					case TradeType.Stop:
					{
						string result = "OP_SELLSTOP";
						return result;
					}
					}
				}
			}
			throw new NotSupportedException();
		}

		protected CodeBase()
		{
			this.LastError = 0;
			this.fileHelper = new MqlFileHelper();
			this.UninitializedReason = Alveo.UserCode.UninitializeReason.REASON_REMOVE;
			this.IndicatorCache = new IndicatorCache();
		}

		public void SetChart(IChart chart)
		{
			this.Chart = chart;
			this.Point = this.Chart.Points;
			this.Digits = this.Chart.Digits;
		}

		public void SetCore(ICore core)
		{
			bool flag = this.Core == null;
			if (flag)
			{
				this.Core = CoreConverter.UserBaseCore(core);
			}
		}

		public virtual void BaseInit()
		{
			this.Instrument = this.Core.DataProvider.Instruments.FirstOrDefault((Instrument i) => i.Symbol.Equals(this.Chart.Symbol));
			bool flag = this.Instrument == null;
			if (flag)
			{
				throw new ArgumentNullException("Instrument");
			}
			this.RefreshRates();
			this.Init();
		}

		public virtual void BaseStart()
		{
			this.RefreshRates();
			this.Start();
		}

		public virtual void BaseDeInit(int reason)
		{
			this.UninitializedReason = (UninitializeReason)reason;
			foreach (IndicatorBase current in this.IndicatorCache)
			{
				current.BaseDeInit(reason);
			}
			this.Deinit();
		}

		public abstract void UpdateProperties(ICode code);

		public abstract object Clone();

		public void Stop()
		{
			throw new NotImplementedException();
		}

		protected virtual int Init()
		{
			return 0;
		}

		protected virtual int Start()
		{
			return 0;
		}

		protected virtual int Deinit()
		{
			return 0;
		}

		protected Array<Bar> GetHistory(string symbol, int timeFrame)
		{
			Array<Bar> array = new Array<Bar>
			{
				IsSeries = true
			};
			foreach (Bar current in this.ChartBars)
			{
				array.Add(current);
			}
			bool flag = array.Count == 0;
			if (flag)
			{
				this.LastError = 4066;
			}
			return array;
		}

		protected double AccountBalance()
		{
			return this.ExceptionHandle<double>(() => (double)this.Core.Broker.AccountInfo.Balance);
		}

		protected double AccountCredit()
		{
			return this.ExceptionHandle<double>(() => (double)this.Core.Broker.Account.Credit);
		}

		protected string AccountCompany()
		{
			return this.ExceptionHandle<string>(() => this.Core.Broker.Account.Company);
		}

		protected string AccountCurrency()
		{
			return this.ExceptionHandle<string>(() => this.Core.Broker.Account.Currency);
		}

		protected double AccountEquity()
		{
			return this.ExceptionHandle<double>(() => (double)this.Core.Broker.AccountInfo.Equity);
		}

		protected double AccountFreeMargin()
		{
			return this.ExceptionHandle<double>(() => (double)this.Core.Broker.AccountInfo.FreeMargin);
		}

		protected double AccountFreeMarginCheck(string symbol, int cmd, double volume)
		{
			int num = 0;
			Instrument instrument = this.SymbolToInstrument(symbol, ref num);
			bool flag = num != 0;
			double result;
			if (flag)
			{
				this.LastError = num;
				result = -1.0;
			}
			else
			{
				bool flag2 = !this.IsValidCmd(cmd, ref num, true);
				if (flag2)
				{
					this.LastError = num;
					result = -1.0;
				}
				else
				{
					bool flag3 = volume < 0.01;
					if (flag3)
					{
						this.LastError = 4051;
						result = -1.0;
					}
					else
					{
						TradeType type;
						TradeSide side;
						bool flag4 = !CodeBase.FromMql4Operation(cmd, out type, out side, ref num);
						if (flag4)
						{
							this.LastError = num;
							result = -1.0;
						}
						else
						{
							Order order = new Order
							{
								Symbol = instrument.Symbol,
								Side = side,
								Type = type,
								Status = OrderStatus.PendingNew
							};
							decimal d = TradingCalculator.CalculateClosedOrderMargin(order, instrument, this.Core.Broker.Account.Leverage);
							decimal num2 = this.Core.Broker.AccountInfo.FreeMargin - d;
							bool flag5 = num2 < decimal.Zero;
							if (flag5)
							{
								this.LastError = 134;
								result = -1.0;
							}
							else
							{
								result = (double)num2;
							}
						}
					}
				}
			}
			return result;
		}

		protected double AccountFreeMarginMode()
		{
			return -1.0;
		}

		protected int AccountLeverage()
		{
			return this.ExceptionHandle<int>(() => this.Core.Broker.Account.Leverage);
		}

		protected double AccountMargin()
		{
			return this.ExceptionHandle<double>(() => (double)this.Core.Broker.AccountInfo.Margin);
		}

		protected string AccountName()
		{
			return this.ExceptionHandle<string>(() => this.Core.Broker.Account.UserName);
		}

		protected int AccountNumber()
		{
			return this.ExceptionHandle<int>(() => (int)this.Core.Broker.Account.AccountId);
		}

		protected double AccountProfit()
		{
			return this.ExceptionHandle<double>(() => (double)this.Core.Broker.AccountInfo.Profit);
		}

		protected string AccountServer()
		{
			return string.Empty;
		}

		protected int AccountStopoutLevel()
		{
			return -1;
		}

		protected int AccountStopoutMode()
		{
			return -1;
		}

		protected int ArrayBsearch<T>(Array<T> array, T value, int count = 0, int start = 0, int direction = 1)
		{
			bool flag = !Enum.IsDefined(typeof(SearchDirection), direction);
			int result;
			if (flag)
			{
				this.LastError = 4051;
				result = -1;
			}
			else
			{
				bool flag2 = start < 0 || count < 0 || typeof(T).GetInterface(typeof(IComparable).FullName) == null;
				if (flag2)
				{
					this.LastError = 4051;
					result = -1;
				}
				else
				{
					try
					{
						result = ((direction == 2) ? array.BinarySearch(start, count, value, null) : array.BinarySearch(start - count, count, value, null));
						return result;
					}
					catch
					{
						this.LastError = 4053;
					}
					result = -1;
				}
			}
			return result;
		}

		protected int ArrayCopy<T>(Array<T> destination, Array<T> source, int startDest = 0, int startSource = 0, int count = 0)
		{
			bool flag = startDest < 0 || startSource < 0 || count < 0;
			int result;
			if (flag)
			{
				this.LastError = 4051;
				result = -1;
			}
			else
			{
				try
				{
					int num = 0;
					int num2 = startDest;
					for (int i = startSource; i < source.Count; i++)
					{
						bool flag2 = num2 >= destination.Count;
						if (flag2)
						{
							break;
						}
						destination[num2, false] = source[i, false];
						num2++;
						num++;
						bool flag3 = count != 0 && num >= count;
						if (flag3)
						{
							break;
						}
					}
					result = num;
					return result;
				}
				catch
				{
					this.LastError = 4053;
				}
				result = -1;
			}
			return result;
		}

		protected int ArrayCopyRates(Array<object[]> array, string symbol = null, int timeFrame = 0)
		{
			bool flag = !string.IsNullOrEmpty(symbol) || timeFrame != 0;
			int result;
			if (flag)
			{
				this.LastError = 4066;
				result = -1;
			}
			else
			{
				try
				{
					int num = 0;
					for (int i = 0; i < this.Bars; i++)
					{
						array.Add(new object[]
						{
							this.ChartBars[i, false].BarTime,
							this.ChartBars[i, false].Open,
							this.ChartBars[i, false].Low,
							this.ChartBars[i, false].High,
							this.ChartBars[i, false].Close,
							this.ChartBars[i, false].Volume
						});
						num++;
					}
					result = num;
					return result;
				}
				catch
				{
					this.LastError = 4053;
				}
				result = -1;
			}
			return result;
		}

		protected int ArrayCopySeries(Array<datetime> array, int series, string symbol = null, int timeFrame = 0)
		{
			bool flag = !string.IsNullOrEmpty(symbol) || timeFrame != 0;
			int result;
			if (flag)
			{
				this.LastError = 4066;
				result = -1;
			}
			else
			{
				try
				{
					int num = 0;
					switch (series)
					{
					case 0:
						for (int i = 0; i < this.Bars; i++)
						{
							array.Add((double)this.ChartBars[i, false].Open);
							num++;
						}
						break;
					case 1:
						for (int j = 0; j < this.Bars; j++)
						{
							array.Add((double)this.ChartBars[j, false].Low);
							num++;
						}
						break;
					case 2:
						for (int k = 0; k < this.Bars; k++)
						{
							array.Add((double)this.ChartBars[k, false].High);
							num++;
						}
						break;
					case 3:
						for (int l = 0; l < this.Bars; l++)
						{
							array.Add((double)this.ChartBars[l, false].Close);
							num++;
						}
						break;
					case 4:
						for (int m = 0; m < this.Bars; m++)
						{
							array.Add((double)this.ChartBars[m, false].Volume);
							num++;
						}
						break;
					case 5:
						for (int n = 0; n < this.Bars; n++)
						{
							array.Add(this.ChartBars[n, false].BarTime);
							num++;
						}
						break;
					}
					result = num;
					return result;
				}
				catch
				{
					this.LastError = 4053;
				}
				result = -1;
			}
			return result;
		}

		protected bool ArrayGetAsSeries<T>(Array<T> array)
		{
			return array.IsSeries;
		}

		protected int ArrayDimension<T>(object array)
		{
			int result;
			try
			{
				int num = 0;
				object obj = array;
				while (true)
				{
					Array<T> array2 = obj as Array<T>;
					bool flag = array2 == null;
					if (flag)
					{
						break;
					}
					num++;
					bool flag2 = array2.Count > 0;
					if (!flag2)
					{
						goto IL_3E;
					}
					obj = array2[0, true];
					if (!(obj is Array<T>))
					{
						goto IL_50;
					}
				}
				result = -1;
				return result;
				IL_3E:
				IL_50:
				result = num;
				return result;
			}
			catch
			{
				this.LastError = 4053;
			}
			result = -1;
			return result;
		}

		protected int ArrayInitialize<T>(Array<T> array, T value)
		{
			int result;
			try
			{
				int num = 0;
				for (int i = 0; i < array.Count; i++)
				{
					array[i, false] = value;
					num++;
				}
				result = num;
				return result;
			}
			catch
			{
				this.LastError = 4053;
			}
			result = -1;
			return result;
		}

		protected int ArrayMaximum<T>(Array<T> array, int count = 0, int start = 0)
		{
			bool flag = typeof(T).GetInterface(typeof(IComparable).FullName) == null || count < 0;
			int result;
			if (flag)
			{
				this.LastError = 4051;
				result = -1;
			}
			else
			{
				try
				{
					bool flag2 = array.Count <= start;
					if (flag2)
					{
						result = -1;
						return result;
					}
					bool flag3 = count == 0;
					if (flag3)
					{
						count = array.Count;
					}
					T t = array[start, false];
					int num = start;
					for (int i = count; i > 0; i--)
					{
						bool flag4 = array.Count <= start;
						if (flag4)
						{
							break;
						}
						bool flag5 = ((IComparable)((object)array[start++, false])).CompareTo(t) > 0;
						if (flag5)
						{
							num = start - 1;
							t = array[num, false];
						}
					}
					result = num;
					return result;
				}
				catch
				{
					this.LastError = 4053;
				}
				result = -1;
			}
			return result;
		}

		protected int ArrayMinimum<T>(Array<T> array, int count = 0, int start = 0)
		{
			bool flag = typeof(T).GetInterface(typeof(IComparable).FullName) == null || count < 0;
			int result;
			if (flag)
			{
				this.LastError = 4051;
				result = -1;
			}
			else
			{
				try
				{
					bool flag2 = array.Count <= start;
					if (flag2)
					{
						result = -1;
						return result;
					}
					bool flag3 = count == 0;
					if (flag3)
					{
						count = array.Count;
					}
					T t = array[start, false];
					int num = start;
					for (int i = count; i > 0; i--)
					{
						bool flag4 = array.Count <= start;
						if (flag4)
						{
							break;
						}
						bool flag5 = ((IComparable)((object)array[start++, false])).CompareTo(t) < 0;
						if (flag5)
						{
							num = start - 1;
							t = array[num, false];
						}
					}
					result = num;
					return result;
				}
				catch
				{
					this.LastError = 4053;
				}
				result = -1;
			}
			return result;
		}

		protected int ArrayResize<T>(Array<T> data, int newSize)
		{
			int result;
			try
			{
				data.Resize(newSize);
				result = newSize;
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		protected bool ArraySetAsSeries<T>(Array<T> data, bool b)
		{
			bool isSeries = data.IsSeries;
			data.IsSeries = b;
			return isSeries;
		}

		protected int ArraySize<T>(Array<T> array)
		{
			return array.Count;
		}

		protected int ArraySort<T>(Array<T> data, int count = 0, int start = 0, int sortDir = 1)
		{
			bool isSeries = data.IsSeries;
			int result;
			if (isSeries)
			{
				this.LastError = 4054;
				result = -1;
			}
			else
			{
				data.Sort((count == 0) ? data.Count : count, start, sortDir == 1);
				result = data.Count;
			}
			return result;
		}

		public int FileOpen(string filename, int mode, char delimiter = ';')
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileOpen(filename, mode, delimiter));
		}

		public void FileClose(int handle)
		{
			this.ExecuteAndUpdateLastError(() => this.fileHelper.FileClose(handle));
		}

		protected void FileDelete(string filename)
		{
			this.ExecuteAndUpdateLastError(() => this.fileHelper.FileDelete(filename));
		}

		protected void FileFlush(int handle)
		{
			this.ExecuteAndUpdateLastError(() => this.fileHelper.FileFlush(handle));
		}

		protected bool FileIsEnding(int handle)
		{
			return this.ExecuteAndUpdateLastError<bool>(() => this.fileHelper.FileIsEnding(handle));
		}

		protected int FileOpenHistory(string filename, int mode, char delimiter = ';')
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileOpenHistory(filename, mode, delimiter));
		}

		protected bool FileIsLineEnding(int handle)
		{
			return this.ExecuteAndUpdateLastError<bool>(() => this.fileHelper.FileIsLineEnding(handle));
		}

		protected int FileReadArray(int handle, double[] array, int start, int count)
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileReadArray(handle, array, start, count));
		}

		protected double FileReadDouble(int handle, int size = 8)
		{
			return this.ExecuteAndUpdateLastError<double>(() => this.fileHelper.FileReadDouble(handle, size));
		}

		protected int FileReadInteger(int handle, int size = 4)
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileReadInteger(handle, size));
		}

		protected double FileReadNumber(int handle)
		{
			return this.ExecuteAndUpdateLastError<double>(() => this.fileHelper.FileReadNumber(handle));
		}

		protected string FileReadString(int handle, int length = 0)
		{
			return this.ExecuteAndUpdateLastError<string>(() => this.fileHelper.FileReadString(handle, length));
		}

		protected bool FileSeek(int handle, int offset, int origin)
		{
			return this.ExecuteAndUpdateLastError<bool>(() => this.fileHelper.FileSeek(handle, offset, origin));
		}

		protected int FileSize(int handle)
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileSize(handle));
		}

		protected int FileTell(int handle)
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileTell(handle));
		}

		protected int FileWrite(int handle, params object[] values)
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileWrite(handle, values));
		}

		protected int FileWriteArray(int handle, object[] array, int start, int count)
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileWriteArray(handle, array, start, count));
		}

		protected int FileWriteDouble(int handle, double value, int size = 8)
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileWriteDouble(handle, value, size));
		}

		protected int FileWriteInteger(int handle, int value, int size = 4)
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileWriteInteger(handle, value, size));
		}

		protected int FileWriteString(int handle, string value, int size)
		{
			return this.ExecuteAndUpdateLastError<int>(() => this.fileHelper.FileWriteString(handle, value, size));
		}

		protected bool GlobalVariableCheck(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			bool result;
			if (flag)
			{
				this.LastError = 4062;
				result = false;
			}
			else
			{
				result = this.Core.GlobalManager.IsGlobalVariableExist(name);
			}
			return result;
		}

		protected bool GlobalVariableDel(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			bool result;
			if (flag)
			{
				this.LastError = 4062;
				result = false;
			}
			else
			{
				try
				{
					result = this.Core.GlobalManager.DeleteGlobalVariable(name);
				}
				catch
				{
					this.LastError = 4058;
					result = false;
				}
			}
			return result;
		}

		protected double GlobalVariableGet(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			double result;
			if (flag)
			{
				this.LastError = 4062;
				result = 0.0;
			}
			else
			{
				try
				{
					result = this.Core.GlobalManager.GetGlobalVariable(name);
					return result;
				}
				catch
				{
					this.LastError = 4058;
				}
				result = 0.0;
			}
			return result;
		}

		protected string GlobalVariableName(int index)
		{
			string result;
			try
			{
				result = this.Core.GlobalManager.GetVariableNameAtIndex(index);
			}
			catch
			{
				this.LastError = 4058;
				result = string.Empty;
			}
			return result;
		}

		protected datetime GlobalVariableSet(string name, double value)
		{
			bool flag = string.IsNullOrEmpty(name);
			datetime result;
			if (flag)
			{
				this.LastError = 4062;
				result = DateTime.MinValue;
			}
			else
			{
				this.Core.GlobalManager.SetGlobalVariable(name, value);
				result = DateTime.Now;
			}
			return result;
		}

		protected bool GlobalVariableSetOnCondition(string name, double value, double checkValue)
		{
			bool flag = string.IsNullOrEmpty(name);
			bool result;
			if (flag)
			{
				this.LastError = 4062;
				result = false;
			}
			else
			{
				try
				{
					result = this.Core.GlobalManager.SetGlobalVariableOnCondition(name, value, checkValue);
					return result;
				}
				catch
				{
					this.LastError = 4058;
				}
				result = false;
			}
			return result;
		}

		protected int GlobalVariablesDeleteAll(string prefixName = "")
		{
			int result;
			try
			{
				result = this.Core.GlobalManager.GlobalVariablesDeleteAll(prefixName);
			}
			catch
			{
				this.LastError = 4057;
				result = 0;
			}
			return result;
		}

		protected int GlobalVariablesTotal()
		{
			return this.Core.GlobalManager.GlobalVariablesTotal();
		}

		protected int GetLastError()
		{
			int lastError = this.LastError;
			this.LastError = 0;
			return lastError;
		}

		protected bool IsConnected()
		{
			return this.ExceptionHandle<bool>(() => this.Core.DataProvider.IsConnected);
		}

		protected bool IsDemo()
		{
			return false;
		}

		protected bool IsDllsAllowed()
		{
			return false;
		}

		protected bool IsExpertEnabled()
		{
			return this.ExceptionHandle<bool>(() => this.Chart.IsExpertsEnabled);
		}

		protected bool IsLibrariesAllowed()
		{
			return false;
		}

		protected bool IsOptimization()
		{
			return false;
		}

		protected bool IsStopped()
		{
			return this.isStopped;
		}

		protected bool IsTesting()
		{
			return false;
		}

		protected bool IsTradeAllowed()
		{
			return true;
		}

		protected bool IsTradeContextBusy()
		{
			return false;
		}

		protected bool IsVisualMode()
		{
			return false;
		}

		protected int UninitializeReason()
		{
			return (int)this.UninitializedReason;
		}

		protected void Alert(params object[] values)
		{
			this.ExceptionHandle(delegate
			{
				StringBuilder stringBuilder = new StringBuilder();
				object[] values2 = values;
				for (int i = 0; i < values2.Length; i++)
				{
					object value = values2[i];
					stringBuilder.Append(value);
				}
				this.Core.NotificationPoster.Notify(new Notification(this.Core.DataProvider.ServerTime, stringBuilder.ToString(), Severity.Normal));
			});
		}

		protected void Notify(string content, Severity severity)
		{
			this.ExceptionHandle(delegate
			{
				this.Core.NotificationPoster.Notify(new Notification(this.Core.DataProvider.ServerTime, content, severity));
			});
		}

		protected void Notify(string content, Severity severity, Button button)
		{
			this.ExceptionHandle(delegate
			{
				this.Core.NotificationPoster.Notify(new Notification(this.Core.DataProvider.ServerTime, content, severity, button));
			});
		}

		protected void Notify(string content, Severity severity, Button button, ActionOnViewed action)
		{
			this.ExceptionHandle(delegate
			{
				this.Core.NotificationPoster.Notify(new Notification(this.Core.DataProvider.ServerTime, content, severity, button, action));
			});
		}

		protected void Comment(params object[] vals)
		{
			this.ExceptionHandle(delegate
			{
				IChart arg_3F_0 = this.Chart;
				string arg_3A_0 = "";
				IEnumerable<object> arg_35_0 = vals;
				Func<object, string> arg_35_1;
				if ((arg_35_1 = CodeBase.__c.__9__647_1) == null)
				{
					arg_35_1 = (CodeBase.__c.__9__647_1 = new Func<object, string>(CodeBase.__c.__9.<Comment>b__647_1));
				}
				arg_3F_0.ProcessComment(string.Join(arg_3A_0, arg_35_0.Select(arg_35_1)));
			});
		}

		protected int GetTickCount()
		{
			return Environment.TickCount;
		}

		protected double MarketInfo(string symbol, int type)
		{
			int lastError = 0;
			Instrument instrument = this.SymbolToInstrument(symbol, ref lastError);
			bool flag = instrument == null;
			double result;
			if (flag)
			{
				this.LastError = lastError;
				result = -1.0;
			}
			else
			{
				bool flag2 = !Enum.IsDefined(typeof(Marketinfo), type);
				if (flag2)
				{
					this.LastError = 4051;
					result = -1.0;
				}
				else
				{
					switch (type)
					{
					case 1:
						result = -1.0;
						return result;
					case 2:
						result = -1.0;
						return result;
					case 3:
					case 4:
					case 6:
					case 7:
					case 8:
					case 16:
						goto IL_367;
					case 5:
					{
						Quote quote = this.Core.DataProvider.GetQuote(instrument.Symbol);
						bool flag3 = quote == null;
						if (flag3)
						{
							result = -1.0;
							return result;
						}
						result = new datetime(quote.Time);
						return result;
					}
					case 9:
						try
						{
							bool flag4 = symbol == this.Chart.Symbol;
							if (flag4)
							{
								result = this.Bid;
								return result;
							}
							result = (double)this.Core.DataProvider.GetQuote(symbol).BidPrice;
							return result;
						}
						catch (Exception var_10_1A1)
						{
							this.LastError = 4106;
							result = -1.0;
							return result;
						}
						break;
					case 10:
						break;
					case 11:
						goto IL_228;
					case 12:
						result = (double)instrument.Digits;
						return result;
					case 13:
						result = -1.0;
						return result;
					case 14:
						result = -1.0;
						return result;
					case 15:
						result = (double)instrument.ContractSize;
						return result;
					case 17:
						result = (double)instrument.TickSize;
						return result;
					case 18:
						result = -1.0;
						return result;
					case 19:
						result = -1.0;
						return result;
					case 20:
						result = -1.0;
						return result;
					case 21:
						result = -1.0;
						return result;
					case 22:
						result = 1.0;
						return result;
					case 23:
						result = (double)this.Core.Broker.Account.MinQuantity;
						return result;
					case 24:
						result = (double)this.Core.Broker.Account.StepQuantity;
						return result;
					case 25:
						result = (double)this.Core.Broker.Account.MaxQuantity;
						return result;
					case 26:
						result = 0.0;
						return result;
					case 27:
						result = 0.0;
						return result;
					case 28:
						result = 0.0;
						return result;
					default:
						goto IL_367;
					}
					try
					{
						bool flag5 = symbol == this.Chart.Symbol;
						if (flag5)
						{
							result = this.Ask;
							return result;
						}
						result = (double)this.Core.DataProvider.GetQuote(symbol).AskPrice;
						return result;
					}
					catch (Exception var_12_209)
					{
						this.LastError = 4106;
						result = -1.0;
						return result;
					}
					IL_228:
					result = (double)instrument.TickSize;
					return result;
					IL_367:
					result = -1.0;
				}
			}
			return result;
		}

		protected int MessageBox(string text = "", string caption = "", int flags = -1)
		{
			bool flag = string.IsNullOrEmpty(text);
			int result;
			if (flag)
			{
				this.LastError = 4062;
				result = -1;
			}
			else
			{
				try
				{
					this.Core.NotificationPoster.Notify(new Notification(this.Core.DataProvider.ServerTime, text, Severity.Normal));
					result = 1;
				}
				catch
				{
					this.LastError = 4051;
					result = -1;
				}
			}
			return result;
		}

		protected void PlaySound(string filename)
		{
			bool flag = string.IsNullOrEmpty(filename);
			if (flag)
			{
				this.LastError = 4101;
			}
			else
			{
				try
				{
					string soundLocation = Path.Combine(this.Core.FolderManager.SoundDirectory, filename);
					SoundPlayer soundPlayer = new SoundPlayer(soundLocation);
					soundPlayer.PlaySync();
				}
				catch
				{
					this.LastError = 4101;
				}
			}
		}

		protected void Print(params object[] vals)
		{
			bool flag = this.Core != null && this.Core.OutputManager != null;
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < vals.Length; i++)
				{
					object value = vals[i];
					stringBuilder.Append(value);
				}
				this.Core.OutputManager.Print(stringBuilder.ToString());
			}
		}

		protected bool SendFTP(string filename, string ftpPath = null)
		{
			return false;
		}

		protected void SendMail(string subject, string someText)
		{
			throw new NotImplementedException();
		}

		protected bool SendNotification(string message)
		{
			return false;
		}

		protected void Sleep(int ms)
		{
			Thread.Sleep(ms);
		}

		protected string CharToStr(int charCode)
		{
			return char.ConvertFromUtf32(charCode);
		}

		protected string DoubleToStr(double value, int digits)
		{
			string format = "0." + new string('0', digits);
			return value.ToString(format);
		}

		protected double NormalizeDouble(double value, int digits)
		{
			return Math.Round(value, digits);
		}

		protected double StrToDouble(string value)
		{
			double result;
			bool flag = !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
			if (flag)
			{
				result = double.NaN;
			}
			return result;
		}

		protected int StrToInteger(string value)
		{
			int result;
			bool flag = !int.TryParse(value, out result);
			if (flag)
			{
				result = -2147483648;
			}
			return result;
		}

		protected datetime StrToTime(string value)
		{
			DateTime minValue;
			bool flag = DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out minValue);
			datetime result;
			if (flag)
			{
				result = minValue;
			}
			else
			{
				minValue = DateTime.MinValue;
				result = minValue;
			}
			return result;
		}

		protected string TimeToStr(object value, int mode = 6)
		{
			string format = string.Empty;
			DateTime dateTime = new DateTime(1970, 1, 1);
			bool flag = value is DateTime;
			if (flag)
			{
				dateTime = (DateTime)value;
			}
			else
			{
				bool flag2 = value is int || value is double;
				if (flag2)
				{
					dateTime = dateTime.AddSeconds(double.Parse(value.ToString()));
				}
			}
			switch (mode)
			{
			case 2:
				format = "yyyy.MM.dd";
				break;
			case 4:
				format = "hh:mm";
				break;
			case 6:
				format = "yyyy.MM.dd hh:mm";
				break;
			case 8:
				format = "ss";
				break;
			case 10:
				format = "yyyy.MM.dd ss";
				break;
			case 12:
				format = "hh:mm:ss";
				break;
			case 14:
				format = "yyyy.MM.dd hh:mm:ss";
				break;
			}
			return dateTime.ToString(format);
		}

		protected int Day()
		{
			return this.Core.DataProvider.ServerTime.Day;
		}

		protected int DayOfWeek()
		{
			return (int)this.Core.DataProvider.ServerTime.DayOfWeek;
		}

		protected int DayOfYear()
		{
			return this.Core.DataProvider.ServerTime.DayOfYear;
		}

		protected int Hour()
		{
			return this.Core.DataProvider.ServerTime.Hour;
		}

		protected int Minute()
		{
			return this.Core.DataProvider.ServerTime.Minute;
		}

		protected int Month()
		{
			return this.Core.DataProvider.ServerTime.Month;
		}

		protected int Seconds()
		{
			return this.Core.DataProvider.ServerTime.Second;
		}

		protected datetime TimeCurrent()
		{
			return this.Core.DataProvider.ServerTime;
		}

		protected int TimeDay(datetime date)
		{
			return date.DateTime.Day;
		}

		protected int TimeDayOfWeek(datetime date)
		{
			return (int)date.DateTime.DayOfWeek;
		}

		protected int TimeDayOfYear(datetime date)
		{
			return date.DateTime.DayOfYear;
		}

		protected int TimeHour(datetime time)
		{
			return time.DateTime.Hour;
		}

		protected DateTime TimeLocal()
		{
			return DateTime.Now;
		}

		protected int TimeMinute(datetime time)
		{
			return time.DateTime.Minute;
		}

		protected int TimeMonth(datetime time)
		{
			return time.DateTime.Month;
		}

		protected int TimeSeconds(datetime time)
		{
			return time.DateTime.Second;
		}

		protected int TimeYear(datetime time)
		{
			return time.DateTime.Year;
		}

		protected int Year()
		{
			return this.Core.DataProvider.ServerTime.Year;
		}

		protected bool CompareString(string s1, string s2, bool ignoreCase = false)
		{
			bool flag = string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2);
				result = (!flag2 && string.Compare(s1, s2, ignoreCase) == 0);
			}
			return result;
		}

		private bool IsValidSymbol(string symbol)
		{
			return !string.IsNullOrWhiteSpace(symbol);
		}

		private T ExecuteAndUpdateLastError<T>(Func<MqlResult<T>> func)
		{
			MqlResult<T> mqlResult = func();
			bool flag = mqlResult != MqlResult.Empty;
			if (flag)
			{
				this.LastError = mqlResult.ErrorCode;
			}
			return mqlResult.Value;
		}

		private void ExecuteAndUpdateLastError(Func<MqlResult> func)
		{
			MqlResult mqlResult = func();
			bool flag = mqlResult.ErrorCode != MqlResult.Empty.ErrorCode;
			if (flag)
			{
				this.LastError = mqlResult.ErrorCode;
			}
		}

		private T ExceptionHandle<T>(Func<T> func)
		{
			T result;
			try
			{
				result = func();
			}
			catch (Exception exception)
			{
				this.Logger.ErrorException("Function Error", exception);
				result = default(T);
			}
			return result;
		}

		private void ExceptionHandle(Action action)
		{
			try
			{
				action();
			}
			catch (Exception exception)
			{
				this.Logger.ErrorException("Function Error", exception);
			}
		}

		private Instrument SymbolToInstrument(string symbol, ref int error)
		{
			bool flag = symbol == "0" || symbol == null;
			if (flag)
			{
				symbol = this.Chart.Symbol;
			}
			Instrument instrumentBySymbol = this.Core.DataProvider.GetInstrumentBySymbol(symbol);
			bool flag2 = instrumentBySymbol == null;
			if (flag2)
			{
				error = 4106;
			}
			return instrumentBySymbol;
		}

		private bool IsValidCmd(int value, ref int error, bool onlyBuySell)
		{
			error = 0;
			bool flag = value == 0;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = value == 1;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = value == 2 && !onlyBuySell;
					if (flag3)
					{
						result = true;
					}
					else
					{
						bool flag4 = value == 3 && !onlyBuySell;
						if (flag4)
						{
							result = true;
						}
						else
						{
							bool flag5 = value == 4 && !onlyBuySell;
							if (flag5)
							{
								result = true;
							}
							else
							{
								bool flag6 = value == 5 && !onlyBuySell;
								if (flag6)
								{
									result = true;
								}
								else
								{
									error = 4051;
									result = false;
								}
							}
						}
					}
				}
			}
			return result;
		}

		protected double MathAbs(double value)
		{
			return Math.Abs(value);
		}

		protected double MathArccos(double x)
		{
			return Math.Acos(x);
		}

		protected double MathArcsin(double x)
		{
			return Math.Asin(x);
		}

		protected double MathArctan(double x)
		{
			return Math.Atan(x);
		}

		protected double MathCeil(double x)
		{
			return Math.Ceiling(x);
		}

		protected double MathCos(double value)
		{
			return Math.Cos(value);
		}

		protected double MathExp(double d)
		{
			return Math.Exp(d);
		}

		protected double MathFloor(double x)
		{
			return Math.Floor(x);
		}

		protected double MathLog(double x)
		{
			return Math.Log(x);
		}

		protected double MathMax(double value1, double value2)
		{
			return Math.Max(value1, value2);
		}

		protected double MathMin(double value1, double value2)
		{
			return Math.Min(value1, value2);
		}

		protected double MathMod(double value, double value2)
		{
			return value % value2;
		}

		protected double MathPow(double value, double exponent)
		{
			return Math.Pow(value, exponent);
		}

		protected int MathRand()
		{
			return this.random.Next(0, 32767);
		}

		protected double MathRound(double value)
		{
			return Math.Round(value);
		}

		protected double MathSin(double value)
		{
			return Math.Sin(value);
		}

		protected double MathSqrt(double x)
		{
			return Math.Sqrt(x);
		}

		protected void MathSrand(int seed)
		{
			this.random = new Random(seed);
		}

		protected double MathTan(double x)
		{
			return Math.Tan(x);
		}

		protected string StringConcatenate(params object[] values)
		{
			return string.Join(",", values);
		}

		protected int StringFind(string text, string matchedText, int start = 0)
		{
			return text.IndexOf(matchedText, start, StringComparison.Ordinal);
		}

		protected int StringGetChar(string text, int pos)
		{
			return (string.IsNullOrEmpty(text) || pos < 0 || pos > text.Length - 1) ? -1 : ((int)text[pos]);
		}

		protected int StringLen(string text)
		{
			return string.IsNullOrEmpty(text) ? -1 : text.Length;
		}

		protected string StringSetChar(string text, int pos, int value)
		{
			StringBuilder stringBuilder = new StringBuilder(text);
			char value2 = char.ConvertFromUtf32(value).ToCharArray()[0];
			stringBuilder[pos] = value2;
			return stringBuilder.ToString();
		}

		protected string StringSubstr(string text, int start, int length = 0)
		{
			bool flag = start < 0 || string.IsNullOrEmpty(text) || start + length > text.Length;
			string result;
			if (flag)
			{
				this.LastError = 4062;
				result = string.Empty;
			}
			else
			{
				result = ((length > 0) ? text.Substring(start, length) : text.Substring(start));
			}
			return result;
		}

		protected string StringTrimLeft(string text)
		{
			return text.TrimStart(new char[0]);
		}

		protected string StringTrimRight(string text)
		{
			return text.TrimEnd(new char[0]);
		}

		protected string TerminalCompany()
		{
			throw new NotImplementedException();
		}

		protected string TerminalName()
		{
			throw new NotImplementedException();
		}

		protected string TerminalPath()
		{
			return Directory.GetCurrentDirectory();
		}

		protected void HideTestIndicators(bool hide)
		{
			throw new NotImplementedException();
		}

		protected int Period()
		{
			return (int)this.Chart.TimeFrame;
		}

		protected bool RefreshRates()
		{
			bool flag = this.Chart == null || this.Chart.Bars == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.ChartBars = new Array<Bar>(this.Chart.Bars)
				{
					IsSeries = true
				};
				IEnumerable<Bar> arg_69_0 = this.ChartBars;
				Func<Bar, double> arg_69_1;
				if ((arg_69_1 = CodeBase.__c.__9__722_0) == null)
				{
					arg_69_1 = (CodeBase.__c.__9__722_0 = new Func<Bar, double>(CodeBase.__c.__9.<RefreshRates>b__722_0));
				}
				this.Open = new Array<double>(arg_69_0.Select(arg_69_1))
				{
					IsSeries = true
				};
				IEnumerable<Bar> arg_A7_0 = this.ChartBars;
				Func<Bar, double> arg_A7_1;
				if ((arg_A7_1 = CodeBase.__c.__9__722_1) == null)
				{
					arg_A7_1 = (CodeBase.__c.__9__722_1 = new Func<Bar, double>(CodeBase.__c.__9.<RefreshRates>b__722_1));
				}
				this.High = new Array<double>(arg_A7_0.Select(arg_A7_1))
				{
					IsSeries = true
				};
				IEnumerable<Bar> arg_E5_0 = this.ChartBars;
				Func<Bar, double> arg_E5_1;
				if ((arg_E5_1 = CodeBase.__c.__9__722_2) == null)
				{
					arg_E5_1 = (CodeBase.__c.__9__722_2 = new Func<Bar, double>(CodeBase.__c.__9.<RefreshRates>b__722_2));
				}
				this.Low = new Array<double>(arg_E5_0.Select(arg_E5_1))
				{
					IsSeries = true
				};
				IEnumerable<Bar> arg_123_0 = this.ChartBars;
				Func<Bar, double> arg_123_1;
				if ((arg_123_1 = CodeBase.__c.__9__722_3) == null)
				{
					arg_123_1 = (CodeBase.__c.__9__722_3 = new Func<Bar, double>(CodeBase.__c.__9.<RefreshRates>b__722_3));
				}
				this.Close = new Array<double>(arg_123_0.Select(arg_123_1))
				{
					IsSeries = true
				};
				IEnumerable<Bar> arg_161_0 = this.ChartBars;
				Func<Bar, double> arg_161_1;
				if ((arg_161_1 = CodeBase.__c.__9__722_4) == null)
				{
					arg_161_1 = (CodeBase.__c.__9__722_4 = new Func<Bar, double>(CodeBase.__c.__9.<RefreshRates>b__722_4));
				}
				this.Volume = new Array<double>(arg_161_0.Select(arg_161_1))
				{
					IsSeries = true
				};
				IEnumerable<Bar> arg_19F_0 = this.ChartBars;
				Func<Bar, datetime> arg_19F_1;
				if ((arg_19F_1 = CodeBase.__c.__9__722_5) == null)
				{
					arg_19F_1 = (CodeBase.__c.__9__722_5 = new Func<Bar, datetime>(CodeBase.__c.__9.<RefreshRates>b__722_5));
				}
				this.Time = new Array<datetime>(arg_19F_0.Select(arg_19F_1))
				{
					IsSeries = true
				};
				this.Ask = this.Chart.AskPrice;
				this.Bid = this.Chart.BidPrice;
				result = true;
			}
			return result;
		}

		protected string Symbol()
		{
			return this.Chart.Symbol;
		}

		protected int WindowBarsPerChart()
		{
			return this.Chart.BarsPerChart;
		}

		protected string WindowExpertName()
		{
			return this.Name;
		}

		protected int WindowsFind(string name)
		{
			return this.Chart.FindWindowByName(name);
		}

		protected int WindowFirstVisibleBar()
		{
			return this.Chart.FirstVisibleBar;
		}

		protected int WindowHandle(string symbol, int timeframe)
		{
			throw new NotSupportedException();
		}

		protected bool WindowIsVisible(int index)
		{
			return this.Chart.IsWindowVisible(index);
		}

		protected int WindowOnDropped()
		{
			throw new NotSupportedException();
		}

		protected double WindowPriceMax(int index = 0)
		{
			return this.Chart.GetWindowMaxPrice(index);
		}

		protected double WindowPriceMin(int index = 0)
		{
			return this.Chart.GetWindowPriceMin(index);
		}

		protected double WindowsPriceOnDropped()
		{
			throw new NotSupportedException();
		}

		protected void WindowRedraw()
		{
			this.Chart.RedrawAll();
		}

		protected bool WindowScreenShot(string filename, int size_x, int size_y, int start_bar = -1, int chart_scale = 0, int chart_mode = -1)
		{
			bool flag = filename.Intersect(Path.GetInvalidFileNameChars()).Any<char>() || filename.Contains("\\");
			bool result;
			if (flag)
			{
				this.LastError = 4101;
				result = false;
			}
			else
			{
				bool flag2 = chart_scale == 0;
				if (flag2)
				{
					chart_scale = 1;
				}
				bool flag3 = chart_scale < 0 || chart_scale > 5;
				if (flag3)
				{
					this.LastError = 4051;
					result = false;
				}
				else
				{
					result = this.Chart.SaveScreenToFile(filename);
				}
			}
			return result;
		}

		protected datetime WindowTimeOnDropped()
		{
			throw new NotSupportedException();
		}

		protected int WindowsTotal()
		{
			return this.Chart.WindowsTotal;
		}

		protected int WindowXOnDropped()
		{
			throw new NotSupportedException();
		}

		protected int WindowYOnDropped()
		{
			throw new NotSupportedException();
		}

		protected bool ObjectCreate(string name, int type, int window, datetime time1 = null, double price1 = 0.0, datetime time2 = null, double price2 = 0.0, datetime time3 = null, double price3 = 0.0)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			bool result;
			if (flag)
			{
				this.LastError = 4204;
				result = false;
			}
			else
			{
				try
				{
				}
				catch
				{
					this.LastError = 4203;
					result = false;
					return result;
				}
				switch (type)
				{
				case 0:
					result = this.DrawVLine(name, window, time1);
					break;
				case 1:
					result = this.DrawHLine(name, window, price1);
					break;
				case 2:
					result = this.DrawTrendLine(name, window, time1, price1, time2, price2);
					break;
				case 3:
					result = this.DrawTrendByAngle(name, window, time1, price1, 0.0);
					break;
				case 4:
					result = this.DrawRegression(name, window, time1, time2);
					break;
				case 5:
					result = this.DrawChannel(name, window, time1, price1, time2, price2);
					break;
				case 6:
					result = this.DrawStdDeviationChannel(name, window, time1, time2);
					break;
				case 7:
					result = this.DrawGannLine(name, window, time1, price1, time2, price2);
					break;
				case 8:
					result = this.DrawGannFan(name, window, time1, price1, time2, price2);
					break;
				case 9:
					result = this.DrawGannGrid(name, window, time1, price1, time2, price2);
					break;
				case 10:
					result = this.DrawFibRetracement(name, window, time1, price1, time2, price2);
					break;
				case 11:
					result = this.DrawFibTimes(name, window, time1, price1, time2, price2);
					break;
				case 12:
					result = this.DrawFibFan(name, window, time1, price1, time2, price2);
					break;
				case 13:
					result = this.DrawFiboArc(name, window, time1, price1, time2, price2);
					break;
				case 14:
					result = this.DrawFibExpansion(name, window, time1, price1, time2, price2, time3, price3);
					break;
				case 15:
					result = this.DrawFibChannel(name, window, time1, price1, time2, price2, time3, price3);
					break;
				case 16:
					result = this.DrawRectangle(name, window, time1, price1, time2, price2);
					break;
				case 17:
					result = this.DrawTriangle(name, window, time1, price1, time2, price2, time3, price3);
					break;
				case 18:
					result = this.DrawEllipse(name, window, time1, price1, time2, price2);
					break;
				case 19:
					result = this.DrawPitchfork(name, window, time1, price1, time2, price2, time3, price3);
					break;
				case 20:
					result = this.DrawCycles(name, window, time1, price1, time2, price2);
					break;
				case 21:
					result = this.DrawText(name, window, time1, price1, "Text");
					break;
				case 22:
					result = this.DrawArrow(name, window, time1, price1, 33);
					break;
				case 23:
					result = this.DrawLabel(name, window, time1, price1, "Label");
					break;
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		protected bool ObjectDelete(string name)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			bool result;
			if (flag)
			{
				this.LastError = 4204;
				result = false;
			}
			else
			{
				RunTimeErrors runTimeErrors = this.Chart.DeleteObject(name);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		protected string ObjectDescription(string name)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			string result;
			if (flag)
			{
				this.LastError = 4204;
				result = string.Empty;
			}
			else
			{
				string text = this.Chart.ObjectDescription(name);
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					this.LastError = 4202;
				}
				result = text;
			}
			return result;
		}

		protected int ObjectFind(string name)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			int result;
			if (flag)
			{
				this.LastError = 4204;
				result = -1;
			}
			else
			{
				result = this.Chart.ObjectFind(name);
			}
			return result;
		}

		protected bool ObjectSetText(string name, string text, int fontSize = 0, string fontName = "", color fontColor = null)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			bool result;
			if (flag)
			{
				this.LastError = 4204;
				result = false;
			}
			else
			{
				int num = (int)this.Chart.ObjectSetText(name, text, fontSize, fontName, fontColor);
				bool flag2 = num != 0;
				if (flag2)
				{
					this.LastError = num;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		protected bool ObjectSet(string name, int index, object value)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			bool result;
			if (flag)
			{
				this.LastError = 4204;
				result = false;
			}
			else
			{
				if (index <= 103)
				{
					switch (index)
					{
					case 0:
						result = this.SetObjectTime1(name, Convert.ToDouble(value));
						return result;
					case 1:
						result = this.SetObjectPrice1(name, Convert.ToDouble(value));
						return result;
					case 2:
						result = this.SetObjectTime2(name, Convert.ToDouble(value));
						return result;
					case 3:
						result = this.SetObjectPrice2(name, Convert.ToDouble(value));
						return result;
					case 4:
						result = this.SetObjectTime3(name, Convert.ToDouble(value));
						return result;
					case 5:
						result = this.SetObjectPrice3(name, Convert.ToDouble(value));
						return result;
					case 6:
						result = this.SetObjectColor(name, Convert.ToInt32(value));
						return result;
					case 7:
						result = this.SetObjectDashStyle(name, Convert.ToInt32(value));
						return result;
					case 8:
						result = this.SetObjectWidth(name, Convert.ToInt32(value));
						return result;
					case 9:
						result = this.SetObjectBack(name, Convert.ToBoolean(value));
						return result;
					case 10:
						result = this.SetObjectRay(name, Convert.ToBoolean(value));
						return result;
					case 11:
						result = this.SetObjectEllipse(name, Convert.ToBoolean(value));
						return result;
					case 12:
						result = this.SetObjectScale(name, Convert.ToDouble(value));
						return result;
					case 13:
						result = this.SetObjectAngle(name, Convert.ToDouble(value));
						return result;
					case 14:
						result = this.SetObjectArrowCode(name, Convert.ToInt32(value));
						return result;
					case 15:
						result = this.SetObjectTimeFrames(name, Convert.ToInt32(value));
						return result;
					case 16:
						result = this.SetObjectDeviation(name, Convert.ToDouble(value));
						return result;
					default:
						switch (index)
						{
						case 100:
							result = this.SetObjectFontSize(name, (float)Convert.ToDouble(value));
							return result;
						case 101:
							result = this.SetObjectCorner(name, Convert.ToInt32(value));
							return result;
						case 102:
							result = this.SetObjectXDistance(name, Convert.ToInt32(value));
							return result;
						case 103:
							result = this.SetObjectYDistance(name, Convert.ToInt32(value));
							return result;
						}
						break;
					}
				}
				else
				{
					switch (index)
					{
					case 200:
						result = this.SetObjectFiboLevels(name, Convert.ToInt32(value));
						return result;
					case 201:
						result = this.SetObjectLevelColor(name, Convert.ToInt32(value));
						return result;
					case 202:
						result = this.SetObjectLevelStyle(name, Convert.ToInt32(value));
						return result;
					case 203:
						result = this.SetObjectLevelWidth(name, Convert.ToInt32(value));
						return result;
					default:
						if (index == 210)
						{
							result = this.SetObjectFirstLevel(name, 0, Convert.ToDouble(value));
							return result;
						}
						break;
					}
				}
				bool flag2 = index >= 210 && index < 242;
				if (flag2)
				{
					this.SetObjectFirstLevel(name, index - 210, Convert.ToDouble(value));
					result = false;
				}
				else
				{
					this.LastError = 4201;
					result = false;
				}
			}
			return result;
		}

		protected double ObjectGet(string name, int index)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			double result;
			if (flag)
			{
				this.LastError = 4204;
				result = -1.0;
			}
			else
			{
				ObjectProperties objectProperties;
				try
				{
					objectProperties = (ObjectProperties)index;
				}
				catch
				{
					bool flag2 = index >= 210 && index < 242;
					if (!flag2)
					{
						this.LastError = 4201;
						result = -1.0;
						return result;
					}
					objectProperties = ObjectProperties.OBJPROP_FIRSTLEVEL;
				}
				ObjectProperties objectProperties2 = objectProperties;
				if (objectProperties2 <= ObjectProperties.OBJPROP_YDISTANCE)
				{
					switch (objectProperties2)
					{
					case ObjectProperties.OBJPROP_TIME1:
						result = this.GetObjectTime1(name);
						return result;
					case ObjectProperties.OBJPROP_PRICE1:
						result = this.GetObjectPrice1(name);
						return result;
					case ObjectProperties.OBJPROP_TIME2:
						result = this.GetObjectTime2(name);
						return result;
					case ObjectProperties.OBJPROP_PRICE2:
						result = this.GetObjectPrice2(name);
						return result;
					case ObjectProperties.OBJPROP_TIME3:
						result = this.GetObjectTime3(name);
						return result;
					case ObjectProperties.OBJPROP_PRICE3:
						result = this.GetObjectPrice3(name);
						return result;
					case ObjectProperties.OBJPROP_COLOR:
						result = (double)this.GetObjectColor(name);
						return result;
					case ObjectProperties.OBJPROP_STYLE:
						result = (double)this.GetObjectStyle(name);
						return result;
					case ObjectProperties.OBJPROP_WIDTH:
						result = this.GetObjectWidth(name);
						return result;
					case ObjectProperties.OBJPROP_BACK:
						result = this.GetObjectBack(name);
						return result;
					case ObjectProperties.OBJPROP_RAY:
						result = this.GetObjectRay(name);
						return result;
					case ObjectProperties.OBJPROP_ELLIPSE:
						result = this.GetObjectEllipse(name);
						return result;
					case ObjectProperties.OBJPROP_SCALE:
						result = this.GetObjectScale(name);
						return result;
					case ObjectProperties.OBJPROP_ANGLE:
						result = this.GetObjectAngle(name);
						return result;
					case ObjectProperties.OBJPROP_ARROWCODE:
						result = (double)this.GetObjectArrowCode(name);
						return result;
					case ObjectProperties.OBJPROP_TIMEFRAMES:
						result = (double)this.GetObjectTimeFrames(name);
						return result;
					case ObjectProperties.OBJPROP_DEVIATION:
						result = this.GetObjectDeviation(name);
						return result;
					default:
						switch (objectProperties2)
						{
						case ObjectProperties.OBJPROP_FONTSIZE:
							result = (double)this.GetObjectFontSize(name);
							return result;
						case ObjectProperties.OBJPROP_CORNER:
							result = (double)this.GetObjectCorner(name);
							return result;
						case ObjectProperties.OBJPROP_XDISTANCE:
							result = (double)this.GetObjectXDistance(name);
							return result;
						case ObjectProperties.OBJPROP_YDISTANCE:
							result = (double)this.GetObjectYDistance(name);
							return result;
						}
						break;
					}
				}
				else
				{
					switch (objectProperties2)
					{
					case ObjectProperties.OBJPROP_FIBOLEVELS:
						result = (double)this.GetObjectFiboLevels(name);
						return result;
					case ObjectProperties.OBJPROP_LEVELCOLOR:
						result = (double)this.GetObjectLevelColor(name);
						return result;
					case ObjectProperties.OBJPROP_LEVELSTYLE:
						result = (double)CodeBase.DashStyleToInt(this.GetObjectLevelStyle(name));
						return result;
					case ObjectProperties.OBJPROP_LEVELWIDTH:
						result = this.GetObjectLevelWidth(name);
						return result;
					default:
						if (objectProperties2 == ObjectProperties.OBJPROP_FIRSTLEVEL)
						{
							result = this.GetObjectFirstLevel(name, index - 210);
							return result;
						}
						break;
					}
				}
				result = -1.0;
			}
			return result;
		}

		protected string ObjectGetFiboDescription(string name, int index)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			string result;
			if (flag)
			{
				this.LastError = 4204;
				result = string.Empty;
			}
			else
			{
				bool flag2 = index < 0 || index >= 32;
				if (flag2)
				{
					this.LastError = 4051;
					result = string.Empty;
				}
				else
				{
					string text = this.HandleError<string>(() => this.Chart.ObjectGetFiboDescription(name, index), string.Empty);
					bool flag3 = string.IsNullOrEmpty(text);
					if (flag3)
					{
						this.LastError = 4202;
					}
					result = text;
				}
			}
			return result;
		}

		protected bool ObjectSetFiboDescription(string name, int index, string text)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			bool result;
			if (flag)
			{
				this.LastError = 4204;
				result = false;
			}
			else
			{
				bool flag2 = index < 0 || index >= 32;
				if (flag2)
				{
					this.LastError = 4051;
					result = false;
				}
				else
				{
					int num = (int)this.Chart.ObjectSetFiboDescription(name, index, text);
					bool flag3 = num != 0;
					if (flag3)
					{
						this.LastError = num;
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		protected int ObjectGetShiftByValue(string name, double value)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			int result;
			if (flag)
			{
				this.LastError = 4204;
				result = -1;
			}
			else
			{
				result = this.HandleError<int>(() => this.Chart.ObjectGetShiftByValue(name, value), -1);
			}
			return result;
		}

		protected double ObjectGetValueByShift(string name, int shift)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			double result;
			if (flag)
			{
				this.LastError = 4204;
				result = -1.0;
			}
			else
			{
				result = this.HandleError<double>(() => this.Chart.ObjectGetValueByShift(name, shift), -1.0);
			}
			return result;
		}

		protected bool ObjectMove(string name, int point, datetime time1, double price1)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			bool result;
			if (flag)
			{
				this.LastError = 4204;
				result = false;
			}
			else
			{
				bool flag2 = point < 0 || point > 2;
				if (flag2)
				{
					this.LastError = 4051;
					result = false;
				}
				else
				{
					int num = (int)this.Chart.ObjectMove(name, point, time1, price1);
					bool flag3 = num != 0;
					if (flag3)
					{
						this.LastError = num;
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		protected string ObjectName(int index)
		{
			bool flag = index < 0;
			string result;
			if (flag)
			{
				this.LastError = 4002;
				result = string.Empty;
			}
			else
			{
				result = this.HandleError<string>(() => this.Chart.ObjectName(index), null);
			}
			return result;
		}

		protected int ObjectsDeleteAll(int window = -1, int type = -1)
		{
			bool flag = type != -1;
			ObjType objType;
			int result;
			if (flag)
			{
				objType = ObjType.OBJ_NONE;
			}
			else
			{
				try
				{
					objType = (ObjType)type;
				}
				catch
				{
					result = -1;
					return result;
				}
			}
			result = this.HandleError<int>(() => this.Chart.ObjectsDeleteAll((window == -1) ? -1 : window, (int)objType), -1);
			return result;
		}

		protected int ObjectsTotal(int type = -1)
		{
			return this.HandleError<int>(() => this.Chart.ObjectsTotal(type), -1);
		}

		protected int ObjectType(string name)
		{
			bool flag = !CodeBase.IsValidObjectName(name);
			int result;
			if (flag)
			{
				this.LastError = 4204;
				result = -1;
			}
			else
			{
				result = this.HandleError<int>(() => this.Chart.GetObjectType(name), -1);
			}
			return result;
		}

		private static bool IsValidObjectName(string name)
		{
			return !string.IsNullOrEmpty(name);
		}

		private static bool IsPointValid(datetime time, double price)
		{
			bool flag = time == null || time == DateTime.MinValue || Math.Abs(price - 0.0) < 1E-06 || double.IsNaN(price);
			return !flag;
		}

		public static int DashStyleToInt(DashStyle style)
		{
			bool flag = style.Equals(DashStyles.Solid);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = style.Equals(DashStyles.Dash);
				if (flag2)
				{
					result = 1;
				}
				else
				{
					bool flag3 = style.Equals(DashStyles.Dot);
					if (flag3)
					{
						result = 2;
					}
					else
					{
						bool flag4 = style.Equals(DashStyles.DashDot);
						if (flag4)
						{
							result = 3;
						}
						else
						{
							bool flag5 = style.Equals(DashStyles.DashDotDot);
							if (flag5)
							{
								result = 4;
							}
							else
							{
								result = -1;
							}
						}
					}
				}
			}
			return result;
		}

		private static DashStyle IntToDashStyle(int style)
		{
			DashStyle result;
			switch (style)
			{
			case 0:
				result = DashStyles.Solid;
				break;
			case 1:
				result = DashStyles.Dash;
				break;
			case 2:
				result = DashStyles.Dot;
				break;
			case 3:
				result = DashStyles.DashDot;
				break;
			case 4:
				result = DashStyles.DashDotDot;
				break;
			default:
				throw new ArgumentOutOfRangeException("style");
			}
			return result;
		}

		private T HandleError<T>(Func<ChartResult<T>> func, T errorValue)
		{
			ChartResult<T> chartResult = func();
			bool hasError = chartResult.HasError;
			T result;
			if (hasError)
			{
				this.LastError = (int)chartResult.RunTimeErrors.Value;
				result = errorValue;
			}
			else
			{
				result = chartResult.Value;
			}
			return result;
		}

		protected bool SetObjectTime1(string name, datetime value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectTime1(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectPrice1(string name, double value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectPrice1(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectTime2(string name, datetime value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectTime2(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectPrice2(string name, double value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectPrice2(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectTime3(string name, datetime value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectTime3(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectPrice3(string name, double value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectPrice3(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectColor(string name, color value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectColor(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectDashStyle(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectDashStyle(name, CodeBase.IntToDashStyle(value));
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectWidth(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectWidth(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectBack(string name, bool value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectBack(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectRay(string name, bool value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectRay(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectEllipse(string name, bool value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectEllipse(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectScale(string name, double value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectScale(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectAngle(string name, double value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectAngle(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectArrowCode(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectArrowCode(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectTimeFrames(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectTimeFrames(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectDeviation(string name, double value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectDeviation(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectFontSize(string name, float value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectFontSize(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectCorner(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectCorner(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectXDistance(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectXDistance(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectYDistance(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectYDistance(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectFiboLevels(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectFiboLevels(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectLevelColor(string name, color value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectLevelColor(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectLevelStyle(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectLevelStyle(name, CodeBase.IntToDashStyle(value));
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectLevelWidth(string name, int value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectLevelWidth(name, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool SetObjectFirstLevel(string name, int index, double value)
		{
			RunTimeErrors runTimeErrors = this.Chart.SetObjectFirstLevel(name, index, value);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected datetime GetObjectTime1(string name)
		{
			return this.HandleError<datetime>(() => this.Chart.GetObjectTime1(name), -1);
		}

		protected double GetObjectPrice1(string name)
		{
			return this.HandleError<double>(() => this.Chart.GetObjectPrice1(name), -1.0);
		}

		protected datetime GetObjectTime2(string name)
		{
			return this.HandleError<datetime>(() => this.Chart.GetObjectTime2(name), -1);
		}

		protected double GetObjectPrice2(string name)
		{
			return this.HandleError<double>(() => this.Chart.GetObjectPrice2(name), -1.0);
		}

		protected datetime GetObjectTime3(string name)
		{
			return this.HandleError<datetime>(() => this.Chart.GetObjectTime3(name), -1);
		}

		protected double GetObjectPrice3(string name)
		{
			return this.HandleError<double>(() => this.Chart.GetObjectPrice3(name), -1.0);
		}

		protected color GetObjectColor(string name)
		{
			return this.HandleError<color>(() => this.Chart.GetObjectColor(name), -1);
		}

		protected int GetObjectStyle(string name)
		{
			return this.HandleError<int>(() => this.Chart.GetObjectStyle(name), -1);
		}

		protected double GetObjectWidth(string name)
		{
			return this.HandleError<double>(() => this.Chart.GetObjectWidth(name), -1.0);
		}

		protected double GetObjectBack(string name)
		{
			return (double)this.HandleError<int>(() => this.Chart.GetObjectBack(name), -1);
		}

		protected double GetObjectRay(string name)
		{
			return (double)this.HandleError<int>(() => this.Chart.GetObjectRay(name), -1);
		}

		protected double GetObjectEllipse(string name)
		{
			return (double)this.HandleError<int>(() => this.Chart.GetObjectEllipse(name), -1);
		}

		protected double GetObjectScale(string name)
		{
			return this.HandleError<double>(() => this.Chart.GetObjectScale(name), -1.0);
		}

		protected double GetObjectAngle(string name)
		{
			return this.HandleError<double>(() => this.Chart.GetObjectAngle(name), -1.0);
		}

		protected int GetObjectArrowCode(string name)
		{
			return this.HandleError<int>(() => this.Chart.GetObjectArrowCode(name), -1);
		}

		protected ObjectVisibility GetObjectTimeFrames(string name)
		{
			return this.HandleError<ObjectVisibility>(delegate
			{
				ChartResult<int> objectTimeFrames = this.Chart.GetObjectTimeFrames(name);
				return new ChartResult<ObjectVisibility>
				{
					RunTimeErrors = objectTimeFrames.RunTimeErrors,
					Value = (ObjectVisibility)objectTimeFrames.Value
				};
			}, ObjectVisibility.EMPTY);
		}

		protected double GetObjectDeviation(string name)
		{
			return this.HandleError<double>(() => this.Chart.GetObjectDeviation(name), -1.0);
		}

		protected float GetObjectFontSize(string name)
		{
			return this.HandleError<float>(() => this.Chart.GetObjectFontSize(name), -1f);
		}

		protected int GetObjectCorner(string name)
		{
			return this.HandleError<int>(() => this.Chart.GetObjectCorner(name), -1);
		}

		protected int GetObjectXDistance(string name)
		{
			return this.HandleError<int>(() => this.Chart.GetObjectXDistance(name), -1);
		}

		protected int GetObjectYDistance(string name)
		{
			return this.HandleError<int>(() => this.Chart.GetObjectYDistance(name), -1);
		}

		protected int GetObjectFiboLevels(string name)
		{
			return this.HandleError<int>(() => this.Chart.GetObjectFiboLevels(name), -1);
		}

		protected color GetObjectLevelColor(string name)
		{
			return this.HandleError<color>(() => this.Chart.GetObjectLevelColor(name), -1);
		}

		protected DashStyle GetObjectLevelStyle(string name)
		{
			return this.HandleError<DashStyle>(delegate
			{
				ChartResult<int> objectLevelStyle = this.Chart.GetObjectLevelStyle(name);
				bool hasError = objectLevelStyle.HasError;
				ChartResult<DashStyle> result;
				if (hasError)
				{
					result = new ChartResult<DashStyle>
					{
						RunTimeErrors = objectLevelStyle.RunTimeErrors
					};
				}
				else
				{
					result = new ChartResult<DashStyle>
					{
						Value = CodeBase.IntToDashStyle(objectLevelStyle.Value)
					};
				}
				return result;
			}, DashStyles.Solid);
		}

		protected double GetObjectLevelWidth(string name)
		{
			return this.HandleError<double>(() => this.Chart.GetObjectLevelWidth(name), -1.0);
		}

		protected double GetObjectFirstLevel(string name, int index)
		{
			return this.HandleError<double>(() => this.Chart.GetObjectFirstLevel(name, index), -1.0);
		}

		protected bool DrawEllipse(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawEllipse(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawHLine(string name, int window, double price1)
		{
			bool flag = double.IsNaN(price1) || double.IsInfinity(price1);
			bool result;
			if (flag)
			{
				this.LastError = 4051;
				result = false;
			}
			else
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawHLine(name, window, price1);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		protected bool DrawLabel(string name, int window, datetime time1, double price1, string text)
		{
			RunTimeErrors runTimeErrors = this.Chart.DrawLabel(name, window, time1, price1, text);
			bool flag = runTimeErrors > (RunTimeErrors)0;
			bool result;
			if (flag)
			{
				this.LastError = (int)runTimeErrors;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		protected bool DrawPitchfork(string name, int window, datetime time1, double price1, datetime time2, double price2, datetime time3, double price3)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2) && CodeBase.IsPointValid(time3, price3);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawPitchfork(name, window, time1, price1, time2, price2, time3, price3);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawRegression(string name, int window, datetime time1, datetime time2)
		{
			bool flag = time1 != null && time2 != null;
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawRegression(name, window, time1, time2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawStdDeviationChannel(string name, int window, datetime time1, datetime time2)
		{
			bool flag = time1 != null && time2 != null;
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawStdDeviationChannel(name, window, time1, time2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawText(string name, int window, datetime time1, double price1, string text)
		{
			bool flag = CodeBase.IsPointValid(time1, price1);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawText(name, window, time1, price1, text);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawTrendByAngle(string name, int window, datetime time1, double price1, double angle)
		{
			bool flag = CodeBase.IsPointValid(time1, price1);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawTrendByAngle(name, window, time1, price1, angle);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawTriangle(string name, int window, datetime time1, double price1, datetime time2, double price2, datetime time3, double price3)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawTriangle(name, window, time1, price1, time2, price2, time3, price3);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawVLine(string name, int window, datetime time1)
		{
			bool flag = time1 != null;
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawVLine(name, window, time1);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawGannLine(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawGannLine(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawGannGrid(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawGannGrid(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawGannFan(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawGannFan(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawFibTimes(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawFibTimes(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawFibRetracement(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawFibRetracement(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawFibFan(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawFibFan(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawFibExpansion(string name, int window, datetime time1, double price1, datetime time2, double price2, datetime time3, double price3)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawFibExpansion(name, window, time1, price1, time2, price2, time3, price3);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawFiboArc(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawFiboArc(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawFibChannel(string name, int window, datetime time1, double price1, datetime time2, double price2, datetime time3, double price3)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawFibChannel(name, window, time1, price1, time2, price2, time3, price3);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawArrow(string name, int window, datetime time1, double price1, int arrow)
		{
			bool flag = CodeBase.IsPointValid(time1, price1);
			bool result;
			if (flag)
			{
				int num = (int)this.Chart.DrawArrow(name, window, time1, price1, arrow);
				bool flag2 = num != 0;
				if (flag2)
				{
					this.LastError = num;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawChannel(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawChannel(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawTrendLine(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawTrendLine(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawCycles(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawCycles(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected bool DrawRectangle(string name, int window, datetime time1, double price1, datetime time2, double price2)
		{
			bool flag = CodeBase.IsPointValid(time1, price1) && CodeBase.IsPointValid(time2, price2);
			bool result;
			if (flag)
			{
				RunTimeErrors runTimeErrors = this.Chart.DrawRectangle(name, window, time1, price1, time2, price2);
				bool flag2 = runTimeErrors > (RunTimeErrors)0;
				if (flag2)
				{
					this.LastError = (int)runTimeErrors;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			else
			{
				this.LastError = 4051;
				result = false;
			}
			return result;
		}

		protected Array<double> GetPrice(Array<Bar> chartData, PriceConstants appliedPrice)
		{
			IEnumerable<double> enumerable;
			switch (appliedPrice)
			{
			case PriceConstants.PRICE_CLOSE:
			{
				Func<Bar, double> arg_4A_1;
				if ((arg_4A_1 = CodeBase.__c.__9__837_0) == null)
				{
					arg_4A_1 = (CodeBase.__c.__9__837_0 = new Func<Bar, double>(CodeBase.__c.__9.<GetPrice>b__837_0));
				}
				enumerable = chartData.Select(arg_4A_1);
				break;
			}
			case PriceConstants.PRICE_OPEN:
			{
				Func<Bar, double> arg_75_1;
				if ((arg_75_1 = CodeBase.__c.__9__837_1) == null)
				{
					arg_75_1 = (CodeBase.__c.__9__837_1 = new Func<Bar, double>(CodeBase.__c.__9.<GetPrice>b__837_1));
				}
				enumerable = chartData.Select(arg_75_1);
				break;
			}
			case PriceConstants.PRICE_HIGH:
			{
				Func<Bar, double> arg_A0_1;
				if ((arg_A0_1 = CodeBase.__c.__9__837_2) == null)
				{
					arg_A0_1 = (CodeBase.__c.__9__837_2 = new Func<Bar, double>(CodeBase.__c.__9.<GetPrice>b__837_2));
				}
				enumerable = chartData.Select(arg_A0_1);
				break;
			}
			case PriceConstants.PRICE_LOW:
			{
				Func<Bar, double> arg_CB_1;
				if ((arg_CB_1 = CodeBase.__c.__9__837_3) == null)
				{
					arg_CB_1 = (CodeBase.__c.__9__837_3 = new Func<Bar, double>(CodeBase.__c.__9.<GetPrice>b__837_3));
				}
				enumerable = chartData.Select(arg_CB_1);
				break;
			}
			case PriceConstants.PRICE_MEDIAN:
			{
				Func<Bar, double> arg_F6_1;
				if ((arg_F6_1 = CodeBase.__c.__9__837_4) == null)
				{
					arg_F6_1 = (CodeBase.__c.__9__837_4 = new Func<Bar, double>(CodeBase.__c.__9.<GetPrice>b__837_4));
				}
				enumerable = chartData.Select(arg_F6_1);
				break;
			}
			case PriceConstants.PRICE_TYPICAL:
			{
				Func<Bar, double> arg_11E_1;
				if ((arg_11E_1 = CodeBase.__c.__9__837_5) == null)
				{
					arg_11E_1 = (CodeBase.__c.__9__837_5 = new Func<Bar, double>(CodeBase.__c.__9.<GetPrice>b__837_5));
				}
				enumerable = chartData.Select(arg_11E_1);
				break;
			}
			case PriceConstants.PRICE_WEIGHTED:
			{
				Func<Bar, double> arg_146_1;
				if ((arg_146_1 = CodeBase.__c.__9__837_6) == null)
				{
					arg_146_1 = (CodeBase.__c.__9__837_6 = new Func<Bar, double>(CodeBase.__c.__9.<GetPrice>b__837_6));
				}
				enumerable = chartData.Select(arg_146_1);
				break;
			}
			default:
				throw new NotImplementedException("invalid Price Type");
			}
			Array<double> array = new Array<double>
			{
				IsSeries = true
			};
			foreach (double current in enumerable)
			{
				array.Add(current);
			}
			return array;
		}

		private void AddIndicatorToIndicatorBuffer(IndicatorBase indicator, string symbol, int timeframe)
		{
			indicator.Symbol = symbol;
			indicator.TimeFrame = timeframe;
			indicator.SetChart(this.Chart);
			indicator.SetCore(this.Core);
			indicator.BaseInit();
			indicator.BaseStart();
			this.IndicatorCache.Add(indicator);
		}

		protected double iMA(string symbol, int timeFrame, int period, int maShift, int maMethod, int appliedPrice, int shift)
		{
			IndicatorBase indicatorBase = null;
			switch (maMethod)
			{
			case 0:
			{
				Type typeFromHandle = typeof(SMA);
				indicatorBase = this.IndicatorCache.GetCash(typeFromHandle, new object[]
				{
					symbol,
					timeFrame,
					period,
					(PriceConstants)appliedPrice
				});
				bool flag = indicatorBase == null;
				if (flag)
				{
					SMA sMA = new SMA
					{
						IndicatorPeriod = period,
						PriceType = (PriceConstants)appliedPrice
					};
					indicatorBase = sMA;
					this.AddIndicatorToIndicatorBuffer(indicatorBase, symbol, timeFrame);
				}
				break;
			}
			case 1:
			{
				Type typeFromHandle = typeof(EMA);
				indicatorBase = this.IndicatorCache.GetCash(typeFromHandle, new object[]
				{
					symbol,
					timeFrame,
					period,
					(PriceConstants)appliedPrice
				});
				bool flag2 = indicatorBase == null;
				if (flag2)
				{
					EMA eMA = new EMA
					{
						IndicatorPeriod = period,
						PriceType = (PriceConstants)appliedPrice
					};
					indicatorBase = eMA;
					this.AddIndicatorToIndicatorBuffer(indicatorBase, symbol, timeFrame);
				}
				break;
			}
			case 2:
			{
				Type typeFromHandle = typeof(SSMA);
				indicatorBase = this.IndicatorCache.GetCash(typeFromHandle, new object[]
				{
					symbol,
					timeFrame,
					period,
					(PriceConstants)appliedPrice
				});
				bool flag3 = indicatorBase == null;
				if (flag3)
				{
					SSMA sSMA = new SSMA
					{
						IndicatorPeriod = period,
						PriceType = (PriceConstants)appliedPrice
					};
					indicatorBase = sSMA;
					this.AddIndicatorToIndicatorBuffer(indicatorBase, symbol, timeFrame);
				}
				break;
			}
			case 3:
			{
				Type typeFromHandle = typeof(LWMA);
				indicatorBase = this.IndicatorCache.GetCash(typeFromHandle, new object[]
				{
					symbol,
					timeFrame,
					period,
					(PriceConstants)appliedPrice
				});
				bool flag4 = indicatorBase == null;
				if (flag4)
				{
					LWMA lWMA = new LWMA
					{
						IndicatorPeriod = period,
						PriceType = (PriceConstants)appliedPrice
					};
					indicatorBase = lWMA;
					this.AddIndicatorToIndicatorBuffer(indicatorBase, symbol, timeFrame);
				}
				break;
			}
			}
			bool flag5 = shift + maShift < 0 || shift + maShift >= indicatorBase.Series[0].Values.Count;
			double result;
			if (flag5)
			{
				result = -1.0;
			}
			else
			{
				result = indicatorBase.Series[0].Values[shift + maShift, true];
			}
			return result;
		}

		public double iAC(string symbol, int timeFrame, int shift)
		{
			AC aC = (AC)this.IndicatorCache.GetCash(typeof(AC), new object[]
			{
				symbol,
				timeFrame
			});
			bool flag = aC == null;
			if (flag)
			{
				aC = new AC();
				this.AddIndicatorToIndicatorBuffer(aC, symbol, timeFrame);
			}
			bool flag2 = shift < 0 || shift >= aC.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = aC.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iMACD(string symbol, int timeFrame, int fastEmaPeriod, int slowEmaPeriod, int signalPeriod, int appliedPrice, int mode, int shift)
		{
			MACD mACD = (MACD)this.IndicatorCache.GetCash(typeof(MACD), new object[]
			{
				symbol,
				timeFrame,
				fastEmaPeriod,
				slowEmaPeriod,
				signalPeriod,
				(PriceConstants)appliedPrice
			});
			bool flag = mACD == null;
			if (flag)
			{
				mACD = new MACD
				{
					FastPeriod = fastEmaPeriod,
					SlowPeriod = slowEmaPeriod,
					SignalPeriod = signalPeriod,
					PriceType = (PriceConstants)appliedPrice
				};
				this.AddIndicatorToIndicatorBuffer(mACD, symbol, timeFrame);
			}
			double result;
			if (mode != 0)
			{
				if (mode != 1)
				{
					result = 2147483647.0;
				}
				else
				{
					bool flag2 = shift < 0 || shift >= mACD.Series[1].Values.Count;
					if (flag2)
					{
						result = -1.0;
					}
					else
					{
						result = mACD.Series[1].Values[shift, true];
					}
				}
			}
			else
			{
				bool flag3 = shift < 0 || shift >= mACD.Series[0].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = mACD.Series[0].Values[shift, true];
				}
			}
			return result;
		}

		public double iAD(string symbol, int timeframe, int shift)
		{
			AD aD = (AD)this.IndicatorCache.GetCash(typeof(AD), new object[]
			{
				symbol,
				timeframe
			});
			bool flag = aD == null;
			if (flag)
			{
				aD = new AD();
				this.AddIndicatorToIndicatorBuffer(aD, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= aD.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = aD.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iAlligator(string symbol, int timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, int ma_method, int applied_price, int mode, int shift)
		{
			Alligator alligator = (Alligator)this.IndicatorCache.GetCash(typeof(Alligator), new object[]
			{
				symbol,
				timeframe,
				jaw_period,
				jaw_shift,
				teeth_period,
				teeth_shift,
				lips_period,
				lips_shift,
				(MovingAverageType)ma_method,
				(PriceConstants)applied_price
			});
			bool flag = alligator == null;
			if (flag)
			{
				alligator = new Alligator
				{
					JawPeriod = jaw_period,
					JawShift = jaw_shift,
					TeethPeriod = teeth_period,
					TeethShift = teeth_shift,
					LipsPeriod = lips_period,
					LipsShift = lips_shift,
					MAType = (MovingAverageType)ma_method,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(alligator, symbol, timeframe);
			}
			double result;
			switch (mode)
			{
			case 1:
			{
				bool flag2 = shift < 0 || shift >= alligator.Series[0].Values.Count;
				if (flag2)
				{
					result = -1.0;
				}
				else
				{
					result = alligator.Series[0].Values[shift, true];
				}
				break;
			}
			case 2:
			{
				bool flag3 = shift < 0 || shift >= alligator.Series[1].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = alligator.Series[1].Values[shift, true];
				}
				break;
			}
			case 3:
			{
				bool flag4 = shift < 0 || shift >= alligator.Series[2].Values.Count;
				if (flag4)
				{
					result = -1.0;
				}
				else
				{
					result = alligator.Series[2].Values[shift, true];
				}
				break;
			}
			default:
				result = -1.0;
				break;
			}
			return result;
		}

		public double iADX(string symbol, int timeframe, int period, int applied_price, int mode, int shift)
		{
			ADX aDX = (ADX)this.IndicatorCache.GetCash(typeof(ADX), new object[]
			{
				symbol,
				timeframe,
				period,
				(PriceConstants)applied_price
			});
			bool flag = aDX == null;
			if (flag)
			{
				aDX = new ADX
				{
					IndicatorPeriod = period,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(aDX, symbol, timeframe);
			}
			double result;
			switch (mode)
			{
			case 0:
			{
				bool flag2 = shift < 0 || shift >= aDX.Series[0].Values.Count;
				if (flag2)
				{
					result = -1.0;
				}
				else
				{
					result = aDX.Series[0].Values[shift, true];
				}
				break;
			}
			case 1:
			{
				bool flag3 = shift < 0 || shift >= aDX.Series[1].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = aDX.Series[1].Values[shift, true];
				}
				break;
			}
			case 2:
			{
				bool flag4 = shift < 0 || shift >= aDX.Series[2].Values.Count;
				if (flag4)
				{
					result = -1.0;
				}
				else
				{
					result = aDX.Series[2].Values[shift, true];
				}
				break;
			}
			default:
				result = -1.0;
				break;
			}
			return result;
		}

		public double iATR(string symbol, int timeframe, int period, int shift)
		{
			ATR aTR = (ATR)this.IndicatorCache.GetCash(typeof(ATR), new object[]
			{
				symbol,
				timeframe,
				period
			});
			bool flag = aTR == null;
			if (flag)
			{
				aTR = new ATR
				{
					IndicatorPeriod = period
				};
				this.AddIndicatorToIndicatorBuffer(aTR, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= aTR.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = aTR.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iAO(string symbol, int timeframe, int shift)
		{
			AO aO = (AO)this.IndicatorCache.GetCash(typeof(AO), new object[]
			{
				symbol,
				timeframe,
				AO.DefaultPeriod1,
				AO.DefaultPeriod2,
				AO.DefaultSmoothing,
				AO.DefaultPriceConstants
			});
			bool flag = aO == null;
			if (flag)
			{
				aO = new AO();
				this.AddIndicatorToIndicatorBuffer(aO, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= aO.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = aO.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iBearsPower(string symbol, int timeframe, int period, int applied_price, int shift)
		{
			BearPower bearPower = (BearPower)this.IndicatorCache.GetCash(typeof(BearPower), new object[]
			{
				symbol,
				timeframe,
				period,
				(PriceConstants)applied_price
			});
			bool flag = bearPower == null;
			if (flag)
			{
				bearPower = new BearPower
				{
					IndicatorPeriod = period,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(bearPower, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= bearPower.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = bearPower.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iBands(string symbol, int timeframe, int period, int deviation, int bands_shift, int applied_price, int mode, int shift)
		{
			Bands bands = (Bands)this.IndicatorCache.GetCash(typeof(Bands), new object[]
			{
				symbol,
				timeframe,
				period,
				deviation,
				(PriceConstants)applied_price
			});
			bool flag = bands == null;
			if (flag)
			{
				bands = new Bands
				{
					IndicatorPeriod = period,
					Deviation = deviation,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(bands, symbol, timeframe);
			}
			double result;
			switch (mode)
			{
			case 0:
			{
				bool flag2 = shift + bands_shift < 0 || shift + bands_shift >= bands.Series[0].Values.Count;
				if (flag2)
				{
					result = -1.0;
				}
				else
				{
					result = bands.Series[0].Values[shift + bands_shift, true];
				}
				break;
			}
			case 1:
			{
				bool flag3 = shift + bands_shift < 0 || shift + bands_shift >= bands.Series[1].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = bands.Series[1].Values[shift + bands_shift, true];
				}
				break;
			}
			case 2:
			{
				bool flag4 = shift + bands_shift < 0 || shift + bands_shift >= bands.Series[2].Values.Count;
				if (flag4)
				{
					result = -1.0;
				}
				else
				{
					result = bands.Series[2].Values[shift + bands_shift, true];
				}
				break;
			}
			default:
				result = -1.0;
				break;
			}
			return result;
		}

		public double iBullsPower(string symbol, int timeframe, int period, int applied_price, int shift)
		{
			BullsPower bullsPower = (BullsPower)this.IndicatorCache.GetCash(typeof(BullsPower), new object[]
			{
				symbol,
				timeframe,
				period,
				(PriceConstants)applied_price
			});
			bool flag = bullsPower == null;
			if (flag)
			{
				bullsPower = new BullsPower
				{
					IndicatorPeriod = period,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(bullsPower, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= bullsPower.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = bullsPower.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iCCI(string symbol, int timeframe, int period, int applied_price, int shift)
		{
			CCI cCI = (CCI)this.IndicatorCache.GetCash(typeof(CCI), new object[]
			{
				symbol,
				timeframe,
				period,
				(PriceConstants)applied_price
			});
			bool flag = cCI == null;
			if (flag)
			{
				cCI = new CCI
				{
					IndicatorPeriod = period,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(cCI, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= cCI.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = cCI.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iEnvelopes(string symbol, int timeframe, int ma_period, int ma_method, int ma_shift, int applied_price, double deviation, int mode, int shift)
		{
			Envelopes envelopes = (Envelopes)this.IndicatorCache.GetCash(typeof(Envelopes), new object[]
			{
				symbol,
				timeframe,
				ma_period,
				(MovingAverageType)ma_method,
				(PriceConstants)applied_price,
				deviation
			});
			bool flag = envelopes == null;
			if (flag)
			{
				envelopes = new Envelopes
				{
					IndicatorPeriod = ma_period,
					MAType = (MovingAverageType)ma_method,
					PriceType = (PriceConstants)applied_price,
					Deviation = deviation
				};
				this.AddIndicatorToIndicatorBuffer(envelopes, symbol, timeframe);
			}
			double result;
			switch (mode)
			{
			case 0:
				result = this.iMA(symbol, timeframe, ma_period, ma_shift, ma_method, applied_price, shift);
				break;
			case 1:
			{
				bool flag2 = shift + ma_shift < 0 || shift + ma_shift >= envelopes.Series[0].Values.Count;
				if (flag2)
				{
					result = -1.0;
				}
				else
				{
					result = envelopes.Series[0].Values[shift + ma_shift, true];
				}
				break;
			}
			case 2:
			{
				bool flag3 = shift + ma_shift < 0 || shift + ma_shift >= envelopes.Series[1].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = envelopes.Series[1].Values[shift + ma_shift, true];
				}
				break;
			}
			default:
				result = -1.0;
				break;
			}
			return result;
		}

		public double iForce(string symbol, int timeframe, int period, int ma_method, int applied_price, int shift)
		{
			Force force = (Force)this.IndicatorCache.GetCash(typeof(Force), new object[]
			{
				symbol,
				timeframe,
				period,
				(MovingAverageType)ma_method,
				(PriceConstants)applied_price
			});
			bool flag = force == null;
			if (flag)
			{
				force = new Force
				{
					IndicatorPeriod = period,
					MAType = (MovingAverageType)ma_method,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(force, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= force.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = force.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iFractals(string symbol, int timeframe, int mode, int shift)
		{
			Fractals fractals = (Fractals)this.IndicatorCache.GetCash(typeof(Fractals), new object[]
			{
				symbol,
				timeframe
			});
			bool flag = fractals == null;
			if (flag)
			{
				fractals = new Fractals();
				this.AddIndicatorToIndicatorBuffer(fractals, symbol, timeframe);
			}
			double result;
			if (mode != 1)
			{
				if (mode != 2)
				{
					result = 2147483647.0;
				}
				else
				{
					bool flag2 = shift < 0 || shift >= fractals.Series[0].Values.Count;
					if (flag2)
					{
						result = -1.0;
					}
					else
					{
						result = fractals.Series[1].Values[shift, true];
					}
				}
			}
			else
			{
				bool flag3 = shift < 0 || shift >= fractals.Series[0].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = fractals.Series[0].Values[shift, true];
				}
			}
			return result;
		}

		public double iGator(string symbol, int timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, int ma_method, int applied_price, int mode, int shift)
		{
			Gactor gactor = (Gactor)this.IndicatorCache.GetCash(typeof(Gactor), new object[]
			{
				symbol,
				timeframe,
				jaw_period,
				jaw_shift,
				teeth_period,
				teeth_shift,
				lips_period,
				lips_shift,
				(MovingAverageType)ma_method,
				(PriceConstants)applied_price
			});
			bool flag = gactor == null;
			if (flag)
			{
				gactor = new Gactor
				{
					JawPeriod = jaw_period,
					JawShift = jaw_shift,
					TeethPeriod = teeth_period,
					TeethShift = teeth_shift,
					LipsPeriod = lips_period,
					LipsShift = lips_shift,
					MAType = (MovingAverageType)ma_method,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(gactor, symbol, timeframe);
			}
			double result;
			if (mode != 1)
			{
				if (mode != 2)
				{
					result = -1.0;
				}
				else
				{
					bool flag2 = shift < 0 || shift >= gactor.Series[1].Values.Count;
					if (flag2)
					{
						result = -1.0;
					}
					else
					{
						result = gactor.Series[1].Values[shift, true];
					}
				}
			}
			else
			{
				bool flag3 = shift < 0 || shift >= gactor.Series[0].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = gactor.Series[0].Values[shift, true];
				}
			}
			return result;
		}

		public double iHMA(string symbol, int timeframe, int period, int method, int mode, int shift)
		{
			IndicatorBase indicatorBase = this.IndicatorCache.GetCash(typeof(HMA), new object[]
			{
				symbol,
				timeframe,
				period,
				method
			});
			bool flag = indicatorBase == null;
			if (flag)
			{
				indicatorBase = new HMA
				{
					period = period,
					method = method
				};
				this.AddIndicatorToIndicatorBuffer(indicatorBase, symbol, timeframe);
			}
			double result;
			if (mode != 0)
			{
				if (mode != 1)
				{
					result = 2147483647.0;
				}
				else
				{
					bool flag2 = shift < 0 || shift >= indicatorBase.Series[1].Values.Count;
					if (flag2)
					{
						result = -1.0;
					}
					else
					{
						result = indicatorBase.Series[1].Values[shift, true];
					}
				}
			}
			else
			{
				bool flag3 = shift < 0 || shift >= indicatorBase.Series[0].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = indicatorBase.Series[0].Values[shift, true];
				}
			}
			return result;
		}

		public double iIchimoku(string symbol, int timeframe, int tenkan_sen, int kijun_sen, int senkou_span_b, int mode, int shift)
		{
			Ichimoku ichimoku = (Ichimoku)this.IndicatorCache.GetCash(typeof(Ichimoku), new object[]
			{
				symbol,
				timeframe
			});
			bool flag = ichimoku == null;
			if (flag)
			{
				ichimoku = new Ichimoku
				{
					TenkanSen = tenkan_sen,
					KijunSen = kijun_sen,
					SenkouSpanB = senkou_span_b
				};
				this.AddIndicatorToIndicatorBuffer(ichimoku, symbol, timeframe);
			}
			double result;
			switch (mode)
			{
			case 1:
			{
				bool flag2 = shift < 0 || shift >= ichimoku.Series[0].Values.Count;
				if (flag2)
				{
					result = -1.0;
				}
				else
				{
					result = ichimoku.Series[0].Values[shift, true];
				}
				break;
			}
			case 2:
			{
				bool flag3 = shift < 0 || shift >= ichimoku.Series[1].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = ichimoku.Series[1].Values[shift, true];
				}
				break;
			}
			case 3:
			{
				bool flag4 = shift + kijun_sen < 0 || shift + kijun_sen >= ichimoku.Series[2].Values.Count;
				if (flag4)
				{
					result = -1.0;
				}
				else
				{
					result = ichimoku.Series[2].Values[shift + kijun_sen, true];
				}
				break;
			}
			case 4:
			{
				bool flag5 = shift + kijun_sen < 0 || shift + kijun_sen >= ichimoku.Series[3].Values.Count;
				if (flag5)
				{
					result = -1.0;
				}
				else
				{
					result = ichimoku.Series[3].Values[shift + kijun_sen, true];
				}
				break;
			}
			case 5:
			{
				bool flag6 = shift - kijun_sen < 0 || shift - kijun_sen >= ichimoku.Series[4].Values.Count;
				if (flag6)
				{
					result = -1.0;
				}
				else
				{
					result = ichimoku.Series[4].Values[shift - kijun_sen, true];
				}
				break;
			}
			default:
				result = 2147483647.0;
				break;
			}
			return result;
		}

		public double iBWMFI(string symbol, int timeframe, int shift)
		{
			BWMFI bWMFI = (BWMFI)this.IndicatorCache.GetCash(typeof(BWMFI), new object[]
			{
				symbol,
				timeframe
			});
			bool flag = bWMFI == null;
			if (flag)
			{
				bWMFI = new BWMFI();
				this.AddIndicatorToIndicatorBuffer(bWMFI, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= bWMFI.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = bWMFI.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iMomentum(string symbol, int timeframe, int period, int applied_price, int shift)
		{
			Momentum momentum = (Momentum)this.IndicatorCache.GetCash(typeof(Momentum), new object[]
			{
				symbol,
				timeframe,
				period,
				(PriceConstants)applied_price
			});
			bool flag = momentum == null;
			if (flag)
			{
				momentum = new Momentum
				{
					IndicatorPeriod = period,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(momentum, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= momentum.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = momentum.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iMFI(string symbol, int timeframe, int period, int shift)
		{
			MFI mFI = (MFI)this.IndicatorCache.GetCash(typeof(MFI), new object[]
			{
				symbol,
				timeframe,
				period
			});
			bool flag = mFI == null;
			if (flag)
			{
				mFI = new MFI
				{
					IndicatorPeriod = period
				};
				this.AddIndicatorToIndicatorBuffer(mFI, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= mFI.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = mFI.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iOsMA(string symbol, int timeframe, int fast_ema_period, int slow_ema_period, int signal_period, int applied_price, int shift)
		{
			OsMA osMA = (OsMA)this.IndicatorCache.GetCash(typeof(OsMA), new object[]
			{
				symbol,
				timeframe,
				fast_ema_period,
				slow_ema_period,
				signal_period,
				(PriceConstants)applied_price
			});
			bool flag = osMA == null;
			if (flag)
			{
				osMA = new OsMA
				{
					FastPeriod = fast_ema_period,
					SlowPeriod = slow_ema_period,
					SignalPeriod = signal_period,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(osMA, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= osMA.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = osMA.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iOBV(string symbol, int timeframe, int applied_price, int shift)
		{
			OBV oBV = (OBV)this.IndicatorCache.GetCash(typeof(OBV), new object[]
			{
				symbol,
				timeframe,
				(PriceConstants)applied_price
			});
			bool flag = oBV == null;
			if (flag)
			{
				oBV = new OBV
				{
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(oBV, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= oBV.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = oBV.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iPivots(string symbol, int timeframe, double pip_diff, int pos_diff, int mode, int shift)
		{
			ApiaryPivots apiaryPivots = (ApiaryPivots)this.IndicatorCache.GetCash(typeof(ApiaryPivots), new object[]
			{
				symbol,
				timeframe,
				pip_diff,
				pos_diff
			});
			bool flag = apiaryPivots == null;
			if (flag)
			{
				apiaryPivots = new ApiaryPivots
				{
					pipDiff = pip_diff,
					posDiff = pos_diff
				};
				this.AddIndicatorToIndicatorBuffer(apiaryPivots, symbol, timeframe);
			}
			double result;
			if (mode != 1)
			{
				if (mode != 2)
				{
					result = 2147483647.0;
				}
				else
				{
					bool flag2 = shift < 0 || shift >= apiaryPivots.Series[0].Values.Count;
					if (flag2)
					{
						result = -1.0;
					}
					else
					{
						result = apiaryPivots.Series[1].Values[shift, true];
					}
				}
			}
			else
			{
				bool flag3 = shift < 0 || shift >= apiaryPivots.Series[0].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = apiaryPivots.Series[0].Values[shift, true];
				}
			}
			return result;
		}

		public double iSAR(string symbol, int timeframe, double step, double maximum, int shift)
		{
			SAR sAR = (SAR)this.IndicatorCache.GetCash(typeof(SAR), new object[]
			{
				symbol,
				timeframe,
				step,
				maximum
			});
			bool flag = sAR == null;
			if (flag)
			{
				sAR = new SAR
				{
					Step = step,
					Maximum = maximum
				};
				this.AddIndicatorToIndicatorBuffer(sAR, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= sAR.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = sAR.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iRSI(string symbol, int timeframe, int period, int applied_price, int shift)
		{
			RSI rSI = (RSI)this.IndicatorCache.GetCash(typeof(RSI), new object[]
			{
				symbol,
				timeframe,
				period,
				(PriceConstants)applied_price
			});
			bool flag = rSI == null;
			if (flag)
			{
				rSI = new RSI
				{
					IndicatorPeriod = period,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(rSI, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= rSI.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = rSI.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iRVI(string symbol, int timeframe, int period, int mode, int shift)
		{
			RVI rVI = (RVI)this.IndicatorCache.GetCash(typeof(RVI), new object[]
			{
				symbol,
				timeframe,
				period
			});
			bool flag = rVI == null;
			if (flag)
			{
				rVI = new RVI
				{
					IndicatorPeriod = period
				};
				this.AddIndicatorToIndicatorBuffer(rVI, symbol, timeframe);
			}
			double result;
			if (mode != 0)
			{
				if (mode != 1)
				{
					result = -1.0;
				}
				else
				{
					bool flag2 = shift < 0 || shift >= rVI.Series[1].Values.Count;
					if (flag2)
					{
						result = -1.0;
					}
					else
					{
						result = rVI.Series[1].Values[shift, true];
					}
				}
			}
			else
			{
				bool flag3 = shift < 0 || shift >= rVI.Series[0].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = rVI.Series[0].Values[shift, true];
				}
			}
			return result;
		}

		public double iStdDev(string symbol, int timeframe, int ma_period, int ma_shift, int ma_method, int applied_price, int shift)
		{
			StdDev stdDev = (StdDev)this.IndicatorCache.GetCash(typeof(StdDev), new object[]
			{
				symbol,
				timeframe,
				ma_period,
				(MovingAverageType)ma_method,
				(PriceConstants)applied_price
			});
			bool flag = stdDev == null;
			if (flag)
			{
				stdDev = new StdDev
				{
					IndicatorPeriod = ma_period,
					MAType = (MovingAverageType)ma_method,
					PriceType = (PriceConstants)applied_price
				};
				this.AddIndicatorToIndicatorBuffer(stdDev, symbol, timeframe);
			}
			bool flag2 = shift + ma_shift < 0 || shift + ma_shift >= stdDev.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = stdDev.Series[0].Values[shift + ma_shift, true];
			}
			return result;
		}

		public double iStochastic(string symbol, int timeframe, int KPeriod, int DPeriod, int slowing, int method, int price_field, int mode, int shift)
		{
			Stochastic stochastic = (Stochastic)this.IndicatorCache.GetCash(typeof(Stochastic), new object[]
			{
				symbol,
				timeframe,
				KPeriod,
				DPeriod,
				slowing,
				(MovingAverageType)method,
				(PriceConstants)price_field
			});
			bool flag = stochastic == null;
			if (flag)
			{
				stochastic = new Stochastic
				{
					KPeriod = KPeriod,
					DPeriod = DPeriod,
					Slowing = slowing,
					MAType = (MovingAverageType)method,
					PriceType = (PriceConstants)price_field
				};
				this.AddIndicatorToIndicatorBuffer(stochastic, symbol, timeframe);
			}
			double result;
			if (mode != 0)
			{
				if (mode != 1)
				{
					result = -1.0;
				}
				else
				{
					bool flag2 = shift < 0 || shift >= stochastic.Series[1].Values.Count;
					if (flag2)
					{
						result = -1.0;
					}
					else
					{
						result = stochastic.Series[1].Values[shift, true];
					}
				}
			}
			else
			{
				bool flag3 = shift < 0 || shift >= stochastic.Series[0].Values.Count;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					result = stochastic.Series[0].Values[shift, true];
				}
			}
			return result;
		}

		public double iWPR(string symbol, int timeframe, int period, int shift)
		{
			WPR wPR = (WPR)this.IndicatorCache.GetCash(typeof(WPR), new object[]
			{
				symbol,
				timeframe,
				period
			});
			bool flag = wPR == null;
			if (flag)
			{
				wPR = new WPR
				{
					IndicatorPeriod = period
				};
				this.AddIndicatorToIndicatorBuffer(wPR, symbol, timeframe);
			}
			bool flag2 = shift < 0 || shift >= wPR.Series[0].Values.Count;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				result = wPR.Series[0].Values[shift, true];
			}
			return result;
		}

		public double iBandsOnArray(Array<double> array, int total, int period, int deviation, int bands_shift, int mode, int shift)
		{
			int num = shift + bands_shift;
			bool flag = total == 0;
			if (flag)
			{
				total = array.Count;
			}
			bool flag2 = num < 0 || num >= total;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				bool flag3 = num + period > total;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					bool flag4 = mode != 0 && mode != 1 && mode != 2;
					if (flag4)
					{
						result = -1.0;
					}
					else
					{
						double num2 = this.iMAOnArray(array, total, period, 0, 0, num);
						double num3 = 0.0;
						double num4 = -1.0;
						for (int i = 0; i < period; i++)
						{
							double num5 = array[num + i, true] - num2;
							num3 += num5 * num5;
						}
						double num6 = (double)deviation * this.MathSqrt(num3 / (double)period);
						switch (mode)
						{
						case 0:
							num4 = num2;
							break;
						case 1:
							num4 = num2 + num6;
							break;
						case 2:
							num4 = num2 - num6;
							break;
						}
						result = num4;
					}
				}
			}
			return result;
		}

		public double iMAOnArray(Array<double> array, int total, int period, int ma_shift, int ma_method, int shift)
		{
			int num = shift + ma_shift;
			bool flag = total == 0;
			if (flag)
			{
				total = array.Count;
			}
			bool flag2 = num < 0 || num >= total;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				bool flag3 = ma_method != 1 && num + period > total;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					double num2 = -1.0;
					double num3 = 0.0;
					switch (ma_method)
					{
					case 0:
						for (int i = 0; i < period; i++)
						{
							num3 += array[num + i, true];
						}
						num2 = num3 / (double)period;
						break;
					case 1:
					{
						int i = total - 1;
						num2 = array[i, true];
						double num4 = 2.0 / (double)(period + 1);
						while (i >= num)
						{
							num2 = array[i, true] * num4 + num2 * (1.0 - num4);
							i--;
						}
						break;
					}
					case 2:
					{
						int i = total - 1;
						int j = 0;
						while (j < period)
						{
							num3 += array[i, true];
							j++;
							i--;
						}
						num2 = num3 / (double)period;
						while (i >= num)
						{
							num2 = (num2 * (double)(period - 1) + array[i, true]) / (double)period;
							i--;
						}
						break;
					}
					case 3:
					{
						double num5 = 0.0;
						double num6 = 0.0;
						for (int i = 1; i <= period; i++)
						{
							double num7 = array[num + period - i, true];
							num3 += num7 * (double)i;
							num5 += num7;
							num6 += (double)i;
						}
						num2 = num3 / num6;
						break;
					}
					}
					result = num2;
				}
			}
			return result;
		}

		public double iCCIOnArray(Array<double> array, int total, int period, int shift)
		{
			bool flag = total == 0;
			if (flag)
			{
				total = array.Count;
			}
			bool flag2 = shift < 0 || shift >= total;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				bool flag3 = shift + period > total;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					int i = shift + period - 1;
					double num = this.iMAOnArray(array, total, period, 0, 0, shift);
					double num2 = 0.0;
					while (i >= shift)
					{
						num2 += this.MathAbs(array[i, true] - num);
						i--;
					}
					double num3 = 0.015 / (double)period;
					double num4 = num2 * num3;
					double num5 = array[shift, true] - num;
					bool flag4 = num4.Equals(0.0);
					double num6;
					if (flag4)
					{
						num6 = 0.0;
					}
					else
					{
						num6 = num5 / num4;
					}
					result = num6;
				}
			}
			return result;
		}

		public double iEnvelopesOnArray(Array<double> array, int total, int ma_period, int ma_method, int ma_shift, double deviation, int mode, int shift)
		{
			int num = shift + ma_shift;
			bool flag = total == 0;
			if (flag)
			{
				total = array.Count;
			}
			bool flag2 = num < 0 || num >= total;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				bool flag3 = ma_method != 1 && num + ma_period > total;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					bool flag4 = mode != 0 && mode != 1 && mode != 2;
					if (flag4)
					{
						result = -1.0;
					}
					else
					{
						double num2 = this.iMAOnArray(array, total, ma_period, 0, 0, num);
						double num3 = -1.0;
						switch (mode)
						{
						case 0:
							num3 = num2;
							break;
						case 1:
						{
							double num4 = 1.0 + deviation / 100.0;
							num3 = num4 * num2;
							break;
						}
						case 2:
						{
							double num5 = 1.0 - deviation / 100.0;
							num3 = num5 * num2;
							break;
						}
						}
						result = num3;
					}
				}
			}
			return result;
		}

		public double iMomentumOnArray(Array<double> array, int total, int period, int shift)
		{
			bool flag = total == 0;
			if (flag)
			{
				total = array.Count;
			}
			bool flag2 = shift < 0 || shift >= total;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				bool flag3 = shift + period >= total;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					double num = array[shift, true] * 100.0 / array[shift + period, true];
					result = num;
				}
			}
			return result;
		}

		public double iRSIOnArray(Array<double> array, int total, int period, int shift)
		{
			bool flag = total == 0;
			if (flag)
			{
				total = array.Count;
			}
			bool flag2 = shift < 0 || shift >= total;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				bool flag3 = shift + period > total;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					double num = 0.0;
					double num2 = 0.0;
					int i;
					for (i = total - 2; i >= total - period - 1; i--)
					{
						double num3 = array[i, true] - array[i + 1, true];
						bool flag4 = num3 > 0.0;
						if (flag4)
						{
							num2 += num3;
						}
						else
						{
							num -= num3;
						}
					}
					double num4 = num2 / (double)period;
					double num5 = num / (double)period;
					bool flag5 = num5.Equals(0.0);
					double num6;
					if (flag5)
					{
						num6 = 0.0;
					}
					else
					{
						num6 = 100.0 - 100.0 / (1.0 + num4 / num5);
					}
					while (i >= shift)
					{
						num = 0.0;
						num2 = 0.0;
						double num3 = array[i, true] - array[i + 1, true];
						bool flag6 = num3 > 0.0;
						if (flag6)
						{
							num2 = num3;
						}
						else
						{
							num = -num3;
						}
						num4 = (num4 * (double)(period - 1) + num2) / (double)period;
						num5 = (num5 * (double)(period - 1) + num) / (double)period;
						bool flag7 = num5.Equals(0.0);
						if (flag7)
						{
							num6 = 0.0;
						}
						else
						{
							num6 = 100.0 - 100.0 / (1.0 + num4 / num5);
						}
						i--;
					}
					result = num6;
				}
			}
			return result;
		}

		public double iStdDevOnArray(Array<double> array, int total, int ma_period, int ma_shift, int ma_method, int shift)
		{
			int num = shift + ma_shift;
			bool flag = total == 0;
			if (flag)
			{
				total = array.Count;
			}
			bool flag2 = num < 0 || num >= total;
			double result;
			if (flag2)
			{
				result = -1.0;
			}
			else
			{
				bool flag3 = ma_method != 1 && num + ma_period > total;
				if (flag3)
				{
					result = -1.0;
				}
				else
				{
					double num2 = 0.0;
					double num3 = this.iMAOnArray(array, total, ma_period, 0, ma_method, num);
					for (int i = 0; i < ma_period; i++)
					{
						double num4 = array[num + i, true];
						num2 += (num4 - num3) * (num4 - num3);
					}
					double num5 = this.MathSqrt(num2 / (double)ma_period);
					result = num5;
				}
			}
			return result;
		}

		private T GetSeriesData<T>(Array<Bar> chartData, SeriesArrays type, int shift)
		{
			object value = default(T);
			Bar bar = chartData[shift, true];
			bool flag = bar == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				switch (type)
				{
				case SeriesArrays.MODE_OPEN:
					value = bar.Open;
					break;
				case SeriesArrays.MODE_LOW:
					value = bar.Low;
					break;
				case SeriesArrays.MODE_HIGH:
					value = bar.High;
					break;
				case SeriesArrays.MODE_CLOSE:
					value = bar.Close;
					break;
				case SeriesArrays.MODE_VOLUME:
					value = bar.Volume;
					break;
				case SeriesArrays.MODE_TIME:
					value = bar.BarTime;
					break;
				}
				result = (T)((object)Convert.ChangeType(value, typeof(T)));
			}
			return result;
		}

		protected int iBars(string symbol, int timeFrame)
		{
			return this.GetHistory(symbol, timeFrame).Count;
		}

		protected int iBarShift(string symbol, int timeframe, datetime time, bool exact = false)
		{
			Array<Bar> history = this.GetHistory(symbol, timeframe);
			bool flag = history.Count == 0;
			int result;
			if (flag)
			{
				this.LastError = 4066;
				result = -1;
			}
			else
			{
				TimeSpan t = TimeSpan.MaxValue;
				int num = -1;
				for (int i = 0; i < history.Count; i++)
				{
					bool flag2 = history[i, true].BarTime == time;
					if (flag2)
					{
						result = i;
						return result;
					}
					bool flag3 = history[i, true].BarTime - time < t;
					if (flag3)
					{
						t = history[i, true].BarTime - time;
						num = i;
					}
				}
				bool flag4 = timeframe == 0;
				if (flag4)
				{
					timeframe = (int)this.Chart.TimeFrame;
				}
				bool flag5 = t.TotalMinutes < (double)timeframe || !exact;
				if (flag5)
				{
					result = num;
				}
				else
				{
					result = -1;
				}
			}
			return result;
		}

		protected double iClose(string symbol, int timeframe, int shift)
		{
			Bar bar = null;
			Array<Bar> history = this.GetHistory(symbol, timeframe);
			bool flag = history != null && history.Count > shift;
			if (flag)
			{
				bar = history[shift, true];
			}
			return (bar != null) ? ((double)bar.Close) : 0.0;
		}

		protected double iHigh(string symbol, int timeframe, int shift)
		{
			Bar bar = null;
			Array<Bar> history = this.GetHistory(symbol, timeframe);
			bool flag = history != null && history.Count > shift;
			if (flag)
			{
				bar = history[shift, true];
			}
			return (bar != null) ? ((double)bar.High) : 0.0;
		}

		protected int iHighest(string symbol, int timeframe, int type, int count = 0, int start = 0)
		{
			int num = -1;
			bool flag = this.Bars <= start;
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				bool flag2 = count == 0;
				if (flag2)
				{
					count = this.Bars;
				}
				Array<Bar> history = this.GetHistory(symbol, timeframe);
				double seriesData = this.GetSeriesData<double>(history, (SeriesArrays)type, start);
				num = start;
				int num2 = 1;
				for (int i = start + 1; i < this.Bars; i++)
				{
					double seriesData2 = this.GetSeriesData<double>(history, (SeriesArrays)type, i);
					bool flag3 = seriesData2 > seriesData;
					if (flag3)
					{
						num = i;
					}
					num2++;
					bool flag4 = num2 >= count;
					if (flag4)
					{
						break;
					}
				}
				result = num;
			}
			return result;
		}

		protected double iLow(string symbol, int timeframe, int shift)
		{
			Bar bar = null;
			Array<Bar> history = this.GetHistory(symbol, timeframe);
			bool flag = history != null && history.Count > shift;
			if (flag)
			{
				bar = history[shift, true];
			}
			return (bar != null) ? ((double)bar.Low) : 0.0;
		}

		protected int iLowest(string symbol, int timeframe, int type, int count = 0, int start = 0)
		{
			int num = -1;
			bool flag = this.Bars <= start;
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				bool flag2 = count == 0;
				if (flag2)
				{
					count = this.Bars;
				}
				Array<Bar> history = this.GetHistory(symbol, timeframe);
				double seriesData = this.GetSeriesData<double>(history, (SeriesArrays)type, start);
				num = start;
				int num2 = 1;
				for (int i = start + 1; i < this.Bars; i++)
				{
					double seriesData2 = this.GetSeriesData<double>(history, (SeriesArrays)type, i);
					bool flag3 = seriesData2 < seriesData;
					if (flag3)
					{
						num = i;
					}
					num2++;
					bool flag4 = num2 >= count;
					if (flag4)
					{
						break;
					}
				}
				result = num;
			}
			return result;
		}

		protected double iOpen(string symbol, int timeframe, int shift)
		{
			Bar bar = null;
			Array<Bar> history = this.GetHistory(symbol, timeframe);
			bool flag = history != null && history.Count > shift;
			if (flag)
			{
				bar = history[shift, true];
			}
			return (bar != null) ? ((double)bar.Open) : 0.0;
		}

		protected datetime iTime(string symbol, int timeframe, int shift)
		{
			Bar bar = null;
			Array<Bar> history = this.GetHistory(symbol, timeframe);
			bool flag = history != null && history.Count > shift;
			if (flag)
			{
				bar = history[shift, true];
			}
			return (bar != null) ? bar.BarTime : default(DateTime);
		}

		protected double iVolume(string symbol, int timeframe, int shift)
		{
			Bar bar = null;
			Array<Bar> history = this.GetHistory(symbol, timeframe);
			bool flag = history != null && history.Count > shift;
			if (flag)
			{
				bar = history[shift, true];
			}
			return (double)((bar != null) ? bar.Volume : 0L);
		}
	}
}
