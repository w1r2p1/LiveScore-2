using LiveScore.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    /// <summary>
    /// Business model that encapsulates game-related data.
    /// </summary>
    public sealed class Game : IEquatable<Game>, IEntity
    {
        /// <summary>Unique identifier</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Reference to home team business model</summary>
        public Team HomeTeam { get; set; }

        /// <summary>Reference to away team business model</summary>
        public Team AwayTeam { get; set; }

        /// <summary>Reference to score business model</summary>
        public Score Score { get; set; }

        /// <summary>Date and time of the game start</summary>
        public DateTime KickOff { get; set; }

        /// <summary>Reference to match day business model</summary>
        public MatchDay MatchDay { get; set; }

        /// <summary>
        /// This method checks if two game business models are equal.
        /// </summary>
        /// <param name="other">Test game model</param>
        /// <returns>Whether or not game model is equal to test game model</returns>
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

        /// <summary>
        /// This method is an override of <see cref="System.Object"/> equality method.
        /// </summary>
        /// <param name="obj">Test object</param>
        /// <returns>Whether or not game model is equal to test object</returns>
        public override bool Equals(Object obj)
        {
            var personObj = obj as Game;
            return (personObj == null) ? false : Equals(personObj);
        }

        /// <summary>
        /// This method is an override of <see cref="System.Object"/> hash method.
        /// </summary>
        /// <returns>Hash value of game model</returns>
        public override int GetHashCode()
        {
            return (this.HomeTeam.Name + this.AwayTeam.Name + this.MatchDay.Id).GetHashCode();
        }
    }
}
