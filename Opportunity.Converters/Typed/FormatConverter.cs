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
    /// Values should implement <see cref="IFormattable"/> or <see cref="FormatProvider"/> should provide an <see cref="ICustomFormatter"/>, otherwise, <see cref="object.ToString()"/> of values will be called.
    /// <para>
    /// Examples:
    /// </para>
    /// <example>
    /// <para><c>
    /// {x:bind value, Converter={StaticResource FormatStringConverter}, ConverterParameter='0 \'days.\''}
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
            DependencyProperty.Register("FormatProvider", typeof(IFormatProvider), typeof(FormatConverter), new PropertyMetadata(CultureInfo.CurrentUICulture, FormatProviderPropertyChanged));

        private static void FormatProviderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var nv = e.NewValue as IFormatProvider;
            var sender = (FormatConverter)d;
            sender.customFormatter = (ICustomFormatter)nv?.GetFormat(typeof(ICustomFormatter));
        }

        private ICustomFormatter customFormatter = (ICustomFormatter)CultureInfo.CurrentUICulture.GetFormat(typeof(ICustomFormatter));

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
            var formatProvider = this.FormatProvider;
            var cf = this.customFormatter;
            if (cf != null)
            {
                result = cf.Format(format, value, formatProvider);
            }
            if (result == null)
            {
                if (value is IFormattable fValue)
                    result = fValue.ToString(format, formatProvider);
                else
                    result = value.ToString();
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
