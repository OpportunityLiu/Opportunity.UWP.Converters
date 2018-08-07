
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Opportunity.UWP.Converters.Typed;

namespace Opportunity.UWP.Converters.Test
{
    [TestClass]
    public class MathExpressionConverterTest
    {
        [UITestMethod]
        public void Square()
        {
            var converter = new MathExpressionConverter()
            {
                ConvertExpression = "x^2",
                ConvertBackExpression = "Sqrt(x)"
            };
            var r = new Random();
            for (int i = 0; i < 100; i++)
            {
                var d = r.NextDouble() * 100;
                Assert.AreEqual(d * d, (double)converter.Convert(d, typeof(double), null, null), 0.00001);
                Assert.AreEqual(d, (double)converter.ConvertBack(d * d, typeof(double), null, null), 0.00001);
            }
        }

        [UITestMethod]
        public void Sin()
        {
            var converter = new MathExpressionConverter()
            {
                ConvertExpression = "Sin(x)",
                ConvertBackExpression = "Asin(x)"
            };
            var r = new Random();
            for (int i = 0; i < 100; i++)
            {
                var d = r.NextDouble();
                Assert.AreEqual(Math.Sin(d), (double)converter.Convert(d, typeof(double), null, null), 0.00001);
                Assert.AreEqual(d, (double)converter.ConvertBack(Math.Sin(d), typeof(double), null, null), 0.00001);
            }
        }
    }
}
