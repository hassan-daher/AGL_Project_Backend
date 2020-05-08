using System;
using System.Collections.Generic;

namespace StudentsCommunity.Models
{
    public partial class User
    {
        public User()
        {
            Document = new HashSet<Document>();
            LostItems = new HashSet<LostItems>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public DateTime? Birthday { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public int FacultyId { get; set; }
        public int RoleId { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Document> Document { get; set; }
        public virtual ICollection<LostItems> LostItems { get; set; }
    }
}
