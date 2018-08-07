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
using Opportunity.UWP.Converters.XBind;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Convert an <see cref="IRandomAccessStreamReference"/> (such as a <see cref="StorageFile"/>)
    /// to a <see cref="BitmapImage"/>.
    /// </summary>
    public sealed class IRandomAccessStreamReferenceToBitmapImageConverter : ValueConverter<IRandomAccessStreamReference, BitmapImage>
    {
        /// <inheritdoc />
        public override BitmapImage Convert(IRandomAccessStreamReference value, object parameter, string language)
            => Image.OfIRandomAccessStreamReference(value);

        /// <summary>
        /// Not implemented.
        /// </summary>
        public override IRandomAccessStreamReference ConvertBack(BitmapImage value, object parameter, string language)
            => throw new NotImplementedException("Not implemented by design.");
    }
}
