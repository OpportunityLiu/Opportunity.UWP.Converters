using Opportunity.Converters.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    /// <summary>
    /// Test a value is <c>null</c> or not, returns a <see cref="bool"/> as result.
    /// </summary>
    public class NullTestConverter : ChainConverter
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
        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            var v = ConvertHelper.ChangeType<bool>(value);
            if (v == IfNull)
                return null;
            return new object();
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value, object parameter, string language)
        {
            if (value == null)
                return IfNull;
            return !IfNull;
        }
    }
}
