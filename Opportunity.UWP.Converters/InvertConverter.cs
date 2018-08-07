using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Opportunity.UWP.Converters
{
    /// <summary>
    /// Inverse the 
    /// <see cref="IValueConverter.Convert(object, Type, object, string)"/> 
    /// and 
    /// <see cref="IValueConverter.ConvertBack(object, Type, object, string)"/>
    /// of <see cref="OriginalConverter"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(OriginalConverter))]
    public sealed class InvertConverter : ValueConverter
    {
        /// <summary>
        /// An <see cref="IValueConverter"/> which will be inverted. 
        /// </summary>
        public IValueConverter OriginalConverter
        {
            get => (IValueConverter)GetValue(OriginalConverterProperty);
            set => SetValue(OriginalConverterProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="OriginalConverter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OriginalConverterProperty =
            DependencyProperty.Register(nameof(OriginalConverter), typeof(IValueConverter), typeof(InvertConverter), new PropertyMetadata(EmptyConverter.Default, OriginalConverterPropertyChangedCallback));

        private static void OriginalConverterPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is null)
                throw new ArgumentNullException(nameof(OriginalConverter));
        }

        /// <summary>
        /// Will call <see cref="IValueConverter.ConvertBack(object, Type, object, string)"/> of <see cref="OriginalConverter"/>.
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            return this.OriginalConverter.ConvertBack(value, targetType, parameter, language);
        }

        /// <summary>
        /// Will call <see cref="IValueConverter.Convert(object, Type, object, string)"/> of <see cref="OriginalConverter"/>.
        /// </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return this.OriginalConverter.Convert(value, targetType, parameter, language);
        }
    }
}
