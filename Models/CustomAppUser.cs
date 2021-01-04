using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Limak.az.Models
{
    public class CustomAppUser : IdentityUser
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string IDSerialNumber { get; set; }
        public string Citizenship { get; set; }
        public string FINkode { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Declaration> Declarations { get; set; }
        public ICollection<UserBalance> UserBalance { get; set; }

    }
}
