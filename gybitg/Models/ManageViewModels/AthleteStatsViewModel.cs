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

        [Range(0, 200, ErrorMessage = "Points per Game must be 0 to 200")]
        [Display(Name = "Points per Game")]
        public decimal PPG { get; set; }

        [Range(0, 100, ErrorMessage = "Rebounds per Game must be 0 to 100")]
        [Display(Name = "Rebounds per Game")]
        public decimal RPG { get; set; }

        [Range(0, 100, ErrorMessage = "Assists per Game must be 0 to 100")]
        [Display(Name = "Assists per Game")]
        public decimal APG { get; set; }

        [Range(0, 100, ErrorMessage = "Games Played must be 0 to 100")]
        [Display(Name = "Games Played")]
        public int GP { get; set; }

        [Range(0, 100, ErrorMessage = "Games Sat must be 0 to 100")]
        [Display(Name = "Games Sat")]
        public int GS { get; set; }

        [Range(0, 100, ErrorMessage = "Average Minutes per Game must be 0 to 100")]
        [Display(Name = "Average Minutes per Game")]
        public decimal MPG { get; set; }

        [Range(0, 100, ErrorMessage = "Field Goals Attempted per Game must be 0 to 100")]
        [Display(Name = "Field Goals Attempted per Game")]
        public decimal FGAG { get; set; }

        [Range(0, 100, ErrorMessage = "Field Goals Made per Game must be 0 to 100")]
        [Display(Name = "Field Goals Made per Game")]
        public decimal FGMG { get; set; }

        [Range(0, 1, ErrorMessage = "Field Goal Percentage per Game must be 0 to 1")]
        [Display(Name = "Field Goal Percentage")]
        public decimal FGG { get; set; }

        [Range(0, 100, ErrorMessage = "Free Throws Attempted per Game must be 0 to 100")]
        [Display(Name = "Free Throws Attempted per Game")]
        public decimal FTAG { get; set; }

        [Range(0, 100, ErrorMessage = "Free Throws Made per Game must be 0 to 100")]
        [Display(Name = "Free Throws Made per Game")]
        public decimal FTMG { get; set; }

        [Range(0, 1, ErrorMessage = "Free Throw Percentage per Game must be 0 to 1")]
        [Display(Name = "Free Throw Percentage")]
        public decimal FTP { get; set; }

        [Range(0, 100, ErrorMessage = "Three Pointers Attempted per Game must be 0 to 100")]
        [Display(Name = "Three Pointers Attempted per Game")]
        public decimal TPAG { get; set; }

        [Range(0, 100, ErrorMessage = "Three Pointers Made per Game must be 0 to 100")]
        [Display(Name = "Three Pointers Made per Game")]
        public decimal TPMG { get; set; }

        [Range(0, 1, ErrorMessage = "Three Point Percentage per Game must be 0 to 1")]
        [Display(Name = "Three Point Percentage")]
        public decimal TPP { get; set; }

        public string StatusMessage { get; set; }

    }
}
