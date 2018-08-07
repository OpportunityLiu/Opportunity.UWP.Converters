using System;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Base class of <see cref="DateTimeOffsetToStringConverter"/> and <see cref="DateTimeToStringConverter"/>.
    /// </summary>
    public abstract class DateTimeToStringConverterBase<T> : ValueConverter<T, string>
    {
        /// <summary>
        /// The template to format a <see cref="DateTime"/> or <see cref="DateTimeOffset"/>,
        /// see remark of <see cref="DateTimeFormatter"/> at https://docs.microsoft.com/en-us/uwp/api/Windows.Globalization.DateTimeFormatting.DateTimeFormatter#remarks.
        /// </summary>
        public string FormatTemplate
        {
            get => (string)GetValue(FormatTemplateProperty);
            set => SetValue(FormatTemplateProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="FormatTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FormatTemplateProperty =
            DependencyProperty.Register(nameof(FormatTemplate), typeof(string), typeof(DateTimeToStringConverterBase<T>), PropertyMetadata.Create("shortdate shorttime", FormatTemplatePropertyChangedCallback));

        private static void FormatTemplatePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var o = (string)e.OldValue;
            var n = (string)e.NewValue;
            if (o == n)
                return;
            var sender = (DateTimeToStringConverterBase<T>)d;
            if (string.IsNullOrWhiteSpace(n))
                sender.formatter = null;
            else
                sender.formatter = new DateTimeFormatter(n);
        }

        private DateTimeFormatter formatter;

        /// <summary>
        /// A <see cref="DateTimeFormatter"/> with a template of <see cref="FormatTemplate"/>.
        /// </summary>
        protected DateTimeFormatter Formatter => this.formatter ?? defaultFormatter;

        private static readonly DateTimeFormatter defaultFormatter = new DateTimeFormatter("shortdate shorttime");
    }
}
