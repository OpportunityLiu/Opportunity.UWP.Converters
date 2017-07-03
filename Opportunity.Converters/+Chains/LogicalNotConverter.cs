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
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public sealed class LogicalNotConverter : ChainConverter<bool, bool>
    {
        /// <inheritdoc />
        protected override bool ConvertBackImpl(bool value, object parameter, string language)
        {
            return !value;
        }

        /// <inheritdoc />
        protected override bool ConvertImpl(bool value, object parameter, string language)
        {
            return !value;
        }
    }
}
