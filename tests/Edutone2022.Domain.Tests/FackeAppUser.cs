using Edutone2022.Domain.Interfaces;
using Edutone2022.Domain.Models;


namespace Edutone2022.Domain.Tests
{
    public class FackeAppUser : IAppUser
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public AppFile? Avatar { get; set; }
        public IEnumerable<AppArticle>? Articles { get; set; }
    }
}
