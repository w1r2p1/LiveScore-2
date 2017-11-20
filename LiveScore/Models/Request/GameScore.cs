namespace LiveScore.Models.Request
{
    public class GameScore
    {
        public string LeagueTitle { get; set; }
        public int MatchDay { get; set; }
        public string Group { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string KickOffAt { get; set; }
        public string Score { get; set; }
    }
}
