using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Opportunity.Converters.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Opportunity.Converters.Typed;

namespace Opportunity.Converters.Test
{
    [TestClass]
    public class ChainConverterTest
    {
        [UITestMethod]
        public void SimpleChain()
        {
            var c0 = new Int32ToBooleanConverter();
            c0.ValuesForTrue.Add(1);
            c0.ValuesForFalse.Add(-1);
            c0.IfBoth = true;
            c0.IfNeither = false;

            var c1 = new BooleanToVisibilityConverter();

            var c = new ChainConverter(c0, c1);

            Assert.AreEqual(Visibility.Visible, c.Convert(1, typeof(Visibility), null, null));
            Assert.AreEqual(Visibility.Collapsed, c.Convert(0, typeof(Visibility), null, null));
            Assert.AreEqual(Visibility.Collapsed, c.Convert(-1, typeof(Visibility), null, null));
            Assert.AreEqual(1, c.ConvertBack(Visibility.Visible, typeof(int), null, null));
            Assert.AreEqual(-1, c.ConvertBack(Visibility.Collapsed, typeof(int), null, null));
        }

        [UITestMethod]
        public void NullChain()
        {
            var c0 = new Int32ToBooleanConverter();
            c0.ValuesForTrue.Add(1);
            c0.ValuesForFalse.Add(-1);
            c0.IfBoth = true;
            c0.IfNeither = false;

            var c1 = new BooleanToVisibilityConverter();

            var c = new ChainConverter(c0, c1);

            // For null input, any result is accepted, without exception thrown.
            Assert.IsNotNull(c.Convert(null, typeof(Visibility), null, null));
            Assert.IsNotNull(c.ConvertBack(null, typeof(int), null, null));
            c0.ValuesForTrue.Add(0);
            Assert.IsNotNull(c.Convert(null, typeof(Visibility), null, null));
            Assert.IsNotNull(c.ConvertBack(null, typeof(int), null, null));
        }

        [UITestMethod]
        public void WrongChain()
        {
            var c0 = new Int32ToBooleanConverter();
            c0.ValuesForTrue.Add(1);
            c0.ValuesForFalse.Add(-1);
            c0.IfBoth = true;
            c0.IfNeither = false;

            var c1 = new Int32ToBooleanConverter();
            c1.ValuesForTrue.Add(1);
            c1.ValuesForFalse.Add(0);
            c1.IfBoth = false;
            c1.IfNeither = true;

            var c = new ChainConverter(c0, c1);

            Assert.AreEqual(false, c.Convert(null, typeof(bool), null, null));
            Assert.AreEqual(-1, c.ConvertBack(null, typeof(int), null, null));
        }
    }
}
