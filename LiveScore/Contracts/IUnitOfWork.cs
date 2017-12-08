using LiveScore.Models.Business;
using System;

namespace LiveScore.Contracts
{
    /// <summary>
    /// Interface that defines unit of work as a single DAL entry point.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>Game scores repository</summary>
        IRepository<Score> Scores { get; }

        /// <summary>Games repository</summary>
        IRepository<Game> Games { get; }

        /// <summary>League groups repository</summary>
        IRepository<Group> Groups { get; }

        /// <summary>League teams repository</summary>
        IRepository<Team> Teams { get; }
        
        /// <summary>League repository</summary>
        IRepository<League> Leagues { get; }

        /// <summary>League match day repository</summary>
        IRepository<MatchDay> MatchDays { get; }

        /// <summary>
        /// This method commits changes to underlying repository mechanism.
        /// </summary>
        void Save();
    }
}
