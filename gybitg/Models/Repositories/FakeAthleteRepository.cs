﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using gybitg.Models;

namespace gybitg.Models.Repositories
{
    public class FakeAthleteRepository : IAthleteRepository
    {
        public IQueryable<AthleteProfile> athleteProfiles => new List<AthleteProfile>
        {
            new AthleteProfile {HighschoolName = "Bothell", HSGraduationDate = new DateTime(2020-06), UserId = "1", HighschoolCoach = "Bob" }, //, ClubCoach = "Mary", Postition = "Point Guard"},
            new AthleteProfile {HighschoolName = "Inglemoor", HSGraduationDate = new DateTime(2021-06), UserId = "2", HighschoolCoach = "Cindy" }, //, ClubCoach = "Susan", Position = "Shooting Guard"},
            new AthleteProfile {HighschoolName = "Woodinville", HSGraduationDate = new DateTime(2021-06), UserId = "3", HighschoolCoach = "Prudence" }, //, ClubCoach = "Sarah", Position ="Small Forward"},
            new AthleteProfile {HighschoolName = "Jackson", HSGraduationDate = new DateTime(2023-06), UserId = "4", HighschoolCoach = "Zack" }, // ClubCoach = "Emily", Position = "Power Forward"},
            new AthleteProfile {HighschoolName = "Juanita", HSGraduationDate = new DateTime(2023-06), UserId = "5", HighschoolCoach = "Teresa" }, // ClubCoach = "Tina", Position = "Center"},
            new AthleteProfile {HighschoolName = "Bothell", HSGraduationDate = new DateTime(2023-96), UserId = "6", HighschoolCoach = "Sally"}, // ClubCoach = "Ashley", Position = "Power Forward"},
            new AthleteProfile {HighschoolName = "Mariner", HSGraduationDate = new DateTime(2021-06), UserId = "7", HighschoolCoach = "Betty"} //, ClubCoach = "Denise", Position = "Center"}


        }.AsQueryable<AthleteProfile>();

        public IQueryable<ApplicationUser> applicationUser => new List<ApplicationUser>
        {
            //Will this link with the above athleteProfiles?
            new ApplicationUser {Position = "Point Guard"},
            new ApplicationUser {Position = "Shooting Guard"},
            new ApplicationUser {Position = "Small Forward"},
            new ApplicationUser {Position = "Power Forward"},
            new ApplicationUser {Position = "Center Forward"}
        }.AsQueryable<ApplicationUser>();


        public IQueryable<AthleteStats> athleteStats => new List<AthleteStats>
        {
            new AthleteStats {}
        }.AsQueryable<AthleteStats>();
        
    }
}
