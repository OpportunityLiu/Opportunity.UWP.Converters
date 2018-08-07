using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Coalesce the value and parameter.
    /// Like the <c>??</c> operator.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(Default))]
    public sealed class NullCoalescingConverter : ValueConverter<object, object>
    {
        /// <summary>
        /// Will be used as parameter while parameter is <c>null</c>.
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

        /// <summary>
        /// Coalesce the <paramref name="value"/> and <paramref name="parameter"/>.
        /// Like the <c>??</c> operator.
        /// </summary>
        /// <param name="value">The first operand.</param>
        /// <param name="parameter">The second operand, <see cref="Default"/> will be used if null.</param>
        /// <param name="language">Not used.</param>
        /// <returns>The result of null coalescing.</returns>
        public override object Convert(object value, object parameter, string language)
        {
            if (value is null)
                return parameter ?? this.Default;
            return value;
        }

        /// <summary>
        /// Convert bakc the coalescing of the return value and <paramref name="parameter"/>.
        /// Like the <c>??</c> operator.
        /// </summary>
        /// <param name="value">The result of null coalescing.</param>
        /// <param name="parameter">The second operand, <see cref="Default"/> will be used if null.</param>
        /// <param name="language">Not used.</param>
        /// <returns>The first operand, if <paramref name="value"/> equals <paramref name="parameter"/>, null will be returned; otherwise, <paramref name="value"/> will be returned.</returns>
        public override object ConvertBack(object value, object parameter, string language)
        {
            if (value is null)
                return null;
            if (parameter != null)
            {
                if (value.Equals(parameter))
                    return null;
            }
            else
            {
                if (value.Equals(Default))
                    return null;
            }
            return value;
        }
    }
}
