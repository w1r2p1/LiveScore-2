using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Group name must be 50 characters or less"), MinLength(1)]
        public string Name { get; set; }

        [Required]
        public int StartYear { get; set; }

        [Required]
        public int EndYear { get; set; }

        [Required]
        public LeagueStatus Status { get; set; }
    }
}
