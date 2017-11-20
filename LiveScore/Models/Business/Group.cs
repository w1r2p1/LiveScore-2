using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScore.Models.Business
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public League League { get; set; }
    }
}
