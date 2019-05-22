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

        //[DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "HS Graduation Date")]
        public string HSGraduationDate { get; set; }
      
        [Display(Name = "High School")]
        public string HighSchool { get; set; }

        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [Display(Name = "AAU Coach")]
        public string AAUCoach { get; set; }

        [Display(Name = "High School Coach")]
        public string HighScoolCoach { get; set; }

        //Designate only specific types of Positions
        public enum PositionType
        {
            Default,
            [Display(Name ="Point Guard")]
            PointGuard,
            [Display(Name = "Shooting Guard")]
            ShootingGuard,
            [Display(Name = "Small Forward")]
            SmallForward,
            [Display(Name = "Power Forward")]
            PowerForward,
            Center
        }
    }
}
