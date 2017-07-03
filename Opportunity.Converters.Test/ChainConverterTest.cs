using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opportunity.Converters.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters.Test
{
    [TestClass]
    public class ChainConverterTest
    {
        [TestMethod]
        public async Task SimpleChain()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                var c0 = new Int32ToBooleanConverter();
                c0.ValuesForTrue.Add(1);
                c0.ValuesForFalse.Add(-1);
                c0.IfBoth = true;
                c0.IfNeither = false;
                var c1 = new BooleanToVisibilityConverter();
                c0.NextConverter = c1;
                Assert.AreEqual(Visibility.Visible, c0.Convert(1, typeof(Visibility), null, null));
                Assert.AreEqual(Visibility.Collapsed, c0.Convert(0, typeof(Visibility), null, null));
                Assert.AreEqual(Visibility.Collapsed, c0.Convert(-1, typeof(Visibility), null, null));
            });
        }

        [TestMethod]
        public async Task NullChain()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                var c0 = new Int32ToBooleanConverter();
                c0.ValuesForTrue.Add(1);
                c0.ValuesForFalse.Add(-1);
                c0.IfBoth = true;
                c0.IfNeither = false;
                var c1 = new BooleanToVisibilityConverter();
                c0.NextConverter = c1;
                Assert.AreEqual(Visibility.Collapsed, c0.Convert(null, typeof(Visibility), null, null));
                c0.ValuesForTrue.Add(0);
                Assert.AreEqual(Visibility.Visible, c0.Convert(null, typeof(Visibility), null, null));
            });
        }
    }
}
