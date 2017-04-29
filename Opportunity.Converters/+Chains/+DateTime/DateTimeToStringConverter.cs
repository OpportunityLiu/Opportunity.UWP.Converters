using System;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert <see cref="DateTime"/> to <see cref="string"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public sealed class DateTimeToStringConverter : DateTimeToStringConverterBase
    {
        /// <inheritdoc />
        protected override object ConvertImpl(object value, object parameter, string language)
        {
            if (value == null)
                return "";
            return this.Formatter.Format((DateTime)value);
        }

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            return DateTime.Parse(value.ToString());
        }
    }
}
