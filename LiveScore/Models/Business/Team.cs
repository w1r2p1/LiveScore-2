using LiveScore.Utils;

namespace LiveScore.Models.Business
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
    }
}
