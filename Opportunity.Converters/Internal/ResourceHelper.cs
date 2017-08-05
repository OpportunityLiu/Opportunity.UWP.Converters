using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Opportunity.Converters.Internal
{
#if DEBUG
    public
#endif
        sealed class ResourceHelper
    {
        private ResourceHelper(ResourceLoader loader)
        {
            this.loader = loader;
        }

        private readonly ResourceLoader loader;
        private readonly Dictionary<string, string> cache = new Dictionary<string, string>();

        public string GetString(string key)
        {
            key = key ?? "";
            if (this.cache.TryGetValue(key, out var val))
                return val;
            return this.cache[key] = getStringWithoutCache(key);
        }

        private string getStringWithoutCache(string key)
        {
            if (key.StartsWith("ms-resource:"))
            {
                if (this.loader == null)
                    return key;
                var value = this.loader.GetString(key);
                if (string.IsNullOrEmpty(value))
                    return key;
                else
                    return value;
            }
            else
            {
                return key;
            }
        }

        private static readonly Dictionary<int, ResourceHelper> dic = new Dictionary<int, ResourceHelper>();
        private static readonly ResourceHelper viewIndependent = new ResourceHelper(tryGetForViewIndependentUse());

        private static ResourceLoader tryGetForViewIndependentUse()
        {
            try
            {
                return ResourceLoader.GetForViewIndependentUse();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static ResourceLoader tryGetForCurrentView()
        {
            try
            {
                return ResourceLoader.GetForCurrentView();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ResourceHelper GetForCurrentView()
        {
            var currentWindew = Window.Current;
            if (currentWindew == null)
                return viewIndependent;
            var id = ApplicationView.GetApplicationViewIdForWindow(currentWindew.CoreWindow);
            if (dic.TryGetValue(id, out var helper))
                return helper;
            return dic[id] = new ResourceHelper(tryGetForCurrentView());
        }

        public static ResourceHelper GetForViewIndependent() => viewIndependent;
    }
}
