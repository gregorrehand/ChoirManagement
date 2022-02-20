using System;
using System.Linq;
using DAL.App.EF;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the dbcontext
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                    );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<AppDbContext>(options =>
                {
                    // do we need unique db?
                    options.UseInMemoryDatabase(builder.GetSetting("test_database_name"));
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                // data is already seeded
                if (db.VoiceGroups.Any()) return;
                
                // seed data
                db.VoiceGroups.Add(new VoiceGroup()
                {
                    Name = "Bass"
                });

                db.Projects.Add(new Project()
                {
                    Name = "seededProjectName",
                    Info = "seededProjectInfo",
                    Programme = "seededProjectProgramme"
                });
                
                db.SaveChanges();

                var roles = new (string roleName, string roleDisplayName)[]
                {
                    ("user", "User"),
                    ("admin", "Admin"),
                    ("manager", "Manager")
                };
                var roleManager = scopedServices.GetService<RoleManager<AppRole>>();
                var userManager = scopedServices.GetService<UserManager<AppUser>>();

                foreach (var (roleName, roleDisplayName) in roles)
                {
                    var role = roleManager!.FindByNameAsync(roleName).Result;
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
                var passWord = "Test.test.2020";
                var firstName = "Andres";
                var lastName = "Käver";

                var user = userManager!.FindByNameAsync(userName).Result;
                if (user == null)
                {
                    user = new AppUser();
                    user.Email = userName;
                    user.UserName = userName;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.VoiceGroupId = db.VoiceGroups.Where(v => v.Name == "Bass").FirstOrDefaultAsync().Result.Id;
                
                    var result = userManager.CreateAsync(user, passWord).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");

                    }
                }
                userManager.AddToRoleAsync(user, "admin");
                
                userName = "user@user.com";
                passWord = "Test.test.2020";
                firstName = "Eesnimi";
                lastName = "Perenimi";

                var user2 = userManager!.FindByNameAsync(userName).Result;
                if (user2 == null)
                {
                    user2 = new AppUser();
                    user2.Email = userName;
                    user2.UserName = userName;
                    user2.FirstName = firstName;
                    user2.LastName = lastName;
                    user2.VoiceGroupId = db.VoiceGroups.Where(v => v.Name == "Bass").FirstOrDefaultAsync().Result.Id;
                
                    var result = userManager.CreateAsync(user2, passWord).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");

                    }
                }
                userManager.AddToRoleAsync(user2, "user");
                
                userManager.AddToRoleAsync(user, "admin");
                
                userName = "user2@user.com";
                passWord = "Test.test.2020";
                firstName = "Eesnimi2";
                lastName = "Perenimi2";

                var user3 = userManager!.FindByNameAsync(userName).Result;
                if (user3 == null)
                {
                    user3 = new AppUser();
                    user3.Email = userName;
                    user3.UserName = userName;
                    user3.FirstName = firstName;
                    user3.LastName = lastName;
                    user3.VoiceGroupId = db.VoiceGroups.Where(v => v.Name == "Bass").FirstOrDefaultAsync().Result.Id;
                
                    var result = userManager.CreateAsync(user3, passWord).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");

                    }
                }
                userManager.AddToRoleAsync(user3, "user");
            });
        }
    }
}