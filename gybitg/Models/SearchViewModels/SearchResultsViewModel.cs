using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using gybitg.Models;

namespace gybitg.Models.SearchViewModels
{
    public class SearchResultsViewModel
    {
        [Key]
        public string UserId { get; set; }

        [Display(Name = "Full Name")]
        public virtual string FullName { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "HS Graduation Date"), DataType(DataType.Date)]
        public DateTime HSGraduationDate { get; set; }

        [Display(Name = "High School")]
        public string HighSchool { get; set; }

        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [Display(Name = "AAU Coach")]
        public string AAUCoach { get; set; }

        [Display(Name = "High School Coach")]
        public string HighScoolCoach { get; set; }


    }
}
