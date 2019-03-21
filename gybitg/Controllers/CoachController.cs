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




namespace gybitg.Controllers
{
    public class CoachController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public CoachController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<CoachController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        // GET: Coach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coachProfile = await _context.CoachProfiles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (coachProfile == null)
            {
                return NotFound();
            }

            return View(coachProfile);
        }

        // GET: Coach/Create
        public IActionResult Create()
        {
            return View();
            //added this to load athletes for view
            //return View(await _context.AthleteProfiles.ToListAsync());
        }

        // POST: Coach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,AAUId,PersnalBio,Wins,Lossess,YearsCoaching,Achievments,Verified")] CoachProfile coachProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coachProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coachProfile);
        }

        // GET: Coach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coachProfile = await _context.CoachProfiles.SingleOrDefaultAsync(m => m.Id == id);
            if (coachProfile == null)
            {
                return NotFound();
            }
            return View(coachProfile);
        }

        // POST: Coach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AAUId,PersnalBio,Wins,Lossess,YearsCoaching,Achievments,Verified")] CoachProfile coachProfile)
        {
            if (id != coachProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coachProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachProfileExists(coachProfile.Id))
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
            return View(coachProfile);
        }

        // GET: Coach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coachProfile = await _context.CoachProfiles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (coachProfile == null)
            {
                return NotFound();
            }

            return View(coachProfile);
        }

        // POST: Coach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coachProfile = await _context.CoachProfiles.SingleOrDefaultAsync(m => m.Id == id);
            _context.CoachProfiles.Remove(coachProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachProfileExists(int id)
        {
            return _context.CoachProfiles.Any(e => e.Id == id);
        }
    }
}
