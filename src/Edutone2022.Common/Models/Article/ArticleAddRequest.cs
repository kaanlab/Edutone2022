namespace Edutone2022.Common.Models.Article
{
    public sealed class ArticleAddRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public FileModel? Image { get; set; }
        public string UserId { get; set; }
    }
}
