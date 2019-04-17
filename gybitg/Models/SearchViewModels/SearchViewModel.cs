﻿using System;
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

        [Display(Name = "Points per Game")]
        public decimal PPG { get; set; }

        [Display(Name = "Average Minutes per Game")]
        public decimal MPG { get; set; }

        [Display(Name = "Three Pointers Made per Game")]
        public decimal TPMG { get; set; }

        [Display(Name = "Free Throws Made per Game")]
        public decimal FTMG { get; set; }

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