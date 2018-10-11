using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using TC = Opportunity.UWP.Converters.XBind.Thickness;

namespace Opportunity.UWP.Converters.Test
{
    [TestClass]
    public class ThicknessConverterTest
    {
        [TestMethod]
        public void Convert()
        {
            Assert.AreEqual(new Thickness(10, 30, 30, 50), TC.Convert(new Thickness(10, 20, 30, 40), " 0,  10 "));
            Assert.AreEqual(new Thickness(0, 15, 40, 80), TC.Convert(new Thickness(10, 20, 30, 40), "-10,x0.5+5,10,*2"));
        }

        [TestMethod]
        public void ConvertBack()
        {
            Assert.AreEqual(new Thickness(10, 20, 30, 40), TC.ConvertBack(new Thickness(10, 30, 30, 50), " 0,  10 "));
            Assert.AreEqual(new Thickness(10, 20, 30, 40), TC.ConvertBack(new Thickness(0, 15, 40, 80), "-10,x0.5+5,10,*2"));
        }

    }
}
