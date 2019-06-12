using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace gybitg.Models.ManageViewModels
{
    public class CoachProfileViewModel
    {
        [Key]
        public string UserId { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(40, ErrorMessage = "AAU Team should be less than or equal to fourty characters.")]
        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(200, ErrorMessage = "Personal Bio should be less than or equal to 200 characters.")]
        [Display(Name = "Personal Bio")]
        public string PersonalBio { get; set; }

        [Range(0, 500, ErrorMessage = "Wins can be between 1 to 500.")]
        [Display(Name = "Wins")]
        public int Wins { get; set; }

        [Range(0, 500, ErrorMessage = "Losses can be between 1 to 500.")]
        [Display(Name = "Losses")]
        public int Losses { get; set; }

        [Display(Name = "Career win-loss record")]
        public virtual string WinLossRecord
        {
            get
            {
                return (Wins + "-" + Losses);
            }
        }

        [Range(0, 100, ErrorMessage = "Years Coaching must be between 0 and 100.")]
        [Display(Name = "Years at current institution")]
        public decimal YearsCoaching { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(200, ErrorMessage = "Achievements should be less than or equal to 200 characters.")]
        [Display(Name = "Achievements")]
        public string Achievements { get; set; }

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical Numerical and .&'- characters are allowed.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Verified")]
        public bool Verified { get; set; }
        
        public string StatusMessage { get; set; }

    }
}
