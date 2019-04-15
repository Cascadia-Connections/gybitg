using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gybitg.Models
{
    public class FakeApplicationUserRepo : IApplicationUserRepo
    {
        public IQueryable<ApplicationUser> Athletes => new List<ApplicationUser>
        {
            new ApplicationUser {FirstName = "Adam", LastName = "A", Position = "PG"},
            new ApplicationUser {FirstName = "Daniel", LastName = "D", Position = "C"},
            new ApplicationUser {FirstName = "Kieth", LastName = "K", Position = "SG"},
            new ApplicationUser {FirstName = "Kevin", LastName = "K", Position = "SF"},
            new ApplicationUser {FirstName = "Alex", LastName = "A", Position = "PF"}
        }.AsQueryable<ApplicationUser>();
    }
}
