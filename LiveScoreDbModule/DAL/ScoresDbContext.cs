using LiveScore.Models.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LiveScoreDbModule.DAL
{
    public class ScoresDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<Score> Scores { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<MatchDay> MatchDays { get; set; }

        public ScoresDbContext() : base()
        {
            var assemblyLocation = typeof(ScoresDbContext).Assembly.Location;
            var pluginDir = Path.GetDirectoryName(assemblyLocation);

            var builder = new ConfigurationBuilder()
                .SetBasePath(pluginDir)
                .AddJsonFile("dbconfig.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.AwayTeam)
                .WithMany(t => t.AwayGames);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Score)
                .WithOne(s => s.Game)
                .HasForeignKey<Score>(s => s.GameRef);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.League)
                .WithMany(l => l.Groups);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Teams)
                .WithOne(t => t.Group);
        }
    }
}
