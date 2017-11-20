using System;

namespace LiveScore.Models.Business
{
    public class Game
    {
        public int Id { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Score Score { get; set; }
        public DateTime KickOff { get; set; }
        public MatchDay MatchDay { get; set; }
    }
}
