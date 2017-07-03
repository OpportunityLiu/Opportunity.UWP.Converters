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
    /// Repersents a chain of <see cref="ValueConverter"/>s,
    /// data will go through the chain and be converted mutiple times.
    /// </summary>
    /// <typeparam name="TFrom">From type.</typeparam>
    /// <typeparam name="TTo">Convert to type.</typeparam>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public abstract class ChainConverter<TFrom, TTo> : ValueConverter
    {
        /// <summary>
        /// Next <see cref="IValueConverter"/> of the chain.
        /// </summary>
        public IValueConverter NextConverter
        {
            get => (IValueConverter)GetValue(NextConverterProperty);
            set => SetValue(NextConverterProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="NextConverter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NextConverterProperty =
            DependencyProperty.Register(nameof(NextConverter), typeof(IValueConverter), typeof(ChainConverter<TFrom, TTo>), new PropertyMetadata(null));

        /// <summary>
        /// The implementation of <see cref="Convert(object, Type, object, string)"/> of current instance of <see cref="ChainConverter{TFrom, TTo}"/>.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        /// <remarks>As a part of the converter chain, a converter has its independent responsibility, and should know nothing about the target type.</remarks>
        protected abstract TTo ConvertImpl(TFrom value, object parameter, string language);

        /// <summary>
        /// The implementation of <see cref="ConvertBack(object, Type, object, string)"/> of current instance of <see cref="ChainConverter{TFrom, TTo}"/>.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The value to be passed to the source object.</returns>
        /// <remarks>As a part of the converter chain, a converter has its independent responsibility, and should know nothing about the target type.</remarks>
        protected abstract TFrom ConvertBackImpl(TTo value, object parameter, string language);

        /// <inheritdoc />
        public sealed override object Convert(object value, Type targetType, object parameter, string language)
        {
            var from = Internal.ConvertHelper.ChangeType<TFrom>(value);
            var convertedByThis = ConvertImpl(from, parameter, language);
            if (this.NextConverter == null)
                return convertedByThis;
            else
                return this.NextConverter.Convert(convertedByThis, targetType, parameter, language);
        }

        /// <inheritdoc />
        public sealed override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            object convertedByInner;
            if (this.NextConverter != null)
                convertedByInner = this.NextConverter.ConvertBack(value, targetType, parameter, language);
            else
                convertedByInner = value;
            var to = Internal.ConvertHelper.ChangeType<TTo>(convertedByInner);
            return ConvertBackImpl(to, parameter, language);
        }
    }
}
