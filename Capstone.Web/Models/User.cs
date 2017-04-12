using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
    }
}