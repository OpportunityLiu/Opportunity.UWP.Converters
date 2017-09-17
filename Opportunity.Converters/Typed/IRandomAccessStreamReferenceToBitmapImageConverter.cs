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
    /// to a <see cref="BitmapImage"/>.
    /// </summary>
    public sealed class IRandomAccessStreamReferenceToBitmapImageConverter : ValueConverter<IRandomAccessStreamReference, BitmapImage>
    {
        private static async Task initImage(BitmapImage img, IRandomAccessStreamReference source)
        {
            using (var stream = await source.OpenReadAsync())
            {
                await img.SetSourceAsync(stream);
            }
        }

        /// <inheritdoc />
        public override BitmapImage Convert(IRandomAccessStreamReference value, object parameter, string language)
        {
            if (value == null)
                return null;
            var img = new BitmapImage();
            var task = initImage(img, value);
            // make sure exception in the task will be thrown.
            task.GetAwaiter().OnCompleted(task.Wait);
            return img;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public override IRandomAccessStreamReference ConvertBack(BitmapImage value, object parameter, string language)
            => throw new NotImplementedException();
    }
}
