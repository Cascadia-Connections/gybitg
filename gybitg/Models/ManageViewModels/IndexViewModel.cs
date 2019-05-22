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

        [Required]
        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(20, ErrorMessage = "First Name should be less than or equal to twenty characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        public virtual string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Required]
        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [StringLength(20, ErrorMessage = "Last Name should be less than or equal to twenty characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Position { get; set; }

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
