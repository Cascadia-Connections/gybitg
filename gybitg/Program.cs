using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using gybitg.Data;
using Microsoft.AspNetCore.Identity;

namespace gybitg
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var context = services.GetRequiredService<ApplicationDbContext>();
            //        DbInitializer.Initialize(context);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occured while seeding the database.");
            //    }
            //}


            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    CreateRoles(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB Roles."); //this error is thrown
                }

                async Task CreateRoles(IServiceProvider serviceProvider)
                {
                    // adding custom roles
                    var RoleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    string[] roleNames = { "Athlete", "Coach" };
                    IdentityResult roleResult;

                    foreach (var roleName in roleNames)
                    {
                        var roleExist = await RoleManager.RoleExistsAsync(roleName);
                        if (!roleExist)
                        {
                            // create the roles and seed them to the database
                            roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                        }
                    }

                }
            }

            InitializeDatabase(host);//call the seed data method below
            host.Run();
             

        }

        // make sure the seed data is used to populate DB
        private static void InitializeDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the database");
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


    }
}
