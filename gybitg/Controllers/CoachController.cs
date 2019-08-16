using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gybitg.Data;
using gybitg.Models;
using gybitg.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using gybitg.Services;




namespace gybitg.Controllers
{
    public class CoachController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public CoachController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            ILogger<CoachController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }
        

       

        // GET: Coach
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoachProfiles.ToListAsync());
        }
        public async Task<IActionResult> _SearchPartial()
        {
            return View(await _context.AthleteProfiles.ToListAsync());
        }

       
        [HttpGet]
        public IActionResult AthleteList(string id)
        {

            id = _userManager.GetUserId(User);

            var _userProfile = _context.AthleteProfiles.SingleOrDefault(p => p.UserId == id);

            var user = _userManager.Users.SingleOrDefault(u => u.Id == id);
            //string roleName = "Athlete";

            //var usersOfRole = await _userManager.GetUsersInRoleAsync(roleName);

            //List<AthleteUserViewModel> athletes = new List<AthleteUserViewModel>();

            //foreach (var u in usersOfRole)
            //{
                AthleteUserViewModel au = new AthleteUserViewModel();
                au.UserId = user.Id;
                au.FirstName = user.FirstName;
                au.LastName = user.LastName;
                au.Position = user.Position;
                au.AvatarImageUrl = user.AvatarImageUrl;
                au.ProfileVideoUrl = user.ProfileVideoUrl;
                au.DateOfBirth = _userProfile.DateOfBirth;
                au.Height = _userProfile.Height;
                au.Weight = _userProfile.Weight;
                au.HighschoolName = _userProfile.HighschoolName;
                au.HSGraduationDate = _userProfile.HSGraduationDate;
            
               

            //    athletes.Add(au);


            //}

            return View(au);

        }

        [HttpGet]
        public async Task<IActionResult> FollowList(string coachId)
        {
            //var AthleteUsers = _userManager.GetUsersInRoleAsync("Athlete");

            IList<ApplicationUser> athleteUsers = await _userManager.GetUsersInRoleAsync("Athlete");
            List<AthleteUserViewModel> athletes = new List<AthleteUserViewModel>();
            foreach(var athlete in athleteUsers)
            {
                var followed = _context.CoachAthletes.Single(ath => ath.CoachId == coachId);
                if (athlete.Id == followed.AthleteId)
                {
                    AthleteUserViewModel au = new AthleteUserViewModel();
                    au.UserId = followed.AthleteId;
                    au.Position = athlete.Position;
                    au.FirstName = athlete.FirstName;
                    au.LastName = athlete.LastName;

                    athletes.Add(au);
                }   
            }
            return View(athletes);
        }

        //This allows the coachId and athleteId to be linked in the database, the current coach is now "following the selected athlete"
        [HttpGet]
        public IActionResult Follow(string id)
        {
            var athletefollowed = _context.CoachAthletes.Single(a => a.AthleteId == id);

            CoachAthlete following = new CoachAthlete();
            following.AthleteId = id;
            following.Athlete = _context.AthleteProfiles.SingleOrDefault(a => a.UserId == id);

            following.CoachId = _userManager.GetUserId(User);
            following.Coach = _context.CoachProfiles.SingleOrDefault(c => c.UserId == following.CoachId);

            //need to add a check and make sure the coach is not already following the selected athlete

            try
            {
                _context.CoachAthletes.Add(following);
                _context.SaveChanges();
            }
            catch { }

            return RedirectToAction("AthleteList");//, following.CoachId);
            //return View();
        }

        [HttpGet]
        public IActionResult AthleteList()
        {
            return View();
        }

        /*[HttpGet]
        public IActionResult Follow(string id)
        {
            string roleName = "Athlete";

            var usersOfRole = await _userManager.GetUsersInRoleAsync(roleName);

            List<AthleteUserViewModel> athletes = new List<AthleteUserViewModel>();

            foreach (var u in usersOfRole)
            {
                AthleteUserViewModel au = new AthleteUserViewModel();
                au.UserId = u.UserName;
                au.FirstName = u.FirstName;
                au.LastName = u.LastName;
                au.Position = u.Position;
                au.AvatarImageUrl = u.AvatarImageUrl;
                au.ProfileVideoUrl = u.ProfileVideoUrl;

                athletes.Add(au);


            }

            return View();

        }*/

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


    }


}
    

