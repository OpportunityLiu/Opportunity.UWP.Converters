using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Opportunity.Converters.MathExpression;
using static Opportunity.Converters.Internal.ConvertHelper;

namespace Opportunity.Converters
{
    /// <summary>
    /// Use a math expression to convert number values.
    /// </summary>
    public class MathExpressionConverter : ChainConverter
    {
        /// <summary>
        /// Expression used to convert value.
        /// <para>
        /// Use x to indicate the input value. Available operators: +, -, *, /, ^; available functions: <see cref="Functions"/> and <see cref="Math"/>.
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
           DependencyProperty.Register("ConvertExpression", typeof(string), typeof(MathExpressionConverter), new PropertyMetadata(null, ConvertExpressionPropertyChangedCallback));

        private IParseResult<MathExpression.Delegates.Function1> convert;

        private static void ConvertExpressionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (MathExpressionConverter)d;
            sender.convert = Parser.Parse1((e.NewValue ?? "").ToString());
        }

        /// <summary>
        /// Expression used to convert value.
        /// <para>
        /// Use x to indicate the input value. Available operators: +, -, *, /, ^; available functions: <see cref="Functions"/> and <see cref="Math"/>.
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
            DependencyProperty.Register("ConvertBackExpression", typeof(string), typeof(MathExpressionConverter), new PropertyMetadata(null, ConvertBackExpressionPropertyChangedCallback));

        private IParseResult<MathExpression.Delegates.Function1> convertback;

        private static void ConvertBackExpressionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (MathExpressionConverter)d;
            sender.convertback = Parser.Parse1((e.NewValue ?? "").ToString());
        }

        /// <inhertdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            return this.convertback.Compiled(ChangeType<double>(value));
        }

        /// <inhertdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            return this.convert.Compiled(ChangeType<double>(value));
        }
    }
}
