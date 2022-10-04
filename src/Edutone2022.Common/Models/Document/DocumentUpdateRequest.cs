﻿using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Common.Models.Document
{
    public sealed class DocumentUpdateRequest
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Title { get; set; }
        public string FileName { get; set; }
        public byte[]? FileContent { get; set; }
    }
}
