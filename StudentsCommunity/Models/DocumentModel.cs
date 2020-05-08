using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsCommunity.Models
{
    public class DocumentModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<IFormFile> docs { get; set; }
    }
}
