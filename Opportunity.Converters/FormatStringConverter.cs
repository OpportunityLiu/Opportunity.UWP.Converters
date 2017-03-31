using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters
{
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public class FormatStringConverter : ChainConverter
    {
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            if(parameter == null)
                return value.ToString();
            var format = Internal.ResourcesHelper.GetString(parameter.ToString());
            return string.Format(CultureInfo.CurrentCulture, format, value);
        }

        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
