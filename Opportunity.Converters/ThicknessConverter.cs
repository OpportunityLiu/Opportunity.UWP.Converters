using System;
using System.Collections.Generic;
using System.Linq;
using Thickness = Windows.UI.Xaml.Thickness;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert <see cref="Thickness"/> value of its four edges.
    /// Use ConverterParameter to assign how to convert.
    /// <example>
    /// <para>
    /// If the ConverterParameter is "0,10",
    /// (10,20,30,40) will convert to (10,30,30,50).
    /// </para>
    /// <para>
    /// If the ConverterParameter is "-10,x0.5,10,x2",
    /// (10,20,30,40) will convert to (0,10,40,80).
    /// </para>
    /// </example>
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public sealed class ThicknessConverter : ChainConverter
    {
        private static readonly object empty = new Thickness();

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            return convertCore(value, parameter.ToString(), false);
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            return convertCore(value, parameter.ToString(), true);
        }

        private static char[] spliter = new[] { ' ', ',' };

        private struct Operation
        {
            public double Value;
            public bool IsOffset;

            public void OperateOn(ref double value, bool direction)
            {
                if (direction)
                {
                    if (this.IsOffset)
                        value += this.Value;
                    else
                        value *= this.Value;
                }
                else
                {
                    if (this.IsOffset)
                        value -= this.Value;
                    else
                        value /= this.Value;
                }
            }

            public static Operation Parse(string s)
            {
                if (s[0] == 'x' || s[0] == 'X' || s[0] == '*' || s[0] == '×')
                    return new Operation
                    {
                        IsOffset = false,
                        Value = double.Parse(s.Substring(1))
                    };
                else
                    return new Operation
                    {
                        IsOffset = true,
                        Value = double.Parse(s)
                    };
            }
        }

        private static Dictionary<string, Operation[]> cache = new Dictionary<string, Operation[]>();

        private static object convertCore(object value, string parameter, bool direction)
        {
            if (!(value is Thickness tk))
                return empty;
            try
            {
                if (!cache.TryGetValue(parameter, out var numbers))
                    cache[parameter] = numbers = parameter.ToString()
                       .Split(spliter, StringSplitOptions.RemoveEmptyEntries)
                       .Select(Operation.Parse)
                       .ToArray();
                var l = tk.Left;
                var r = tk.Right;
                var t = tk.Top;
                var b = tk.Bottom;
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
                    break;
                }
                return new Thickness(l, t, r, b);
            }
            catch (FormatException)
            {
                return tk;
            }
        }
    }
}
