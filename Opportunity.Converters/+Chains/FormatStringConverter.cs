using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters
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
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public sealed class FormatStringConverter : ChainConverter<object, string>
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
        protected override string ConvertImpl(object value, object parameter, string language)
        {
            if (parameter == null)
                return value.ToString();
            var format = Internal.ResourceHelper.GetForCurrentView().GetString(parameter.ToString());
            return string.Format(FormatProvider ?? CultureInfo.CurrentUICulture, format, value);
        }

        /// <inheritdoc />
        protected override object ConvertBackImpl(string value, object parameter, string language)
        {
            return value;
        }
    }
}
