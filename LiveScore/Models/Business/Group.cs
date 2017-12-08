using LiveScore.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    /// <summary>
    /// Business model that encapsulates group-related data.
    /// </summary>
    public sealed class Group : IEntity
    {
        // <summary>Unique identifier</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Name of the group</summary>
        public string Name { get; set; }

        /// <summary>Reference to league business model</summary>
        public League League { get; set; }

        /// <summary>Inverse reference to team business model</summary>
        public List<Team> Teams { get; set; }
    }
}
