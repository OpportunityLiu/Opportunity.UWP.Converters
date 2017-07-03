using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert from <see cref="ApplicationTheme"/> to <see cref="ElementTheme"/>.
    /// </summary>
    public sealed class ApplicationThemeToElementThemeConverter : ChainConverter<ApplicationTheme, ElementTheme>
    {
        /// <inhertdoc/>
        protected override ElementTheme ConvertImpl(ApplicationTheme value, object parameter, string language)
        {
            switch (value)
            {
            case ApplicationTheme.Light:
                return ElementTheme.Light;
            case ApplicationTheme.Dark:
                return ElementTheme.Dark;
            default:
                return ElementTheme.Default;
            }
        }

        /// <inhertdoc/>
        protected override ApplicationTheme ConvertBackImpl(ElementTheme value, object parameter, string language)
        {
            switch (value)
            {
            case ElementTheme.Light:
                return ApplicationTheme.Light;
            case ElementTheme.Dark:
                return ApplicationTheme.Dark;
            default:
                return Application.Current.RequestedTheme;
            }
        }
    }
}
