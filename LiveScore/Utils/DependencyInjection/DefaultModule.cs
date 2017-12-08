using Autofac;
using LiveScore.Contracts;
using LiveScore.Filters;
using LiveScore.Models.Business;
using LiveScore.Services;

namespace LiveScore.Utils.DependencyInjection
{
    /// <summary>
    /// Default module that loads core application services services.
    /// </summary>
    public class DefaultModule : Module
    {
        /// <summary>
        /// This method uses container builder to register core application services.
        /// </summary>
        /// <param name="builder">Autofac container builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GroupService>().As<IGroupService>();
            builder.RegisterType<GamesFilterFactory>().As<IFilterFactory<Game>>();
            builder.RegisterType<ScoresService>().As<IScoresService>();
        }
    }
}
