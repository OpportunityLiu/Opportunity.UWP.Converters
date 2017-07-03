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
        private class ChangeTypeData
        {
            private static readonly Exception defaultError = new Exception("Hasn't set result.");

            public ChangeTypeData(object value, Type targetType)
            {
                this.OriginalValue = value;
                this.TargetType = targetType;
                this.ConvertedValue = null;
                this.Error = defaultError;
            }

            public object OriginalValue { get; }
            public object ConvertedValue { get; private set; }
            public Type TargetType { get; }
            public Exception Error { get; private set; }

            public bool Succeed => this.Error == null;

            public object GetResultOrThrow()
            {
                if (Error == null)
                    return ConvertedValue;
                if (OriginalValue == null)
                    throw new InvalidCastException($"Failed to convert null to {TargetType}.", Error);
                else
                    throw new InvalidCastException($"Failed to convert {OriginalValue} from {OriginalValue.GetType()} to {TargetType}.", Error);
            }

            public void SetError(Exception error)
            {
                this.ConvertedValue = null;
                this.Error = error;
            }

            public void SetResult(object convertedValue)
            {
                this.ConvertedValue = convertedValue;
                this.Error = null;
            }
        }

        private class ChangeTypeData<T> : ChangeTypeData
        {
            public ChangeTypeData(object value) : base(value, typeof(T))
            {
            }

            public new T ConvertedValue => (T)base.ConvertedValue;

            public void SetResult(T convertedValue)
            {
                base.SetResult(convertedValue);
            }

            public new T GetResultOrThrow()
            {
                return (T)base.GetResultOrThrow();
            }
        }

        public static bool TryChangeType<T>(object value, out T result)
        {
            var data = new ChangeTypeData<T>(value);
            changeTypeImpl(data);
            if (data.Succeed)
            {
                result = data.ConvertedValue;
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }

        public static T ChangeType<T>(object value)
        {
            var data = new ChangeTypeData<T>(value);
            changeTypeImpl(data);
            return data.GetResultOrThrow();
        }

        public static bool TryChangeType(object value, Type targetType, out object result)
        {
            var data = new ChangeTypeData(value, targetType);
            changeTypeImpl(data);
            if (data.Succeed)
            {
                result = data.ConvertedValue;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public static object ChangeType(object value, Type targetType)
        {
            var data = new ChangeTypeData(value, targetType);
            changeTypeImpl(data);
            return data.GetResultOrThrow();
        }

        private static void changeTypeImpl<T>(ChangeTypeData<T> data)
        {
            if (data.OriginalValue is T v)
            {
                data.SetResult(v);
                return;
            }
            if (data.OriginalValue == null)
            {
                if (TypeTraits<T>.Info.CanBeNull)
                {
                    data.SetResult(default(T));
                    return;
                }
                else
                {
                    data.SetError(new ArgumentNullException("value", $"Cannot convert null to non-nullable type {typeof(T)}."));
                    return;
                }
            }
            if (TypeTraits<T>.Info.IsEnum)
            {
                changeToEnumCore(data);
                if (data.Succeed)
                    return;
            }
            changeTypeCore(data);
        }

        private static void changeTypeImpl(ChangeTypeData data)
        {
            if (data.TargetType.IsInstanceOfType(data.OriginalValue))
            {
                data.SetResult(data.OriginalValue);
                return;
            }
            var info = TypeTraits.InfoOf(data.TargetType);
            if (data.OriginalValue == null)
            {
                if (info.CanBeNull)
                {
                    data.SetResult(null);
                    return;
                }
                else
                {
                    data.SetError(new ArgumentNullException("value", $"Cannot convert null to non-nullable type {data.TargetType}."));
                    return;
                }
            }
            if (info.IsEnum)
            {
                changeToEnumCore(data);
                if (data.Succeed)
                    return;
            }
            changeTypeCore(data);
        }

        private static void changeToEnumCore(ChangeTypeData data)
        {
            if (data.OriginalValue is string s)
            {
                try
                {
                    data.SetResult(Enum.Parse(data.TargetType, s, true));
                    return;
                }
                catch (Exception ex)
                {
                    // Try next method.
                    data.SetError(ex);
                }
            }
            if (data.OriginalValue is IConvertible cv)
            {
                try
                {
                    var uType = Enum.GetUnderlyingType(data.TargetType);
                    var task2 = new ChangeTypeData(data.OriginalValue, uType);
                    changeTypeCore(task2);
                    if (task2.Succeed)
                    {
                        data.SetResult(Enum.ToObject(data.TargetType, task2.ConvertedValue));
                        return;
                    }
                    data.SetError(task2.Error);
                }
                catch (Exception ex)
                {
                    // Try next method.
                    data.SetError(ex);
                }
            }
            try
            {
                data.SetResult(Enum.Parse(data.TargetType, data.OriginalValue.ToString(), true));
                return;
            }
            catch (Exception ex)
            {
                // Try next method.
                data.SetError(ex);
            }
        }

        private static void changeTypeCore(ChangeTypeData data)
        {
            try
            {
                if (data.OriginalValue is IConvertible ic)
                {
                    data.SetResult(Convert.ChangeType(ic, data.TargetType));
                    return;
                }
            }
            catch (Exception ex)
            {
                // Try next method.
                data.SetError(ex);
            }
            try
            {
                var r = XamlBindingHelper.ConvertValue(data.TargetType, data.OriginalValue);
                if (r.GetType() != data.TargetType)
                {
                    data.SetError(new InvalidOperationException("XamlBindingHelper.ConvertValue(Type, object) returns a wrong type."));
                    return;
                }
                data.SetResult(r);
                return;
            }
            catch (Exception ex)
            {
                // Try next method.
                data.SetError(ex);
            }
            try
            {
                data.SetResult(Convert.ChangeType(Convert.ToDouble(data.OriginalValue), data.TargetType));
                return;
            }
            catch (Exception ex)
            {
                // Try next method.
                data.SetError(ex);
            }
        }
    }
}
