using Edutone2022.Common;
using Edutone2022.Common.Models;
using Edutone2022.Common.Models.User;
using Edutone2022.Storage;
using Edutone2022.Storage.Mapper;
using Edutone2022.Storage.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Edutone2022.WebApi.Services
{
    public sealed class UserService : IUserService
    {
        private readonly UserManager<AppUserDb> userManager;
        private readonly AppDbContext dbContext;

        public UserService(UserManager<AppUserDb> userManager, AppDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<AppUserChangePassResponse> ChangePassword(AppUserChangePassRequest request)
        {
            var appUser = await userManager.FindByNameAsync(request.UserName);
            var result = await userManager.ChangePasswordAsync(appUser, request.CurrentPassword, request.Password);
            var errors = string.Join(' ', result.Errors.Select(error => error.Description).ToArray());
            return result.Succeeded ? new AppUserChangePassResponse { IsSucceed = true, Message = "Успешно!" } : new AppUserChangePassResponse { IsSucceed = false, Message = errors };
        }

        public async Task<AppUserModel> CreateUser(AppUserCreateRequest request)
        {
            var avatar = await dbContext.Files.FirstOrDefaultAsync(x => x.Id == request.Avatar.Id);

            var user = new AppUserDb
            {
                DisplayName = request.DisplayName,
                UserName = request.UserName,
                Email = request.Email,
                Avatar = avatar
            };
            await userManager.CreateAsync(user, request.Password);
            await userManager.AddToRoleAsync(user, Global.Roles.USER);

            return Use.Mapper.Map<AppUserModel>(user);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await userManager.FindByNameAsync(userId);
            var result = await userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<IEnumerable<AppUserModel>> GetUsers()
        {
            var users = await userManager.Users.Include(x => x.Avatar).ToListAsync();
            return Use.Mapper.Map<IEnumerable<AppUserModel>>(users);
        }

        public async Task<AppUserModel> UpdateUser(AppUserUpdateRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            var avatar = await dbContext.Files.FirstOrDefaultAsync(x => x.Id == request.Avatar.Id);

            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Avatar = avatar;
            user.DisplayName = request.DisplayName;

            await userManager.UpdateAsync(user);

            return Use.Mapper.Map<AppUserModel>(user);
        }
    }
}
