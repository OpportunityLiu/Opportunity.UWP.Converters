using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="short"/> conversion.
    /// </summary>
    public static class Int16
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static short Add(short value, short addition) => (short)(value + addition);
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static short Subtract(short value, short subtraction) => (short)(value - subtraction);
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static short Increase(short value) => (short)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static short Decrease(short value) => (short)(value - 1);
    }
}
