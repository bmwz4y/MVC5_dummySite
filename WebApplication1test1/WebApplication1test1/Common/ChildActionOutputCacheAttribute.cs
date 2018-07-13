using System.Web.Configuration;
using System.Web.Mvc;

namespace WebApplication1test1.Common
{
    /*
     * This was added on lesson 74
     */

    public class ChildActionOutputCacheAttribute : OutputCacheAttribute
    {
        public ChildActionOutputCacheAttribute(string cacheProfile)
        {
            var settings = (OutputCacheSettingsSection)WebConfigurationManager.GetSection("system.web/caching/outputCacheSettings");
            var profile = settings.OutputCacheProfiles[cacheProfile];
            Duration = profile.Duration;
            VaryByParam = profile.VaryByParam;
            VaryByCustom = profile.VaryByCustom;
        }
    }
}