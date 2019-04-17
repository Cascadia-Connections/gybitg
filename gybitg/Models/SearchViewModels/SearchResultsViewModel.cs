/*File Created by Daniel Watkins 4/13/2019*/
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

        //[Display(Name = "First Name")]
        //public virtual string FirstName { get; set; }

        //[Display(Name = "Last Name")]
        //public virtual string LastName { get; set; }
        [Display(Name = "Full Name")]
        public virtual string FullName { get; set; }

        [Display(Name = "Position")]
        public String Position { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "HS Graduation Date"), DataType(DataType.Date)]
        public DateTime HSGraduationDate { get; set; }

        [Display(Name = "Points per Game")]
        public decimal PPG { get; set; }

        [Display(Name = "Average Minutes per Game")]
        public decimal MPG { get; set; }

        [Display(Name = "Three Pointers Made per Game")]
        public decimal TPMG { get; set; }

        [Display(Name = "Free Throws Made per Game")]
        public decimal FTMG { get; set; }


    }
}
