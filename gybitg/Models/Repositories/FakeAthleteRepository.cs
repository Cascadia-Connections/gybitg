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
            new AthleteProfile {HighschoolName = "Bothell", HSGraduationDate = new DateTime(2020-06), UserId = "1", HighschoolCoach = "Bob" }, //, ClubCoach = "Mary"},
            new AthleteProfile {HighschoolName = "Inglemoor", HSGraduationDate = new DateTime(2021-06), UserId = "2", HighschoolCoach = "Cindy" }, //, ClubCoach = "Susan"},
            new AthleteProfile {HighschoolName = "Woodinville", HSGraduationDate = new DateTime(2021-06), UserId = "3", HighschoolCoach = "Prudence" }, //, ClubCoach = "Sarah" },
            new AthleteProfile {HighschoolName = "Jackson", HSGraduationDate = new DateTime(2023-06), UserId = "4", HighschoolCoach = "Zack" }, // ClubCoach = "Emily" },
            new AthleteProfile {HighschoolName = "Juanita", HSGraduationDate = new DateTime(2023-06), UserId = "5", HighschoolCoach = "Teresa" }, // ClubCoach = "Tina" },

        }.AsQueryable<AthleteProfile>();



        public IQueryable<AthleteStats> athleteStats => new List<AthleteStats>
        {
            new AthleteStats {}
        }.AsQueryable<AthleteStats>();
        
    }
}
