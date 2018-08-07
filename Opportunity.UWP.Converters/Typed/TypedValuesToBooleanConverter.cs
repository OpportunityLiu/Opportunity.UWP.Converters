using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static Opportunity.UWP.Converters.Internal.ConvertHelper;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Convert <see cref="object"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class ObjectToBooleanConverter : ValuesToBooleanConverter<object>
    {

    }

    /// <summary>
    /// Convert <see cref="string"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class StringToBooleanConverter : ValuesToBooleanConverter<string>
    {

    }

    /// <summary>
    /// Convert <see cref="char"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class CharToBooleanConverter : ValuesToBooleanConverter<char>
    {

    }

    /// <summary>
    /// Convert <see cref="ulong"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class UInt64ToBooleanConverter : ValuesToBooleanConverter<ulong>
    {

    }

    /// <summary>
    /// Convert <see cref="long"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class Int64ToBooleanConverter : ValuesToBooleanConverter<long>
    {

    }

    /// <summary>
    /// Convert <see cref="uint"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class UInt32ToBooleanConverter : ValuesToBooleanConverter<uint>
    {

    }

    /// <summary>
    /// Convert <see cref="int"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class Int32ToBooleanConverter : ValuesToBooleanConverter<int>
    {

    }

    /// <summary>
    /// Convert <see cref="ushort"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class UInt16ToBooleanConverter : ValuesToBooleanConverter<ushort>
    {

    }

    /// <summary>
    /// Convert <see cref="short"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class Int16ToBooleanConverter : ValuesToBooleanConverter<short>
    {

    }

    /// <summary>
    /// Convert <see cref="byte"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class ByteToBooleanConverter : ValuesToBooleanConverter<byte>
    {

    }

    /// <summary>
    /// Convert <see cref="sbyte"/>s to <see cref="bool"/> values.
    /// </summary>
    public sealed class SByteToBooleanConverter : ValuesToBooleanConverter<sbyte>
    {
    }
}
