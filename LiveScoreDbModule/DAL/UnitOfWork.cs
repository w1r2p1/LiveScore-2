using LiveScore.Contracts;
using LiveScore.Models.Business;
using System;

namespace LiveScoreDbModule.DAL
{
    /// <summary>
    /// Unit of work class, used as a single entry point for DAL.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private ScoresDbContext context;

        /// <summary>DB repository used to query and save instances of Score business model</summary>
        public IRepository<Score> Scores { get; }

        /// <summary>DB repository used to query and save instances of Game business model</summary>
        public IRepository<Game> Games { get; }

        /// <summary>DB repository used to query and save instances of Group business model</summary>
        public IRepository<Group> Groups { get; }

        /// <summary>DB repository used to query and save instances of Team business model</summary>
        public IRepository<Team> Teams { get; }

        /// <summary>DB repository used to query and save instances of League business model</summary>
        public IRepository<League> Leagues { get; }

        /// <summary>DB repository used to query and save instances of MatchDay business model</summary>
        public IRepository<MatchDay> MatchDays { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UnitOfWork()
        {
            disposedValue = false;
            context = new ScoresDbContext();

            Scores = new DbSetRepository<Score>(context);
            Games = new DbSetRepository<Game>(context);
            Groups = new DbSetRepository<Group>(context);
            Teams = new DbSetRepository<Team>(context);
            Leagues = new DbSetRepository<League>(context);
            MatchDays = new DbSetRepository<MatchDay>(context);
        }

        /// <summary>
        /// This method commits changes to underlying repository mechanism.
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
