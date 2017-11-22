using LiveScore.Models.Business;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreDbModule.DAL
{
    public class ScoresDbContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<MatchDay> MatchDays { get; set; }
    }
}
