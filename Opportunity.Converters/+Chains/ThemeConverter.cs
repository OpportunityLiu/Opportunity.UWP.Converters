using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert between <see cref="ElementTheme"/> and <see cref="ApplicationTheme"/>.
    /// </summary>
    public class ThemeConverter : ChainConverter
    {
        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override object ConvertImpl(object value, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private static object convert(object value)
        {
            switch (value)
            {
            case ElementTheme et:
                switch (et)
                {
                case ElementTheme.Light:
                    return ApplicationTheme.Light;
                case ElementTheme.Dark:
                    return ApplicationTheme.Dark;
                default:
                    return Application.Current.RequestedTheme;
                }
            case ApplicationTheme at:
                switch (at)
                {
                case ApplicationTheme.Light:
                    return ElementTheme.Light;
                case ApplicationTheme.Dark:
                    return ElementTheme.Dark;
                default:
                    return ElementTheme.Default;
                }
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
