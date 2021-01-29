using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsAPI.Models;
using NewsAppData;
using NewsAppData.ViewModels;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IRepository _repository;
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IRepository repository, IUserService userService)
        {
            _logger = logger;
            _repository = repository;
            _userService = userService;
        }


        [HttpGet]
        public async Task<List<User>> Get([FromQuery] int PageNumber)
        {
            return await _repository.GetAllAsync<User>(new Models.Parameters.PageParameters() { PageNumber = PageNumber }, s => true, s => s.CreatedAt);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel userParam)
        {
            var user = await _userService.Authenticate(userParam.Username, userParam.Password);
            
            if (user == null)
                return Unauthorized("Username or password is incorrect");

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost,Route("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid object");

            if ((await _repository.Find<User>(s => s.Equals(user))).FirstOrDefault() != null)
            {
                user.CreatedAt = DateTime.Now;
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
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


        [HttpPost, Route("delete/{userid}")]
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
