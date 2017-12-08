using LiveScore.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    /// <summary>
    /// Business model that encapsulates team-related data.
    /// </summary>
    public class Team : IEntity
    {
        // <summary>Unique identifier</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Name of the team</summary>
        public string Name { get; set; }

        /// <summary>Reference to group business model</summary>
        public Group Group { get; set; }

        /// <summary>Inverse reference to game business model</summary>
        public List<Game> HomeGames { get; set; }

        /// <summary>Inverse reference to game business model</summary>
        public List<Game> AwayGames { get; set; }
    }
}
