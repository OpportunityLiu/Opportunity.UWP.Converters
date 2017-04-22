using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert <c>value</c> to <c>!value</c>.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public sealed class LogicalNotConverter : ChainConverter
    {
        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            return convert(value);
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            return convert(value);
        }

        private static object convert(object value)
        {
            var v = Internal.ConvertHelper.ChangeType<bool>(value);
            return !v;
        }
    }
}
