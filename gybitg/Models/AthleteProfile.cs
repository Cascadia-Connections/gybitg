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
        //[Key]
        //public int Id { get; set; }

        [Key]
        public string UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Height (In.)")]
        public decimal Height { get; set; }

        [Display(Name = "Weight (Lbs.)")]
        public decimal Weight { get; set; }

        [Display(Name = "Personal Bio")]
        public string PersnalBio { get; set; }

        [Display(Name = "Current Highschool")]
        public string HighschoolName { get; set; }

        [Display(Name = "Current Highschool Coach")]
        public string HighschoolCoach { get; set; }

        public string Position { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "HS Graduation Date"), DataType(DataType.Date)]
        public DateTime HSGraduationDate { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }
    }
}
