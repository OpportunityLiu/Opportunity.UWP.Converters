using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Convert <c>value</c> to <c>!value</c>.
    /// </summary>
    public sealed class LogicalNotConverter : ValueConverter<bool, bool>
    {
        /// <inheritdoc />
        public override bool Convert(bool value, object parameter, string language)
        {
            return !value;
        }

        /// <inheritdoc />
        public override bool ConvertBack(bool value, object parameter, string language)
        {
            return !value;
        }
    }
}
