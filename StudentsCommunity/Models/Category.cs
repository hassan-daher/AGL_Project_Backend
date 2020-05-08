using System;
using System.Collections.Generic;

namespace StudentsCommunity.Models
{
    public partial class Category
    {
        public Category()
        {
            Document = new HashSet<Document>();
            LostItems = new HashSet<LostItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Document> Document { get; set; }
        public virtual ICollection<LostItems> LostItems { get; set; }
    }
}
