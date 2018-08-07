
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Opportunity.UWP.Converters.Typed;

namespace Opportunity.UWP.Converters.Test
{
    [TestClass]
    public class NumberOffsetConverterTest
    {
        [UITestMethod]
        public void Int32()
        {
            var converter = new Int32OffsetConverter();
            var r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var d = r.Next();
                var o = r.Next();
                converter.Offset = o;
                Assert.AreEqual(d + o, (int)converter.Convert(d, typeof(int), null, null));
                Assert.AreEqual(d, (int)converter.ConvertBack(d + o, typeof(int), null, null));
            }
        }

        [UITestMethod]
        public void UInt32()
        {
            var converter = new UInt32OffsetConverter();
            var r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var d = (uint)r.Next();
                var o = (uint)r.Next();
                converter.Offset = o;
                Assert.AreEqual(d + o, (uint)converter.Convert(d, typeof(uint), null, null));
                Assert.AreEqual(d, (uint)converter.ConvertBack(d + o, typeof(uint), null, null));
            }
        }

        [UITestMethod]
        public void Int64()
        {
            var converter = new Int64OffsetConverter();
            var r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var d = (long)r.Next() * r.Next();
                var o = (long)r.Next() * r.Next();
                converter.Offset = o;
                Assert.AreEqual(d + o, (long)converter.Convert(d, typeof(long), null, null));
                Assert.AreEqual(d, (long)converter.ConvertBack(d + o, typeof(long), null, null));
            }
        }

        [UITestMethod]
        public void UInt64()
        {
            var converter = new UInt64OffsetConverter();
            var r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var d = (ulong)r.Next() * (ulong)r.Next();
                var o = (ulong)r.Next() * (ulong)r.Next();
                converter.Offset = o;
                Assert.AreEqual(d + o, (ulong)converter.Convert(d, typeof(ulong), null, null));
                Assert.AreEqual(d, (ulong)converter.ConvertBack(d + o, typeof(ulong), null, null));
            }
        }

        [UITestMethod]
        public void Double()
        {
            var converter = new DoubleOffsetConverter();
            var r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var d = (r.NextDouble() - 0.5) * r.Next();
                var o = (r.NextDouble() - 0.5) * r.Next();
                converter.Offset = o;
                Assert.AreEqual(d + o, (double)converter.Convert(d, typeof(double), null, null), 0.00001);
                Assert.AreEqual(d, (double)converter.ConvertBack(d + o, typeof(double), null, null), 0.00001);
            }
        }
    }
}
