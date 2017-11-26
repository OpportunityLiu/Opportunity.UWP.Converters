using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Markup;
using System.Reflection;
using Opportunity.Helpers;

namespace Opportunity.Converters.Internal
{
    static class ConvertHelper
    {
        private struct ChangeTypeData<T>
        {
            private static readonly Exception defaultError = new Exception("Hasn't set result.");

            public ChangeTypeData(object value)
            {
                this.OriginalValue = value;
                this.TargetType = TypeTraits.Of<T>();
                this.ConvertedValue = default;
                this.Error = defaultError;
            }

            public ChangeTypeData(object value, Type targetType)
                : this(value)
            {
                this.TargetType = TypeTraits.Of(targetType);
            }

            public object OriginalValue { get; }
            public T ConvertedValue { get; private set; }
            public TypeTraitsInfo TargetType { get; }
            public Exception Error { get; private set; }

            public bool Succeed => this.Error == null;

            public T GetResultOrThrow()
            {
                if (Error == null)
                    return ConvertedValue;
                if (OriginalValue == null)
                    throw new InvalidCastException($"Failed to convert null to {TargetType.Type}.", Error);
                else
                    throw new InvalidCastException($"Failed to convert {OriginalValue} from {OriginalValue.GetType()} to {TargetType.Type}.", Error);
            }

            public void SetError(Exception error)
            {
                this.ConvertedValue = default;
                this.Error = error;
            }

            public void SetResult(T convertedValue)
            {
                this.ConvertedValue = convertedValue;
                this.Error = null;
            }
        }

        public static bool TryChangeType<T>(object value, out T result)
        {
            var data = new ChangeTypeData<T>(value);
            changeTypeImpl(ref data);
            if (data.Succeed)
            {
                result = data.ConvertedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public static T ChangeType<T>(object value)
        {
            var data = new ChangeTypeData<T>(value);
            changeTypeImpl(ref data);
            return data.GetResultOrThrow();
        }

        public static bool TryChangeType(object value, Type targetType, out object result)
        {
            var data = new ChangeTypeData<object>(value, targetType);
            changeTypeImpl(ref data);
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
            var data = new ChangeTypeData<object>(value, targetType);
            changeTypeImpl(ref data);
            return data.GetResultOrThrow();
        }

        private static void changeTypeImpl<T>(ref ChangeTypeData<T> data)
        {
            if (data.OriginalValue is T v)
            {
                data.SetResult(v);
                return;
            }
            if (data.OriginalValue == null)
            {
                data.SetResult(default);
                return;
            }
            if (data.OriginalValue is string s && string.IsNullOrEmpty(s) && data.TargetType.CanBeNull)
            {
                data.SetResult(default);
                return;
            }
            if (data.TargetType.Type.IsEnum)
            {
                changeToEnumCore(ref data);
                if (data.Succeed)
                    return;
            }
            changeTypeCore(ref data);
        }

        private static void changeTypeImpl(ref ChangeTypeData<object> data)
        {
            if (data.TargetType.Type.AsType().IsInstanceOfType(data.OriginalValue))
            {
                data.SetResult(data.OriginalValue);
                return;
            }
            if (data.OriginalValue == null)
            {
                data.SetResult(data.TargetType.Default);
                return;
            }
            if (data.OriginalValue is string s && string.IsNullOrEmpty(s) && data.TargetType.CanBeNull)
            {
                data.SetResult(data.TargetType.Default);
                return;
            }
            if (data.TargetType.Type.IsEnum)
            {
                changeToEnumCore(ref data);
                if (data.Succeed)
                    return;
            }
            changeTypeCore(ref data);
        }

        private static void changeToEnumCore<T>(ref ChangeTypeData<T> data)
        {
            if (data.OriginalValue is string s)
            {
                try
                {
                    data.SetResult((T)Enum.Parse(data.TargetType.Type.AsType(), s, true));
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
                    var uType = Enum.GetUnderlyingType(data.TargetType.Type.AsType());
                    var task2 = new ChangeTypeData<object>(data.OriginalValue, uType);
                    changeTypeCore(ref task2);
                    if (task2.Succeed)
                    {
                        data.SetResult((T)Enum.ToObject(data.TargetType.Type.AsType(), task2.ConvertedValue));
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
                data.SetResult((T)Enum.Parse(data.TargetType.Type.AsType(), data.OriginalValue.ToString(), true));
                return;
            }
            catch (Exception ex)
            {
                // Try next method.
                data.SetError(ex);
            }
        }

        private static void changeTypeCore<T>(ref ChangeTypeData<T> data)
        {
            try
            {
                if (data.OriginalValue is IConvertible ic)
                {
                    data.SetResult((T)Convert.ChangeType(ic, data.TargetType.Type.AsType()));
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
                var r = XamlBindingHelper.ConvertValue(data.TargetType.Type.AsType(), data.OriginalValue);
                if (r.GetType() != data.TargetType.Type.AsType())
                {
                    data.SetError(new InvalidOperationException("XamlBindingHelper.ConvertValue(Type, object) returns a wrong type."));
                    return;
                }
                data.SetResult((T)r);
                return;
            }
            catch (Exception ex)
            {
                // Try next method.
                data.SetError(ex);
            }
            try
            {
                data.SetResult((T)Convert.ChangeType(Convert.ToDouble(data.OriginalValue), data.TargetType.Type.AsType()));
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
