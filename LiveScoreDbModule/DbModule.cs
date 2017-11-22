using Autofac;
using LiveScore.Contracts;
using LiveScoreDbModule.DAL;

namespace LiveScoreDbModule
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
