using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static Opportunity.Converters.Internal.ConvertHelper;

namespace Opportunity.Converters
{
    /// <summary>
    /// Apply an <see cref="Offset"/> to values.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public class NumberOffsetConverter : ChainConverter
    {
        /// <summary>
        /// The offset value used.
        /// </summary>
        public double Offset
        {
            get => (double)GetValue(OffsetProperty); set => SetValue(OffsetProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Offset"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(double), typeof(NumberOffsetConverter), new PropertyMetadata(0d, OffsetPropertyChangedCallback));

        private static void OffsetPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if((double)e.OldValue == (double)e.NewValue)
                return;
            if(double.IsNaN((double)e.NewValue))
                throw new ArgumentOutOfRangeException(nameof(Offset));
        }

        /// <summary>
        /// Unapply <see cref="Offset"/>.
        /// </summary>
        /// <param name="value">value canbe converted to <see cref="double"/>.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns><paramref name="value"/> - <see cref="Offset"/></returns>
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            var v = ChangeType<double>(value);
            return ChangeType(v - this.Offset, targetType);
        }

        /// <summary>
        /// Apply <see cref="Offset"/>.
        /// </summary>
        /// <param name="value">value canbe converted to <see cref="double"/>.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns><paramref name="value"/> + <see cref="Offset"/></returns>
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            var v = ChangeType<double>(value);
            return ChangeType(v + this.Offset, targetType);
        }
    }
}
