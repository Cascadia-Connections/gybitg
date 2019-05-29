using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace gybitg.Models.ManageViewModels
{
    public class AthleteProfileViewModel
    {
        [Key]
        public string UserId { get; set; }

        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

     
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Height (inches)")]
        public decimal Height { get; set; }

        [Display(Name = "Weight (Lbs.)")]
        public decimal Weight { get; set; }

        [Display(Name = "Personal Bio")]
        public string PersonalBio { get; set; }

        [Display(Name = "Current Highschool")]
        public string HighschoolName { get; set; }

        [Display(Name = "Current Highschool Coach")]
        public string HighschoolCoach { get; set; }

  
        [Display(Name = "HS Graduation Date")]
        public string HSGraduationDate { get; set; }

        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        public string StatusMessage { get; set; }
    }
}
