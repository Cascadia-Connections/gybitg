using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gybitg.Models;

namespace gybitg.Models.SearchViewModels
{
    public class SearchResultsViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<AthleteProfile> AthletesProfile { get; set; }
        public IEnumerable<AthleteStats> AthleteStats { get; set; }
    }
}
