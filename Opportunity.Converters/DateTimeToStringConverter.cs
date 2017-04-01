using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public class DateTimeToStringConverter : ChainConverter
    {
        /// <summary>
        /// The template to format a <see cref="DateTimeOffset"/> or <see cref="DateTime"/>,
        /// see remark or <see cref="DateTimeFormatter"/>.
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
            DependencyProperty.Register(nameof(FormatTemplate), typeof(string), typeof(DateTimeToStringConverter), PropertyMetadata.Create("shortdate shorttime", FormatTemplatePropertyChangedCallback));

        private static void FormatTemplatePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (DateTimeToStringConverter)d;
            if(e.OldValue.ToString() == e.NewValue.ToString())
                return;
            sender.formatter = new DateTimeFormatter(e.NewValue.ToString());
        }

        private DateTimeFormatter formatter = new DateTimeFormatter("shortdate shorttime");

        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            if(value == null)
                return "";
            DateTimeOffset d;
            if(value is DateTimeOffset dto)
                d = dto.ToLocalTime();
            else if(value is DateTime dt)
                d = new DateTimeOffset(dt).ToLocalTime();
            else
                return "";
            return this.formatter.Format(d);
        }

        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            if(targetType == typeof(DateTimeOffset) || targetType == typeof(DateTimeOffset?))
                return DateTimeOffset.Parse(value.ToString());
            else if(targetType == typeof(DateTime) || targetType == typeof(DateTime?))
                return DateTime.Parse(value.ToString());
            else
                return DateTimeOffset.Parse(value.ToString());
        }
    }
}
