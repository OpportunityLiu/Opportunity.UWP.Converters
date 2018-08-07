using Opportunity.UWP.Converters.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using static Opportunity.UWP.Converters.Internal.ConvertHelper;

namespace Opportunity.UWP.Converters
{
    /// <summary>
    /// An abstract class implemented <see cref="IValueConverter"/> with strong type of value,
    /// which can be used for the base class for value converters.
    /// </summary>
    /// <typeparam name="TFrom">Convert form type.</typeparam>
    /// <typeparam name="TTo">Convert to type.</typeparam>
    public abstract class ValueConverter<TFrom, TTo> : ValueConverter
    {

        /// <inheritdoc />
        public sealed override object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!TryChangeType<TFrom>(value, out var from))
                throw new ArgumentException($"Input value can't convert to TFrom:\"{typeof(TFrom)}\"", nameof(value));

            return Convert(from, parameter, language);
        }

        /// <inheritdoc />
        public sealed override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!TryChangeType<TTo>(value, out var to))
                throw new ArgumentException($"Input value can't convert to TTo:\"{typeof(TTo)}\"", nameof(value));

            return ConvertBack(to, parameter, language);
        }

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        public abstract TTo Convert(TFrom value, object parameter, string language);

        /// <summary>
        /// Modifies the target data before passing it to the source object. This method is called only in TwoWay bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The value to be passed to the source object.</returns>
        public abstract TFrom ConvertBack(TTo value, object parameter, string language);
    }
}
