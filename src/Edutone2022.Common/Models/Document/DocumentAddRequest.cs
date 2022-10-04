using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models.Document
{
    public sealed class DocumentAddRequest
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Title { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
