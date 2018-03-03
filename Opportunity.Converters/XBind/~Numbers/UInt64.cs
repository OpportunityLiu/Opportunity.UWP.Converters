using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="ulong"/> conversion.
    /// </summary>
    public static class UInt64
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static ulong Add(ulong value, long addition)
        {
            if (addition >= 0)
                return value + (ulong)addition;
            else
                return value - (ulong)(-addition);
        }

        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static ulong Subtract(ulong value, long subtraction)
        {
            if (subtraction >= 0)
                return value - (ulong)subtraction;
            else
                return value + (ulong)(-subtraction);
        }

        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static ulong Increase(ulong value) => value + 1UL;
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static ulong Decrease(ulong value) => value - 1UL;
    }
}
