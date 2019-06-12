using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gybitg.Models
{
    public class CoachAthlete
    {   public string AthleteId { get; set; }
        public AthleteProfile Athlete { get; set; }

        public string CoachId { get; set; }
        public CoachProfile Coach { get; set; }
      
    }
}
