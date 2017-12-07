using Autofac;
using LiveScore.Contracts;
using LiveScore.Filters;
using LiveScore.Models.Business;
using LiveScore.Services;

namespace LiveScore.Utils.DependencyInjection
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GroupService>().As<IGroupService>();
            builder.RegisterType<GamesFilterFactory>().As<IFilterFactory<Game>>();
            builder.RegisterType<ScoresService>().As<IScoresService>();
            
        }
    }
}
