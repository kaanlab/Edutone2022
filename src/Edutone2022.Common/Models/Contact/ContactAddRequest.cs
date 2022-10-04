using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models.Contact
{
    public sealed class ContactAddRequest
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string JobTitle { get; set; }
        public string? Responsibility { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public FileModel? Image { get; set; }
    }
}
