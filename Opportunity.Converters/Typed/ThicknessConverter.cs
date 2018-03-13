using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Thickness = Windows.UI.Xaml.Thickness;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Convert <see cref="Thickness"/> value of its four edges.
    /// Use ConverterParameter to assign how to convert.
    /// <example>
    /// <para>
    /// If the ConverterParameter is "0,10",
    /// (10,20,30,40) will convert to (10,30,30,50).
    /// </para>
    /// <para>
    /// If the ConverterParameter is "-10,x0.5,10,x2",
    /// (10,20,30,40) will convert to (0,10,40,80).
    /// </para>
    /// </example>
    /// </summary>
    public sealed class ThicknessConverter : ValueConverter<Thickness, Thickness>
    {
        private static readonly object empty = new Thickness();

        /// <inheritdoc />
        public override Thickness Convert(Thickness value, object parameter, string language)
            => XBind.Thickness.Convert(value, parameter?.ToString());

        /// <inheritdoc />
        public override Thickness ConvertBack(Thickness value, object parameter, string language)
            => XBind.Thickness.ConvertBack(value, parameter?.ToString());
    }
}
