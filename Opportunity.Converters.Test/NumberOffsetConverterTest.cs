
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

namespace Opportunity.Converters.Test
{
    [TestClass]
    public class NumberOffsetConverterTest
    {
        [TestMethod]
        public async Task Int32()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                var converter = new Int32OffsetConverter();
                var r = new Random();
                for (int i = 0; i < 100; i++)
                {
                    var d = r.Next();
                    var o = r.Next();
                    converter.Offset = o;
                    Assert.AreEqual(d + o, converter.Convert(d, typeof(int), null, null));
                    Assert.AreEqual(d, converter.ConvertBack(d + o, typeof(int), null, null));
                }
            });
        }
        [TestMethod]
        public async Task UInt32()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                var converter = new UInt32OffsetConverter();
                var r = new Random();
                for (int i = 0; i < 100; i++)
                {
                    var d = (uint)r.Next();
                    var o = (uint)r.Next();
                    converter.Offset = o;
                    Assert.AreEqual(d + o, converter.Convert(d, typeof(uint), null, null));
                    Assert.AreEqual(d, converter.ConvertBack(d + o, typeof(uint), null, null));
                }
            });
        }
        [TestMethod]
        public async Task Int64()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                var converter = new Int64OffsetConverter();
                var r = new Random();
                for (int i = 0; i < 100; i++)
                {
                    var d = (long)r.Next() * r.Next();
                    var o = (long)r.Next() * r.Next();
                    converter.Offset = o;
                    Assert.AreEqual(d + o, converter.Convert(d, typeof(long), null, null));
                    Assert.AreEqual(d, converter.ConvertBack(d + o, typeof(long), null, null));
                }
            });
        }
        [TestMethod]
        public async Task UInt64()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                var converter = new UInt64OffsetConverter();
                var r = new Random();
                for (int i = 0; i < 100; i++)
                {
                    var d = (ulong)r.Next() * (ulong)r.Next();
                    var o = (ulong)r.Next() * (ulong)r.Next();
                    converter.Offset = o;
                    Assert.AreEqual(d + o, converter.Convert(d, typeof(ulong), null, null));
                    Assert.AreEqual(d, converter.ConvertBack(d + o, typeof(ulong), null, null));
                }
            });
        }
        [TestMethod]
        public async Task Double()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                var converter = new DoubleOffsetConverter();
                var r = new Random();
                for (int i = 0; i < 100; i++)
                {
                    var d = (r.NextDouble() - 0.5) * r.Next();
                    var o = (r.NextDouble() - 0.5) * r.Next();
                    converter.Offset = o;
                    Assert.AreEqual(d + o, (double)converter.Convert(d, typeof(double), null, null), 0.00001);
                    Assert.AreEqual(d, (double)converter.ConvertBack(d + o, typeof(double), null, null), 0.00001);
                }
            });
        }
    }
}
