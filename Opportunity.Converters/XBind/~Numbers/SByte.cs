using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="sbyte"/> conversion.
    /// </summary>
    public static class SByte
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static sbyte Add(sbyte value, byte addition) => (sbyte)(value + addition);
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static sbyte Subtract(sbyte value, byte subtraction) => (sbyte)(value - subtraction);
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static sbyte Increase(sbyte value) => (sbyte)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static sbyte Decrease(sbyte value) => (sbyte)(value - 1);
    }
}
