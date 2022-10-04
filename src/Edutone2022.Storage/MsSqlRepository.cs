using Edutone2022.Common.Interfaces;
using Edutone2022.Common.Models;
using Edutone2022.Common.Models.Article;
using Edutone2022.Common.Models.Contact;
using Edutone2022.Common.Models.Document;
using Edutone2022.Common.Models.Page;
using Edutone2022.Storage.Mapper;
using Edutone2022.Storage.Models;

using Microsoft.EntityFrameworkCore;


namespace Edutone2022.Storage
{
    public sealed class MsSqlRepository : IRepository
    {
        private readonly AppDbContext dbContext;

        public MsSqlRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ArticleModel> CreateArticle(ArticleAddRequest article)
        {
            var author = await dbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(article.UserId));
            var image = article.Image is not null ? await dbContext.Files.FirstOrDefaultAsync(x => x.Id == article.Image.Id) : null;

            var newArticle = new ArticleDb
            {
                Id = Guid.NewGuid(),
                Title = article.Title,
                Content = article.Content,
                Author = author,
                Image = image,
                CreationDate = DateTimeOffset.UtcNow,
                IsDeleted = false
            };

            await dbContext.Articles.AddAsync(newArticle);
            await dbContext.SaveChangesAsync();

            return Use.Mapper.Map<ArticleModel>(newArticle);
        }

        public async Task DeleteArticle(Guid id)
        {
            var article = await dbContext.Articles.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            article.DeletedAt = DateTimeOffset.UtcNow;
            article.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }

        public async Task<ArticleModel> GetArticleById(Guid id)
        {
            var article = await dbContext.Articles.Include(x => x.Author).Include(x => x.Image).FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            return Use.Mapper.Map<ArticleModel>(article);
        }

        public async Task<IEnumerable<ArticleModel>> GetArticles()
        {
            var articles = await dbContext.Articles.Where(x => x.IsDeleted == false).Include(x => x.Author).ThenInclude(y => y.Avatar).Include(x => x.Image).OrderByDescending(a => a.CreationDate).ToArrayAsync();
            return Use.Mapper.Map<IEnumerable<ArticleModel>>(articles);
        }
        public async Task<ArticleModel> UpdateArticle(ArticleModel article)
        {
            var updatedArticle = await dbContext.Articles.Include(x => x.Author).Include(x => x.Image).FirstOrDefaultAsync(x => x.Id == article.Id && x.IsDeleted == false);
            var author = await dbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(article.UserId));
            var image = article.Image is not null ? await dbContext.Files.FirstOrDefaultAsync(x => x.Id == article.Image.Id) : null;

            updatedArticle.Title = article.Title;
            updatedArticle.Content = article.Content;
            updatedArticle.Author = author;
            updatedArticle.Image = image;
            updatedArticle.UpdatedAt = DateTimeOffset.UtcNow;

            await dbContext.SaveChangesAsync();

