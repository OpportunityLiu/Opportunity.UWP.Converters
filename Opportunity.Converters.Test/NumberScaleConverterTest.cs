
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Opportunity.Converters.Typed;

namespace Opportunity.Converters.Test
{
    [TestClass]
    public class NumberScaleConverterTest
    {
        [UITestMethod]
        public void Int32()
        {
            var converter = new Int32ScaleConverter();
            var r = new Random();
            for (var i = 1; i < 100; i++)
            {
                var d = r.Next() / i;
                converter.Scale = i;
                Assert.AreEqual(d * i, (int)converter.Convert(d, typeof(int), null, null));
                Assert.AreEqual(d, (int)converter.ConvertBack(d * i, typeof(int), null, null));
            }
        }

        [UITestMethod]
        public void UInt32()
        {
            var converter = new UInt32ScaleConverter();
            var r = new Random();
            for (var i = 1u; i < 100; i++)
            {
                var d = (uint)r.Next() / i;
                converter.Scale = i;
                Assert.AreEqual(d * i, (uint)converter.Convert(d, typeof(uint), null, null));
                Assert.AreEqual(d, (uint)converter.ConvertBack(d * i, typeof(uint), null, null));
            }
        }

        [UITestMethod]
        public void Int64()
        {
            var converter = new Int64ScaleConverter();
            var r = new Random();
            for (var i = 1; i < 100; i++)
            {
                var d = (long)r.Next();
                converter.Scale = i;
                Assert.AreEqual(d * i, (long)converter.Convert(d, typeof(long), null, null));
                Assert.AreEqual(d, (long)converter.ConvertBack(d * i, typeof(long), null, null));
            }
        }

        [UITestMethod]
        public void UInt64()
        {
            var converter = new UInt64ScaleConverter();
            var r = new Random();
            for (var i = 1ul; i < 100; i++)
            {
                var d = (ulong)r.Next();
                converter.Scale = i;
                Assert.AreEqual(d * i, (ulong)converter.Convert(d, typeof(ulong), null, null));
                Assert.AreEqual(d, (ulong)converter.ConvertBack(d * i, typeof(ulong), null, null));
            }
        }

        [UITestMethod]
        public void Double()
        {
            var converter = new DoubleScaleConverter();
            var r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var d = (r.NextDouble() - 0.5) * r.Next();
                var o = r.NextDouble();
                converter.Scale = o;
                Assert.AreEqual(d * o, (double)converter.Convert(d, typeof(double), null, null), 0.00001);
                Assert.AreEqual(d, (double)converter.ConvertBack(d * o, typeof(double), null, null), 0.00001);
            }
        }
    }
}
