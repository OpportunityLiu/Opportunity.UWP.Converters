
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

namespace Opportunity.Converters.Test
{
    [TestClass]
    public class NumberScaleConverterTest
    {
        [TestMethod]
        public async Task Int32()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var converter = new Int32ScaleConverter();
                var r = new Random();
                for (int i = 1; i < 100; i++)
                {
                    var d = r.Next() / i;
                    converter.Scale = i;
                    Assert.AreEqual((int)(d * i), converter.Convert(d, null, null, null));
                    Assert.AreEqual(d, converter.ConvertBack((int)(d * i), null, null, null));
                }
            });
        }
        [TestMethod]
        public async Task UInt32()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var converter = new UInt32ScaleConverter();
                var r = new Random();
                for (uint i = 1; i < 100; i++)
                {
                    var d = (uint)r.Next() / i;
                    converter.Scale = i;
                    Assert.AreEqual((uint)(d * i), converter.Convert(d, null, null, null));
                    Assert.AreEqual(d, converter.ConvertBack((uint)(d * i), null, null, null));
                }
            });
        }
        [TestMethod]
        public async Task Int64()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var converter = new Int64ScaleConverter();
                var r = new Random();
                for (int i = 1; i < 100; i++)
                {
                    var d = (long)r.Next();
                    converter.Scale = i;
                    Assert.AreEqual((long)(d * i), converter.Convert(d, null, null, null));
                    Assert.AreEqual(d, converter.ConvertBack((long)(d * i), null, null, null));
                }
            });
        }
        [TestMethod]
        public async Task UInt64()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var converter = new UInt64ScaleConverter();
                var r = new Random();
                for (uint i = 1; i < 100; i++)
                {
                    var d = (ulong)r.Next();
                    converter.Scale = i;
                    Assert.AreEqual((ulong)(d * (ulong)i), converter.Convert(d, null, null, null));
                    Assert.AreEqual(d, converter.ConvertBack((ulong)(d * (ulong)i), null, null, null));
                }
            });
        }
        [TestMethod]
        public async Task Double()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var converter = new DoubleScaleConverter();
                var r = new Random();
                for (int i = 0; i < 100; i++)
                {
                    var d = (r.NextDouble() - 0.5) * r.Next();
                    var o = r.NextDouble();
                    converter.Scale = o;
                    Assert.AreEqual(d * o, (double)converter.Convert(d, null, null, null), 0.00001);
                    Assert.AreEqual(d, (double)converter.ConvertBack(d * o, null, null, null), 0.00001);
                }
            });
        }
    }
}
