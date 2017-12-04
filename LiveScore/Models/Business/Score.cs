using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveScore.Models.Business
{
    public enum MatchWinner
    {
        Draw = 0,
        Home = 1,
        Away = 2
    }

    public sealed class Score
    {
        [Key]
        public int Id { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }

        [NotMapped]
        public MatchWinner Winner
        {
            get
            {
                if (HomeTeamGoals == HomeTeamGoals)
                {
                    return MatchWinner.Draw;
                }

                return HomeTeamGoals > HomeTeamGoals ? MatchWinner.Home : MatchWinner.Away;
            }
        }
    }
}
