using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace gybitg.Models.ManageViewModels
{
    public class AthleteUserViewModel
    {
        [Key]
        public string UserId { get; set; }

        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        public string Text { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public virtual string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Position { get; set; }

        public string AvatarImageUrl { get; set; }

        public string ProfileVideoUrl { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "HS Graduation Date"), DataType(DataType.Date)]
        public DateTime HSGraduationDate { get; set; }
        [Display(Name = "Points per Game")]
        public decimal PPG { get; set; }

        [Display(Name = "Height (inches)")]
        public decimal Height { get; set; }

        [Display(Name = "Weight (Lbs.)")]
        public decimal Weight { get; set; }

        [Display(Name = "Current Highschool")]
        public string HighschoolName { get; set; }


        [Display(Name = "Rebounds per Game")]
        public decimal RPG { get; set; }

        [Display(Name = "Assists per Game")]
        public decimal APG { get; set; }

        [Display(Name = "Games Played")]
        public int GP { get; set; }

        [Display(Name = "Games Sat")]
        public int GS { get; set; }

        [Display(Name = "Average Minutes per Game")]
        public decimal MPG { get; set; }

        [Display(Name = "Field Goals Attempted per Game")]
        public decimal FGAG { get; set; }

        [Display(Name = "Field Goals Made per Game")]
        public decimal FGMG { get; set; }

        [Display(Name = "Field Goal Percentage")]
        public decimal FGG { get; set; }

    }
}
