using System;
using System.Collections.Generic;

namespace StudentsCommunity.Models
{
    public partial class LostItems
    {
        public LostItems()
        {
            Image = new HashSet<Image>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Image> Image { get; set; }
    }
}
