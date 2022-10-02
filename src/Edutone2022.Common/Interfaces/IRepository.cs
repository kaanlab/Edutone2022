using Edutone2022.Common.Models;
using Edutone2022.Common.Models.Article;

namespace Edutone2022.Common.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<ArticleModel>> GetArticles();
        Task<ArticleModel> GetArticleById(Guid id);
        Task<ArticleModel> CreateArticle(ArticleAddRequest request);
        Task<ArticleModel> UpdateArticle(ArticleModel article);
        Task DeleteArticle(Guid id);

        Task<IEnumerable<DocumentModel>> GetDocuments();
        Task<DocumentModel> GetDocumentById(Guid id);
        Task<DocumentModel> CreateDocument(DocumentModel document);
        Task<DocumentModel> UpdateDocument(DocumentModel document);
        Task DeleteDocument(Guid id);

        Task<IEnumerable<EmployeeContactModel>> GetContacts();
        Task<EmployeeContactModel> GetContactById(Guid id);
        Task<EmployeeContactModel> CreateContact(EmployeeContactModel contact);
        Task<EmployeeContactModel> UpdateContact(EmployeeContactModel contact);
        Task DeleteContact(Guid id);

        Task<FileModel> SaveFile(string fileName, string uploadPath);
    }
}
