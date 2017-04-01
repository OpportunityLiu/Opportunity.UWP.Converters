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
    /// Inverse the 
    /// <see cref="IValueConverter.Convert(object, Type, object, string)"/> 
    /// and 
    /// <see cref="IValueConverter.ConvertBack(object, Type, object, string)"/>
    /// of <see cref="ValueConverterBase.InnerConverter"/>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public sealed class InvertConverter : ValueConverterBase
    {
        /// <summary>
        /// Will call <see cref="IValueConverter.ConvertBack(object, Type, object, string)"/> of <see cref="ValueConverterBase.InnerConverter"/>.
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return this.InnerConverter.ConvertBack(value, targetType, parameter, language);
            }
            catch(Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <summary>
        /// Will call <see cref="IValueConverter.Convert(object, Type, object, string)"/> of <see cref="ValueConverterBase.InnerConverter"/>.
        /// </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return this.InnerConverter.Convert(value, targetType, parameter, language);
        }

        /// <inheritdoc />
        protected override void OnInnerConverterChanged(DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue == null)
                throw new ArgumentNullException(nameof(InnerConverter));
        }
    }
}
