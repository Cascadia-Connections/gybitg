using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gybitg.Models.Repositories
{
    public interface IAthleteRepository
    {
        IQueryable<AthleteProfile> athleteProfiles { get; }
        //IQueryable<AthleteStats> athleteStats { get; }
    }
}
