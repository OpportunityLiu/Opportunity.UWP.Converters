using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.ViewManagement;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert an <see cref="IRandomAccessStreamReference"/> (such as a <see cref="StorageFile"/>)
    /// to an <see cref="ImageSource"/>.
    /// </summary>
    public class IRandomAccessStreamReferenceToImageSourceConverter : IValueConverter
    {
        private static async Task initImage(BitmapImage img, IRandomAccessStreamReference source)
        {
            using (var stream = await source.OpenReadAsync())
            {
                await img.SetSourceAsync(stream);
            }
        }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is IRandomAccessStreamReference f))
                return null;
            var img = new BitmapImage();
            var task = initImage(img, f);
            task.GetAwaiter().OnCompleted(task.Wait);
            return img;
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
