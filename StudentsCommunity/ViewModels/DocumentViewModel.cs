﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsCommunity.ViewModels
{
    public class DocumentViewModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string doc { get; set; }
    }
}
