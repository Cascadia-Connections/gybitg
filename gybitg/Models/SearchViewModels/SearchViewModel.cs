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

        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Only Numeric 0-9 characters allowed: yyyy.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "HS Graduation Date"), DataType(DataType.Date)]
        public DateTime HSGraduationDate { get; set; }

        [RegularExpression("^[a-zA-Z .&'-_]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "High School")]
        public string HighSchool { get; set; }

        [RegularExpression("^[a-zA-Z .&'-_]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [RegularExpression("^[a-zA-Z .&'-_]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "AAU Coach")]
        public string AAUCoach { get; set; }

        [RegularExpression("^[a-zA-Z .&'-_]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "High School Coach")]
        public string HighScoolCoach { get; set; }

        //Designate only specific types of Positions
        public enum PositionType
        {
            Default,
            PointGuard,
            ShootingGuard,
            SmallForward,
            PowerForward,
            Center
        }
    }
}
