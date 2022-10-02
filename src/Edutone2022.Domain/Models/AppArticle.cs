using Edutone2022.Domain.Interfaces;

namespace Edutone2022.Domain.Models
{
    public sealed class AppArticle : Entity
    {
        public string Title { get; private set; }   
        public string Content { get; private set; }
        public AppFile? Image { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public IAppUser Author { get; private set; }

        public AppArticle(string title, string content, IAppUser user, AppFile image = null)
        {
            InitFill();
            Title = title;
            Content = content;
            Author = user;
            Image = image;
        }
    }
}
