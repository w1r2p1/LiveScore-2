using System.Reflection;

namespace LiveScore.Utils.Config
{
    public class Options
    {
        public string PluginPath { get; set; }

        public Options()
        {
            PluginPath = Assembly.GetEntryAssembly().Location;
        }
    }
}
