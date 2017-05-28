using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters._Chains
{
    /// <summary>
    /// Convert <see cref="bool"/> to objects.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public sealed class BooleanToObjectConverter : ChainConverter
    {
        /// <summary>
        /// Value for <c>true</c> to convert to.
        /// </summary>
        public object ValueForTrue
        {
            get => (GetValue(ValueForTrueProperty)); set => SetValue(ValueForTrueProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ValueForTrue"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueForTrueProperty =
            DependencyProperty.Register("ValueForTrue", typeof(object), typeof(BooleanToObjectConverter), new PropertyMetadata(null));

        /// <summary>
        /// Value for <c>false</c> to convert to.
        /// </summary>
        public object ValueForFalse
        {
            get => (GetValue(ValueForFalseProperty)); set => SetValue(ValueForFalseProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ValueForFalse"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueForFalseProperty =
            DependencyProperty.Register("ValueForFalse", typeof(object), typeof(BooleanToObjectConverter), new PropertyMetadata(null));


        /// <summary>
        /// Convert objects back to <see cref="bool"/>.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="parameter">
        /// Will be return value 
        /// if <paramref name="value"/> is both <see cref="ValueForFalse"/> and <see cref="ValueForTrue"/>
        /// or <paramref name="value"/> is neither <see cref="ValueForFalse"/> nor <see cref="ValueForTrue"/>.
        /// Defalut value of <paramref name="parameter"/> will be false.
        /// </param>
        /// <param name="language">Not used.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        /// <remarks>
        /// Default comparer will be used to compare <paramref name="value"/> and <see cref="ValueForFalse"/> and <see cref="ValueForTrue"/>.
        /// </remarks>
        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            var isT = Equals(value, ValueForTrue);
            var isF = Equals(value, ValueForFalse);
            if (isT && !isF)
                return true;
            if (isF && !isT)
                return false;
            return Internal.ConvertHelper.ChangeType<bool>(parameter);
        }

        /// <summary>
        /// Convert <see cref="bool"/> to object.
        /// </summary>
        /// <param name="value">A <see cref="bool"/> to convert.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns>Converted value.</returns>
        protected override object ConvertImpl(object value, object parameter, string language)
        {
            var v = Internal.ConvertHelper.ChangeType<bool>(value);
            if (v)
                return ValueForTrue;
            else
                return ValueForFalse;
        }
    }
}
