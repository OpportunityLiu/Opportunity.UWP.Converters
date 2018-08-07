using System;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Convert <see cref="DateTimeOffset"/> to <see cref="string"/>.
    /// </summary>
    public sealed class DateTimeOffsetToStringConverter : DateTimeToStringConverterBase<DateTimeOffset>
    {
        /// <inheritdoc />
        public override string Convert(DateTimeOffset value, object parameter, string language)
        {
            if (value == default)
            {
                return "";
            }
            return this.Formatter.Format(value);
        }

        /// <inheritdoc />
        public override DateTimeOffset ConvertBack(string value, object parameter, string language)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }
            return DateTimeOffset.Parse(value);
        }
    }
}
