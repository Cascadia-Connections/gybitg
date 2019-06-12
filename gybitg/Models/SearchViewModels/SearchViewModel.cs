using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace gybitg.Models.SearchViewModels
{
    public class SearchViewModel
    {
        [RegularExpression("^[a-zA-Z .&'-_]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [Display(Name = "Position")]
        public PositionType Position { get; set; }

        [RegularExpression("^[0-9/]{7}$", ErrorMessage = "Only this format allowed: mm/yyyy")]
        [Display(Name = "HS Graduation Date")]
        public string HSGraduationDate { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "High School")]
        public string HighSchool { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "AAU Coach")]
        public string AAUCoach { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "High School Coach")]
        public string HighScoolCoach { get; set; }

        //Designate only specific types of Positions
        public enum PositionType
        {
            [Display(Name = "Not Selected")]
            NotSelected,
            [Display(Name = "Point Guard")]
            PointGuard,
            [Display(Name = "Shooting Guard")]
            ShootingGuard,
            [Display(Name = "Small Guard")]
            SmallForward,
            [Display(Name = "Power Guard")]
            PowerForward,
            [Display(Name = "Center")]
            Center
        }
    }
}
