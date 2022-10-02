using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Edutone2022.WebClient
{
    public class AppAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public AppAuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = ParseClaimsFromJwt(savedToken);

            if (claims.Count() <= 0)
            {
                await localStorage.RemoveItemAsync("authToken");
                MarkUserAsLoggedOut();
                httpClient.DefaultRequestHeaders.Authorization = null;

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            if (TokenIsExpired(claims.First(c => c.Type == ClaimTypes.Expired)))
            {
                await localStorage.RemoveItemAsync("authToken");
                MarkUserAsLoggedOut();
                httpClient.DefaultRequestHeaders.Authorization = null;

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwtToken)
        {
            var token = new JwtSecurityToken(jwtEncodedString: jwtToken);

            var sid = token.Claims.FirstOrDefault(c => c.Type.Contains("sid"))?.Value;
            if (!string.IsNullOrEmpty(sid))
                yield return new Claim(ClaimTypes.Sid, sid);

            var uniqueName = token.Claims.FirstOrDefault(c => c.Type.Equals("unique_name"))?.Value;
            if (!string.IsNullOrEmpty(uniqueName))
                yield return new Claim(ClaimTypes.Name, uniqueName);

            var givenName = token.Claims.FirstOrDefault(c => c.Type.Equals("given_name"))?.Value;
            if (!string.IsNullOrEmpty(givenName))
                yield return new Claim(ClaimTypes.GivenName, givenName);

            var avatar = token.Claims.FirstOrDefault(c => c.Type.Equals("userdata"))?.Value;
            if (!string.IsNullOrEmpty(avatar))
                yield return new Claim(ClaimTypes.UserData, avatar);

            var role = token.Claims.FirstOrDefault(c => c.Type.Equals("role"))?.Value;
            if (!string.IsNullOrEmpty(role))
                yield return new Claim(ClaimTypes.Role, role);

            var exp = token.Claims.FirstOrDefault(c => c.Type.Equals("exp"))?.Value;
            if (!string.IsNullOrEmpty(exp))
                yield return new Claim(ClaimTypes.Expired, exp);
        }

        private bool TokenIsExpired(Claim exp)
        {
            if (exp is not null)
            {
                var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp.Value));
                var timeUTC = DateTime.UtcNow;
                var diff = expTime - timeUTC;
                if (diff.TotalMinutes <= 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
