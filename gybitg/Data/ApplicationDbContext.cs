using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using gybitg.Models;
using gybitg.Models.ManageViewModels;

namespace gybitg.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AthleteProfile> AthleteProfiles { get; set; }
        public DbSet<AthleteStats> AthleteStats { get; set; }
        public DbSet<CoachProfile> CoachProfiles { get; set; }
        public DbSet<CoachAthlete> CoachAthletes { get; set; }
       
   
       
        //public DbSet<Membership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CoachAthlete>()
               .HasKey(ca => new { ca.CoachId, ca.AthleteId });

            builder.Entity<CoachAthlete>()
                .HasOne(ca => ca.Coach)
                .WithMany(c => c.CoachAthletes)
                .HasForeignKey(ca => ca.CoachId);

            builder.Entity<CoachAthlete>()
                .HasOne(ca => ca.Athlete)
                .WithMany(a => a.CoachAthletes)
                .HasForeignKey(ca => ca.AthleteId);
           



            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        //TODO: on VS-MAC use the reference https://www.ciclosoftware.com/2018/03/14/sql-server-with-net-core-and-entityframework-on-mac/
        //TODO: Update with your Database, User, and Password
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Environment.GetEnvironmentVariable("LOCAL_ENVIRONMENT") == "Mac-Docker")
                optionsBuilder.UseSqlServer("Server=localhost,1433; Database=GYBITGv1;User=SA; Password=P@ssword909");
        }

        //TODO: on VS-MAC use the reference https://www.ciclosoftware.com/2018/03/14/sql-server-with-net-core-and-entityframework-on-mac/
        //TODO: Update with your Database, User, and Password
        public DbSet<gybitg.Models.ManageViewModels.AthleteUserViewModel> AthleteUserViewModel { get; set; }

        //TODO: on VS-MAC use the reference https://www.ciclosoftware.com/2018/03/14/sql-server-with-net-core-and-entityframework-on-mac/
        //TODO: Update with your Database, User, and Password


    }
}
