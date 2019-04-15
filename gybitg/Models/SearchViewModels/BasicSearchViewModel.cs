//File and code created by Kevin Durgan on 4/15/19

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace gybitg.ViewModels
{
    public class BasicSearchViewModel
    {
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        [Display(Name = "Position")]
        public PositionType Position { get; set; }
    }

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
