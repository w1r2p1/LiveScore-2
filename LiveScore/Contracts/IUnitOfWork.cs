using LiveScore.Models.Business;
using System;

namespace LiveScore.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Score> Scores { get; }
        IRepository<Game> Games { get; }
        IRepository<Group> Groups { get; }
        IRepository<Team> Teams { get; }
        IRepository<League> Leagues { get; }
        IRepository<MatchDay> MatchDays { get; }

        void Save();
    }
}
