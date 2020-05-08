using System;
using System.Collections.Generic;

namespace StudentsCommunity.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
