using Limak.az.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.az.Data
{
    public static class AdminRoleExtension
    {
        public async static void SeedRole(this IApplicationBuilder builder)
        {
            RoleManager<IdentityRole> role = builder.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<CustomAppUser> db = builder.ApplicationServices.GetRequiredService<UserManager<CustomAppUser>>();

            if (role.Roles.Count() == 0)
            {
                var result = await role.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });

            }

            if (!db.Users.Any())
            {
                CustomAppUser admin = new CustomAppUser
                {
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    EmailConfirmed = true
                };

                IdentityResult identityResult = await db.CreateAsync(admin, "admin123");
                if (identityResult.Succeeded)
                {
                    Task<IdentityResult> res = db.AddToRoleAsync(admin, "Admin");
                    res.Wait();
                }
                else
                {
                    IEnumerable<IdentityError> identityErrors = identityResult.Errors;
                }
            }
        }
    }
}
