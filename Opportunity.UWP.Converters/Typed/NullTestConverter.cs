using Opportunity.UWP.Converters.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Test a value is <c>null</c> or not, returns a <see cref="bool"/> as result.
    /// </summary>
    public class NullTestConverter : ValueConverter<object, bool>
    {
        /// <summary>
        /// Return <see cref="bool"/> when the value is <c>null</c>.
        /// </summary>
        public bool IfNull
        {
            get => (bool)GetValue(IfNullProperty); set => SetValue(IfNullProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IfNull"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IfNullProperty =
            DependencyProperty.Register("IfNull", typeof(bool), typeof(NullTestConverter), new PropertyMetadata(true));

        /// <inheritdoc />
        public override bool Convert(object value, object parameter, string language)
        {
            if (value is null)
                return IfNull;
            return !IfNull;
        }

        /// <inheritdoc />
        public override object ConvertBack(bool value, object parameter, string language)
        {
            if (value == IfNull)
                return null;
            return new object();
        }
    }
}
