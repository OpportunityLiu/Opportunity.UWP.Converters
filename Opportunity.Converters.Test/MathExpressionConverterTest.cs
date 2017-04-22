
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

namespace Opportunity.Converters.Test
{
    [TestClass]
    public class MathExpressionConverterTest
    {
        [TestMethod]
        public async Task Square()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
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
                    Assert.AreEqual(d * d, (double)converter.Convert(d, null, null, null), 0.00001);
                    Assert.AreEqual(d, (double)converter.ConvertBack(d * d, null, null, null), 0.00001);
                }
            });
        }

        [TestMethod]
        public async Task Sin()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
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
                    Assert.AreEqual(Math.Sin(d), (double)converter.Convert(d, null, null, null), 0.00001);
                    Assert.AreEqual(d, (double)converter.ConvertBack(Math.Sin(d), null, null, null), 0.00001);
                }
            });
        }
    }
}
