namespace LiveScore.Models.Response
{
    /// <summary>
    /// Response model that encapsulates standings-related data on club level.
    /// </summary>
    public class ClubStandings
    {
        /// <summary>Group rank</summary>
        public int Rank { get; set; }

        /// <summary>Group rank</summary>
        public string Team { get; set; }

        /// <summary>Played games count</summary>
        public int PlayedGames { get; set; }

        /// <summary>Point count</summary>
        public int Points { get; set; }

        /// <summary>Goals scored count</summary>
        public int Goals { get; set; }

        /// <summary>Goals scored against count</summary>
        public int GoalsAgainst { get; set; }

        /// <summary>Goal difference count</summary>
        public int GoalDifference { get; set; }

        /// <summary>Wins count</summary>
        public int Win { get; set; }

        /// <summary>Loses count</summary>
        public int Lose { get; set; }

        /// <summary>Draw count</summary>
        public int Draw { get; set; }
    }
}
