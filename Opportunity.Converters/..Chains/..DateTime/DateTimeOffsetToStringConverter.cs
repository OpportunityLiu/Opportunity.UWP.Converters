using System;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert <see cref="DateTimeOffset"/> to <see cref="string"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public sealed class DateTimeOffsetToStringConverter : DateTimeToStringConverterBase
    {
        /// <inheritdoc />
        protected override object ConvertImpl(object value, object parameter, string language)
        {
            if (value == null)
                return "";
            return this.Formatter.Format((DateTimeOffset)value);
        }

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            return DateTimeOffset.Parse(value.ToString());
        }
    }
}
