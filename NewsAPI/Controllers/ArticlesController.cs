using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsAPI.Models.Pagging;
using NewsAppData;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {

        private readonly IRepository _repository;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(ILogger<ArticlesController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }


        [HttpGet]
        public async Task<PagedList<Article>> Get([FromQuery] int PageNumber, string searchTerm)
        {
            return await _repository.GetAllAsync<Article>(new Models.Parameters.PageParameters() { PageNumber = PageNumber }, s => true, s => s.CreatedAt);
        }

        [HttpGet("{id}")]
        public async Task<Article> GetById( int id)
        {
            return await _repository.SelectById<Article>(id);
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid object");
            
            await _repository.CreateAsync<Article>(article);
            return Ok();
        }


        [HttpPost, Route("edit/{articleId}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] int articleId, [FromBody] Article modifiedArticle)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid object");
            var article = await _repository.SelectById<Article>(articleId);
            if (article == null)
            {
                return BadRequest("Article does not exist");
            }
            modifiedArticle.ArticleId = article.ArticleId;
            await _repository.UpdateAsync<Article>(modifiedArticle);
            return Ok();
        }


        [HttpPost, Route("delete{articleId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] int articleId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid object");
            var article = await _repository.SelectById<Article>(articleId);
            if (article == null)
            {
                return BadRequest("Article does not exist");
            }
            await _repository.DeleteAsync<Article>(article);
            return Ok();
        }


    }
}
