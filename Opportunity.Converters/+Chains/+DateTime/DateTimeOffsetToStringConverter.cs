using System;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert <see cref="DateTimeOffset"/> to <see cref="string"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public sealed class DateTimeOffsetToStringConverter : DateTimeToStringConverterBase<DateTimeOffset>
    {
        /// <inheritdoc />
        protected override string ConvertImpl(DateTimeOffset value, object parameter, string language)
        {
            if (value == default(DateTimeOffset))
                return "";
            return this.Formatter.Format(value);
        }

        /// <inheritdoc />
        protected override DateTimeOffset ConvertBackImpl(string value, object parameter, string language)
        {
            if (string.IsNullOrWhiteSpace(value))
                return default(DateTimeOffset);
            return DateTimeOffset.Parse(value);
        }
    }
}
