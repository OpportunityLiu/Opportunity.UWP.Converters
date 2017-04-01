using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Opportunity.Converters
{
    /// <summary>
    /// An abstract class of value converter with an <see cref="InnerConverter"/> property.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public abstract class ValueConverterBase : ValueConverter
    {
        /// <summary>
        /// Inner converter used for <see cref="ChainConverter"/> and <see cref="InvertConverter"/>.
        /// </summary>
        public IValueConverter InnerConverter
        {
            get => (IValueConverter)GetValue(InnerConverterProperty); set => SetValue(InnerConverterProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="InnerConverter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InnerConverterProperty =
            DependencyProperty.Register(nameof(InnerConverter), typeof(IValueConverter), typeof(ValueConverterBase), new PropertyMetadata(null, InnerConverterPropertyChangedCallback));

        private static void InnerConverterPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ValueConverterBase)d).OnInnerConverterChanged(e);
        }

        /// <summary>
        /// Fires when <see cref="InnerConverter"/> changed.
        /// </summary>
        /// <param name="e">Data for the event.</param>
        protected virtual void OnInnerConverterChanged(DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
