using LiveScore.Utils;
using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Country Country { get; set; }
    }
}
