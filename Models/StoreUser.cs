using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEESEE_GAMING.Models
{
    public class StoreUser
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }
    }
}