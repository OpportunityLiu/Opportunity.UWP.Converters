using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="long"/> conversion.
    /// </summary>
    public static class Int64
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static long Add(long value, long addition) => value + addition;
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static long Subtract(long value, long subtraction) => value - subtraction;
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static long Increase(long value) => value + 1L;
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static long Decrease(long value) => value - 1L;
    }
}
