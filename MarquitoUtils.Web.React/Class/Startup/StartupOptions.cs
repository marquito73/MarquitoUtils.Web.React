using MarquitoUtils.Main.Class.Entities.Translation;
using MarquitoUtils.Web.React.Class.Entities.File;

namespace MarquitoUtils.Web.React.Class.Startup
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
        /// Translations
        /// </summary>
        internal List<Translation> Translations { get; set; }
    }
}
