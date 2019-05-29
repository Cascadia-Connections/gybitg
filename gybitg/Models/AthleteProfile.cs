using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace gybitg.Models
{
    public class AthleteProfile
    {
        [Key]
        public string UserId { get; set; }

        [Display(Name = "Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Height (In.)")]
        public decimal Height { get; set; }

        [Display(Name = "Weight (Lbs.)")]
        public decimal Weight { get; set; }

        [Display(Name = "Personal Bio")]
        public string PersonalBio { get; set; }

        [Display(Name = "Current Highschool")]
        public string HighschoolName { get; set; }

        [Display(Name = "Current Highschool Coach")]
        public string HighschoolCoach { get; set; }

        [Display(Name = "HS Graduation Date"), DataType(DataType.Date)]
        public DateTime HSGraduationDate { get; set; }

        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [Display(Name = "Current AAU Coach")]
        public string AAUCoach { get; set; }

        public ICollection<CoachAthlete> CoachAthletes { get; set; }
    }
}
