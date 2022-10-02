using Edutone2022.Common.Models.Auth;

namespace Edutone2022.WebApi.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
