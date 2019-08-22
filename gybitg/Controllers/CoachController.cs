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

        //This allows the coachId and athleteId to be linked in the database, the current coach is now "following the selected athlete"
        [HttpGet]
        public async Task<IActionResult> Follow(string id)
        {
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
            catch { throw new ApplicationException($"Already following that athlete."); }

            //We can have this route to wherever, for right now it would go the a list of 
              //athletes the Coach is following, but that is under development
            return RedirectToAction("AthleteList", id);
        }

        //There have been a couple different attemps and populating a view with a list of athletes that a coach is 
            // "following".  Below are those attempts, currently they do not work.
        [HttpGet]
        public async Task<IActionResult> AthleteList(string coachId)
        {
            // GET: Coach

            var uID = _userManager.GetUserId(User);

            IEnumerable<CoachAthlete> profiles = _context.CoachAthletes.Include(a => a.Athlete).Where(c => c.CoachId == uID);
            List<AthleteProfile> followingAthletes = new List<AthleteProfile>();
            foreach (var p in profiles)
            {
                followingAthletes.Add(p.Athlete);
            }

            return View(followingAthletes);
            /*
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
            */
        }

        /*[HttpGet]
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

        }*/
    }


}
    

