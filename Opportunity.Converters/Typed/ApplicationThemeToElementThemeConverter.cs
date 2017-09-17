using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Convert from <see cref="ApplicationTheme"/> to <see cref="ElementTheme"/>.
    /// </summary>
    public sealed class ApplicationThemeToElementThemeConverter : ValueConverter<ApplicationTheme, ElementTheme>
    {
        /// <inhertdoc/>
        public override ElementTheme Convert(ApplicationTheme value, object parameter, string language) => value.ToElementTheme();

        /// <inhertdoc/>
        public override ApplicationTheme ConvertBack(ElementTheme value, object parameter, string language) => value.ToApplicationTheme();
    }
}
