using Microsoft.AspNetCore.Identity;

namespace Edutone2022.Storage.Models
{
    public sealed class AppUserDb : IdentityUser
    {
        public string DisplayName { get; set; }
        public FileDb? Avatar { get; set; }
        public IEnumerable<ArticleDb>? Articles { get; set; }
    }
}
