namespace LiveScore.Models.Response
{
    public class GroupStandings
    {
        public string LeagueTitle { get; set; }
        public int MatchDay { get; set; }
        public string Group { get; set; }
        public ClubStandings[] Standings { get; set; }
    }
}
