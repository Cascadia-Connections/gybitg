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

        //Get for AdvancedSearch view
        [HttpGet]
        public IActionResult AdvancedSearch()
        {
            return View();
        }
        //Post for AdvancedSearch view - checks what is entered into the search and if the entries are valid it redirects to SearchResults
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

            decimal SearchMaxPPG = SearchParam.MaxPPG;
            decimal SearchMinPPG = SearchParam.MinPPG;

            decimal SearchMaxMPG = SearchParam.MaxMPG;
            decimal SearchMinMPG = SearchParam.MinMPG;

            decimal SearchMaxTPMG = SearchParam.MaxTPMG;
            decimal SearchMinTPMG = SearchParam.MinTPMG;

            decimal SearchMaxFTMG = SearchParam.MaxFTMG;
            decimal SearchMinFTMG = SearchParam.MinFTMG;

            List<SearchResultsViewModel> athletes = new List<SearchResultsViewModel>();

            /*This if statement checks to see that at least one search parameters is not default*/
            if (!string.IsNullOrEmpty(SearchName) || !string.IsNullOrEmpty(SearchPosition) || SearchGraduation != DateTime.MinValue 
                || SearchMaxPPG != 0M || SearchMaxMPG != 0M || SearchMaxTPMG != 0M || SearchMaxFTMG != 0M
                || SearchMinPPG != 0M || SearchMinMPG != 0M || SearchMinTPMG != 0M || SearchMinFTMG != 0M)
            {
                //runs through all athlete users
                foreach(var a in usersOfRole)
                {
                    //Checks to see if any part of the athlete matches the search parameters and if any part does add them to the list of athletes to return
                    if(a.FullName.Contains(SearchName) || a.Position.Contains(SearchPosition) 
                      || _athleteRepository.HSGraduationDate.Where(ap => ap.UserId == a.Id) == SearchGraduation 
                      || (
                      //Checks to see if both Max and Min values are not default
                      (SearchMaxPPG != 0M && SearchMinPPG != 0M)?
                      //If both Max and Min values are not default search to see if the athlete stat is between the two
                      (_statsRepository.PPG.Where(ap => ap.UserId == a.Id) <= SearchMaxPPG && _statsRepository.PPG.Where(ap => ap.UserId == a.Id) >= SearchMinPPG)
                      :
                      //If either Max or Min are default need to check based on which one is not default
                      ((SearchMinPPG != 0M)?
                      //If Min is not default search to see if athlete stat is greater than or equal to Min
                      (_statsRepository.PPG.Where(ap => ap.UserId == a.Id) >= SearchMinPPG)
                      :
                      //If Min is default then check if Max is default and if not, search to see if athlete stat is less than or equal to Max, and if Max is default move on to next stat
                      ((SearchMaxPPG != 0M) && (_statsRepository.PPG.Where(ap => ap.UserId == a.Id) <= SearchMaxPPG))))
                      || (
                      (SearchMaxMPG != 0M && SearchMinMPG != 0M) ?
                      (_statsRepository.MPG.Where(ap => ap.UserId == a.Id) <= SearchMaxMPG && _statsRepository.MPG.Where(ap => ap.UserId == a.Id) >= SearchMinMPG)
                      :
                      ((SearchMinMPG != 0M) ?
                      (_statsRepository.MPG.Where(ap => ap.UserId == a.Id) >= SearchMinMPG)
                      :
                      ((SearchMaxMPG != 0M) && (_statsRepository.MPG.Where(ap => ap.UserId == a.Id) <= SearchMaxMPG))))
                      || (
                      (SearchMaxTPMG != 0M && SearchMinTPMG != 0M) ?
                      (_statsRepository.TPMG.Where(ap => ap.UserId == a.Id) <= SearchMaxTPMG && _statsRepository.TPMG.Where(ap => ap.UserId == a.Id) >= SearchMinTPMG)
                      :
                      ((SearchMinTPMG != 0M) ?
                      (_statsRepository.TPMG.Where(ap => ap.UserId == a.Id) >= SearchMinTPMG)
                      :
                      ((SearchMaxTPMG != 0M) && (_statsRepository.TPMG.Where(ap => ap.UserId == a.Id) <= SearchMaxTPMG))))
                      || (
                      (SearchMaxFTMG != 0M && SearchMinFTMG != 0M) ?
                      (_statsRepository.FTMG.Where(ap => ap.UserId == a.Id) <= SearchMaxFTMG && _statsRepository.FTMG.Where(ap => ap.UserId == a.Id) >= SearchMinFTMG)
                      :
                      ((SearchMinFTMG != 0M) ?
                      (_statsRepository.FTMG.Where(ap => ap.UserId == a.Id) >= SearchMinFTMG)
                      :
                      ((SearchMaxFTMG != 0M) && (_statsRepository.FTMG.Where(ap => ap.UserId == a.Id) <= SearchMaxFTMG))))
                      )
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
        [HttpPost]
        public IActionResult SearchResults(string athleteId)
        {
            //There currently does not exist an athlete profile view that a coach can view so this currently redirects to nothing
            return RedirectToAction("AthleteProfile", athleteId);
        }
    }
}
