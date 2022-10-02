using Edutone2022.Common.Interfaces;
using Edutone2022.Common.Models;
using Edutone2022.Common.Models.Article;
using Edutone2022.Storage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Edutone2022.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ArticleController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly UserManager<AppUserDb> userManager;
        public ArticleController(IRepository repository, UserManager<AppUserDb> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArticleModel>))]
        public async Task<IActionResult> All() => Ok(await repository.GetArticles());

        [Authorize]
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ArticleModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(ArticleAddRequest request)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user.Id != request.UserId)
            {
                return BadRequest();
            }

            return Ok(await repository.CreateArticle(request));
        }

        [Authorize]
        [HttpPut("update/{articleId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid articleId, ArticleModel article)
        {
            var updatedArticle = await repository.GetArticleById(articleId);
            if (updatedArticle is null)
            {
                return NotFound();
            }

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user.Id != article.UserId || updatedArticle.Id != article.Id)
            {
                return BadRequest();
            }

            return Ok(await repository.UpdateArticle(article));
        }

        [Authorize]
        [HttpDelete("delete/{articleId:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid articleId)
        {
            var deletedArticle = await repository.GetArticleById(articleId);
            if (deletedArticle is null)
            {
                return NotFound();
            }

            await repository.DeleteArticle(articleId);

            return NoContent();
        }
    }
}
