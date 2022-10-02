namespace Edutone2022.Common.Models.Auth
{
    public sealed class LoginResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
