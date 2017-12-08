namespace LiveScore.Models.Response
{
    /// <summary>
    /// Response model that encapsulates standings-related data on group level.
    /// </summary>
    public class GroupStandings
    {
        /// <summary>League season full title</summary>
        public string LeagueTitle { get; set; }

        /// <summary>Match day number</summary>
        public int MatchDay { get; set; }

        /// <summary>League group name</summary>
        public string Group { get; set; }

        /// <summary>Collection of club standings</summary>
        public ClubStandings[] Standings { get; set; }
    }
}
