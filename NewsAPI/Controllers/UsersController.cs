using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<List<User>> Get([FromQuery]int PageNumber)
        {
            return await _repository.GetAllAsync<User>(new Models.Parameters.PageParameters() { PageNumber = PageNumber }, s => true, s => s.CreatedAt);
        }
    }
}
