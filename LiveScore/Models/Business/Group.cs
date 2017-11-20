using System.ComponentModel.DataAnnotations;

namespace LiveScore.Models.Business
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Group name must be 10 characters or less"), MinLength(1)]
        public string Name { get; set; }

        [Required]
        public League League { get; set; }
    }
}
