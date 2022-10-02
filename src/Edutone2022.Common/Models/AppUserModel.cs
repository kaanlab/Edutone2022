namespace Edutone2022.Common.Models
{
    public sealed class AppUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public FileModel? Avatar { get; set; }
    }
}
