using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models.Auth
{
    public sealed class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
