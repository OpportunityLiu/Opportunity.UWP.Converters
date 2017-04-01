using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    /// <summary>
    /// Repersents a chain of <see cref="ValueConverter"/>s,
    /// data will go through the chain and be converted mutiple times.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public abstract class ChainConverter : ValueConverterBase
    {
        /// <summary>
        /// The implementation of <see cref="Convert(object, Type, object, string)"/> of current instance of <see cref="ChainConverter"/>.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The type of the target property, as a type reference (<see cref="Type"/> for Microsoft .NET, a TypeName helper struct for Visual C++ component extensions (C++/CX)).</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        protected abstract object ConvertImpl(object value, Type targetType, object parameter, string language);

        /// <summary>
        /// The implementation of <see cref="ConvertBack(object, Type, object, string)"/> of current instance of <see cref="ChainConverter"/>.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The type of the target property, as a type reference (<see cref="Type"/> for Microsoft .NET, a TypeName helper struct for Visual C++ component extensions (C++/CX)).</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The value to be passed to the source object.</returns>
        protected abstract object ConvertBackImpl(object value, Type targetType, object parameter, string language);

        /// <inheritdoc />
        public sealed override object Convert(object value, Type targetType, object parameter, string language)
        {
            var convertedByThis = ConvertImpl(value, targetType, parameter, language);
            if(this.InnerConverter == null)
                return convertedByThis;
            else
                return this.InnerConverter.Convert(convertedByThis, targetType, parameter, language);
        }

        /// <inheritdoc />
        public sealed override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            object convertedByInner;
            if(this.InnerConverter != null)
                convertedByInner = this.InnerConverter.ConvertBack(value, targetType, parameter, language);
            else
                convertedByInner = value;
            return ConvertBackImpl(convertedByInner, targetType, parameter, language);
        }
    }
}
