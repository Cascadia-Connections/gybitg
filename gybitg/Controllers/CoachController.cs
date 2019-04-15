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
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoachProfiles.ToListAsync());
        }
        public async Task<IActionResult> _SearchPartial()
        {
            return View(await _context.AthleteProfiles.ToListAsync());
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


        
        //Basic athlete search method
        //Not sure if this is the correct idea to use the model below or if that needs to be tweaked
        [HttpPost]
        public IActionResult BasicAthleteSearch(ApplicationUser athletes)
        {
            if (athletes.LastName == null && athletes.FirstName != null)
            {
                var athletesToDisplay = _context.AthleteUserViewModel
                    .Where(a => a.FirstName.Contains(athletes.FirstName));
                return View(athletesToDisplay);
            }
            if (athletes.FirstName == null && athletes.LastName != null)
            {
                var athletesToDisplay = _context.AthleteUserViewModel
                    .Where(a => a.LastName.Contains(athletes.LastName)); 
                return View(athletesToDisplay);
            }
            if (athletes.FirstName != null && athletes.LastName != null && athletes.Position != null)
            {
                var athletesToDisplay = _context.AthleteUserViewModel
                    .Where(a => a.LastName.Contains(athletes.LastName) && 
                                a.FirstName.Contains(athletes.FirstName) &&
                                a.Position == athletes.Position);
                return View(athletesToDisplay);
            }
            if (athletes.FirstName != null && athletes.LastName != null)
            {
                var athletesToDisplay = _context.AthleteUserViewModel
                    .Where(a => a.LastName.Contains(athletes.LastName) &&
                                a.FirstName.Contains(athletes.FirstName));
                return View(athletesToDisplay);
            }
            if (athletes.Position == null)
            {
                //if we come into here no info was entered for athlete search so grab all athletes?
                var athletesToDisplay = _context.AthleteUserViewModel
                    ;
                return View(athletesToDisplay);
            }
            else
            {
                var athletesToDisplay = _context.AthleteUserViewModel
                    .Select(a => a.Position == athletes.Position);
                return View(athletesToDisplay);
            }
        }

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
    

