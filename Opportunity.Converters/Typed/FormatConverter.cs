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
    public sealed class FormatConverter : ValueConverter<object, string>
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
            DependencyProperty.Register("FormatProvider", typeof(IFormatProvider), typeof(FormatConverter), new PropertyMetadata(CultureInfo.CurrentUICulture));

        /// <inheritdoc />
        public override string Convert(object value, object parameter, string language)
        {
            if (value is null)
                return string.Empty;

            var format = parameter?.ToString();
            if (format != null && format.StartsWith("ms-resource:"))
            {
                format = LocalizedStrings.GetValue(format);
                if (format == "")
                    format = null;
            }

            var result = default(string);

            if (format is null)
            {
                if (value is IFormattable fValue)
                    result = fValue.ToString(format, this.FormatProvider);
                else
                    result = value.ToString();
            }
            else
            {
                result = string.Format(this.FormatProvider, format, value);
            }
            return result ?? string.Empty;
        }

        /// <inheritdoc />
        public override object ConvertBack(string value, object parameter, string language)
        {
            return value;
        }
    }
}
