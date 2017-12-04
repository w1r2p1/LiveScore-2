using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    public class League
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }

        public List<Group> Groups { get; set; }
        public List<MatchDay> MatchDays { get; set; }
    }
}
