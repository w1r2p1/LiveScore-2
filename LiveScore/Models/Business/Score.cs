using LiveScore.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveScore.Models.Business
{
    /// <summary>
    /// Match outcome enumeration
    /// </summary>
    public enum MatchWinner
    {
        Draw = 0,
        Home = 1,
        Away = 2
    }

    /// <summary>
    /// Business model that encapsulates score-related data.
    /// </summary>
    public sealed class Score : IEntity
    {
        // <summary>Unique identifier</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Home team goals count</summary>
        public int HomeTeamGoals { get; set; }

        /// <summary>Away team goals count</summary>
        public int AwayTeamGoals { get; set; }

        /// <summary>Reference Id of game business model</summary>
        public int GameRef { get; set; }

        // <summary>Reference to game business model</summary>
        public Game Game { get; set; }

        /// <summary>Winner of the game, calculated based on goal count</summary>
        [NotMapped]
        public MatchWinner Winner
        {
            get
            {
                if (HomeTeamGoals == AwayTeamGoals)
                {
                    return MatchWinner.Draw;
                }

                return HomeTeamGoals > AwayTeamGoals ? MatchWinner.Home : MatchWinner.Away;
            }
        }
    }
}
