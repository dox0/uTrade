using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace uTrade.Controls
{

    /// <summary>
    /// 接触操作类型。
    /// </summary>
    public enum PointerAction
    {
        /// <summary>
        /// 无。
        /// </summary>
        None,
        /// <summary>
        /// 放大。
        /// </summary>
        ZoomIn,
        /// <summary>
        /// 测量。
        /// </summary>
        Measure,
        /// <summary>
        /// 选择。
        /// </summary>
        Select
    }

    sealed class ConstStrings
    {
        public const string DateTimeFormat = "yyyyMMdd";
    }

    class FontConst
    {
        public static readonly FontFamily DefaultFontFamily = new FontFamily("Segoe UI");
        public const double DefaultFontSize = 10;
    }
}
