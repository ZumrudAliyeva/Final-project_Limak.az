using Final_project_Limak.az.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_project_Limak.az.Contexts
{
    public class CustomAppDbContext: IdentityDbContext<CustomAppUser>
    {
        public CustomAppDbContext(DbContextOptions<CustomAppDbContext>dbcontextoptions):base(dbcontextoptions){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomAppUser>().HasData(
                   new CustomAppUser
                   {
                       CustomerId = 1000000,
                       Name = "Zumrud",
                       UserName = "Zumrud",
                       Surname = "Aliyeva",
                       Email = "zumrudaliyeva8@gmail.com",
                       PhoneNumber = "+994514400144",
                       IDSerialNumber = "30552643",
                       Citizenship = "azerbaycanli",
                       FINkode = "4zt7s8e",
                       Birthday =
                       Birthmonth =
                       Birthyear =
                       Gender = "qadin",
                       Address = "Baki"
                   }
               );
        }

    }
}
