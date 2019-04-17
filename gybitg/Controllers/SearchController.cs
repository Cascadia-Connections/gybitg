using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gybitg.Models.SearchViewModels;
using gybitg.Models;
using gybitg.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using gybitg.Services;
using gybitg.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace gybitg.Controllers
{
    public class SearchController : Controller
    {
        //Start of old Idea
        //private IAthleteProfileRepository athleteRepository;
        //private IApplicationUserRepository userRepository;
        //private IAthleteStatsRepository statsRepository;

        //public ViewResult SearchResults(string SearchName, string SearchPosition, DateTime SearchGraduation, decimal SearchPPG, decimal SearchMPG, decimal SearchTPMG, decimal SearchFTMG)         
        //    => View(new SearchResultsViewModel
        //    {

        //    });
        //Start of new Idea
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IAthleteStatsRepository _statsRepository;
        private readonly IAthleteProfileRepository _athleteRepository;

        public SearchController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IAthleteStatsRepository statsRepository,
            IAthleteProfileRepository athleteRepository)
        {
            _userManager = userManager;
            _context = context;
            _statsRepository = statsRepository;
            _athleteRepository = athleteRepository;
        }

        //Parameters should be passed from the AdvancedSearch post method and the BasicSearch post method
        [HttpGet]
        public async Task<IActionResult> SearchResults(string SearchName, string SearchPosition, DateTime SearchGraduation, decimal SearchPPG, decimal SearchMPG, decimal SearchTPMG, decimal SearchFTMG)
        {
            //Next two lines splits the athlete users from the coach users 
            string roleName = "Athlete";
            var usersOfRole = await _userManager.GetUsersInRoleAsync(roleName);

            List<SearchResultsViewModel> athletes = new List<SearchResultsViewModel>();

            /*This if statement checks to see that at least some search parameters are not default*/
            if (!string.IsNullOrEmpty(SearchName) || !string.IsNullOrEmpty(SearchPosition) || SearchGraduation != DateTime.MinValue 
                || SearchPPG != 0M || SearchMPG != 0M || SearchTPMG != 0M || SearchFTMG != 0M)
            {
                //runs through all athlete users
                foreach(var a in usersOfRole)
                {
                    //Checks to see if any part of the athlete matches the search parameters and if any part does add them to the list of athletes to return
                    if(/*a.FirstName.Contains(SearchName) || a.LastName.Contains(SearchName) ||*/ a.FullName.Contains(SearchName) || a.Position.Contains(SearchPosition) 
                      || _athleteRepository.HSGraduationDate.Where(ap => ap.UserId == a.Id) == SearchGraduation || _statsRepository.PPG.Where(ap => ap.UserId == a.Id) == SearchPPG 
                      || _statsRepository.MPG.Where(ap => ap.UserId == a.Id) == SearchMPG || _statsRepository.TPMG.Where(ap => ap.UserId == a.Id) == SearchTPMG 
                      || _statsRepository.FTMG.Where(ap => ap.UserId == a.Id) == SearchFTMG)
                    {
                        SearchResultsViewModel srA = new SearchResultsViewModel();
                        srA.UserId = a.Id;
                        srA.FullName = a.FullName;
                        srA.Position = a.Position;
                        srA.HSGraduationDate = _athleteRepository.HSGraduationDate.Where(ap => ap.UserId == a.Id);
                        srA.PPG = _statsRepository.PPG.Where(ap => ap.UserId == a.Id);
                        srA.MPG = _statsRepository.MPG.Where(ap => ap.UserId == a.Id);
                        srA.TPMG = _statsRepository.TPMG.Where(ap => ap.UserId == a.Id);
                        srA.FTMG = _statsRepository.FTMG.Where(ap => ap.UserId == a.Id);
                        athletes.Add(srA);
                    }
                }
            }
            /*default search returns all athletes*/
            else
            {
                //runs through all athlete users
                foreach (var a in usersOfRole)
                {
                    SearchResultsViewModel srA = new SearchResultsViewModel();
                    srA.FullName = a.FullName;
                    srA.Position = a.Position;
                    srA.HSGraduationDate = _athleteRepository.HSGraduationDate.Where(ap => ap.UserId == a.Id);
                    srA.PPG = _statsRepository.PPG.Where(ap => ap.UserId == a.Id);
                    srA.MPG = _statsRepository.MPG.Where(ap => ap.UserId == a.Id);
                    srA.TPMG = _statsRepository.TPMG.Where(ap => ap.UserId == a.Id);
                    srA.FTMG = _statsRepository.FTMG.Where(ap => ap.UserId == a.Id);
                    athletes.Add(srA);
                }
            }
            return View(athletes);
        }
    }
}
