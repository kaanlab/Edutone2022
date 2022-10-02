using Edutone2022.Common.Models;
using Edutone2022.Common.Models.User;

namespace Edutone2022.WebApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AppUserModel>> GetUsers();
        Task<AppUserModel> CreateUser(AppUserCreateRequest request);
        Task<AppUserModel> UpdateUser(AppUserUpdateRequest request);
        Task<bool> ChangePassword(AppUserChangePassRequest request);
        Task<bool> DeleteUser(string id);
    }
}
