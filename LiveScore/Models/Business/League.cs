using LiveScore.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    /// <summary>
    /// Business model that encapsulates league-related data.
    /// </summary>
    public sealed class League : IEntity
    {
        /// <summary>Unique identifier</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Name of the league</summary>
        public string Name { get; set; }

        /// <summary>League season start year</summary>
        public int StartYear { get; set; }

        /// <summary>League season end year</summary>
        public int EndYear { get; set; }

        /// <summary>Inverse reference to group business model</summary>
        public List<Group> Groups { get; set; }

        /// <summary>Inverse reference to match day business model</summary>
        public List<MatchDay> MatchDays { get; set; }
    }
}
