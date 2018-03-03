using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="byte"/> conversion.
    /// </summary>
    public static class Byte
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static byte Add(byte value, byte addition) => (byte)(value + addition);
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static byte Subtract(byte value, byte subtraction) => (byte)(value - subtraction);
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static byte Increase(byte value) => (byte)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static byte Decrease(byte value) => (byte)(value - 1);
    }
}
