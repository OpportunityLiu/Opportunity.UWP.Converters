﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Opportunity.UWP.Converters.Typed;

namespace Opportunity.UWP.Converters.Test
{
    [TestClass]
    public class IRandomAccessStreamReferenceToBitmapImageConverterTest
    {
        [TestInitialize]
        public void Init()
        {
            var uri = new Uri("ms-appx:///Assets/StoreLogo.png");
            this.file1 = StorageFile.GetFileFromApplicationUriAsync(uri).AsTask().Result;
        }

        private StorageFile file1;

        [UITestMethod]
        public void Image()
        {
            var converter = new IRandomAccessStreamReferenceToBitmapImageConverter();
            var r = (BitmapImage)converter.Convert(this.file1, typeof(ImageSource), null, null);
        }
    }
}
