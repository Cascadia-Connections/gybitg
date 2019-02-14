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
using gybitg.Models.AccountViewModels;
using gybitg.Services;
using Newtonsoft.Json;
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

        public UserController(
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

        // GET: api/User/allfullnames
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

        // GET: api/User/username
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

        // GET: api/User/username/fullname
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
                // Get the username from the email
                string mUserName = email.Substring(0, email.IndexOf('@'));
            
                // Initialize a new Application User
                var mUser = new ApplicationUser { UserName = mUserName, Email = email };

                var result = await _userManager.CreateAsync(mUser, password);   // Get the result of adding the new user to the AspNetUsers table
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(mUser, role); // Add the new user to the AspNetUserRoles table
                    _logger.LogInformation($"Successfully registered new user:  { mUser}");
                    return Ok(mUser);
                }
                else // If new user was unable to be registered; log the error
                {
                    _logger.LogError($"Unable to regiseter user {mUser}");
                    return StatusCode(400, "New user was not created");
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
        public async Task<IActionResult> Login([FromForm]string email,[FromForm] string password,[FromForm] bool rememberMe)
        {
            try
            {
                // Grab the user from the db from the provided username for password comparison
                var _user = await _context.Users.SingleAsync(u => u.Email == email);

                // Use the Signin Manager to compare the user:password provided
                var result = await _signInManager.CheckPasswordSignInAsync(_user, password, false);
                if (result.Succeeded)
                {
                    // After getting a confirmed user<->password result, go ahead and sign the user in
                    await _signInManager.SignInAsync(_user, rememberMe);
                    if (_signInManager.IsSignedIn(User) && User.Identity.Name == _user.UserName)  // Double check whether the right user was signed in
                    {
                        _logger.LogInformation($"Successfully logged in with user, {_user}");
                        return Ok(_user);   // Return the signed in user
                    } else     
                    {
                    _logger.LogError($"User, {_user}, could not log in.");   
                    return StatusCode(400, "Login failed, Email/Password was incorrect, please try again.");

                    }
                }
                else    // If here - email or password was incorrect
                {
                    _logger.LogError($"Email or password was incorrect, please try again");
                    return StatusCode(400, "Login failed: Email/Password incorrect, please try agin.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong with POST user/Login action: {e.Message}");
                return StatusCode(500, "Internal server error");
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
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> AddStat()
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("Athlete") == false)    // Make sure current user is an Athlete
            {
                return StatusCode(400, "You must be logged in as an Athlete to access this action");
            }
            else
            {
                try
                {
                    ApplicationUser _user = await _userManager.FindByNameAsync(User.Identity.Name);     // Initialize application user to currently signed in user
                    _logger.LogInformation($"You have been uthorized to add new game stat");
                    return Ok(_user);
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
