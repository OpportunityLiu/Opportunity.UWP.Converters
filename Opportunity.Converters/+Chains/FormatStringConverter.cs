using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public sealed class FormatStringConverter : ChainConverter
    {
        /// <inheritdoc />
        protected override object ConvertImpl(object value, object parameter, string language)
        {
            if (parameter == null)
                return value.ToString();
            var format = Internal.ResourcesHelper.GetString(parameter.ToString());
            return string.Format(CultureInfo.CurrentCulture, format, value);
        }

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            return value;
        }
    }
}
