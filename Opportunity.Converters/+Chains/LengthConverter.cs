using System;
using Windows.UI.Xaml;
using GridLength = Windows.UI.Xaml.GridLength;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert between <see cref="GridLength"/> and <see cref="double"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public sealed class LengthConverter : ChainConverter
    {
        private static object convert(object value)
        {
            switch(value)
            {
            case double vd:
                return new GridLength(vd);
            case GridLength vgl:
                return vgl.IsAbsolute ? vgl.Value : double.NaN;
            default:
                return DependencyProperty.UnsetValue;
            }
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value,  object parameter, string language)
        {
            return convert(value);
        }

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value,  object parameter, string language)
        {
            return convert(value);
        }
    }
}
