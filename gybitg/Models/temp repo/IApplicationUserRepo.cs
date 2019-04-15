using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gybitg.Models
{
    public interface IApplicationUserRepo
    {
        IQueryable<ApplicationUser> Athletes { get; }
    }
}
