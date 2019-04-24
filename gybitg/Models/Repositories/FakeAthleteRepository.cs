using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace gybitg.Models.Repositories
{
    public class FakeAthleteRepository : IAthleteRepository
    {
        //Is this workable?  Can I have these two things be populated by the seed data
        // I have in another file?
        public IQueryable<AthleteProfile> athleteProfiles => new List<AthleteProfile>
        {
            new AthleteProfile {HighschoolName = "Bothell", HSGraduationDate = new DateTime(2020-05), UserId = "1" }

        }.AsQueryable<AthleteProfile>();



        public IQueryable<AthleteStats> athleteStats => new List<AthleteStats>
        {

        }.AsQueryable<AthleteStats>();
        
    }
}
