using Opportunity.Helpers.Universal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Convert values with the format string provided by converter parameter.
    /// You can use both raw strings and ms-resource strings as converter parameter.
    /// <para>
    /// Examples:
    /// </para>
    /// <example>
    /// <para><c>
    /// {x:bind value, Converter={StaticResource FormatStringConverter}, ConverterParameter='{0} days.'}
    /// </c></para>
    /// <para><c>
    /// {x:bind value, Converter={StaticResource FormatStringConverter}, ConverterParameter='ms-resource:DayFormatString'}
    /// </c></para>
    /// </example>
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(FormatProvider))]
    public sealed class FormatStringConverter : ValueConverter<object, string>
    {
        /// <summary>
        /// <see cref="IFormatProvider"/> used to format string.
        /// Default value is <see cref="CultureInfo.CurrentUICulture"/>.
        /// </summary>
        public IFormatProvider FormatProvider
        {
            get => (IFormatProvider)GetValue(FormatProviderProperty);
            set => SetValue(FormatProviderProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="FormatProvider"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FormatProviderProperty =
            DependencyProperty.Register("FormatProvider", typeof(IFormatProvider), typeof(FormatStringConverter), new PropertyMetadata(CultureInfo.CurrentUICulture));

        /// <inheritdoc />
        public override string Convert(object value, object parameter, string language)
        {
            if (parameter == null)
            {
                if (value == null)
                    return "";
                else if (value is IFormattable fValue)
                    return fValue.ToString(null, FormatProvider ?? CultureInfo.CurrentUICulture);
                return value.ToString();
            }

            var format = parameter.ToString();
            if (format.StartsWith("ms-resource:"))
                format = LocalizedStrings.GetValue(format);
            return string.Format(FormatProvider ?? CultureInfo.CurrentUICulture, format, value);
        }

        /// <inheritdoc />
        public override object ConvertBack(string value, object parameter, string language)
        {
            return value;
        }
    }
}
