using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace gybitg.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        public virtual string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Display(Name = "Position")]
        public string Position { get; set; } //PositionType

        //Designate only specific types of Positions
        public enum PositionType
        {
            Default,
            [Display(Name = "Point Guard")]
            PointGuard,
            [Display(Name = "Shooting Guard")]
            ShootingGuard,
            [Display(Name = "Small Forward")]
            SmallForward,
            [Display(Name = "Power Forward")]
            PowerForward,
            Center
        }

        public string City { get; set; }

        public string State { get; set; }

        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string Zip { get; set; }

        [Display(Name ="Upload a profile picture")]
        public IFormFile AvatarImage { get; set; }

        public string AvatarImageUrl { get; set; }

        public string StatusMessage { get; set; }

        [Display (Name = "Paste YouTube Embed code for profile video")]
        public string ProfileVideoUrl { get; set; }
    }
}
