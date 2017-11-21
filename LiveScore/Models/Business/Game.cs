using System;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Team HomeTeam { get; set; }

        [Required]
        public Team AwayTeam { get; set; }

        [Required]
        public Score Score { get; set; }

        [Required]
        public DateTime KickOff { get; set; }

        [Required]
        public MatchDay MatchDay { get; set; }
    }
}
