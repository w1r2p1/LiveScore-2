using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LiveScore
{
    public class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Application input parameters</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// This method builds web host from input parameters.
        /// </summary>
        /// <param name="args">Application input parameters</param>
        /// <returns><see cref="IWebHost"/></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
