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

    public class LoggingEvents
    {
        public const int GenerateItems = 1000;
        public const int ListItems = 1001;
        public const int GetItem = 1002;
        public const int InsertItem = 1003;
        public const int UpdateItem = 1004;
        public const int DeleteItem = 1005;

        public const int GetItemNotFound = 4000;
        public const int UpdateItemNotFound = 4001;
    }


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
        [HttpGet]
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userManager.Users.ToList();
        }

        // GET: api/User/dev
        [HttpGet("{username}", Name = "GetUser")]
        public IActionResult GetByUsername(string username)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting User {userName}", username);

            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "GetByUsername({userName}) NOT FOUND", username);

                return NotFound();
            }
            return new ObjectResult(user);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Create([FromBody] ApplicationUser user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var _user = new ApplicationUser
            {

            };

            _context.Users.Add(_user);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { UserName = user.UserName }, user);

        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] ApplicationUser user)
        {
            if (user == null || user.Id != id)
            {
                return BadRequest();
            }

            var _user = _context.Users.FirstOrDefault(t => t.Id == id);
            if (_user == null)
            {
                return NotFound();
            }



            _context.Users.Update(_user);
            _context.SaveChanges();
            return new NoContentResult();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
