using System;
using static Opportunity.Converters.Internal.ConvertHelper;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

namespace Opportunity.Converters
{
    /// <summary>
    /// Default conversion by <see cref="System.Convert.ChangeType(object, Type)"/> and <see cref="XamlBindingHelper.ConvertValue(Type, object)"/>.
    /// </summary>
    public sealed class SystemConverter : ValueConverter
    {
        /// <summary>
        /// A default instance of <see cref="SystemConverter"/>.
        /// </summary>
        public static SystemConverter Default { get; } = new SystemConverter();

        /// <inheritdoc />
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            return ChangeType(value, targetType);
        }

        /// <inheritdoc />
        public override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ChangeType(value, targetType);
        }
    }
}
