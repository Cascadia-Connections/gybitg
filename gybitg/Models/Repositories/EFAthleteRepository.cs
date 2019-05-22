using gybitg.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gybitg.Models.Repositories
{
    public class EFAthleteRepository : IAthleteRepository
    {
        private ApplicationDbContext context;

        public EFAthleteRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<AthleteProfile> athleteProfiles => context.AthleteProfiles;
        public IQueryable<AthleteStats> athleteStats => context.AthleteStats;
    }
}
