using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models
{
    public sealed class EmployeeContactModel : BaseModel
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
