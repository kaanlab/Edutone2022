using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Edutone2022.Common;
using Edutone2022.Common.Models.User;
using Edutone2022.WebApi.Services;
using Edutone2022.Common.Models;

namespace Edutone2022.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = Global.Roles.ADMIN)]
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppUserModel>))]
        public async Task<IActionResult> All() => Ok(await userService.GetUsers());

        [Authorize(Roles = Global.Roles.ADMIN)]
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppUserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AppUserCreateRequest request) => Ok(await userService.CreateUser(request));

        [Authorize(Roles = Global.Roles.ADMIN)]
        [HttpPut("update/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppUserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string userId, AppUserUpdateRequest request)
        {
            if (userId != request.Id)
            {
                return BadRequest();
            }

            return  Ok(await userService.UpdateUser(request));
        }

        [Authorize(Roles = Global.Roles.ADMIN)]
        [HttpPost("change-pass")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(AppUserChangePassRequest request)
        {
            var result = await userService.ChangePassword(request);
            return result ? NoContent() : BadRequest();
        }

        [Authorize(Roles = Global.Roles.ADMIN)]
        [HttpDelete("delete/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(string userId)
        {
            if (userId == Global.SITE_ADMIN_ACCOUNT)
            {
                return BadRequest();
            }

            var result = await userService.DeleteUser(userId);
            return result ? NoContent() : BadRequest(); 
        }
    }
}
