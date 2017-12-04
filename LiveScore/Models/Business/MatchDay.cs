using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    public class MatchDay
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public League League { get; set; }

        public List<Game> Games { get; set; }
    }
}
