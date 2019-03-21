﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace gybitg.Models.ManageViewModels
{
    public class StatViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Game Date")]
        public DateTime DateOfEntry { get; set; }

        [Range(0, 100)]
        public int Points { get; set; }

        [Range(0, 100)]
        public int Rebounds { get; set; }

        [Range(0, 100)]
        public int Assists { get; set; }

        [Range(0, 100)]
        public int Steals { get; set; }

        [Range(0, 100)]
        public int Blocks { get; set; }

        [Range(0, 60)]
        public int MinutesPlayed { get; set; }

        public string StatusMessage { get; set; }
    }
}
