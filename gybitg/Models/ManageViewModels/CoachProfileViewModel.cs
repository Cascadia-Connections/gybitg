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

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(40, ErrorMessage = "AAU Team must be less than or equal to fourty characters.")]
        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(280, ErrorMessage = "Personal Bio must be less than or equal to 280 characters.")]
        [Display(Name = "Personal Bio")]
        public string PersonalBio { get; set; }

        [Range(0, 200, ErrorMessage = "Wins must be between 1 to 200.")]
        [Display(Name = "Wins")]
        public int Wins { get; set; }

        [Range(0, 200, ErrorMessage = "Losses must be between 1 to 200.")]
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

        [Range(0, 50, ErrorMessage = "Years Coaching must be between 0 and 50.")]
        [Display(Name = "Years at current institution")]
        public decimal YearsCoaching { get; set; }

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(280, ErrorMessage = "Achievements should be less than or equal to 280 characters.")]
        [Display(Name = "Achievements")]
        public string Achievements { get; set; }

        //Why is this located here and not under IndexViewModel above City, State, and Zip?
        //Same within the EditCoachProfile.cshtml, Address input textbox should probably be within the Manage/Index View, above City, State, and Zip
        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(40, ErrorMessage = "Address must be less than or equal to fourty characters.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Verified")]
        public bool Verified { get; set; }

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(40, ErrorMessage = "Status Message must be less than or equal to fourty characters.")]
        [Display(Name = "Status Message")]
        public string StatusMessage { get; set; }

    }
}
