using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models.Page
{
    public sealed class AboutPageModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Content { get; set; }
    }
}
