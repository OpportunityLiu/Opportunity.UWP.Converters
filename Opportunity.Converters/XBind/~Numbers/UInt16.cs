using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="ushort"/> conversion.
    /// </summary>
    public static class UInt16
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static ushort Add(ushort value, short addition) => (ushort)(value + addition);
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static ushort Subtract(ushort value, short subtraction) => (ushort)(value - subtraction);
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static ushort Increase(ushort value) => (ushort)(value + 1U);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static ushort Decrease(ushort value) => (ushort)(value - 1U);
    }
}
