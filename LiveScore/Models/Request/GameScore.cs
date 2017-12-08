namespace LiveScore.Models.Request
{
    /// <summary>
    /// Request model that encapsulates gamescore-related data.
    /// </summary>
    public class GameScore
    {
        /// <summary>League season full title</summary>
        public string LeagueTitle { get; set; }

        /// <summary>Match day number</summary>
        public int MatchDay { get; set; }

        /// <summary>League group name</summary>
        public string Group { get; set; }

        /// <summary>Home team name</summary>
        public string HomeTeam { get; set; }

        /// <summary>Away team name</summary>
        public string AwayTeam { get; set; }

        /// <summary>Game start date and time in ISO8601 string format</summary>
        public string KickOffAt { get; set; }

        /// <summary>Goal count by both teams</summary>
        public string Score { get; set; }
    }
}
