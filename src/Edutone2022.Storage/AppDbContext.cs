using Edutone2022.Storage.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Edutone2022.Storage
{
    public sealed class AppDbContext : IdentityDbContext<AppUserDb>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ArticleDb> Articles { get; set; }
        public DbSet<DocumentDb> Documents { get; set; }
        public DbSet<EmployeeContactDb> EmployeeContacts { get; set; }
        public DbSet<FileDb> Files { get; set; }
        public DbSet<AboutPageDb> AboutPages { get; set; }
        public DbSet<MainPageDb> MainPages { get; set; }
    }
}
