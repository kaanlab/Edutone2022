namespace Edutone2022.Common.Models.User
{
    public sealed class AppUserUpdateRequest
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public FileModel Avatar { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
