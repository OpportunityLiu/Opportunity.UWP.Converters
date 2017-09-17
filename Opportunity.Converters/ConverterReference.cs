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
    /// Represent a reference to an <see cref="IValueConverter"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(Converter))]
    public sealed class ConverterReference : ValueConverter
    {
        /// <summary>
        /// The <see cref="IValueConverter"/> reference.
        /// </summary>
        public IValueConverter Converter
        {
            get => (IValueConverter)GetValue(ConverterProperty);
            set => SetValue(ConverterProperty, value);
        }

        /// <summary>
        /// Indentify <see cref="Converter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ConverterProperty =
            DependencyProperty.Register(nameof(Converter), typeof(IValueConverter), typeof(ConverterReference), new PropertyMetadata(SystemConverter.Default, ConverterPropertyChanged));

        private static void ConverterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                throw new ArgumentNullException(nameof(e.NewValue));
        }

        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, string language)
            => Converter.Convert(value, targetType, parameter, language);

        /// <inheritdoc/>
        public override object ConvertBack(object value, Type targetType, object parameter, string language)
            => Converter.ConvertBack(value, targetType, parameter, language);
    }
}
