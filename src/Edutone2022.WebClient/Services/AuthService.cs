using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Edutone2022.Common.Models.Auth;

namespace Edutone2022.WebClient.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly ILocalStorageService localStorage;

        public AuthService(HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
            this.localStorage = localStorage;
        }


        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var response = await httpClient.PostAsJsonAsync("api/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (response.IsSuccessStatusCode)
            {
                await localStorage.SetItemAsync("authToken", result.Token);
                ((AppAuthStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(result.Token);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            }

            return result;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((AppAuthStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
