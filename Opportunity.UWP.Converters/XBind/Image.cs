
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Opportunity.UWP.Converters.XBind
{
    /// <summary>
    /// Methods of iamge conversion.
    /// </summary>
    public static class Image
    {
        /// <summary>
        /// Read image from <see cref="StorageFile"/>.
        /// </summary>
        /// <param name="file"><see cref="StorageFile"/> of image.</param>
        /// <returns>Result of read.</returns>
        public static BitmapImage OfStorageFile(StorageFile file)
            => OfIRandomAccessStreamReference(file);

        /// <summary>
        /// Read image from <see cref="IStorageFile"/>.
        /// </summary>
        /// <param name="file"><see cref="IStorageFile"/> of image.</param>
        /// <returns>Result of read.</returns>
        public static BitmapImage OfIStorageFile(IStorageFile file)
            => OfIRandomAccessStreamReference(file);

        /// <summary>
        /// Read image from <see cref="IRandomAccessStreamReference"/>.
        /// </summary>
        /// <param name="streamReference"><see cref="IRandomAccessStreamReference"/> of image.</param>
        /// <returns>Result of read.</returns>
        public static BitmapImage OfIRandomAccessStreamReference(IRandomAccessStreamReference streamReference)
        {
            if (streamReference is null)
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
            if (softwareBitmap is null)
                return null;
            var image = new SoftwareBitmapSource();
            var task = image.SetBitmapAsync(softwareBitmap);
            // make sure exception in the task will be thrown.
            task.GetAwaiter().OnCompleted(task.GetResults);
            return image;
        }
    }
}
