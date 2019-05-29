﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gybitg.Data;

namespace gybitg.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("ApplicationUserId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("gybitg.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AvatarImageUrl");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Position");

                    b.Property<string>("ProfileVideoUrl");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("State");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("gybitg.Models.AthleteProfile", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AAUCoach");

                    b.Property<string>("AAUId");

                    b.Property<string>("DateOfBirth");

                    b.Property<string>("HSGraduationDate");

                    b.Property<decimal>("Height");

                    b.Property<string>("HighschoolCoach");

                    b.Property<string>("HighschoolName");

                    b.Property<string>("PersonalBio");

                    b.Property<decimal>("Weight");

                    b.HasKey("UserId");

                    b.ToTable("AthleteProfiles");
                });

            modelBuilder.Entity("gybitg.Models.AthleteStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("APG");

                    b.Property<DateTime>("DateOFEntry");

                    b.Property<decimal>("FGAG");

                    b.Property<decimal>("FGG");

                    b.Property<decimal>("FGMG");

                    b.Property<decimal>("FTAG");

                    b.Property<decimal>("FTMG");

                    b.Property<decimal>("FTP");

                    b.Property<int>("GP");

                    b.Property<int>("GS");

                    b.Property<decimal>("MPG");

                    b.Property<decimal>("PPG");

                    b.Property<decimal>("RPG");

                    b.Property<decimal>("TPAG");

                    b.Property<decimal>("TPMG");

                    b.Property<decimal>("TPP");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AthleteStats");
                });

            modelBuilder.Entity("gybitg.Models.CoachAthlete", b =>
                {
                    b.Property<string>("CoachId");

                    b.Property<string>("AthleteId");

                    b.HasKey("CoachId", "AthleteId");

                    b.HasIndex("AthleteId");

                    b.ToTable("CoachAthletes");
                });

            modelBuilder.Entity("gybitg.Models.CoachProfile", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AAUId");

                    b.Property<string>("Achievements");

                    b.Property<int>("Losses");

                    b.Property<string>("PersonalBio");

                    b.Property<bool>("Verified");

                    b.Property<int>("Wins");

                    b.Property<decimal>("YearsCoaching");

                    b.HasKey("UserId");

                    b.ToTable("CoachProfiles");
                });

            modelBuilder.Entity("gybitg.Models.ManageViewModels.AthleteUserViewModel", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("APG");

                    b.Property<string>("AvatarImageUrl");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<decimal>("FGAG");

                    b.Property<decimal>("FGG");

                    b.Property<decimal>("FGMG");

                    b.Property<string>("FirstName");

                    b.Property<int>("GP");

                    b.Property<int>("GS");

                    b.Property<DateTime>("HSGraduationDate");

                    b.Property<decimal>("Height");

                    b.Property<string>("HighschoolName");

                    b.Property<string>("LastName");

                    b.Property<decimal>("MPG");

                    b.Property<decimal>("PPG");

                    b.Property<string>("Position");

                    b.Property<string>("ProfileVideoUrl");

                    b.Property<decimal>("RPG");

                    b.Property<string>("Text");

                    b.Property<decimal>("Weight");

                    b.HasKey("UserId");

                    b.ToTable("AthleteUserViewModel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("gybitg.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("gybitg.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("gybitg.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("gybitg.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("gybitg.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gybitg.Models.CoachAthlete", b =>
                {
                    b.HasOne("gybitg.Models.AthleteProfile", "Athlete")
                        .WithMany("CoachAthletes")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("gybitg.Models.CoachProfile", "Coach")
                        .WithMany("CoachAthletes")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
