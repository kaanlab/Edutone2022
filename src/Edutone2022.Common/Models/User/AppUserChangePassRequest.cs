using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models.User
{
    public sealed class AppUserChangePassRequest
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
