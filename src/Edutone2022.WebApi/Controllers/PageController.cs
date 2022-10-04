using Edutone2022.Common.Interfaces;
using Edutone2022.Common.Models.Page;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edutone2022.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class PageController : ControllerBase
    {
        private readonly IRepository repository;

        public PageController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("main")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MainPageModel>))]
        public async Task<IActionResult> MainPage() => Ok(await repository.LoadMainPage());

        [Authorize]
        [HttpPost("update/main")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainPageModel))]
        public async Task<IActionResult> UpdateMain(MainPageModel request) => Ok(await repository.SaveMainPage(request));


        [HttpGet("about")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AboutPageModel>))]
        public async Task<IActionResult> AboutPage() => Ok(await repository.LoadAboutPage());

        [Authorize]
        [HttpPost("update/about")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AboutPageModel))]
        public async Task<IActionResult> UpdateAbout(AboutPageModel request) => Ok(await repository.SaveAboutPage(request));
    }
}
