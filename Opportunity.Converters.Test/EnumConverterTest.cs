using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
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
    public class EnumConverterTest
    {
        [UITestMethod]
        public void Baice()
        {
            var c = new EnumToBooleanConverter();
            c.Values.Add(Visibility.Visible);
            Assert.IsTrue(c.Convert(Visibility.Visible, null, null));
            Assert.IsFalse(c.Convert(Visibility.Collapsed, null, null));
            Assert.IsFalse(c.Convert(FocusState.Unfocused, null, null));
            Assert.IsFalse(c.Convert((Visibility)100, null, null));
        }

        [UITestMethod]
        public void EnumType()
        {
            var c = new EnumToBooleanConverter();
            Assert.AreEqual(null, c.Values.EnumType);

            c.Values.Add(Visibility.Visible);
            Assert.ThrowsException<ArgumentNullException>(() => c.Values.Add(null));
            Assert.ThrowsException<ArgumentException>(() => c.Values.Add(FocusState.Unfocused));
            Assert.AreEqual(typeof(Visibility), c.Values.EnumType);
            c.Values.Add(Visibility.Collapsed);

            Assert.IsTrue(c.Values.Remove(Visibility.Visible));
            c.Values.Clear();
            Assert.AreEqual(null, c.Values.EnumType);

            c.Values.Add(FocusState.Unfocused);
            Assert.AreEqual(typeof(FocusState), c.Values.EnumType);
            c.Values.Add(FocusState.Keyboard);
            Assert.ThrowsException<ArgumentException>(() => c.Values.Add(Visibility.Collapsed));
        }

    }
}
