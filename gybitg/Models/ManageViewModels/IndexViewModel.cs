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
        [StringLength(40, ErrorMessage = "Email Address must be less than or equal to fourty characters.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(20, ErrorMessage = "First Name must be less than or equal to twenty characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(20, ErrorMessage = "Last Name must be less than or equal to twenty characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public virtual string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

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

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(40, ErrorMessage = "City must be less than or equal to fourty characters.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(40, ErrorMessage = "State must be less than or equal to fourty characters.")]
        [Display(Name = "State")]
        public string State { get; set; }

        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        //[FileExtensions(Extensions ="jpg,jpeg,png", ErrorMessage = "Upload only .jpg .jpeg .png files")]
        //Commented out FileExtensions above because jpg,jpeg,png don't get allowed to be uploaded like they should
        [Display(Name ="Upload a profile picture")] 
        public IFormFile AvatarImage { get; set; }

        [Url]// Does this even get used anywhere? Remove?
        public string AvatarImageUrl { get; set; }

        [RegularExpression("^[a-zA-Z0-9 .&'-]*$", ErrorMessage = "Only Alphabetical, Numerical, and .&'- characters are allowed.")]
        [StringLength(40, ErrorMessage = "Status Message must be less than or equal to fourty characters.")]
        [Display(Name = "Status Message")]
        public string StatusMessage { get; set; }

        [Url]
        [StringLength(50, ErrorMessage = "Video URL must be less than or equal to fifty characters.")]
        [Display(Name = "Paste YouTube Embed code for profile video")]
        public string ProfileVideoUrl { get; set; }
    }
}
