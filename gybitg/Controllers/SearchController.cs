/*File created by Daniel Watkins*/
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

namespace gybitg.Controllers
{
    public class SearchController : Controller
    {

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

        //Basic Search [get][post]
        //Basic athlete search method
        [HttpGet] // --- Adam
        public IActionResult BasicSearch()
        {
            return View();
        }

        [HttpPost] //Post method for the BasicAthleteSearch --- Adam
        public IActionResult BasicSearch(SearchViewModel athleteSearched)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SearchResults", athleteSearched);
            }
            else
            {
                //there is something wrong with the data values
                return View(athleteSearched);
            }
        }

        [HttpPost]
        public IActionResult AdvancedSearch(SearchViewModel athleteSearched)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SearchResults", athleteSearched);
            }
            else
            {
                //there is something wrong with the data values
                return View(athleteSearched);
            }
        }

        //IMPORTANT: Parameters should be passed from the AdvancedSearch post method and the BasicSearch post method
        [HttpGet]
        public async Task<IActionResult> SearchResults(SearchViewModel SearchParam)
        {
            //Next two lines splits the athlete users from the coach users 
            string roleName = "Athlete";
            var usersOfRole = await _userManager.GetUsersInRoleAsync(roleName);

            //Splits up SearchViewModel SearchParam in to components to save typing later
            string SearchName = SearchParam.Name;
            string SearchPosition = SearchParam.Position.ToString();
            DateTime SearchGraduation = SearchParam.HSGraduationDate;
            decimal SearchPPG = SearchParam.PPG;
            decimal SearchMPG = SearchParam.MPG;
            decimal SearchTPMG = SearchParam.TPMG;
            decimal SearchFTMG = SearchParam.FTMG;

            List<SearchResultsViewModel> athletes = new List<SearchResultsViewModel>();

            /*This if statement checks to see that at least one search parameters is not default*/
            if (!string.IsNullOrEmpty(SearchName) || !string.IsNullOrEmpty(SearchPosition) || SearchGraduation != DateTime.MinValue 
                || SearchPPG != 0M || SearchMPG != 0M || SearchTPMG != 0M || SearchFTMG != 0M)
            {
                //runs through all athlete users
                foreach(var a in usersOfRole)
                {
                    //Checks to see if any part of the athlete matches the search parameters and if any part does add them to the list of athletes to return
                    if(a.FullName.Contains(SearchName) || a.Position.Contains(SearchPosition) 
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
                //if no users were added to the athletes list, no results were found
                if (athletes == null)
                {
                    ViewBag.Error = "No results were found";//populates viewbag with error message
                    return View("AdvancedSearch");                
                }
            }
            /*default search returns all athletes - only happens when all search fields are left blank*/
            else
            {
                //runs through all athlete users and adds them to the list of athletes to return
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
