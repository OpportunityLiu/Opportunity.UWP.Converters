using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using System.Reflection;
using static Opportunity.Converters.Internal.ConvertHelper;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert <see cref="object"/>s to <see cref="bool"/> values.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public class ObjectToBooleanConverter : ChainConverter
    {
        /// <summary>
        /// <see cref="object"/>s will be converted to <c>true</c>.
        /// </summary>
        public object ValueForTrue
        {
            get => GetValue(ValueForTrueProperty); set => SetValue(ValueForTrueProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ValueForTrue"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueForTrueProperty =
            DependencyProperty.Register("ValueForTrue", typeof(object), typeof(ObjectToBooleanConverter), new PropertyMetadata(null, ValueChangedCallback));

        /// <summary>
        /// <see cref="object"/>s will be converted to <c>false</c>.
        /// </summary>
        public object ValueForFalse
        {
            get => GetValue(ValueForFalseProperty); set => SetValue(ValueForFalseProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ValueForFalse"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueForFalseProperty =
            DependencyProperty.Register("ValueForFalse", typeof(object), typeof(ObjectToBooleanConverter), new PropertyMetadata(null, ValueChangedCallback));

        /// <summary>
        /// Returns when <c>value != ValueForTrue &amp;&amp; value != ValueForFalse</c>.
        /// </summary>
        public bool IfNeither
        {
            get => (bool)GetValue(IfNeitherProperty);
            set => SetValue(IfNeitherProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IfNeither"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IfNeitherProperty =
            DependencyProperty.Register("IfNeither", typeof(bool), typeof(ObjectToBooleanConverter), new PropertyMetadata(false));

        /// <summary>
        /// Returns when <c>value == ValueForTrue &amp;&amp; value == ValueForFalse</c>.
        /// </summary>
        public bool IfBoth
        {
            get => (bool)GetValue(IfBothProperty);
            set => SetValue(IfBothProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IfBoth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IfBothProperty =
            DependencyProperty.Register("IfBoth", typeof(bool), typeof(ObjectToBooleanConverter), new PropertyMetadata(false));

        private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var s = (ObjectToBooleanConverter)d;
            var tType = s.ValueForTrue?.GetType();
            var fType = s.ValueForFalse?.GetType();
            if(tType == null && fType == null)
            {
                s.valueType = typeof(object);
                return;
            }
            if(tType == null)
            {
                s.valueType = fType;
                return;
            }
            if(fType == null || tType == fType)
            {
                s.valueType = tType;
                return;
            }
            // UNDONE: tType和fType的共同基类
            s.valueType = typeof(object);
        }

        private Type valueType = typeof(object);

        /// <inheritdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            value = ChangeType(value, this.valueType);
            var isTrue = Equals(value, this.ValueForTrue);
            var isFalse = Equals(value, this.ValueForFalse);
            if(isTrue && isFalse)
                return this.IfBoth;
            if(isTrue)
                return true;
            if(isFalse)
                return false;
            return this.IfNeither;
        }

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            var v = ChangeType<bool>(value);
            if(v)
                return this.ValueForTrue;
            else
                return this.ValueForFalse;
        }
    }
}
