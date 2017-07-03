using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.Internal
{

#if DEBUG
    public
#endif
        static class TypeTraits
    {
        private static Dictionary<Type, TypeTraitsInfo> typeTraitsCache = new Dictionary<Type, TypeTraitsInfo>();

        public static TypeTraitsInfo InfoOf(Type type)
        {
            if (typeTraitsCache.TryGetValue(type, out var info))
                return info;
            var infoType = typeof(TypeTraits<>).MakeGenericType(type);
            var f = infoType.GetField("Info", BindingFlags.Static | BindingFlags.Public);
            info = (TypeTraitsInfo)f.GetValue(null);
            typeTraitsCache[type] = info;
            return info;
        }
    }

#if DEBUG
    public
#endif
        static class TypeTraits<T>
    {
        public static readonly TypeTraitsInfo Info;

        static TypeTraits()
        {
            var info = default(TypeTraitsInfo);
            info.CanBeNull = (default(T) == null);
            info.IsNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
            info.IsClass = info.CanBeNull && !info.IsNullable;
            info.IsEnum = default(T) is Enum;
            Info = info;
        }
    }

    public struct TypeTraitsInfo
    {
        public bool CanBeNull;
        public bool IsNullable;
        public bool IsClass;
        public bool IsEnum;
    }
}
