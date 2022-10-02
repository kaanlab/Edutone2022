namespace Edutone2022.Common.Models
{
    public sealed class EmployeeContactModel : BaseModel
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string? Responsibility { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public FileModel? Image { get; set; }
    }
}
