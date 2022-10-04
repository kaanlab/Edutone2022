using Edutone2022.Storage.Models;
using Edutone2022.Storage;
using Microsoft.AspNetCore.Identity;
using Edutone2022.Common;

namespace Edutone2022.WebApi
{
    public sealed class SeedData
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUserDb> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SeedData(AppDbContext context, UserManager<AppUserDb> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task AddUsers()
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Global.Roles.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(Global.Roles.USER));
            }

            if (!userManager.Users.Any())
            {
                var admin = new AppUserDb { DisplayName = "Администратор", UserName = "siteadmin", Email = "petrpku@mil.ru" };
                await userManager.CreateAsync(admin, "1Password!");
                await userManager.AddToRoleAsync(admin, Global.Roles.ADMIN);

                var user = new AppUserDb { DisplayName = "Петрозаводское ПКУ", UserName = "petrpku", Email = "petrpku@mil.ru" };
                await userManager.CreateAsync(user, "1Password!");
                await userManager.AddToRoleAsync(user, Global.Roles.USER);
            }

            if(!context.AboutPages.Any())
            {
                var about = new AboutPageDb { Id = Guid.NewGuid(), CreationDate = DateTimeOffset.UtcNow, IsDeleted = false, Title = "About title", Content = "About content" };
                await context.AboutPages.AddAsync(about);
                await context.SaveChangesAsync();
            }

            if(!context.MainPages.Any())
            {
                var main = new MainPageDb { Id = Guid.NewGuid(), CreationDate = DateTimeOffset.UtcNow, IsDeleted = false, Title = "Main title", Description = "Main description" };
                await context.MainPages.AddAsync(main);
                await context.SaveChangesAsync();
            }
        }
    }
}
