using Edutone2022.Common.Models;
using Edutone2022.Common.Models.Article;
using Edutone2022.Common.Models.Contact;
using Edutone2022.Common.Models.Document;
using Edutone2022.Common.Models.Page;

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
        Task<DocumentModel> CreateDocument(string uploadPath, DocumentAddRequest request);
        Task<DocumentModel> UpdateDocument(string uploadPath, DocumentUpdateRequest request);
        Task DeleteDocument(Guid id);

        Task<IEnumerable<EmployeeContactModel>> GetContacts();
        Task<EmployeeContactModel> GetContactById(Guid id);
        Task<EmployeeContactModel> CreateContact(ContactAddRequest request);
        Task<EmployeeContactModel> UpdateContact(EmployeeContactModel contact);
        Task DeleteContact(Guid id);

        Task<FileModel> SaveFile(string fileName, string uploadPath);

        Task<MainPageModel> SaveMainPage(MainPageModel mainPage);
        Task<MainPageModel> LoadMainPage();

        Task<AboutPageModel> SaveAboutPage(AboutPageModel aboutPage);
        Task<AboutPageModel> LoadAboutPage();
    }
}
