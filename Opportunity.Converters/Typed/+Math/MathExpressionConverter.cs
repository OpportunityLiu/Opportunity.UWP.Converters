using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Opportunity.MathExpression;
using Opportunity.MathExpression.Delegates;
using static Opportunity.Converters.Internal.ConvertHelper;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Use a math expression to convert number values.
    /// </summary>
    public sealed class MathExpressionConverter : ValueConverter<double, double>
    {
        /// <summary>
        /// Expression used to convert value.
        /// <para>
        /// Use x to indicate the input value. Available operators: +, -, *, /, ^; available functions: see <see cref="Functions"/> and <see cref="Math"/>.
        /// </para>
        /// <para>
        /// Example:
        /// <example>Sin(Max(x, 0)) + x ^ 2</example>
        /// </para>
        /// </summary>
        public string ConvertExpression
        {
            get => (string)GetValue(ConvertExpressionProperty);
            set => SetValue(ConvertExpressionProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ConvertExpression"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ConvertExpressionProperty =
           DependencyProperty.Register("ConvertExpression", typeof(string), typeof(MathExpressionConverter), new PropertyMetadata("x", ConvertExpressionPropertyChangedCallback));

        private Function1 convert;

        private static Function1 getResult(object expression)
        {
            if (expression == null)
                return x => x;
            var value = expression.ToString();
            if (string.IsNullOrWhiteSpace(value))
                return x => x;
            return Parser.Parse1(value).Compiled;
        }

        private static void ConvertExpressionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (MathExpressionConverter)d;
            sender.convert = getResult(e.NewValue);
        }

        /// <summary>
        /// Expression used to convert value.
        /// <para>
        /// Use x to indicate the input value. Available operators: +, -, *, /, ^; available functions: see <see cref="Functions"/> and <see cref="Math"/>.
        /// </para>
        /// <para>
        /// Example:
        /// <example>Sin(Max(x, 0)) + x ^ 2</example>
        /// </para>
        /// </summary>
        public string ConvertBackExpression
        {
            get => (string)GetValue(ConvertBackExpressionProperty);
            set => SetValue(ConvertBackExpressionProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ConvertBackExpression"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ConvertBackExpressionProperty =
            DependencyProperty.Register("ConvertBackExpression", typeof(string), typeof(MathExpressionConverter), new PropertyMetadata("x", ConvertBackExpressionPropertyChangedCallback));

        private Function1 convertback;

        private static void ConvertBackExpressionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (MathExpressionConverter)d;
            sender.convertback = getResult(e.NewValue);
        }

        /// <inhertdoc />
        public override double Convert(double value, object parameter, string language)
        {
            return this.convert(value);
        }

        /// <inhertdoc />
        public override double ConvertBack(double value, object parameter, string language)
        {
            return this.convertback(value);
        }
    }
}
