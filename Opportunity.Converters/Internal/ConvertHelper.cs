using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Markup;
using System.Reflection;

namespace Opportunity.Converters.Internal
{
#if DEBUG
    public
#endif
        static class ConvertHelper
    {
        public static T ChangeType<T>(object value)
        {
            if (value is T v)
                return v;
            if (value == null)
            {
                if (TypeTraits<T>.Info.CanBeNull)
                    return default(T);
                else
                    throw new InvalidCastException($"Failed to convert null to {typeof(T)}.");
            }
            if (TypeTraits<T>.Info.IsEnum)
            {
                return (T)changeToEnumCore(value, typeof(T));
            }
            var targetType = typeof(T);
            return (T)changeTypeCore(value, typeof(T));
        }

        public static object ChangeType(object value, Type targetType)
        {
            if (targetType.IsInstanceOfType(value))
                return value;
            if (value == null)
            {
                if (TypeTraits.InfoOf(targetType).CanBeNull)
                    return null;
                else
                    throw new InvalidCastException($"Failed to convert null to {targetType}.");
            }
            return changeTypeCore(value, targetType);
        }

        private static object changeToEnumCore(object value, Type targetType)
        {
            Exception lastError;
            if (value is string s)
            {
                try
                {
                    return Enum.Parse(targetType, value.ToString(), true);
                }
                catch (Exception ex)
                {
                    // Try next method.
                    lastError = ex;
                }
            }
            if (value is IConvertible cv)
            {
                try
                {
                    var uType = Enum.GetUnderlyingType(targetType);
                    var r = changeTypeCore(value, uType);
                    return Enum.ToObject(targetType, r);
                }
                catch (Exception ex)
                {
                    // Try next method.
                    lastError = ex;
                }
            }
            try
            {
                return Enum.Parse(targetType, value.ToString(), true);
            }
            catch (Exception ex)
            {
                // Try next method.
                lastError = ex;
            }
            if (value == null)
                throw new InvalidCastException($"Failed to convert null to {targetType}.", lastError);
            else
                throw new InvalidCastException($"Failed to convert {value} from {value.GetType()} to {targetType}.", lastError);
        }

        private static object changeTypeCore(object value, Type targetType)
        {
            Exception lastError;
            try
            {
                if (value is IConvertible ic)
                    return Convert.ChangeType(ic, targetType);
            }
            catch (Exception ex)
            {
                // Try next method.
                lastError = ex;
            }
            try
            {
                var r = XamlBindingHelper.ConvertValue(targetType, value);
                if (r.GetType() != targetType)
                    throw new InvalidOperationException("XamlBindingHelper.ConvertValue(Type, object) returns a wrong type.");
                return r;
            }
            catch (Exception ex)
            {
                // Try next method.
                lastError = ex;
            }
            try
            {
                return Convert.ChangeType(Convert.ToDouble(value), targetType);
            }
            catch (Exception ex)
            {
                // Try next method.
                lastError = ex;
            }
            if (value == null)
                throw new InvalidCastException($"Failed to convert null to {targetType}.", lastError);
            else
                throw new InvalidCastException($"Failed to convert {value} from {value.GetType()} to {targetType}.", lastError);
        }
    }
}
