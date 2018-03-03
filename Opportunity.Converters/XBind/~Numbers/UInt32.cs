using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="uint"/> conversion.
    /// </summary>
    public static class UInt32
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static uint Add(uint value, int addition) => (uint)(value + addition);
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static uint Subtract(uint value, int subtraction) => (uint)(value - subtraction);
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static uint Increase(uint value) => value + 1u;
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static uint Decrease(uint value) => value - 1u;
    }
}
