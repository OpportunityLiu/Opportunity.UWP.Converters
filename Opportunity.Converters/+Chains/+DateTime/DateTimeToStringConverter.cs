using System;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert <see cref="DateTime"/> to <see cref="string"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public sealed class DateTimeToStringConverter : DateTimeToStringConverterBase<DateTime>
    {
        /// <inheritdoc />
        protected override string ConvertImpl(DateTime value, object parameter, string language)
        {
            if (value == default(DateTime))
                return "";
            return this.Formatter.Format(value);
        }

        /// <inheritdoc />
        protected override DateTime ConvertBackImpl(string value, object parameter, string language)
        {
            if (string.IsNullOrWhiteSpace(value))
                return default(DateTime);
            return DateTime.Parse(value);
        }
    }
}
