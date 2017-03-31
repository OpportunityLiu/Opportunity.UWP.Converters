using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Windows.UI.Xaml;
using static Opportunity.Converters.Internal.ConvertHelper;

namespace Opportunity.Converters
{
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public class SystemConverter : ChainConverter
    {
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            return ChangeType(value, targetType);
        }

        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            return ChangeType(value, targetType);
        }
    }
}
