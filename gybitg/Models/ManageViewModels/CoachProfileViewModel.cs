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

        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [Display(Name = "Personal Bio")]
        public string PersonalBio { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        [Display(Name = "Career win-loss record")]
        public virtual string WinLossRecord
        {
            get
            {
                return (Wins + "-" + Losses);
            }
        }

        [Display(Name = "Years at current institution")]
        public decimal YearsCoaching { get; set; }

        public string Achievements { get; set; }

        public string Address { get; set; }

        public bool Verified { get; set; }

        public string StatusMessage { get; set; }

    }
}
