using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public Group Group { get; set; }
        public List<Game> HomeGames { get; set; }
        public List<Game> AwayGames { get; set; }
    }
}
