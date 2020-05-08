using System;
using System.Collections.Generic;

namespace StudentsCommunity.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int? LostItemId { get; set; }

        public virtual LostItems LostItem { get; set; }
    }
}
