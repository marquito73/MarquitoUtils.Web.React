using MarquitoUtils.Main.Translate.Entities;
using MarquitoUtils.Web.React.Config;
using MarquitoUtils.Web.React.Config.WebFront;

namespace MarquitoUtils.Web.React.Startup
{
    /// <summary>
    /// Options configured at Startup time
    /// </summary>
    public class StartupOptions
    {
        /// <summary>
        /// SyncFusion configuration, allow to use charts
        /// </summary>
        public SyncFusionConfiguration SyncFusionConfiguration { get; internal set; }
        /// <summary>
        /// Web configuration for the website (fonts, sizes, colors, etc ..)
        /// </summary>
        public WebFrontConfiguration WebFrontConfiguration { get; internal set; }
        /// <summary>
        /// Translations
        /// </summary>
        internal List<Translation> Translations { get; set; }
    }
}