            return Use.Mapper.Map<ArticleModel>(updatedArticle);
        }

        public async Task<DocumentModel> CreateDocument(string uploadPath, DocumentAddRequest document)
        {
            var newDocument = new DocumentDb
            {
                Id = Guid.NewGuid(),
                Title = document.Title,
                FileName = document.FileName,
                UploadPath = uploadPath,
                CreationDate = DateTimeOffset.UtcNow,
                IsDeleted = false
            };

            await dbContext.Documents.AddAsync(newDocument);
            await dbContext.SaveChangesAsync();

            return Use.Mapper.Map<DocumentModel>(newDocument);
        }

        public async Task DeleteDocument(Guid id)
        {
            var document = await dbContext.Documents.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            document.DeletedAt = DateTimeOffset.UtcNow;
            document.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }

        public async Task<DocumentModel> GetDocumentById(Guid id)
        {
            var document = await dbContext.Documents.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            return Use.Mapper.Map<DocumentModel>(document);
        }

        public async Task<IEnumerable<DocumentModel>> GetDocuments()
        {
            var documents = await dbContext.Documents.Where(x => x.IsDeleted == false).ToArrayAsync();
            return Use.Mapper.Map<IEnumerable<DocumentModel>>(documents);
        }

        public async Task<DocumentModel> UpdateDocument(string uploadPath, DocumentUpdateRequest document)
        {
            var updatedDocument = await dbContext.Documents.FirstOrDefaultAsync(x => x.Id == document.Id && x.IsDeleted == false);

            updatedDocument.Title = document.Title;
            updatedDocument.FileName = document.FileName;
            updatedDocument.UploadPath = uploadPath;

            await dbContext.SaveChangesAsync();

            return Use.Mapper.Map<DocumentModel>(updatedDocument);
        }

        public async Task<IEnumerable<EmployeeContactModel>> GetContacts()
        {
            var contacts = await dbContext.EmployeeContacts.Where(x => x.IsDeleted == false).Include(x => x.Image).ToArrayAsync();
            return Use.Mapper.Map<IEnumerable<EmployeeContactModel>>(contacts);
        }

        public async Task<EmployeeContactModel> GetContactById(Guid id)
        {
            var contact = await dbContext.EmployeeContacts.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            return Use.Mapper.Map<EmployeeContactModel>(contact);
        }

        public async Task<EmployeeContactModel> CreateContact(ContactAddRequest contact)
        {
            var image = contact.Image is not null ? await dbContext.Files.FirstOrDefaultAsync(x => x.Id == contact.Image.Id) : null;
            var newContact = new EmployeeContactDb()
            {
                Id = Guid.NewGuid(),
                Name = contact.Name,
                Responsibility = contact.Responsibility,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                JobTitle = contact.JobTitle,
                Image = image,
                CreationDate = DateTimeOffset.UtcNow,
                IsDeleted = false
            };

            await dbContext.EmployeeContacts.AddAsync(newContact);
            await dbContext.SaveChangesAsync();

            return Use.Mapper.Map<EmployeeContactModel>(newContact);
        }

        public async Task<EmployeeContactModel> UpdateContact(EmployeeContactModel contact)
        {
            var updatedContact = await dbContext.EmployeeContacts.FirstOrDefaultAsync(x => x.Id == contact.Id && x.IsDeleted == false);
            var image = contact.Image is not null ? await dbContext.Files.FirstOrDefaultAsync(x => x.Id == contact.Image.Id) : null;

            updatedContact.Name = contact.Name;
            updatedContact.Responsibility = contact.Responsibility;
            updatedContact.Email = contact.Email;
            updatedContact.PhoneNumber = contact.PhoneNumber;
            updatedContact.JobTitle = contact.JobTitle;
            updatedContact.Image = image;

            await dbContext.SaveChangesAsync();

            return Use.Mapper.Map<EmployeeContactModel>(updatedContact);
        }

        public async Task DeleteContact(Guid id)
        {
            var deletedContact = await dbContext.EmployeeContacts.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            deletedContact.DeletedAt = DateTimeOffset.UtcNow;
            deletedContact.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }

        public async Task<FileModel> SaveFile(string fileName, string uploadPath)
        {
            var newFile = new FileDb
            {
                Id = Guid.NewGuid(),
                FileName = fileName,
                UploadPath = uploadPath,
                CreationDate = DateTimeOffset.UtcNow,
                IsDeleted = false
            };

            await dbContext.Files.AddAsync(newFile);
            await dbContext.SaveChangesAsync();

            return Use.Mapper.Map<FileModel>(newFile);
        }

        public async Task<MainPageModel> SaveMainPage(MainPageModel mainPage)
        {
            var main = await dbContext.MainPages.FirstOrDefaultAsync();
            main.Title = mainPage.Title;
            main.Description = mainPage.Description;
            await dbContext.SaveChangesAsync();

            return new MainPageModel { Title = main.Title, Description = main.Description };
        }

        public async Task<MainPageModel> LoadMainPage()
        {
            var main = await dbContext.MainPages.FirstOrDefaultAsync();
            return new MainPageModel { Title = main.Title, Description = main.Description };
        }

        public async Task<AboutPageModel> SaveAboutPage(AboutPageModel aboutPage)
        {
            var about = await dbContext.AboutPages.FirstOrDefaultAsync();
            about.Title = aboutPage.Title;
            about.Content = aboutPage.Content;
            await dbContext.SaveChangesAsync();

            return new AboutPageModel { Title = about.Title, Content = about.Content };
        }

        public async Task<AboutPageModel> LoadAboutPage()
        {
            var about = await dbContext.AboutPages.FirstOrDefaultAsync();
            return new AboutPageModel { Title = about.Title, Content = about.Content };
        }
    }
}
