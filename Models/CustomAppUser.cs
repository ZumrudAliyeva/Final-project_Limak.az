using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_project_Limak.az.Models
{
    public class CustomAppUser : IdentityUser
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime Birthmonth { get; set; }
        public DateTime Birthyear { get; set; }
        public string IDSerialNumber { get; set; }
        public string Citizenship { get; set; }
        public string FINkode { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
