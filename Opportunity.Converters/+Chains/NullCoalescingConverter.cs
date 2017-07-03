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
    /// Like the <c>??</c> operator.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(Default))]
    public sealed class NullCoalescingConverter : ChainConverter<object, object>
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
        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            if (value == null)
                return null;
            if (value.Equals(this.Default))
                return null;
            return value;
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value, object parameter, string language)
        {
            if (value == null)
                return this.Default;
            return value;
        }
    }
}
