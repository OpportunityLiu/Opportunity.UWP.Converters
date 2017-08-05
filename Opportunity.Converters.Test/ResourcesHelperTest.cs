using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
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
        private ResourceHelper def;

        [TestInitialize]
        public void Init()
        {
            def = ResourceHelper.GetForViewIndependent();
        }

        [TestMethod]
        public void LoadDefault()
        {
            var r = ResourceHelper.GetForCurrentView();
            var r2 = ResourceHelper.GetForCurrentView();
            Assert.AreEqual(r2, r);
            Assert.AreEqual(r, def);
        }

        [UITestMethod]
        public void LoadForView()
        {
            var r = ResourceHelper.GetForCurrentView();
            var r2 = ResourceHelper.GetForCurrentView();
            Assert.AreEqual(r2, r);
            Assert.AreNotEqual(def, r);
        }
    }
}
