using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gybitg.Models;

namespace gybitg.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any user accounts
            if (context.Users.Any())
            {
                return; // DB Has been seeded
            }

            // Create the membership types
            var memberships = new Membership[]
            {
                new Membership{Type="Athlete" },
                new Membership{Type="Coach" }
            };
            // Seed the Db / Memberships table
            foreach (Membership m in memberships)
            {
                context.Memberships.Add(m);
            }
            context.SaveChanges();
        }

    }
}
