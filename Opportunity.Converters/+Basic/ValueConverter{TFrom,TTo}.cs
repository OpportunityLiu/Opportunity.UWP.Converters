using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static Opportunity.Converters.Internal.ConvertHelper;

namespace Opportunity.Converters
{
    public abstract class ValueConverter<TFrom, TTo> : ValueConverter, IValueConverterFrom<TFrom>, IValueConverterTo<TTo>
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

        object IValueConverterFrom<TFrom>.Convert(TFrom value, Type targetType, object parameter, string language)
        {
            return Convert(value, parameter, language);
        }

        TFrom IValueConverterFrom<TFrom>.ConvertBack(object value, object parameter, string language)
        {
            if (!TryChangeType<TTo>(value, out var to))
                throw new ArgumentException($"Input value can't convert to TTo:\"{typeof(TTo)}\"", nameof(value));

            return ConvertBack(to, parameter, language);
        }

        TTo IValueConverterTo<TTo>.Convert(object value, object parameter, string language)
        {
            if (!TryChangeType<TFrom>(value, out var from))
                throw new ArgumentException($"Input value can't convert to TFrom:\"{typeof(TFrom)}\"", nameof(value));

            return Convert(from, parameter, language);
        }

        object IValueConverterTo<TTo>.ConvertBack(TTo value, Type targetType, object parameter, string language)
        {
            return ConvertBack(value, parameter, language);
        }

        public abstract TTo Convert(TFrom value, object parameter, string language);
        public abstract TFrom ConvertBack(TTo value, object parameter, string language);
    }
}
