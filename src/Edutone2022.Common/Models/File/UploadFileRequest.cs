namespace Edutone2022.Common.Models.File
{
    public sealed class UploadFileRequest
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
