using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gybitg.Models;
using gybitg.Data;
using Microsoft.EntityFrameworkCore;

namespace gybitg.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [RequireHttps]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public UserController (
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<UserController> logger)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET: api/User
        // return all the users in the database and all their property fields
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    ApplicationUser _user = await _userManager.FindByNameAsync(User.Identity.Name);
                    _logger.LogInformation($"User found");
                    return Ok(_user);
                } else {
                    return StatusCode(404, "You are not signed in, please sign in first.");     
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong while retrieving user: {e.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        // GET: api/user/allfullnames
        [HttpGet("allfullnames")]
        public IActionResult GetAllFullNames()
        {
            try
            {
                // initialize new list of strings
                List<string> ListOfNames = new List<string>();

                // create list of all the users
                var AllUsers = _context.Users.ToList();

                // go through each user in the user list add their name to the ListOfNames list
                foreach (var user in AllUsers)
                {
                    // if the user hasnt set their name in account settings
                    if (user.FullName == " " || user.FullName == null)
                    {
                        ListOfNames.Add("User " + user.UserName + " has not set their full name");
                    }
                    else
                    {
                        ListOfNames.Add("Username: " + user.UserName + ", fullname: " + user.FullName);
                    }
                }
                return Ok(ListOfNames);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong with GetAllFullNames: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/user/username
        // Retrieve all specific user data
        [HttpGet("{username}", Name = "Get")]
        public IActionResult Get(string username)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(m => m.UserName == username);

                if (user.UserName == null)
                {
                    _logger.LogError($"User with username: {username}, hasn't been found in database");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned user with details for user: {username}");
                    return Ok(user);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong with GET User/username action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        // GET: api/user/username/fullname
        // Retrieve the users fullname
        [HttpGet("{username}/fullname", Name = "GetUserFullName")]
        public IActionResult GetUserFullName(string username)
        {
            var user = _context.Users.SingleOrDefault(m => m.UserName == username);

            var userFullName = user.FullName;

            return Ok(userFullName);

        }

        // This API method registers a new user by passing an email, password, and role from the form/body
        // POST: api/user/registeruser
        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromForm]string email,[FromForm]string password,[FromForm]string role)
        {
            try
            {            
                // Initialize a new Application User
                var mUser = new ApplicationUser { UserName = email, Email = email };

                var result = await _userManager.CreateAsync(mUser, password);   // Get the result of adding the new user to the AspNetUsers table
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(mUser, role); // Add the new user to the AspNetUserRoles table
                    // add the user to the appropriate 'Profile' table
                    switch (role)
                    {
                        case "Athlete":
                            AthleteProfile mAthleteProfile = new AthleteProfile
                            {
                                UserId = mUser.Id
                            };
                            _context.AthleteProfiles.Add(mAthleteProfile);
                            _context.SaveChanges();
                            break;
                        case "Coach":
                            CoachProfile mCoachProfile = new CoachProfile
                            {
                                UserId = mUser.Id
                            };
                            _context.CoachProfiles.Add(mCoachProfile);
                            _context.SaveChanges();
                            break;
                    }
                    _logger.LogInformation($"Successfully registered new user:  { mUser}");
                    return Ok(mUser);
                }
                else // If new user was unable to be registered; log the error. Most likely because duplicate user email
                {
                    _logger.LogError($"Unable to regiseter> {result.Errors}");
                    return StatusCode(400, result.Errors.LastOrDefault().Description);
                }
            }
            catch (Exception e) // Return any errors 
            {
                _logger.LogError($"Something went wrong with POST User/register action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromForm]string email, [FromForm] string password, [FromForm] bool rememberMe)
        {
            try
            {
                // Check to see if the user is already logged in
                if (User.Identity.IsAuthenticated)
                {
                    return StatusCode(400, "You are already logged in");
                }

                // Grab the user from the db from the provided username for password comparison
                var _user = await _context.Users.SingleAsync(u => u.Email == email);

                // Use the Signin Manager to compare the user:password provided
                var result = await _signInManager.CheckPasswordSignInAsync(_user, password, false);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(_user, rememberMe);
                    _logger.LogInformation($"Successfully logged in with user, {_user}");
                    return Ok(_user);   // Return the signed in user
                }
                else    // If here - email or password was incorrect
                {
                    _logger.LogError($"The email or password was incorrect.");
                    return StatusCode(400, "The email or password is incorrect.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong with POST user/Login action: {e.Message}");
                return StatusCode(500, "Internal server error, please try again.");
            }
        }

        // POST: api/user/logout
        [HttpPost("logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                if (User.Identity.IsAuthenticated)  // Check to make sure there is a user logged in
                {
                    await _signInManager.SignOutAsync();    // Log user out
                    _logger.LogInformation($"User successfully logged out.");
                    return StatusCode(200, "Successfully logged out");
                }
                else   // If here - then there wasnt a user logged in
                {
                    _logger.LogError($"No user logged in to logout");  
                    return StatusCode(404, "You are not logged in.");
                }

            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong with POST user/Logout action: {e.Message}");
                return StatusCode(500, "Internal server error with POST user/Logout action");
            }
        }

        // POST: api/user/addstat
        [HttpPost("addstat")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddStat([FromForm] int points, [FromForm] int rebounds, [FromForm] int steals,
            [FromForm] int assists, [FromForm] int blocks, [FromForm] int minutesPlayed, [FromForm] DateTime dateOfGame)
        {

            if (!User.Identity.IsAuthenticated || User.IsInRole("Athlete") == false)    // Make sure current user is an Athlete
            {
                return StatusCode(400, "You must be logged in as an Athlete to access this action");
            }
            else
            {
                try
                {
                    // initialize the new stat entity
                    Stat mStat = new Stat();
                    ApplicationUser _user = await _userManager.GetUserAsync(User);     // Initialize application user to currently signed in user

                    // Create the new Stat entity with the stat values
                    mStat.UserId = _user.Id;
                    mStat.DateOfEntry = dateOfGame;
                    mStat.Points = points;
                    mStat.Rebounds = rebounds;
                    mStat.Steals = steals;
                    mStat.Assists = assists;
                    mStat.Blocks = blocks;
                    mStat.MinutesPlayed = minutesPlayed;

                    // add the new stat to the db
                    _context.Stats.Add(mStat);
                    // save the db
                    _context.SaveChanges();

                    // log result
                    _logger.LogInformation($"You have been uthorized to add new game stat");
                    return Ok(mStat);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Something went wrong with the POST user/addstat action: {e.Message}");
                    return StatusCode(500, "Internal server error with POST user/addstat action");
                }
            }
        }


        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromForm]string value)
        {
            var mValue = value;

            return Ok(mValue);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
