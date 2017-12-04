using System;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Score Score { get; set; }
        public DateTime KickOff { get; set; }
        public MatchDay MatchDay { get; set; }
    }
}
