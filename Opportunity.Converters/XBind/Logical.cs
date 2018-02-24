using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Static methods for bool operations.
    /// </summary>
    public static class Logical
    {
        /// <summary>
        /// Logical not.
        /// </summary>
        /// <param name="value">Original value</param>
        /// <returns>Result of <c>!<paramref name="value"/></c>.</returns>
        public static bool Not(bool value) => !value;

        /// <summary>
        /// Logical and.
        /// </summary>
        /// <param name="v1">First value.</param>
        /// <param name="v2">Second value.</param>
        /// <returns>Result of <c><paramref name="v1"/> &amp;&amp; <paramref name="v2"/></c>.</returns>
        public static bool And(bool v1, bool v2) => v1 && v2;

        /// <summary>
        /// Logical nand.
        /// </summary>
        /// <param name="v1">First value.</param>
        /// <param name="v2">Second value.</param>
        /// <returns>Result of <c>!(<paramref name="v1"/> &amp;&amp; <paramref name="v2"/>)</c>.</returns>
        public static bool Nand(bool v1, bool v2) => !(v1 && v2);

        /// <summary>
        /// Logical or.
        /// </summary>
        /// <param name="v1">First value.</param>
        /// <param name="v2">Second value.</param>
        /// <returns>Result of <c><paramref name="v1"/> || <paramref name="v2"/></c>.</returns>
        public static bool Or(bool v1, bool v2) => v1 || v2;

        /// <summary>
        /// Logical nor.
        /// </summary>
        /// <param name="v1">First value.</param>
        /// <param name="v2">Second value.</param>
        /// <returns>Result of <c>!(<paramref name="v1"/> || <paramref name="v2"/>)</c>.</returns>
        public static bool Nor(bool v1, bool v2) => !(v1 || v2);

        /// <summary>
        /// Logical xor.
        /// </summary>
        /// <param name="v1">First value.</param>
        /// <param name="v2">Second value.</param>
        /// <returns>Result of <c><paramref name="v1"/> ^ <paramref name="v2"/></c>.</returns>
        public static bool Xor(bool v1, bool v2) => v1 ^ v2;

        /// <summary>
        /// Logical xnor.
        /// </summary>
        /// <param name="v1">First value.</param>
        /// <param name="v2">Second value.</param>
        /// <returns>Result of <c>!(<paramref name="v1"/> ^ <paramref name="v2"/>)</c>.</returns>
        public static bool Xnor(bool v1, bool v2) => !(v1 ^ v2);
    }
}
