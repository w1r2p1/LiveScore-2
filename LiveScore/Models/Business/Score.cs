using LiveScore.Utils;

namespace LiveScore.Models.Business
{
    public enum MatchWinner
    {
        Draw = 0,
        Home = 1,
        Away = 2,
        HomeOT = 3,
        AwayOT = 4,
        HomePenalty = 5,
        AwayPenalty = 6
    }

    public class Score
    {
        public int Id { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public int HomeTeamOTGoals { get; set; }
        public int AwayTeamOTGoals { get; set; }
        public int HomeTeamPenaltyGoals { get; set; }
        public int AwayTeamPenaltyGoals { get; set; }

        public MatchWinner Winner
        {
            get
            {
                var homeTeamTotalGoals = HomeTeamGoals;
                var awayTeamTotalGoals = AwayTeamGoals;

                if (HomeTeamPenaltyGoals + AwayTeamPenaltyGoals > 0)
                {
                    homeTeamTotalGoals += HomeTeamOTGoals + HomeTeamPenaltyGoals;
                    awayTeamTotalGoals += AwayTeamOTGoals + AwayTeamPenaltyGoals;

                    return Compare(homeTeamTotalGoals, awayTeamTotalGoals, MatchWinner.HomePenalty, MatchWinner.AwayPenalty);
                }

                if (HomeTeamOTGoals + AwayTeamOTGoals > 0)
                {
                    homeTeamTotalGoals += HomeTeamOTGoals;
                    awayTeamTotalGoals += AwayTeamOTGoals;

                    return Compare(homeTeamTotalGoals, awayTeamTotalGoals, MatchWinner.HomeOT, MatchWinner.AwayOT);
                }

                return Compare(homeTeamTotalGoals, awayTeamTotalGoals, MatchWinner.Home, MatchWinner.Away);
            }
        }

        private static MatchWinner Compare(int scoreA, int scoreB, MatchWinner less, MatchWinner greater)
        {
            if (scoreA == scoreB)
            {
                return MatchWinner.Draw;
            }

            return scoreA < scoreB ? less : greater;
        }
    }
}
