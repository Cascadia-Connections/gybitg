using System;
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
                        new AthleteProfile {HighschoolName = "Bothell", HSGraduationDate = new DateTime(06-01-2020), UserId = "1", HighschoolCoach = "Bob", AAUCoach = "Mary", AAUId = "3"},
            new AthleteProfile {HighschoolName = "Inglemoor", HSGraduationDate = new DateTime(06-01-2021), UserId = "2", HighschoolCoach = "Cindy", AAUCoach = "Susan", AAUId = "7"},
            new AthleteProfile {HighschoolName = "Woodinville", HSGraduationDate = new DateTime(06-02-2021), UserId = "3", HighschoolCoach = "Prudence", AAUCoach = "Sarah", AAUId = "17"},
            new AthleteProfile {HighschoolName = "Jackson", HSGraduationDate = new DateTime(06-01-2023), UserId = "4", HighschoolCoach = "Zack", AAUCoach = "Emily", AAUId = "2"},
            new AthleteProfile {HighschoolName = "Juanita", HSGraduationDate = new DateTime(06-03-2023), UserId = "5", HighschoolCoach = "Teresa", AAUCoach = "Tina", AAUId = "11"},
            new AthleteProfile {HighschoolName = "Bothell", HSGraduationDate = new DateTime(05-28-2023), UserId = "6", HighschoolCoach = "Sally", AAUCoach = "Ashley", AAUId = "0"},
            new AthleteProfile {HighschoolName = "Mariner", HSGraduationDate = new DateTime(06-01-2021), UserId = "7", HighschoolCoach = "Betty", AAUCoach = "Denise", AAUId = "6"}
        }.AsQueryable<AthleteProfile>();

        public IQueryable<ApplicationUser> applicationUser => new List<ApplicationUser>
        {
            //Will this link with the above athleteProfiles?
            new ApplicationUser {Position = "Point Guard", Id = "1", FirstName = "Sue", LastName = "Bean"},
            new ApplicationUser {Position = "Shooting Guard", Id = "2", FirstName = "Daisey", LastName = "Shield"},
            new ApplicationUser {Position = "Small Forward", Id = "3", FirstName = "Black", LastName = "Widow"},
            new ApplicationUser {Position = "Power Forward", Id = "4", FirstName = "Wasp", LastName = "B"},
            new ApplicationUser {Position = "Center Forward", Id = "5", FirstName = "Angela", LastName = "Jole"},
            new ApplicationUser {Position = "Shooting Guard", Id = "6", FirstName = "Sabrina", LastName = "Witch"},
            new ApplicationUser {Position = "Small Forward", Id = "7", FirstName = "Alicia", LastName = "Avenger"},

            new ApplicationUser {Id = "10", FirstName = "Bill", LastName = "Bob"}
        }.AsQueryable<ApplicationUser>();


        public IQueryable<AthleteStats> athleteStats => new List<AthleteStats>
        {
            new AthleteStats {}
        }.AsQueryable<AthleteStats>();
        
    }
}
