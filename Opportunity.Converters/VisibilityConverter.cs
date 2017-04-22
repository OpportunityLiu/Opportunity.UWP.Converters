using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert between <see cref="bool"/> and <see cref="Visibility"/>.
    /// </summary>
    public sealed class VisibilityConverter : ChainConverter
    {
        /// <summary>
        /// Convert <c>true</c> to <see cref="Visibility.Visible"/>.
        /// The default value is <c>true</c>.
        /// If set to <c>false</c>, will convert <c>true</c> to <see cref="Visibility.Collapsed"/>.
        /// </summary>
        public bool TrueForVisible
        {
            get => (bool)GetValue(TrueForVisibleProperty);
            set => SetValue(TrueForVisibleProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="TrueForVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TrueForVisibleProperty =
            DependencyProperty.Register("TrueForVisible", typeof(bool), typeof(VisibilityConverter), new PropertyMetadata(true));

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            return convertCore(value);
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            return convertCore(value);
        }

        private object convertCore(object value)
        {
            switch (value)
            {
            case bool b:
                if (TrueForVisible)
                    b = !b;
                if (b)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            case Visibility v:
                if (TrueForVisible)
                    return v == Visibility.Visible;
                else
                    return v != Visibility.Visible;
            default:
                throw new Exception("Can't handle value. Only bool and Visibility accepted.");
            }
        }
    }
}
