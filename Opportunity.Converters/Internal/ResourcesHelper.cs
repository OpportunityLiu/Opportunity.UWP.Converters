using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Opportunity.Converters.Internal
{
    static class ResourcesHelper
    {
        private static ResourceLoader loader = ResourceLoader.GetForViewIndependentUse();

        public static string GetString(string key)
        {
            key = key ?? "";
            if(key.StartsWith("ms-resource:"))
            {
                var value = loader.GetString(key);
                if(string.IsNullOrEmpty(value))
                    return key;
                else
                    return value;
            }
            else
            {
                return key;
            }
        }
    }
}
