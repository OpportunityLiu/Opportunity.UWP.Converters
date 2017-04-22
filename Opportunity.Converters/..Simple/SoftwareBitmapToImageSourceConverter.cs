using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert a <see cref="SoftwareBitmap"/> to an <see cref="ImageSource"/>.
    /// </summary>
    public sealed class SoftwareBitmapToImageSourceConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var sb = value as SoftwareBitmap;
            if(sb == null)
                return null;
            var image = new SoftwareBitmapSource();
            var ignore = image.SetBitmapAsync(sb);
            return image;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
