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

            return RedirectToAction("SearchResults", basic);
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

            string roleName = "Athlete";
            IList<ApplicationUser> athleteUsers = await _userManager.GetUsersInRoleAsync(roleName);
            //Splits up SearchViewModel SearchParam in to components to save typing later
            string SearchName = SearchParam.Name;
            string SearchPosition;
            if(SearchParam.Position.ToString() != "Default")
            {
                SearchPosition = SearchParam.Position.ToString();
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
            if(SearchParam.AAUId == null)
            {
                SearchAAU = "";
            }
            else
            {
                SearchAAU = SearchParam.AAUId;
            }

            List<SearchResultsViewModel> athletes = new List<SearchResultsViewModel>();

            /*This if statement checks to see that at least one search parameters is not default*/
            if (!string.IsNullOrEmpty(SearchName) || !string.IsNullOrEmpty(SearchPosition) || !string.IsNullOrEmpty(SearchGraduation)
                || !string.IsNullOrEmpty(SearchHS) || SearchAAU != "" || !string.IsNullOrEmpty(SearchHSCoach) || SearchAAUCoach != "")
            {
                //runs through all athlete users
                foreach(var a in athleteUsers)
                {
                    char[] delimiterChars = { '/' };
                    string[] words = _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HSGraduationDate.Split(delimiterChars);
                    string athleteGradDate = words[0] + "/" + words[2];

                    //Checks to see if any part of the athlete matches the search parameters and if any part does add them to the list of athletes to return
                    if (a.FullName == SearchName || a.Position == SearchPosition
                      || athleteGradDate == SearchGraduation 
                      || _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolName == SearchHS
                      || _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUId == SearchAAU
                      || _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).HighschoolCoach == SearchHSCoach
                      || _athleteRepository.athleteProfiles.SingleOrDefault<AthleteProfile>(ap => ap.UserId == a.Id).AAUCoach == SearchAAUCoach)

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
                foreach (var a in athleteUsers)
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
