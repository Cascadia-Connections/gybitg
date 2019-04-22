using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gybitg.Models.Repositories
{
    public class FakeAthleteRepository : IAthleteRepository
    {
        //Is this workable?  Can I have these two things be populated by the seed data
        // I have in another file?
        public IQueryable<AthleteProfile> athleteProfiles => new List<AthleteProfile>
        {
            new AthleteProfile{}
        }.AsQueryable<AthleteProfile>();

        public IQueryable<AthleteStats> athleteStats => new List<AthleteStats>
        {

        }.AsQueryable<AthleteStats>();
    }
}
