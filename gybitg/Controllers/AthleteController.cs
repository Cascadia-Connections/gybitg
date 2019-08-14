using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gybitg.Data;
using gybitg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using gybitg.Services;
using gybitg.Models.ManageViewModels;
using gybitg.Models.SearchViewModels;

namespace gybitg.Controllers
{
    public class AthleteController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AthleteController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AthleteController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }
       

        //// GET: Athlete
        //public IActionResult Index(string id)
        //{
            //int a = 1;
            //return View(id, a);
            //return View(await _context.AthleteProfiles.ToListAsync());
        //}

        // GET: Athlete/Details/5
        [HttpGet]
        public IActionResult Index(string id) //, int i)
        {
          
            //var athleteProfile = await _context.AthleteProfiles.SingleOrDefaultAsync(m => m.UserId == id);
            //var au =  _context.AthleteUserViewModel.SingleOrDefault(m => m.UserId == id);
            var athleteProfile = _context.AthleteProfiles.SingleOrDefault(p => p.UserId == id);
            var athleteStats =  _context.AthleteStats.SingleOrDefault(m => m.UserId == id);

            var user = _userManager.Users.SingleOrDefault(u => u.Id == id);
           
            AthleteUserViewModel au = new AthleteUserViewModel();
            if (au == null)
            {
                return NotFound();
            }
            au.UserId = id;
            au.FirstName = user.FirstName;
            au.LastName = user.LastName;
            au.Position = user.Position;
            au.AvatarImageUrl = user.AvatarImageUrl;
            au.ProfileVideoUrl = user.ProfileVideoUrl;
            au.DateOfBirth = athleteProfile.DateOfBirth;
            au.Height = athleteProfile.Height;
            au.Weight = athleteProfile.Weight;
            au.HighschoolName = athleteProfile.HighschoolName;
            au.HSGraduationDate = athleteProfile.HSGraduationDate;
            au.PPG = athleteStats.PPG;
            au.RPG = athleteStats.PPG;
            au.FGAG = athleteStats.FGAG;
            au.FGG = athleteStats.FGG;
            au.FGMG = athleteStats.FGMG;
            au.GP = athleteStats.GP;
            
            return View(au);

        }

        // GET: Athlete/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Athlete/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,DateOfBirth,Height,Weight,PersnalBio,HighschoolName,HighschoolCoach,HSGraduationDate,AAUId")] AthleteProfile athleteProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(athleteProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(athleteProfile);
        }

        // GET: Athlete/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athleteProfile = await _context.AthleteProfiles.SingleOrDefaultAsync(m => m.UserId == id);
            if (athleteProfile == null)
            {
                return NotFound();
            }
            return View(athleteProfile);
        }

        // POST: Athlete/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,DateOfBirth,Height,Weight,PersnalBio,HighschoolName,HighschoolCoach,HSGraduationDate,AAUId")] AthleteProfile athleteProfile)
        {
            if (id != athleteProfile.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(athleteProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AthleteProfileExists(athleteProfile.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(athleteProfile);
        }

        // GET: Athlete/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athleteProfile = await _context.AthleteProfiles
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (athleteProfile == null)
            {
                return NotFound();
            }

            return View(athleteProfile);
        }

        // POST: Athlete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var athleteProfile = await _context.AthleteProfiles.SingleOrDefaultAsync(m => m.UserId == id);
            _context.AthleteProfiles.Remove(athleteProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AthleteProfileExists(string id)
        {
            return _context.AthleteProfiles.Any(e => e.UserId == id);
        }

        private bool AthleteUserViewModelExists(string id)
        {
            return _context.AthleteUserViewModel.Any(e => e.UserId == id);
        }
    }
}
