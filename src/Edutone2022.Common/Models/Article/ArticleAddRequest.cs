using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models.Article
{
    public sealed class ArticleAddRequest
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Content { get; set; }
        public FileModel? Image { get; set; }
        public string UserId { get; set; }
    }
}
