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

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(20, ErrorMessage = "First Name must be less than or equal to twenty characters.")]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(20, ErrorMessage = "Last Name must be less than or equal to twenty characters.")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        [RegularExpression("^[0-9/]*$", ErrorMessage = "Only numerical digits and Forward-Slashes allowed '/'")]
        [StringLength(10, ErrorMessage = "Date of Birth must be less than or equal to 10 characters.")]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }

        [Range(1, 120, ErrorMessage = "Height must be between 1 to 120 inches. 1ft=12in, 5ft=60in" )]
        [Display(Name = "Height (inches)")]
        public decimal Height { get; set; }

        [Range(1, 500, ErrorMessage = "Weight must be between 1 to 500 Lbs.")]
        [Display(Name = "Weight (Lbs.)")]
        public decimal Weight { get; set; }

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(280, ErrorMessage = "Personal Bio must be less than or equal to 280 characters.")]
        [Display(Name = "Personal Bio")]
        public string PersonalBio { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(40, ErrorMessage = "Current Highschool must be less than or equal to fourty characters.")]
        [Display(Name = "Current Highschool")]
        public string HighschoolName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(40, ErrorMessage = "Current Highschool Coach must be less than or equal to fourty characters.")]
        [Display(Name = "Current Highschool Coach")]
        public string HighschoolCoach { get; set; }

        [Required]
        [RegularExpression("^[0-9/]*$", ErrorMessage = "Only numerical digits and Forward-Slashes allowed '/'")]
        [StringLength(10, ErrorMessage = "High School Graduation Date must be less than or equal to 10 characters.")]
        [Display(Name = "HS Graduation Date")]
        public string HSGraduationDate { get; set; }

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(40, ErrorMessage = "AAU Team must be less than or equal to fourty characters.")]
        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        public string StatusMessage { get; set; }

    }
}
