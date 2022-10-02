namespace Edutone2022.Common.Models
{
    public class FileModel : BaseModel
    {
        public string UploadPath { get; set; }
        public string FileName { get; set; }

        public string FullPath => Path.Combine(UploadPath, FileName);
    }
}
