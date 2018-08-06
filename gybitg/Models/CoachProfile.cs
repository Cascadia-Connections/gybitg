using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace gybitg.Models
{
    public class CoachProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [Display(Name = "Personal Bio")]
        public string PersnalBio { get; set; }

        public int Wins { get; set; }

        public int Lossess { get; set; }

        [Display(Name = "Career Win-Loss record")]
        public virtual string WinLossRecord
        {
            get
            {
                return (Wins + "-" + Lossess);
            }
        }
        [Display(Name = "Years at current institution")]
        public decimal YearsCoaching { get; set; }

        public string Achievments { get; set; }

        public string Address { get; set; }

        public bool Verified { get; set; }

    }
}
