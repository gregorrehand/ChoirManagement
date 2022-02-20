using System;
using System.Collections.Generic;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }
        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppDbContext context)
        {
            
            var roles = new (string roleName, string roleDisplayName)[]
            {
                ("user", "User"),
                ("admin", "Admin"),
                ("manager", "Manager")
            };
            foreach (var (roleName, roleDisplayName) in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole()
                    {
                        Name = roleName,
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
                
            }
            

            var userName = "akaver@akaver.com";
            var passWord = "Kala.maja.2020";
            var firstName = "Andres";
            var lastName = "Käver";

            var user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new AppUser();
                user.Email = userName;
                user.UserName = userName;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.VoiceGroupId = context.VoiceGroups.Where(v => v.Name == "Sopran").FirstOrDefaultAsync().Result.Id;
                
                var result = userManager.CreateAsync(user, passWord).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");

                }
            }


            var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
            
            var userName2 = "manager@manager.com";
            var passWord2 = "Akavermanager1549.";
            var firstName2 = "Eesnimi";
            var lastName2 = "Perenimi";
            var user2 = userManager.FindByNameAsync(userName2).Result;
            if (user2 == null)
            {
                user2 = new AppUser();
                user2.Email = userName2;
                user2.UserName = userName2;
                user2.FirstName = firstName2;
                user2.LastName = lastName2;
                user2.VoiceGroupId = context.VoiceGroups.Where(v => v.Name == "Bass").FirstOrDefaultAsync().Result.Id;

                var result2 = userManager.CreateAsync(user2, passWord2).Result;
                if (!result2.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
            
                }
            }
            
            
            var roleResult2 = userManager.AddToRoleAsync(user2, "manager").Result;

        }
        
        public static void SeedData(AppDbContext context)
        {
            context.VoiceGroups.AddRange(new List<VoiceGroup>()
            {
                new VoiceGroup()
                {
                    Name = "Sopran",

                },
                new VoiceGroup()
                {
                    Name = "Alt",

                },
                new VoiceGroup()
                {
                    Name = "Tenor",

                },
                new VoiceGroup()
                {
                    Name = "Bass",

                }
            });
            context.SaveChanges();
        }
        

    }
}
