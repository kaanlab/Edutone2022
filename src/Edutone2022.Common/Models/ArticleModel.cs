namespace Edutone2022.Common.Models
{
    public sealed class ArticleModel : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public FileModel? Image { get; set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public FileModel? Avatar { get; set; }
    }
}
