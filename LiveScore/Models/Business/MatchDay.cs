using LiveScore.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    /// <summary>
    /// Business model that encapsulates matchday-related data.
    /// </summary>
    public sealed class MatchDay : IEntity
    {
        // <summary>Unique identifier</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Match day number</summary>
        public int Number { get; set; }

        /// <summary>Reference to league business model</summary>
        public League League { get; set; }

        /// <summary>Inverse reference to game business model</summary>
        public List<Game> Games { get; set; }
    }
}
