using System.Reflection;

namespace LiveScore.Utils.Config
{
    /// <summary>
    /// Configuration model that encapsulates plugin-related options.
    /// </summary>
    public class Options
    {
        /// <summary>Path to the plugin directory</summary>
        public string PluginPath { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Options()
        {
            PluginPath = Assembly.GetEntryAssembly().Location;
        }
    }
}
