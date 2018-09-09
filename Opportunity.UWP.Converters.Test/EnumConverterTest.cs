using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Opportunity.UWP.Converters.Typed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.UWP.Converters.Test
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

        [UITestMethod]
        public void EnumToString()
        {
            var c = new EnumToStringConverter();

            Assert.AreEqual(Visibility.Collapsed.ToString(), c.Convert(Visibility.Collapsed, null, null));
            Assert.AreEqual(StringComparison.CurrentCulture.ToString(), c.Convert(StringComparison.CurrentCulture, null, null));

            string fom(StringComparison vvv) => $"({vvv})";

            c.NameProvider = v => fom((StringComparison)v);

            Assert.AreEqual(fom(StringComparison.CurrentCulture), c.Convert(StringComparison.CurrentCulture, null, null));
            Assert.AreEqual((StringComparison.CurrentCulture | StringComparison.InvariantCulture).ToFriendlyNameString(fom), c.Convert(StringComparison.CurrentCulture | StringComparison.InvariantCulture, null, null));
        }

    }
}
