using Edutone2022.Domain.Models;

namespace Edutone2022.Domain.Interfaces
{
    public interface IStorageRepository
    {
        Task SaveArtice(AppArticle article);
        Task<AppArticle> LoadArticeById(Guid id);
        Task<IEnumerable<AppArticle>> LoadArticles();
    }
}
