﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gybitg.Models.SearchViewModels;
using gybitg.Models;
using gybitg.Models.Repositories;
using gybitg.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using gybitg.Services;
using gybitg.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace gybitg.Controllers
{
    [Authorize(Roles = "Coach")]
    public class SearchController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ApplicationDbContext _context;
        private readonly IAthleteRepository _athleteRepository;

        public SearchController(
            UserManager<ApplicationUser> userManager,
            //ApplicationDbContext context,
            IAthleteRepository athleteRepository)
        {
            _userManager = userManager;
            //_context = context;
            _athleteRepository = athleteRepository;
        }

        //Basic Search [post]
        //Basic athlete search method - used for searchbar
        [HttpPost] //Post method for the BasicAthleteSearch --- Adam
        public IActionResult BasicSearch(string SearchParam)
        {
            SearchViewModel basic = new SearchViewModel();

            basic.Name = SearchParam;
            basic.HighSchool = SearchParam;

            //return RedirectToAction("SearchResults", basic);
            //Using a new BasicSearchResults method to keep basic search seperate from adv search
            return RedirectToAction("BasicSearchResults", basic);
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
            //Currently a position must be selected for the adv search to run, maybe this should be changed or a message appear letting the user know
            // they need to select a position
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

        //I found it easier to seperate Basic Search and Adv Search
        [HttpGet]
        public async Task<IActionResult> BasicSearchResults(SearchViewModel SearchParam)
        {
            IList<ApplicationUser> athleteUsers = await _userManager.GetUsersInRoleAsync("Athlete");
            List<SearchResultsViewModel> athletes = new List<SearchResultsViewModel>();
            foreach (var a in athleteUsers)
            {
                if (SearchParam.Name != null)
                {
                    if (a.FullName.ToLower().Contains(SearchParam.Name.ToLower()) == true ||
                        _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolName == SearchParam.HighSchool)
                    {
                        SearchResultsViewModel srA = new SearchResultsViewModel();
                        srA.FullName = a.FullName;
                        srA.Position = a.Position;
                        srA.HSGraduationDate = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HSGraduationDate;
                        srA.HighSchool = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolName;
                        srA.AAUId = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUId;
                        srA.HighScoolCoach = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolCoach;
                        srA.AAUCoach = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUCoach;
                        athletes.Add(srA);
                    }
                }
            }
            return View("SearchResults", athletes);
        }

        //IMPORTANT: Parameters should be passed from the AdvancedSearch post method
        [HttpGet]
        public async Task<IActionResult> SearchResults(SearchViewModel SearchParam)
        {
            string roleName = "Athlete";
            IList<ApplicationUser> athleteUsers = await _userManager.GetUsersInRoleAsync(roleName);
            List<SearchResultsViewModel> athletes = new List<SearchResultsViewModel>();            //Splits up SearchViewModel SearchParam in to components to save typing later
            string SearchName = SearchParam.Name;
            string SearchPosition;

            //The following logic is for Advanced Search logic
            if (SearchParam.Position.ToString() != "--Select--")
            {
                SearchPosition = SearchParam.Position.ToString();
            }
            else if (SearchParam.Position.ToString() == "All")
            {
                SearchPosition = "All";
            }
            else
            {
                SearchPosition = null;
            }
            string SearchGraduation = SearchParam.HSGraduationDate;
            string SearchHS = SearchParam.HighSchool;
            string SearchHSCoach = SearchParam.HighScoolCoach;
            string SearchAAUCoach;
            string SearchAAU;
            if (SearchParam.AAUCoach == null)
            {
                SearchAAUCoach = "";
            }
            else
            {
                SearchAAUCoach = SearchParam.AAUCoach;
            }
            if (SearchParam.AAUId == null)
            {
                SearchAAU = "";
            }
            else
            {
                SearchAAU = SearchParam.AAUId;
            }

            //This is default search list for all althetes in any position since the user has only searched by "All" positions.
            if (SearchName == null && SearchGraduation == null && SearchHS == null && SearchAAU == "" && SearchHSCoach == null
                && SearchAAUCoach == "" && SearchPosition == "All")
            {
                foreach (var a in athleteUsers)
                {
                    char[] delimiterChars = { '/' };
                    var HgradDate = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HSGraduationDate;

                    if (HgradDate == null)
                    {
                    }
                    else
                    {
                        string[] words = _athleteRepository.athleteProfiles.SingleOrDefault(ap => ap.UserId == a.Id).HSGraduationDate.Split(delimiterChars);
                        string athleteGradDate = words[0] + "/" + words[1];

                        SearchResultsViewModel srA = new SearchResultsViewModel();
                        srA.UserId = a.Id;
                        srA.FullName = a.FullName;
                        srA.Position = a.Position;
                        srA.HSGraduationDate = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HSGraduationDate;
                        srA.HighSchool = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolName;
                        srA.AAUId = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUId;
                        srA.HighScoolCoach = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolCoach;
                        srA.AAUCoach = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUCoach;
                        athletes.Add(srA);
                    }
                }
                //if no users were added to the athletes list, no results were found
                if (athletes == null)
                {
                    ViewBag.Error = "No results were found";//populates viewbag with error message
                    return View("AdvancedSearch");
                }
                return View(athletes);
            }

            //Start of a new Advanced Search Prototype (Adam)
            foreach (var a in athleteUsers)
            {
                char[] delimiterChars = { '/' };
                var HgradDate = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HSGraduationDate;

                if (HgradDate == null)
                { }
                else
                {
                    string[] words = _athleteRepository.athleteProfiles.SingleOrDefault(ap => ap.UserId == a.Id).HSGraduationDate.Split(delimiterChars);
                    string athleteGradDate = words[0] + "/" + words[1];

                    if (a.FirstName == SearchName || a.LastName == SearchName || SearchName == null)
                    {
                        if (athleteGradDate == SearchGraduation || SearchGraduation == null)
                        {
                            if (_athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolName == SearchHS || SearchHS == null)
                            {
                                if (_athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUId == SearchAAU || SearchAAU == "")
                                {
                                    if (_athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolCoach == SearchHSCoach || SearchHSCoach == null)
                                    {
                                        if (_athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUCoach == SearchAAUCoach || SearchAAUCoach == "")
                                        {
                                            if (a.Position == SearchPosition || SearchPosition == "All")
                                            {
                                                SearchResultsViewModel srA = new SearchResultsViewModel();
                                                srA.UserId = a.Id;
                                                srA.FullName = a.FullName;
                                                srA.Position = a.Position;
                                                srA.HSGraduationDate = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HSGraduationDate;
                                                srA.HighSchool = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolName;
                                                srA.AAUId = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUId;
                                                srA.HighScoolCoach = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolCoach;
                                                srA.AAUCoach = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUCoach;
                                                athletes.Add(srA);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
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
