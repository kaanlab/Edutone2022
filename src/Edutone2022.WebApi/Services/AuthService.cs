using Edutone2022.Common;
using Edutone2022.Common.Models.Auth;
using Edutone2022.Storage.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Edutone2022.WebApi.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<AppUserDb> userManager;
        private readonly SignInManager<AppUserDb> signInManager;

        public AuthService(UserManager<AppUserDb> userManager, SignInManager<AppUserDb> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new LoginResponse { IsSuccessful = false, Message = "Неверное имя пользователя или пароль!" };
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                new LoginResponse { IsSuccessful = false, Message = "Неверное имя пользователя или пароль!" };
            }

            return new LoginResponse
            {
                Token = await CreateToken(user),
                Message = "Успех!",
                IsSuccessful = true
            };
        }

        private async Task<string> CreateToken(AppUserDb user)
        {
            var roles = await userManager.GetRolesAsync(user);

            var userAvatar = user.Avatar is not null ? user.Avatar.FullPath : @"\upload\empty.jpg";
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.UserData, userAvatar),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var credentials = new SigningCredentials(Global.KEY, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
