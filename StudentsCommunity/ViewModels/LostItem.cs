using StudentsCommunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsCommunity.ViewModels
{
    public class LostItem
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
      
        public string CategoryName { get; set; }
        public List<string> ImageUrls { get; set; }
        public string FoundUserName { get; set; }
    }
}
