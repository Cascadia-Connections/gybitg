using System;
using System.Collections.Generic;
using Bogus;
using System.Linq;
using System.Threading.Tasks;
using gybitg.Data;
using gybitg.Models;

namespace gybitg
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            //await Seed(services.GetRequiredService<ApplicationDbContext>());
        }

        public static async Task Seed(ApplicationDbContext context)
        {
            if (context.AthleteProfiles.Any())
            {
                return; //already has data, don't add any more test data
            }

            // Trying to get data for the db context for testing
            var firstName = new[] { "Adam", "Alex", "Kevin", "Daniel", "Keith" };
            var lastName = new[] { "A", "B", "C", "D", "E" };
            var Position = new[] { "Point Guard", "Center", "Shooting Guard", "Small Forward", "Power Forward" };
            //  (1) Import NuGet Package "Bogus" fake data generator, then
            //  Use "dotnet ef database drop", and run the application and inspect your data
            Randomizer.Seed = new Random(8672309);
            var athleteIndex = 0;
            //Athletes
            var testAthletes = new Faker<ApplicationUser>()
                .RuleFor(fn => fn.FirstName, f => firstName[athleteIndex++])
                .RuleFor(ln => ln.LastName, f => lastName[athleteIndex++])
                .RuleFor(w => w.Position, t => t.PickRandom(Position));
            var athletes = testAthletes.Generate(5); // (2) create a collection of 5 athletes

            
            /*
            //Books
            var testBooks = new Faker<Book>()
                .RuleFor(b => b.Title, t => t.PickRandom(titles))
                .RuleFor(b => b.SKU, n => n.Random.Replace("IB****-##"))
                .RuleFor(b => b.Price, f => f.Random.Decimal(9.99M, 149.99M))
                .RuleFor(b => b.Author, f => f.PickRandom(writers));
            var books = testBooks.Generate(100); // (2) create a collection of 100 books

            //(3) Add the writers collection to the 
            await context.Books.AddRangeAsync(books);
            await context.Writers.AddRangeAsync(writers);

            await context.SaveChangesAsync();*/
        }
    }
}