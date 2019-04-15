using System;
using System.Collections.Generic;
using Bogus;
using System.Linq;
using System.Threading.Tasks;
using gybitg.Data;
using gybitg.Models;
using gybitg.Models.ManageViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace gybitg
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await Seed(services.GetRequiredService<ApplicationDbContext>());
        }

        public static async Task Seed(ApplicationDbContext context)
        {
            if (context.AthleteUserViewModel.Any())
            {
                return; //already has data, don't add any more test data
            }

            // Trying to get data for the db context for testing
            //this was not working correctly
            //var firstName = new[] { "Adam", "Alex", "Kevin", "Daniel", "Keith" };
            //var lastName = new[] { "A", "B", "C", "D", "E" };
            //var Position = new[] { "Point Guard", "Center", "Shooting Guard", "Small Forward", "Power Forward" };
            //  (1) Import NuGet Package "Bogus" fake data generator, then
            //  Use "dotnet ef database drop", and run the application and inspect your data
            /*
             * Some trial and error, first ideas
             * Randomizer.Seed = new Random(8672309);
            var athleteIndex = 0;
            //Athletes
            var testAthletes = new Faker<ApplicationUser>()
                .RuleFor(fn => fn.FirstName, f => firstName[athleteIndex++])
                .RuleFor(ln => ln.LastName, f => lastName[athleteIndex++])
                .RuleFor(w => w.Position, t => t.PickRandom(Position));
            var athletes = testAthletes.Generate(5); // (2) create a collection of 5 athletes
            */

            //second idea - working
            //athlete 0
            AthleteUserViewModel athlete = new AthleteUserViewModel();
            athlete.FirstName = "Adam";
            athlete.LastName = "A";
            athlete.Position = "Point Guard";
            context.AthleteUserViewModel.Add(athlete);
            //athlete 1
            AthleteUserViewModel athlete1 = new AthleteUserViewModel();
            athlete1.FirstName = "Alex";
            athlete1.LastName = "B";
            athlete1.Position = "Center";
            context.AthleteUserViewModel.Add(athlete1);
            //athlete 2
            AthleteUserViewModel athlete2 = new AthleteUserViewModel();
            athlete2.FirstName = "Kevin";
            athlete2.LastName = "C";
            athlete2.Position = "Shooting Guard";
            context.AthleteUserViewModel.Add(athlete2);
            //athlete 3
            AthleteUserViewModel athlete3 = new AthleteUserViewModel();
            athlete3.FirstName = "Daniel";
            athlete3.LastName = "D";
            athlete3.Position = "Small Foward";
            context.AthleteUserViewModel.Add(athlete3);
            //athlete 4
            AthleteUserViewModel athlete4 = new AthleteUserViewModel();
            athlete4.FirstName = "Kieth";
            athlete4.LastName = "E";
            athlete4.Position = "Power Foward";
            context.AthleteUserViewModel.Add(athlete4);

            context.SaveChanges();
            
        }
    }
}