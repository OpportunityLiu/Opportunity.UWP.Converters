using System;
using Windows.UI.Xaml;
using GridLength = Windows.UI.Xaml.GridLength;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert between <see cref="GridLength"/> and <see cref="double"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public class LengthConverter : ChainConverter
    {
        /// <inheritdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            return convert(value, targetType);
        }

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            return convert(value, targetType);
        }

        private static object convert(object value, Type targetType)
        {
            var result = 0d;
            switch(value)
            {
            case double vd:
                result = vd;
                break;
            case GridLength vgl:
                result = vgl.IsAbsolute ? vgl.Value : double.NaN;
                break;
            default:
                result = System.Convert.ToDouble(value);
                break;
            }
            if(targetType == typeof(double))
                return result;
            if(targetType == typeof(GridLength))
                return new GridLength(result);
            return DependencyProperty.UnsetValue;
        }
    }
}
