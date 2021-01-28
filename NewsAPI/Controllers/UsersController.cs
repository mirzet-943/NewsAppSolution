using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsAppData;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IRepository _repository;
        private readonly ILogger<UsersController> _logger;


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public UsersController(ILogger<UsersController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }


        [HttpGet]
        public async Task<List<User>> Get([FromQuery] int PageNumber)
        {
            return await _repository.GetAllAsync<User>(new Models.Parameters.PageParameters() { PageNumber = PageNumber }, s => true, s => s.CreatedAt);
        }

        [HttpPost,Route("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid object");

            if ((await _repository.Find<User>(s => s.Equals(user))).FirstOrDefault() != null)
            {
                await _repository.CreateAsync<User>(user);
                return Ok();
            }
            return StatusCode(403, "User already exists");
        }


        [HttpPost, Route("edit/{userid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userid,[FromBody] User modifiedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid object");
            var user = await _repository.SelectById<User>(userid);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            modifiedUser.Token = user.Token;
            modifiedUser.UserID = user.UserID;
            await _repository.UpdateAsync<User>(modifiedUser);
            return Ok();
        }


        [HttpPost, Route("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] int userid)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid object");
            var user = await _repository.SelectById<User>(userid);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            await _repository.DeleteAsync<User>(user);
            return Ok();
        }


    }
}
