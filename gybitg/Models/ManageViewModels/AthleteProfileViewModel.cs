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

        [Required]
        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(20, ErrorMessage = "First Name should be less than or equal to twenty characters.")]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(20, ErrorMessage = "Last Name should be less than or equal to twenty characters.")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Range(1, 999, ErrorMessage = "Height can be between 1 to 999 inches." )]
        [Display(Name = "Height (inches)")]
        public decimal Height { get; set; }

        [Range(1, 999, ErrorMessage = "Weight can be between 1 to 999 Lbs.")]
        [Display(Name = "Weight (Lbs.)")]
        public decimal Weight { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(200, ErrorMessage = "Personal Bio should be less than or equal to 200 characters.")]
        [Display(Name = "Personal Bio")]
        public string PersonalBio { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(40, ErrorMessage = "Current Highschool should be less than or equal to fourty characters.")]
        [Display(Name = "Current Highschool")]
        public string HighschoolName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(40, ErrorMessage = "Current Highschool Coach should be less than or equal to fourty characters.")]
        [Display(Name = "Current Highschool Coach")]
        public string HighschoolCoach { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "HS Graduation Date"), DataType(DataType.Date)]
        public DateTime HSGraduationDate { get; set; }
        
        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(40, ErrorMessage = "AAU Team should be less than or equal to fourty characters.")]
        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        public string StatusMessage { get; set; }
    }
}
