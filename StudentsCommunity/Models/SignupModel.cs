using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsCommunity.Models
{
    public class SignupModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime birthday { get; set; }
        public string password { get; set; }
        public int phone { get; set; }
        public int faculty { get; set; }

    }
}
