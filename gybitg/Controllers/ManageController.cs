using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using gybitg.Models;
using gybitg.Data;
using gybitg.Models.ManageViewModels;
using gybitg.Services;

namespace gybitg.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;



        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          RoleManager<IdentityRole> roleManager,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder,
          ApplicationDbContext context,
          IHostingEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _context = context;
            _environment = environment;
        }

        [TempData]
        public string StatusMessage { get; set; }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Position = user.Position,
                City = user.City,
                State = user.State,
                Zip = user.Zip,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            var newFilename = string.Empty;

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Avatar image upload process
            if (model.AvatarImage != null)  
            {
                string PathDB = string.Empty;
                var filename = string.Empty;

                filename = ContentDispositionHeaderValue
                                        .Parse(model.AvatarImage.ContentDisposition)
                                        .FileName
                                        .Trim('"');
                
                //Assigning Unique Filename (Guid)
                var myUniqueFilename = Convert.ToString(Guid.NewGuid());

                //Getting file Extension
                var FileExtension = Path.GetExtension(filename);

                // concating  FileName + FileExtension
                newFilename = myUniqueFilename + FileExtension;

                // Combines two strings into a path.
                filename = Path.Combine(_environment.WebRootPath, "avatars") + $@"\{newFilename}";

                // if you want to store path of folder in database
                PathDB = "avatars/" + newFilename;

                using (FileStream fs = System.IO.File.Create(filename))
                {
                    await model.AvatarImage.CopyToAsync(fs);    // asynchronously copy the file to the avatar folder
                    fs.Flush();
                }

                user.AvatarImageUrl = PathDB;

                var setAvatarResult = await _userManager.UpdateAsync(user);
                if (!setAvatarResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occured setting avatar for the user with ID '{user.Id}'.");
                }
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            if (model.FirstName != user.FirstName || model.LastName != user.LastName)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var setNameResult = await _userManager.UpdateAsync(user);
                if (!setNameResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occured setting name for the user with ID '{user.Id}'.");
                }
            }

            var position = user.Position;
            if (model.Position != user.Position)
            {
                user.Position = model.Position;

                var setPositionResult = await _userManager.UpdateAsync(user);
                if (!setPositionResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occured setting position for the user with ID '{user.Id}'.");
                }
            }

            var city = user.City;
            if (model.City != user.City)
            {
                user.City = model.City;

                var setCityResult = await _userManager.UpdateAsync(user);
                if (!setCityResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occured setting city for the user with ID '{user.Id}'.");
                }
            }

            var state = user.State;
            if (model.State != user.State)
            {
                user.State = model.State;

                var setStateResult = await _userManager.UpdateAsync(user);
                if (!setStateResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occured setting state for the user with ID '{user.Id}'.");
                }
            }

            var zip = user.Zip;
            if (model.Zip != user.Zip)
            {
                user.Zip = model.Zip;

                var setZipResult = await _userManager.UpdateAsync(user);
                if (!setZipResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occured setting zip for the user with ID '{user.Id}'.");
                }
            }

            var profileVideoUrl = user.ProfileVideoUrl;
            if (model.ProfileVideoUrl != user.ProfileVideoUrl)
            {
                user.ProfileVideoUrl = model.ProfileVideoUrl;

                var setProfileVideoUrl = await _userManager.UpdateAsync(user);
                if (!setProfileVideoUrl.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurd setting profile video for the user wiht ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditAthleteProfile(string id)
        {
            id = _userManager.GetUserId(User);

            var _userProfile = _context.AthleteProfiles.SingleOrDefault(p => p.UserId == id);

            var user = _userManager.Users.SingleOrDefault(u => u.Id == id);

            if (_userProfile == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var vModel = new AthleteProfileViewModel
            {
                UserId = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = _userProfile.DateOfBirth,
                Height = _userProfile.Height,
                Weight = _userProfile.Weight,
                PersnalBio = _userProfile.PersnalBio,
                HighschoolName = _userProfile.HighschoolName,
                HighschoolCoach = _userProfile.HighschoolCoach,
                HSGraduationDate = _userProfile.HSGraduationDate,
                AAUId = _userProfile.AAUId,
                StatusMessage = StatusMessage
            };

            return View(vModel);
        }

        [HttpPost]
        public IActionResult EditAthleteProfile(AthleteProfileViewModel vmodel, string id)
        {
            if (!ModelState.IsValid)
            {
                return View(vmodel);
            }

            var userProfile = _context.AthleteProfiles.SingleOrDefault(u => u.UserId == vmodel.UserId);
            id = userProfile.UserId;

            if (userProfile == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var dob = userProfile.DateOfBirth;
            if (vmodel.DateOfBirth != dob)
            {
                userProfile.DateOfBirth = vmodel.DateOfBirth;
            }

            var height = userProfile.Height;
            if (vmodel.Height != height)
            {
                userProfile.Height = vmodel.Height;
            }

            var weight = userProfile.Weight;
            if (vmodel.Weight != weight)
            {
                userProfile.Weight = vmodel.Weight;
            }
            var personalBio = userProfile.PersnalBio;
            if (vmodel.PersnalBio != personalBio)
            {
                userProfile.PersnalBio = vmodel.PersnalBio;
            }

            var hsCoach = userProfile.HighschoolCoach;
            if (vmodel.HighschoolCoach != hsCoach)
            {
                userProfile.HighschoolCoach = vmodel.HighschoolCoach;
            }

            var hsName = userProfile.HighschoolName;
            if (vmodel.HighschoolName != hsName)
            {
                userProfile.HighschoolName = vmodel.HighschoolName;
            }

            var hsGraduation = userProfile.HSGraduationDate;
            if (vmodel.HSGraduationDate != hsGraduation)
            {
                userProfile.HSGraduationDate = vmodel.HSGraduationDate;
            }

            _context.Update(userProfile);
            _context.SaveChanges();

            StatusMessage = "Your profile has been updated.";

            return RedirectToAction(nameof(EditAthleteProfile), "Manage", userProfile, id);
        }

        [HttpGet]
        public IActionResult EditAthleteStats(string id)
        {
            id = _userManager.GetUserId(User);

            var _userStats = _context.AthleteStats.SingleOrDefault(p => p.UserId == id);

            var user = _userManager.Users.SingleOrDefault(u => u.Id == id);

            if (_userStats == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var vModel = new AthleteStatsViewModel
            {
                UserId = id,
                PPG = _userStats.PPG,
                RPG = _userStats.RPG,
                APG = _userStats.APG,
                GP = _userStats.GP,
                GS = _userStats.GS,
                MPG = _userStats.MPG,
                FGAG = _userStats.FGAG,
                FGMG = _userStats.FGMG,
                FGG = _userStats.FGG,
                FTAG = _userStats.FTAG,
                FTMG = _userStats.FTMG,
                FTP = _userStats.FTP,
                TPAG = _userStats.TPAG,
                TPMG = _userStats.TPMG,
                TPP = _userStats.TPP,
                StatusMessage = StatusMessage
            };

            return View(vModel);
        }

        [HttpPost]
        public IActionResult EditAthleteStats(AthleteStatsViewModel vmodel, string id)
        {
            if (!ModelState.IsValid)
            {
                return View(vmodel);
            }

            var userStats = _context.AthleteStats.SingleOrDefault(u => u.UserId == vmodel.UserId);
            id = userStats.UserId;

            if (userStats == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var PPG = userStats.PPG;
            var RPG = userStats.RPG;
            var APG = userStats.APG;
            var GP = userStats.GP;
            var GS = userStats.GS;
            var MPG = userStats.MPG;
            var FGAG = userStats.FGAG;
            var FGMG = userStats.FGMG;
            var FGG = userStats.FGG;
            var FTAG = userStats.FTAG;
            var FTMG = userStats.FTMG;
            var FTP = userStats.FTP;
            var TPAG = userStats.TPAG;
            var TPMG = userStats.TPMG;
            var TPP = userStats.TPMG;

            if (vmodel.PPG != PPG)
            {
                userStats.PPG = vmodel.PPG;
            }
            if (vmodel.RPG != RPG)
            {
                userStats.RPG = vmodel.RPG;
            }
            if (vmodel.APG != APG)
            {
                userStats.APG = vmodel.APG;
            }
            if (vmodel.GP != GP)
            {
                userStats.GP = vmodel.GP;
            }
            if (vmodel.GS != GS)
            {
                userStats.GS = vmodel.GS;
            }
            if (vmodel.MPG != MPG)
            {
                userStats.MPG = vmodel.MPG;
            }
            if (vmodel.FGAG != FGAG)
            {
                userStats.FGAG = vmodel.FGAG;
            }
            if (vmodel.FGMG != FGMG)
            {
                userStats.FGMG = vmodel.FGMG;
            }
            if (vmodel.FGG != FGG)
            {
                userStats.FGG = vmodel.FGG;
            }
            if (vmodel.FTAG != FTAG)
            {
                userStats.FTAG = vmodel.FTAG;
            }
            if (vmodel.FTMG != FTMG)
            {
                userStats.FTMG = vmodel.FTMG;
            }
            if (vmodel.FTP != FTP)
            {
                userStats.FTP = vmodel.FTP;
            }
            if (vmodel.TPAG != TPAG)
            {
                userStats.TPAG = vmodel.TPAG;
            }
            if (vmodel.TPMG != TPMG)
            {
                userStats.TPMG = vmodel.TPMG;
            }
            if (vmodel.TPP != TPP)
            {
                userStats.TPP = vmodel.TPP;
            }

            _context.Update(userStats);
            _context.SaveChanges();

            StatusMessage = "Your statistics have been updated.";

            return RedirectToAction(nameof(EditAthleteStats), "Manage", userStats, id);
        }


        [HttpGet]
        public IActionResult EditCoachProfile(string id)
        {
            id = _userManager.GetUserId(User);

            var _coachProfile = _context.CoachProfiles.SingleOrDefault(m => m.UserId == id);

            var _user = _userManager.Users.SingleOrDefault(m => m.Id == id);

            if (_coachProfile == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var vmodel = new CoachProfileViewModel
            {
                UserId = id,
                AAUId = _coachProfile.AAUId,
                PersnalBio = _coachProfile.PersnalBio,
                YearsCoaching = _coachProfile.YearsCoaching,
                Wins = _coachProfile.Wins,
                Lossess = _coachProfile.Lossess,
                Achievments = _coachProfile.Achievments,
                Verified = _coachProfile.Verified,
                StatusMessage = StatusMessage
            };

            return View(vmodel);
        }

        [HttpPost]
        public IActionResult EditCoachProfile(CoachProfileViewModel vmodel, string id)
        {
            if (!ModelState.IsValid)
            {
                return View(vmodel);
            }

            var coachProfile = _context.CoachProfiles.SingleOrDefault(m => m.UserId == vmodel.UserId);
            id = coachProfile.UserId;

            if (coachProfile == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var AAUId = coachProfile.AAUId;
            var PersonalBio = coachProfile.PersnalBio;
            var YearsCoaching = coachProfile.YearsCoaching;
            var Wins = coachProfile.Wins;
            var Lossess = coachProfile.Lossess;
            var Achievements = coachProfile.Achievments;
            var Verified = coachProfile.Verified;

            if (AAUId != vmodel.AAUId)
            {
                coachProfile.AAUId = vmodel.AAUId;
            }
            if (PersonalBio != vmodel.PersnalBio)
            {
                coachProfile.PersnalBio = vmodel.PersnalBio;
            }
            if (YearsCoaching != vmodel.YearsCoaching)
            {
                coachProfile.YearsCoaching = vmodel.YearsCoaching;
            }
            if (Wins != vmodel.Wins)
            {
                coachProfile.Wins = vmodel.Wins;
            }
            if (Lossess != vmodel.Lossess)
            {
                coachProfile.Lossess = vmodel.Lossess;
            }
            if (Achievements != vmodel.Achievments)
            {
                coachProfile.Achievments = vmodel.Achievments;
            }
            if (Verified != vmodel.Verified)
            {
                coachProfile.Verified = vmodel.Verified;
            }

            _context.Update(coachProfile);
            _context.SaveChanges();

            StatusMessage = "Your profile has been updated.";

            return RedirectToAction(nameof(EditCoachProfile), "Manage", coachProfile, id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            var email = user.Email;
            await _emailSender.SendEmailConfirmationAsync(email, callbackUrl);

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLogins()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new ExternalLoginsViewModel { CurrentLogins = await _userManager.GetLoginsAsync(user) };
            model.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            model.ShowRemoveButton = await _userManager.HasPasswordAsync(user) || model.CurrentLogins.Count > 1;
            model.StatusMessage = StatusMessage;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback));
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> LinkLoginCallback()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(user.Id);
            if (info == null)
            {
                throw new ApplicationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }

            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = "The external login was added.";
            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var result = await _userManager.RemoveLoginAsync(user, model.LoginProvider, model.ProviderKey);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred removing external login for user with ID '{user.Id}'.");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "The external login was removed.";
            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new TwoFactorAuthenticationViewModel
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2faEnabled = user.TwoFactorEnabled,
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Disable2faWarning()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'.");
            }

            return View(nameof(Disable2fa));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable2fa()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'.");
            }

            _logger.LogInformation("User with ID {UserId} has disabled 2fa.", user.Id);
            return RedirectToAction(nameof(TwoFactorAuthentication));
        }

        [HttpGet]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            var model = new EnableAuthenticatorViewModel
            {
                SharedKey = FormatKey(unformattedKey),
                AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Strip spaces and hypens
            var verificationCode = model.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                ModelState.AddModelError("model.Code", "Verification code is invalid.");
                return View(model);
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            _logger.LogInformation("User with ID {UserId} has enabled 2FA with an authenticator app.", user.Id);
            return RedirectToAction(nameof(GenerateRecoveryCodes));
        }

        [HttpGet]
        public IActionResult ResetAuthenticatorWarning()
        {
            return View(nameof(ResetAuthenticator));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            _logger.LogInformation("User with id '{UserId}' has reset their authentication app key.", user.Id);

            return RedirectToAction(nameof(EnableAuthenticator));
        }

        [HttpGet]
        public async Task<IActionResult> GenerateRecoveryCodes()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Cannot generate recovery codes for user with ID '{user.Id}' as they do not have 2FA enabled.");
            }

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            var model = new GenerateRecoveryCodesViewModel { RecoveryCodes = recoveryCodes.ToArray() };

            _logger.LogInformation("User with ID {UserId} has generated new 2FA recovery codes.", user.Id);

            return View(model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenicatorUriFormat,
                _urlEncoder.Encode("gybitg"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        #endregion
    }
}
