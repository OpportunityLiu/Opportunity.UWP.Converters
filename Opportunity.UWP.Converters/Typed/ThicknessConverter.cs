using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Thickness = Windows.UI.Xaml.Thickness;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Convert <see cref="Thickness"/> value of its four edges.
    /// Use ConverterParameter to assign how to convert.
    /// </summary>
    public sealed class ThicknessConverter : ValueConverter<Thickness, Thickness>
    {
        private static readonly object empty = new Thickness();

        /// <summary>
        /// Uses <see cref="XBind.Thickness.Convert(Thickness, string)"/>.
        /// </summary>
        /// <inheritdoc />
        public override Thickness Convert(Thickness value, object parameter, string language)
            => XBind.Thickness.Convert(value, parameter?.ToString());

        /// <summary>
        /// Uses <see cref="XBind.Thickness.ConvertBack(Thickness, string)"/>.
        /// </summary>
        /// <inheritdoc />
        public override Thickness ConvertBack(Thickness value, object parameter, string language)
            => XBind.Thickness.ConvertBack(value, parameter?.ToString());
    }
}
