using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models.User
{
    public sealed class AppUserCreateRequest
    {
        public string DisplayName { get; set; }
        public FileModel Avatar { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
