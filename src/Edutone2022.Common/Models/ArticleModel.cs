using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models
{
    public sealed class ArticleModel : BaseModel
    {
        [Required(ErrorMessage ="Поле обязательно для заполнения!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Content { get; set; }
        public FileModel? Image { get; set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public FileModel? Avatar { get; set; }
    }
}
