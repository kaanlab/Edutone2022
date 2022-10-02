namespace Edutone2022.Storage.Models
{
    public sealed class DocumentDb : EntityDb
    {
        public string Title { get; set; }
        public string UploadPath { get; set; }
        public string FileName { get; set; }
    }
}
