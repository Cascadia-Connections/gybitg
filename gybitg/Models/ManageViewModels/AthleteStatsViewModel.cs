using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace gybitg.Models.ManageViewModels
{
    public class AthleteStatsViewModel
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        public DateTime DateOFEntry { get; set; }

        [Display(Name = "Points per Game")]
        public decimal PPG { get; set; }

        [Display(Name = "Rebounds per Game")]
        public decimal RPG { get; set; }

        [Display(Name = "Assists per Game")]
        public decimal APG { get; set; }

        [Display(Name = "Games Played")]
        public int GP { get; set; }

        [Display(Name = "Games Sat")]
        public int GS { get; set; }

        [Display(Name = "Average Minutes per Game")]
        public decimal MPG { get; set; }

        [Display(Name = "Field Goals Attempted per Game")]
        public decimal FGAG { get; set; }

        [Display(Name = "Field Goals Made per Game")]
        public decimal FGMG { get; set; }

        [Display(Name = "Field Goal Percentage")]
        public decimal FGG { get; set; }

        [Display(Name = "Free Throws Attempted per Game")]
        public decimal FTAG { get; set; }

        [Display(Name = "Free Throws Made per Game")]
        public decimal FTMG { get; set; }

        [Display(Name = "Free Throw Percentage")]
        public decimal FTP { get; set; }

        [Display(Name = "Three Pointers Attempted per Game")]
        public decimal TPAG { get; set; }

        [Display(Name = "Three Pointers Made per Game")]
        public decimal TPMG { get; set; }

        [Display(Name = "Three Point Percentage")]
        public decimal TPP { get; set; }

        public string StatusMessage { get; set; }

    }
}
