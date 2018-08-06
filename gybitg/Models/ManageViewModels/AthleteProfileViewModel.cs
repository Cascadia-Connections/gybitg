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

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Height (inches)")]
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

        public string StatusMessage { get; set; }
    }
}
