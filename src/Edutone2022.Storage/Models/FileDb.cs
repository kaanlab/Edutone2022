namespace Edutone2022.Storage.Models
{
    public sealed class FileDb : EntityDb
    {
        public string UploadPath { get; set; }
        public string FileName { get; set; }
        public string FullPath => Path.Combine(UploadPath, FileName);
    }
}
