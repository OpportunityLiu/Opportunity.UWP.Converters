using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRTVisibility = Windows.UI.Xaml.Visibility;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="WinRTVisibility"/> conversion.
    /// </summary>
    public static class Visibility
    {
        /// <summary>
        /// Convert <see langword="true"/> to <see cref="WinRTVisibility.Visible"/>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Result of convertion.</returns>
        public static WinRTVisibility OfBoolean(bool value)
            => value ? WinRTVisibility.Visible : WinRTVisibility.Collapsed;
        /// <summary>
        /// Convert <see langword="true"/> to <see cref="WinRTVisibility.Collapsed"/>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Result of convertion.</returns>
        public static WinRTVisibility OfBooleanInv(bool value)
            => value ? WinRTVisibility.Collapsed : WinRTVisibility.Visible;

        /// <summary>
        /// Convert <see cref="WinRTVisibility.Visible"/> to <see langword="true"/>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Result of convertion.</returns>
        public static bool ToBoolean(WinRTVisibility value)
            => value == WinRTVisibility.Visible;
        /// <summary>
        /// Convert <see cref="WinRTVisibility.Visible"/> to <see langword="false"/>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Result of convertion.</returns>
        public static bool ToBooleanInv(WinRTVisibility value)
            => value != WinRTVisibility.Visible;
    }
}
