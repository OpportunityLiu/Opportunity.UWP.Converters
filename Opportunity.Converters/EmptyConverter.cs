using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Opportunity.Converters
{
    /// <summary>
    /// An empty converter who dose nothing to values.
    /// </summary>
    public sealed class EmptyConverter : IValueConverter
    {
        /// <summary>
        /// A default instance of <see cref="EmptyConverter"/>.
        /// </summary>
        public static EmptyConverter Default { get; } = new EmptyConverter();

        /// <summary>
        /// Do nothing to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns><paramref name="value"/> itself.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        /// <summary>
        /// Do nothing to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns><paramref name="value"/> itself.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
