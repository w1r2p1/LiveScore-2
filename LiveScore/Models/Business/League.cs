namespace LiveScore.Models.Business
{
    public enum LeagueStatus
    {
        Scheduled = 0,
        InProgress = 1,
        Finished = 2,
        Canceled = 3
    }

    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public LeagueStatus Status { get; set; }
    }
}
