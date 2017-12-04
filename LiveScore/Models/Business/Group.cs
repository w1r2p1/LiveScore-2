using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public League League { get; set; }

        public List<Team> Teams { get; set; }
    }
}
