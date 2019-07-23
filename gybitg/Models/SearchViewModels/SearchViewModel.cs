using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace gybitg.Models.SearchViewModels
{
    public class SearchViewModel
    {
        [RegularExpression("^[a-zA-Z .&'-_]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [Display(Name = "Position")]
        public PositionType Position { get; set; }

        public enum PositionType
        {
            [Display(Name = "Point Guard")]
            PointGuard,
            [Display(Name = "Shooting Guard")]
            ShootingGuard,
            [Display(Name = "Small Forward")]
            SmallForward,
            [Display(Name = "Power Forward")]
            PowerForward,
            [Display(Name = "Center")]
            Center
        }

        [RegularExpression("^[0-9/]*$", ErrorMessage = "Only numerical digits and Forward-Slashes allowed '/'")]
        [StringLength(10, ErrorMessage = "High School Graduation Date should be less than or equal to 10 characters.")]
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
    }
}
