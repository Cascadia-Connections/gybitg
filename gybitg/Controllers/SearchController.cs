using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gybitg.Models.SearchViewModels;
using gybitg.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace gybitg.Controllers
{
    public class SearchController : Controller
    {
        private IAthleteProfileRepository athleteRepository;
        private IApplicationUserRepository userRepository;
        private IAthleteStatsRepository statsRepository;

        public ViewResult SearchResults()         
            => View(new SearchResultsViewModel
            {

            });
        
    }
}
