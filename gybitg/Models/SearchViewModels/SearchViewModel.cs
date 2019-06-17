using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gybitg.Models.SearchViewModels
{
    public class SearchViewModel
    {
        [RegularExpression("^[a-zA-Z .&'-_]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [Display(Name = "Position")]
        public PositionType Position { get; set; }

        [Required]
        [RegularExpression("^[0-9/]{7}$", ErrorMessage = "Only this format allowed: mm/yyyy")]
        [Display(Name = "HS Graduation Date")]
        public string HSGraduationDate { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "High School")]
        public string HighSchool { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "AAU Team")]
        public string AAUId { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "AAU Coach")]
        public string AAUCoach { get; set; }

        [RegularExpression("^[a-zA-Z .&'-]*$", ErrorMessage = "Only Alphabetical characters allowed.")]
        [Display(Name = "High School Coach")]
        public string HighScoolCoach { get; set; }

        //Designate only specific types of Positions
        public enum PositionType
        {
            //[Display(Name = "Not Selected")]
            [Description("Not Selected")]
            NotSelected,
            [Display(Name = "Point Guard")]
            PointGuard,
            [Display(Name = "Shooting Guard")]
            ShootingGuard,
            [Display(Name = "Small Guard")]
            SmallForward,
            [Display(Name = "Power Guard")]
            PowerForward,
            [Display(Name = "Center")]
            Center
        }

        //the following code is an effort to get the app to display the Display or Description names instead of the values.
        //e.g. Not Selected vs NotSelected
        //Code was borrowed from: https://nimblegecko.com/aspnetmvc-dropdowns-with-enums/
        //This is a work in progress (WiP) June/2019

        //private IEnumerable<SelectListItem> GetSelectListItems()
        //{
        //    var selectList = new List<SelectListItem>();

        //    var enumValues = Enum.GetValues(typeof(PositionType)) as PositionType[];
        //    if (enumValues == null)
        //        return null;

        //    foreach(var enumValue in enumValues)
        //    {
        //        selectList.Add(new SelectListItem
        //        {
        //            Value = enumValue.ToString(),
        //            Text = GetPositionName(enumValue)
        //        });
        //    }
        //    return selectList;
        //}

        //private string GetPositionName(PositionType value)
        //{
        //    var positionInfo = value.GetType().GetMember(value.ToString());
        //    if (positionInfo.Length != 1)
        //        return null;

        //    var displayAttribute = positionInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)
        //                            as DisplayAttribute[];
        //    if (displayAttribute == null || displayAttribute.Length != 1)
        //        return null;

        //    return displayAttribute[0].Name;
        //}
    }
}
