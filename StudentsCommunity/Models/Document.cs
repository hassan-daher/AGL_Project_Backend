using System;
using System.Collections.Generic;

namespace StudentsCommunity.Models
{
    public partial class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}
