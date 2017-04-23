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
        private sealed class Converter : IDisposable
        {
            private CoreDispatcher dispatcher;
            public BitmapImage ImageSource = new BitmapImage();
            private IRandomAccessStreamReference streamRef;
            private IRandomAccessStream stream;

            public Converter(CoreDispatcher dispatcher, IRandomAccessStreamReference streamRef)
            {
                this.dispatcher = dispatcher;
                this.streamRef = streamRef;
                this.streamRef.OpenReadAsync().Completed = streamOpened;
            }

            private void streamOpened(IAsyncOperation<IRandomAccessStreamWithContentType> asyncInfo, AsyncStatus asyncStatus)
            {
                if (asyncStatus != AsyncStatus.Completed)
                {
                    Dispose();
                    throwException(asyncInfo.ErrorCode);
                    return;
                }
                this.stream = asyncInfo.GetResults();
                var ignore = this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, setStream);
            }

            private void setStream()
            {
                this.ImageSource.SetSourceAsync(stream).Completed = streamSet;
            }

            private void streamSet(IAsyncAction asyncInfo, AsyncStatus asyncStatus)
            {
                Dispose();
                if (asyncStatus != AsyncStatus.Completed)
                {
                    throwException(asyncInfo.ErrorCode);
                }
            }

            private void throwException(Exception ex)
            {
                if (ex == null)
                    return;
                var ignore = this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => throw ex);
            }
            
            public void Dispose()
            {
                this.ImageSource = null;
                this.streamRef = null;
                this.stream?.Dispose();
                this.stream = null;
            }
        }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is IRandomAccessStreamReference f))
                return null;
            return new Converter(Window.Current.Dispatcher, f).ImageSource;
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
