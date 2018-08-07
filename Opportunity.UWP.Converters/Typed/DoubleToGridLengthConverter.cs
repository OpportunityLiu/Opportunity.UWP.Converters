using System;
using GridLength = Windows.UI.Xaml.GridLength;
using GridUnitType = Windows.UI.Xaml.GridUnitType;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Convert <see cref="double"/> to <see cref="GridLength"/>.
    /// </summary>
    public sealed class DoubleToGridLengthConverter : ValueConverter<double, GridLength>
    {
        /// <summary>
        /// Default value for <see cref="GridUnitType"/> when did not specified.
        /// </summary>
        public GridUnitType DefaultType
        {
            get => (GridUnitType)GetValue(DefaultTypeProperty); set => SetValue(DefaultTypeProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="DefaultType"/> dependency property.
        /// </summary>
        public static readonly Windows.UI.Xaml.DependencyProperty DefaultTypeProperty =
            Windows.UI.Xaml.DependencyProperty.Register("DefaultType", typeof(GridUnitType), typeof(DoubleToGridLengthConverter), new Windows.UI.Xaml.PropertyMetadata(GridUnitType.Pixel));

        /// <summary>
        /// Convert <see cref="double"/> to <see cref="GridLength"/>, which is made by <see cref="GridLength.GridLength(double, GridUnitType)"/>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="parameter"><see cref="GridUnitType"/> for returned <see cref="GridLength"/>, default value is <see cref="DefaultType"/>.</param>
        /// <param name="language">Not used.</param>
        /// <returns>Converted <see cref="GridLength"/>, which is made by <see cref="GridLength.GridLength(double, GridUnitType)"/>.</returns>
        public override GridLength Convert(double value, object parameter, string language)
        {
            if (parameter is null)
                return new GridLength(value, DefaultType);
            if (parameter is GridUnitType type)
                return new GridLength(value, type);
            if (Enum.TryParse(parameter.ToString(), out type))
                return new GridLength(value, type);
            throw new ArgumentException("Invalid GridUnitType", nameof(parameter));
        }

        /// <summary>
        /// Convert <see cref="GridLength"/> to <see cref="double"/>, which is made by <see cref="GridLength.Value"/>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns>Converted <see cref="double"/>, which is made by <see cref="GridLength.Value"/>.</returns>
        public override double ConvertBack(GridLength value, object parameter, string language)
        {
            return !value.IsAuto ? value.Value : double.NaN;
        }
    }
}
