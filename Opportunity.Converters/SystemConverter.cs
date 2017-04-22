using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Windows.UI.Xaml;
using static Opportunity.Converters.Internal.ConvertHelper;
using Windows.UI.Xaml.Markup;

namespace Opportunity.Converters
{
    /// <summary>
    /// Default conversion by <see cref="Convert"/> and <see cref="XamlBindingHelper"/>.
    /// </summary>
    [ContentProperty(Name = nameof(InnerConverter))]
    public sealed class SystemConverter : ChainConverter
    {
        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            return ChangeType(value, targetType);
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            return ChangeType(value, targetType);
        }
    }
}
