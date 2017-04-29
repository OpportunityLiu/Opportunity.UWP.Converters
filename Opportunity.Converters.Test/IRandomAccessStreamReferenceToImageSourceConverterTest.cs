
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Opportunity.Converters.Test
{
    [TestClass]
    public class IRandomAccessStreamReferenceToImageSourceConverterTest
    {
        [TestMethod]
        public async Task Image()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                var converter = new IRandomAccessStreamReferenceToImageSourceConverter();
                var uri = new Uri("ms-appx:///Assets/StoreLogo.png");
                var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                var r = (BitmapImage)converter.Convert(file, typeof(ImageSource), null, null);
            });
        }

        [TestMethod]
        public async Task WrongFile()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                var converter = new IRandomAccessStreamReferenceToImageSourceConverter();
                var uri = new Uri("ms-appx:///Assets/XMLFile1.xml");
                var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                var r = (BitmapImage)converter.Convert(file, typeof(ImageSource), null, null);
                await Task.Delay(1000);
            });
        }
    }
}
