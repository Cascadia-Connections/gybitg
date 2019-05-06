using System;
using System.Collections.Generic;
using Bogus;
using System.Linq;
using System.Threading.Tasks;
using gybitg.Data;
using gybitg.Models;
using gybitg.Models.ManageViewModels;
using Microsoft.Extensions.DependencyInjection;
using gybitg.Models.Repositories;

namespace gybitg
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await Seed(services.GetRequiredService<ApplicationDbContext>());
        }

        public static async Task Seed(ApplicationDbContext context)
        {
            if (context.AthleteUserViewModel.Any())
            {
                return; //already has data, don't add any more test data
            }
            /*if (context.athleteStats.Any())//context.AthleteStats.Any())
            {
                return;
            }*/

            //Removed some unnecessary comments from previous ideas.


            // 
            ApplicationUser user1 = new ApplicationUser();
            user1.FirstName = "Athlete";
            user1.LastName = "1";
            ApplicationUser user2 = new ApplicationUser();
            user2.FirstName = "Athlete";
            user2.LastName = "2";

            ApplicationUser user3 = new ApplicationUser();
            user3.FirstName = "Coach";
            user3.LastName = "1";
            


            context.Users.AddRange(user1, user2, user3);
            context.SaveChanges();


            /* Commented out, will not seed the DB, rather using a FakeAthleteRepository
             * 
            AthleteUserViewModel athlete = new AthleteUserViewModel();
            athlete.FirstName = "Adam";
            athlete.LastName = "A";
            athlete.Position = "Point Guard";
            athlete.UserId = "0";
            AthleteStats stats = new AthleteStats();
            stats.APG = 4;
            stats.FGAG = 7;
            stats.FGG = stats.FGAG / stats.FGMG;
            stats.FGMG = 6;
            stats.FTAG = 10;
            stats.FTMG = 10;
            stats.FTP = stats.FTAG / stats.FTMG;
            stats.GP = 6;
            stats.GS = 2;
            stats.MPG = 32;
            stats.PPG = 24;
            stats.RPG = 3;
            stats.TPAG = 7;
            stats.TPMG = 4;
            stats.TPP = stats.TPAG / stats.TPMG;
            stats.UserId = athlete.UserId;
            //context.AthleteStats.Add(stats);
            //context.AthleteUserViewModel.Add(athlete);

            //athlete 1
            AthleteUserViewModel athlete1 = new AthleteUserViewModel();
            athlete1.FirstName = "Alex";
            athlete1.LastName = "B";
            athlete1.Position = "Center";
            athlete.UserId = "1";
            AthleteStats stats1 = new AthleteStats();
            stats1.APG = 3;
            stats1.FGAG = 4;
            stats1.FGG = stats.FGAG / stats.FGMG;
            stats1.FGMG = 8;
            stats1.FTAG = 12;
            stats1.FTMG = 12;
            stats1.FTP = stats.FTAG / stats.FTMG;
            stats1.GP = 8;
            stats1.GS = 1;
            stats1.MPG = 35;
            stats1.PPG = 22;
            stats1.RPG = 7;
            stats1.TPAG = 4;
            stats1.TPMG = 2;
            stats1.TPP = stats.TPAG / stats.TPMG;
            stats1.UserId = athlete1.UserId;
            //context.AthleteStats.Add(stats1);
            //context.AthleteUserViewModel.Add(athlete1);

            //athlete 2
            AthleteUserViewModel athlete2 = new AthleteUserViewModel();
            athlete2.FirstName = "Kevin";
            athlete2.LastName = "C";
            athlete2.Position = "Shooting Guard";
            athlete.UserId = "2";
            AthleteStats stats2 = new AthleteStats();
            stats2.APG = 7;
            stats2.FGAG = 10;
            stats2.FGG = stats.FGAG / stats.FGMG;
            stats2.FGMG = 8;
            stats2.FTAG = 12;
            stats2.FTMG = 10;
            stats2.FTP = stats.FTAG / stats.FTMG;
            stats2.GP = 8;
            stats2.GS = 1;
            stats2.MPG = 35;
            stats2.PPG = 22;
            stats2.RPG = 7;
            stats2.TPAG = 4;
            stats2.TPMG = 2;
            stats2.TPP = stats.TPAG / stats.TPMG;
            stats2.UserId = athlete2.UserId;
            //context.AthleteStats.Add(stats2);
            //context.AthleteUserViewModel.Add(athlete2);

            //athlete 3
            AthleteUserViewModel athlete3 = new AthleteUserViewModel();
            athlete3.FirstName = "Daniel";
            athlete3.LastName = "D";
            athlete3.Position = "Small Foward";
            athlete.UserId = "3";
            AthleteStats stats3 = new AthleteStats();
            stats3.APG = 7;
            stats3.FGAG = 10;
            stats3.FGG = stats.FGAG / stats.FGMG;
            stats3.FGMG = 8;
            stats3.FTAG = 12;
            stats3.FTMG = 10;
            stats3.FTP = stats.FTAG / stats.FTMG;
            stats3.GP = 8;
            stats3.GS = 1;
            stats3.MPG = 35;
            stats3.PPG = 22;
            stats3.RPG = 7;
            stats3.TPAG = 4;
            stats3.TPMG = 2;
            stats3.TPP = stats.TPAG / stats.TPMG;
            stats3.UserId = athlete3.UserId;
            //context.AthleteStats.Add(stats3);
            //context.AthleteUserViewModel.Add(athlete3);

            //athlete 4
            AthleteUserViewModel athlete4 = new AthleteUserViewModel();
            athlete4.FirstName = "Keith";
            athlete4.LastName = "E";
            athlete4.Position = "Power Foward";
            athlete.UserId = "4";
            AthleteStats stats4 = new AthleteStats();
            stats2.APG = 7;
            stats2.FGAG = 10;
            stats2.FGG = stats.FGAG / stats.FGMG;
            stats2.FGMG = 8;
            stats2.FTAG = 12;
            stats2.FTMG = 10;
            stats2.FTP = stats.FTAG / stats.FTMG;
            stats2.GP = 8;
            stats2.GS = 1;
            stats2.MPG = 35;
            stats2.PPG = 22;
            stats2.RPG = 7;
            stats2.TPAG = 4;
            stats2.TPMG = 2;
            stats2.TPP = stats.TPAG / stats.TPMG;
            stats4.UserId = athlete4.UserId;
            //context.AthleteStats.Add(stats4);
            //context.AthleteUserViewModel.Add(athlete4);
            */

            //context.SaveChanges();
            
        }
    }
}