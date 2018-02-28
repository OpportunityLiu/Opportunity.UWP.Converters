using Opportunity.Converters.Typed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of string conversion.
    /// </summary>
    public static class Stringify
    {
        /// <summary>
        /// Convert a byte size to its <see cref="string"/> representation.
        /// </summary>
        /// <param name="size">The byte size to convert.</param>
        /// <returns>The <see cref="string"/> representation of byte size.</returns>
        public static string OfByteSizeBinary(long size)
            => ByteSizeToStringConverter.ByteSizeToString(size, UnitPrefix.Binary);

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <returns>The byte size.</returns>
        public static long FromByteSizeBinary(string sizeStr)
            => ByteSizeToStringConverter.StringToByteSize(sizeStr, UnitPrefix.Binary);
        /// <summary>
        /// Convert a byte size to its <see cref="string"/> representation.
        /// </summary>
        /// <param name="size">The byte size to convert.</param>
        /// <returns>The <see cref="string"/> representation of byte size.</returns>
        public static string OfByteSizeMetric(long size)
            => ByteSizeToStringConverter.ByteSizeToString(size, UnitPrefix.Metric);

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <returns>The byte size.</returns>
        public static long FromByteSizeMetric(string sizeStr)
            => ByteSizeToStringConverter.StringToByteSize(sizeStr, UnitPrefix.Metric);
    }
}
