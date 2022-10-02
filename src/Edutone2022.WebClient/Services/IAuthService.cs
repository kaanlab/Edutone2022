using Edutone2022.Common.Models.Auth;

namespace Edutone2022.WebClient.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task Logout();
    }
}
