using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="int"/> conversion.
    /// </summary>
    public static class Int32
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static int Add(int value, int addition) => value + addition;
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static int Subtract(int value, int subtraction) => value - subtraction;
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static int Increase(int value) => value + 1;
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static int Decrease(int value) => value - 1;
    }
}
