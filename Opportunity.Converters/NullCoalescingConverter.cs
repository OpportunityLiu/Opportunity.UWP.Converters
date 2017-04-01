using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    /// <summary>
    /// Coalesce the value and <see cref="Default"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(Default))]
    public class NullCoalescingConverter : ChainConverter
    {
        /// <summary>
        /// Returned value while the value is <c>null</c>.
        /// </summary>
        public object Default
        {
            get => GetValue(DefaultProperty); set => SetValue(DefaultProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Default"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DefaultProperty =
            DependencyProperty.Register("Default", typeof(object), typeof(NullCoalescingConverter), new PropertyMetadata(null));

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            if(value == this.Default)
                return null;
            return value;
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            if(value == null)
                return this.Default;
            return value;
        }
    }
}
