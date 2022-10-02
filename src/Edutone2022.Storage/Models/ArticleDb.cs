namespace Edutone2022.Storage.Models
{
    public sealed class ArticleDb : EntityDb
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public FileDb? Image { get; set; }
        public AppUserDb Author { get; set; }
    }
}
