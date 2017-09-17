using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Imaging;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Convert a <see cref="SoftwareBitmap"/> to an <see cref="ImageSource"/>.
    /// </summary>
    public sealed class SoftwareBitmapToImageSourceConverter : ValueConverter<SoftwareBitmap, ImageSource>
    {
        /// <inheritdoc />
        public override ImageSource Convert(SoftwareBitmap value, object parameter, string language)
        {
            if (value == null)
                return null;
            var image = new SoftwareBitmapSource();
            var ignore = image.SetBitmapAsync(value);
            return image;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public override SoftwareBitmap ConvertBack(ImageSource value, object parameter, string language) => throw new NotImplementedException("Not implemented bt design.");
    }
}
