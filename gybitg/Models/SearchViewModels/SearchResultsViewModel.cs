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
        public String Position { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "HS Graduation Date"), DataType(DataType.Date)]
        public DateTime HSGraduationDate { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter only a number between 0 and 100")]
        [Display(Name = "Points per Game")]
        public decimal PPG { get; set; }

        [Range(0, 1000, ErrorMessage = "Please enter only a number from 0 to 1000")]
        [Display(Name = "Average Minutes per Game")]
        public decimal MPG { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter year between 1900 and 2100")]
        [Display(Name = "Three Pointers Made per Game")]
        public decimal TPMG { get; set; }

        [Display(Name = "Free Throws Made per Game")]
        public decimal FTMG { get; set; }


    }
}
