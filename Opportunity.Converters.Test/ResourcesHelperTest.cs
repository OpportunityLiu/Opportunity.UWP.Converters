using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opportunity.Converters.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.Test
{
    [TestClass]
    public class ResourcesHelperTest
    {
        [TestMethod]
        public void LoadDefault()
        {
            var r = ResourcesHelper.GetForCurrentView();
            var r2 = ResourcesHelper.GetForCurrentView();
            Assert.AreEqual(r2, r);
        }

        [TestMethod]
        public async Task LoadForView()
        {
            var r0 = ResourcesHelper.GetForCurrentView();
            await TestHelper.RunAtUIThread(() =>
            {
                var r2 = ResourcesHelper.GetForCurrentView();
                var r = ResourcesHelper.GetForCurrentView();
                Assert.AreEqual(r2, r);
                Assert.AreNotEqual(r0, r);
            });
        }
    }
}
