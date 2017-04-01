using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert an <see cref="Enum"/> to its <see cref="string"/> representation.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public class EnumToStringConverter : ChainConverter
    {
        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            return Enum.Parse(targetType, value.ToString());
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }
    }
}
