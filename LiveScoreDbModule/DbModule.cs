using Autofac;
using LiveScore.Contracts;
using LiveScoreDbModule.DAL;

namespace LiveScoreDbModule
{
    /// <summary>
    /// Database module that uses SQL Server as data store and Entity Framework as DAL.
    /// </summary>
    public class DbModule : Module
    {
        /// <summary>
        /// This method uses container builder to register DAL services.
        /// </summary>
        /// <param name="builder">Autofac container builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
