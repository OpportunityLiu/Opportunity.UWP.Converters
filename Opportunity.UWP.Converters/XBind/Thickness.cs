using System;
using System.Collections.Generic;
using System.Linq;
using WinRTThickness = Windows.UI.Xaml.Thickness;

namespace Opportunity.UWP.Converters.XBind
{
    /// <summary>
    /// Methods of <see cref="WinRTThickness"/> conversion.
    /// </summary>
    public static class Thickness
    {
        private struct Operation
        {
            public double Scale;
            public double Offset;

            public void OperateOn(ref double value, bool direction)
            {
                if (direction)
                {
                    value *= this.Scale;
                    value += this.Offset;
                }
                else
                {
                    value -= this.Offset;
                    value /= this.Scale;
                }
            }

            private static readonly char[] pm = "+-".ToCharArray();
            public static Operation Parse(string s)
            {
                switch (s[0])
                {
                case 'x':
                case 'X':
                case '*':
                case '×':
                    if (double.TryParse(s.Substring(1), out var sca))
                        return new Operation
                        {
                            Scale = sca,
                            Offset = 0,
                        };
                    else
                    {
                        var op = s.LastIndexOfAny(pm);
                        if (op <= 1)
                            throw new FormatException("Wrong operation format.");
                        return new Operation
                        {
                            Scale = double.Parse(s.Substring(1, op - 1)),
                            Offset = double.Parse(s.Substring(op)),
                        };
                    }
                default:
                    return new Operation
                    {
                        Scale = 1,
                        Offset = double.Parse(s),
                    };
                }
            }

            public override string ToString() => $"*{this.Scale}+{this.Offset}";
        }

        private static Dictionary<string, Operation[]> cache = new Dictionary<string, Operation[]>();

        private static char[] spliter = new[] { ' ', ',' };
        private static WinRTThickness convertCore(WinRTThickness value, string parameter, bool direction)
        {
            try
            {
                if (!cache.TryGetValue(parameter, out var numbers))
                {
                    numbers = parameter.Split(spliter, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(Operation.Parse)
                                       .ToArray();
                    cache[parameter] = numbers;
                }
                var l = value.Left;
                var r = value.Right;
                var t = value.Top;
                var b = value.Bottom;
                switch (numbers.Length)
                {
                case 1:
                    numbers[0].OperateOn(ref l, direction);
                    numbers[0].OperateOn(ref r, direction);
                    numbers[0].OperateOn(ref t, direction);
                    numbers[0].OperateOn(ref b, direction);
                    break;
                case 2:
                    numbers[0].OperateOn(ref l, direction);
                    numbers[0].OperateOn(ref r, direction);
                    numbers[1].OperateOn(ref t, direction);
                    numbers[1].OperateOn(ref b, direction);
                    break;
                case 4:
                    numbers[0].OperateOn(ref l, direction);
                    numbers[2].OperateOn(ref r, direction);
                    numbers[1].OperateOn(ref t, direction);
                    numbers[3].OperateOn(ref b, direction);
                    break;
                default:
                    throw new ArgumentException("Wrong format.", nameof(parameter));
                }
                return new WinRTThickness(l, t, r, b);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Wrong format.", nameof(parameter), ex);
            }
        }

        /// <summary>
        /// Convert <see cref="WinRTThickness"/> value of its four edges.
        /// Use <paramref name="parameter"/> to assign how to convert.
        /// <example>
        /// <para>
        /// If the <paramref name="parameter"/> is "0,10",
        /// (10,20,30,40) will convert to (10,30,30,50).
        /// </para>
        /// <para>
        /// If the <paramref name="parameter"/> is "-10,x0.5+5,10,x2",
        /// (10,20,30,40) will convert to (0,15,40,80).
        /// </para>
        /// </example>
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="parameter">Parameter controls conversion.</param>
        /// <returns>Converted value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="parameter"/> is <see langword="null"/>.</exception>
        public static WinRTThickness Convert(WinRTThickness value, string parameter)
            => convertCore(value, parameter ?? throw new ArgumentNullException(nameof(parameter)), true);

        /// <summary>
        /// Convert back <see cref="WinRTThickness"/> value of its four edges.
        /// Use <paramref name="parameter"/> to assign how to convert.
        /// <example>
        /// <para>
        /// If the <paramref name="parameter"/> is "0,10",
        /// (10,30,30,50) will convert to (10,20,30,40).
        /// </para>
        /// <para>
        /// If the <paramref name="parameter"/> is "-10,x0.5+5,10,x2",
        /// (0,15,40,80) will convert to (10,20,30,40).
        /// </para>
        /// </example>
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="parameter">Parameter controls conversion.</param>
        /// <returns>Converted value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="parameter"/> is <see langword="null"/>.</exception>
        public static WinRTThickness ConvertBack(WinRTThickness value, string parameter)
            => convertCore(value, parameter ?? throw new ArgumentNullException(nameof(parameter)), false);
    }
}
