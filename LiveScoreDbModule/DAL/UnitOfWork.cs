using LiveScore.Contracts;
using LiveScore.Models.Business;
using System;

namespace LiveScoreDbModule.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private ScoresDbContext context;

        public IRepository<Score> Scores { get; }
        public IRepository<Game> Games { get; }
        public IRepository<Group> Groups { get; }
        public IRepository<Team> Teams { get; }
        public IRepository<League> Leagues { get; }
        public IRepository<MatchDay> MatchDays { get; }

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
