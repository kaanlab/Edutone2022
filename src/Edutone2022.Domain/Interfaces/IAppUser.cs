using Edutone2022.Domain.Models;


namespace Edutone2022.Domain.Interfaces
{
    public interface IAppUser
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public AppFile? Avatar { get; set; }

        IEnumerable<AppArticle>? Articles { get; set; }
    }
}
