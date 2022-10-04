using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models
{
    public class DocumentModel : FileModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Title { get; set; }
    }
}
