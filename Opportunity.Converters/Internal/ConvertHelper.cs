using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Markup;
using System.Reflection;

namespace Opportunity.Converters.Internal
{
    internal static class ConvertHelper
    {
        public static T ChangeType<T>(object value)
        {
            if (value is T v)
                return v;
            if (ChangeType(value, typeof(T)) is T r)
                return r;
            return default(T);
        }

        public static object ChangeType(object value, Type targetType)
        {
            if (value == null)
                return null;
            if (targetType.IsInstanceOfType(targetType))
                return value;
            if (value is IConvertible ic)
                return Convert.ChangeType(ic, targetType);
            return XamlBindingHelper.ConvertValue(targetType, value);
        }
    }
}
