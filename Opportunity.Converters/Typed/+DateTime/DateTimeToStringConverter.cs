using System;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Convert <see cref="DateTime"/> to <see cref="string"/>.
    /// </summary>
    public sealed class DateTimeToStringConverter : DateTimeToStringConverterBase<DateTime>
    {
        /// <inheritdoc />
        public override string Convert(DateTime value, object parameter, string language)
        {
            if (value == default)
            {
                return "";
            }
            return this.Formatter.Format(value);
        }

        /// <inheritdoc />
        public override DateTime ConvertBack(string value, object parameter, string language)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }
            return DateTime.Parse(value);
        }
    }
}
