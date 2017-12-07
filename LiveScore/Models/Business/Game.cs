using System;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    public class Game : IEquatable<Game>
    {
        [Key]
        public int Id { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Score Score { get; set; }
        public DateTime KickOff { get; set; }
        public MatchDay MatchDay { get; set; }

        public bool Equals(Game other)
        {
            if (other == null ||
                this.HomeTeam.Id != other.HomeTeam.Id ||
                this.AwayTeam.Id != other.AwayTeam.Id ||
                this.MatchDay.Id != other.MatchDay.Id)
            {
                return false;
            }

            return true;
        }

        public override bool Equals(Object obj)
        {
            var personObj = obj as Game;
            return (personObj == null) ? false : Equals(personObj);
        }

        public override int GetHashCode()
        {
            return (this.HomeTeam.Name + this.AwayTeam.Name + this.MatchDay.Id).GetHashCode();
        }
    }
}
