using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gybitg.Models;
using gybitg.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gybitg.Data;

namespace gybitg.Controllers
{
    public class CoachController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public CoachController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          RoleManager<IdentityRole> roleManager,
          ApplicationDbContext context
         )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AthleteList()
        {
            string roleName = "Athlete";

            var usersOfRole = await _userManager.GetUsersInRoleAsync(roleName);

            List<AthleteUserViewModel> athletes = new List<AthleteUserViewModel>();

            foreach (var u in usersOfRole)
            {
                AthleteUserViewModel au = new AthleteUserViewModel();
                au.UserId = u.Id;
                au.FirstName = u.FirstName;
                au.LastName = u.LastName;
                au.Position = u.Position;
                au.AvatarImageUrl = u.AvatarImageUrl;
                au.ProfileVideoUrl = u.ProfileVideoUrl;

                athletes.Add(au);


            }

            return View(athletes);

        }

        [HttpGet]
        public IActionResult Follow()
        {
            //string roleName = "Athlete";

            //var usersOfRole = await _userManager.GetUsersInRoleAsync(roleName);

            //List<AthleteUserViewModel> athletes = new List<AthleteUserViewModel>();

            //foreach (var u in usersOfRole)
            //{
            //    AthleteUserViewModel au = new AthleteUserViewModel();
            //    au.UserId = u.UserName;
            //    au.FirstName = u.FirstName;
            //    au.LastName = u.LastName;
            //    au.Position = u.Position;
            //    au.AvatarImageUrl = u.AvatarImageUrl;
            //    au.ProfileVideoUrl = u.ProfileVideoUrl;

            //    athletes.Add(au);


            //}

            return View();

        }

        //[HttpPost]
        //public async Task<IActionResult> AthleteList(string SearchString)
        //{
        //    string roleName = "Athlete";

        //    var usersOfRole = await _userManager.GetUsersInRoleAsync(roleName);

        //         List<AthleteUserViewModel> searchusers = new List<AthleteUserViewModel>();

        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        foreach (var u in usersOfRole)
        //    {
        //            if (u.LastName.Contains(SearchString) || u.FirstName.Contains(SearchString)|| u.Position.Contains(SearchString))
        //            {
        //                AthleteUserViewModel sau = new AthleteUserViewModel();
        //                sau.FirstName = u.FirstName;
        //                sau.LastName = u.LastName;
        //                sau.Position = u.Position;
        //                sau.AvatarImageUrl = u.AvatarImageUrl;
        //                sau.ProfileVideoUrl = u.ProfileVideoUrl;
        //                searchusers.Add(sau);
        //            }

        //    } if (string.IsNullOrEmpty(SearchString))
        //            foreach (var u in usersOfRole)
        //            {

        //                    AthleteUserViewModel sau = new AthleteUserViewModel();
        //                    sau.FirstName = u.FirstName;
        //                    sau.LastName = u.LastName;
        //                    sau.Position = u.Position;
        //                    sau.AvatarImageUrl = u.AvatarImageUrl;
        //                    sau.ProfileVideoUrl = u.ProfileVideoUrl;
        //                    searchusers.Add(sau);


        //            }

        //    }
        //    return View(searchusers);
        //}



        [HttpPost]
        public IActionResult Follow(string UserId)
        {
         

            CoachAthlete following = new CoachAthlete();
            following.AthleteId = UserId;
            following.Athlete = _context.AthleteProfiles.SingleOrDefault(a => a.UserId == UserId);
           
            following.CoachId = _userManager.GetUserId(User);
            following.Coach = _context.CoachProfiles.SingleOrDefault(c => c.UserId == following.CoachId);

            _context.CoachAthletes.Add(following);
            _context.SaveChanges();

            return View();

        }
    }


}