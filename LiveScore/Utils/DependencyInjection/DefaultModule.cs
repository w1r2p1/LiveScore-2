using Autofac;
using LiveScore.Contracts;
using LiveScore.Services;

namespace LiveScore.Utils.DependencyInjection
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GroupService>().As<IGroupService>();
            builder.RegisterType<ScoresService>().As<IScoresService>();
        }
    }
}
