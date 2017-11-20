namespace LiveScore.Models.Business
{
    public enum MatchDayStatus
    {
        Scheduled = 0,
        InProgress = 1,
        Played = 2,
        PartiallyPlayed = 3,
        Delayed = 4,
        Canceled = 5
    }

    public class MatchDay
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public MatchDayStatus Status { get; set; }
        public League League { get; set; }
    }
}
