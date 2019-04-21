using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace gybitg.Models.SearchViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [Display(Name = "Position")]
        public PositionType Position { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "HS Graduation Date"), DataType(DataType.Date)]
        public DateTime HSGraduationDate { get; set; }
      
        [Display(Name = "Minimum Points per Game")]
        public decimal MinPPG { get; set; }

        [Display(Name = "Maximim Points per Game")]
        public decimal MaxPPG { get; set; }

        [Display(Name = "Minimum Minutes per Game")]
        public decimal MinMPG { get; set; }

        [Display(Name = "Maximum Minutes per Game")]
        public decimal MaxMPG { get; set; }

        [Display(Name = "Mimimum Three Pointers Made per Game")]
        public decimal MinTPMG { get; set; }

        [Display(Name = "Maximum Three Pointers Made per Game")]
        public decimal MaxTPMG { get; set; }

        [Display(Name = "Minimum Free Throws Made per Game")]
        public decimal MinFTMG { get; set; }

        [Display(Name = "Maximum Free Throws Made per Game")]
        public decimal MaxFTMG { get; set; }
        //Designate only specific types of Positions
        public enum PositionType
        {
            PointGuard,
            ShootingGuard,
            SmallForward,
            PowerForward,
            Center
        }
    }
}
