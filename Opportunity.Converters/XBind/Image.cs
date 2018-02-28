
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Methods of iamge conversion.
    /// </summary>
    public static class Image
    {
        /// <summary>
        /// Read image from <see cref="IRandomAccessStreamReference"/>.
        /// </summary>
        /// <param name="streamReference"><see cref="IRandomAccessStreamReference"/> of image.</param>
        /// <returns>Result of read.</returns>
        public static BitmapImage OfRandomAccessStreamReference(IRandomAccessStreamReference streamReference)
        {
            if (streamReference == null)
                return null;
            var img = new BitmapImage();
            var task = initImage(img, streamReference);
            // make sure exception in the task will be thrown.
            task.GetAwaiter().OnCompleted(task.Wait);
            return img;

            async Task initImage(BitmapImage i, IRandomAccessStreamReference s)
            {
                using (var stream = await s.OpenReadAsync())
                {
                    await i.SetSourceAsync(stream);
                }
            }
        }
        /// <summary>
        /// Read image from <see cref="SoftwareBitmap"/>.
        /// </summary>
        /// <param name="softwareBitmap"><see cref="SoftwareBitmap"/> of image.</param>
        /// <returns>Result of read.</returns>
        public static SoftwareBitmapSource OfSoftwareBitmap(SoftwareBitmap softwareBitmap)
        {
            if (softwareBitmap == null)
                return null;
            var image = new SoftwareBitmapSource();
            var ignore = image.SetBitmapAsync(softwareBitmap);
            return image;
        }
    }
}
