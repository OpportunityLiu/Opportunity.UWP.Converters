using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.UWP.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="float"/> conversion.
    /// </summary>
    public static partial class Single
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static float Increase(float value) => (float)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static float Decrease(float value) => (float)(value - 1);
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static float Add(float value, float addition) => value + addition;
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static float Subtract(float value, float subtraction) => value - subtraction;
        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(float value, float addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(float value, float subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(float value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(float value) => Decrease(value).ToString();
        
        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(float value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsNotZero(float value) => value != 0;
        /// <summary>
        /// Test <paramref name="value1"/> equals <paramref name="value2"/> or not.
        /// </summary>
        public static bool AreEqual(float value1, float value2) => value1 == value2;
        /// <summary>
        /// Test <paramref name="value1"/> equals <paramref name="value2"/> or not.
        /// </summary>
        public static bool AreNotEqual(float value1, float value2) => value1 != value2;
    }
    /// <summary>
    /// Method of <see cref="double"/> conversion.
    /// </summary>
    public static partial class Double
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static double Increase(double value) => (double)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static double Decrease(double value) => (double)(value - 1);
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static double Add(double value, double addition) => value + addition;
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static double Subtract(double value, double subtraction) => value - subtraction;
        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(double value, double addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(double value, double subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(double value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(double value) => Decrease(value).ToString();
        
        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(double value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsNotZero(double value) => value != 0;
        /// <summary>
        /// Test <paramref name="value1"/> equals <paramref name="value2"/> or not.
        /// </summary>
        public static bool AreEqual(double value1, double value2) => value1 == value2;
        /// <summary>
        /// Test <paramref name="value1"/> equals <paramref name="value2"/> or not.
        /// </summary>
        public static bool AreNotEqual(double value1, double value2) => value1 != value2;
    }
    /// <summary>
    /// Method of <see cref="decimal"/> conversion.
    /// </summary>
    public static partial class Decimal
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static decimal Increase(decimal value) => (decimal)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static decimal Decrease(decimal value) => (decimal)(value - 1);
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static decimal Add(decimal value, decimal addition) => value + addition;
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static decimal Subtract(decimal value, decimal subtraction) => value - subtraction;
        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(decimal value, decimal addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(decimal value, decimal subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(decimal value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(decimal value) => Decrease(value).ToString();
        
        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(decimal value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsNotZero(decimal value) => value != 0;
        /// <summary>
        /// Test <paramref name="value1"/> equals <paramref name="value2"/> or not.
        /// </summary>
        public static bool AreEqual(decimal value1, decimal value2) => value1 == value2;
        /// <summary>
        /// Test <paramref name="value1"/> equals <paramref name="value2"/> or not.
        /// </summary>
        public static bool AreNotEqual(decimal value1, decimal value2) => value1 != value2;
    }
}
