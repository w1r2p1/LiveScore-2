using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public MatchDayStatus Status { get; set; }

        [Required]
        public League League { get; set; }
    }
}
