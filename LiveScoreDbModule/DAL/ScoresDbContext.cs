using LiveScore.Models.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LiveScoreDbModule.DAL
{
    /// <summary>
    /// Database context with properties which provide access to SQL tables.
    /// </summary>
    public class ScoresDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Default constructor.
        /// </summary>
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

        /// <summary>
        /// This method configures usage of SQL Server with provided connection string.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// This method uses <see cref="ModelBuilder"/> fluid API to define foreign keys.
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
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
