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

namespace gybitg.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UserController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            ILogger<UserController> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        // GET: api/User
        // return all the users in the database and all their property fields
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var AllUsers = _context.Users.ToList();
                _logger.LogInformation($"Returned all users from the database");
                return Ok(AllUsers);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong while retrieving users: {e.Message}");
                return StatusCode(500, "Internal server errror");

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


        // POST: api/User
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
