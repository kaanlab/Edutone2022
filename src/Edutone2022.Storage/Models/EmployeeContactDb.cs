namespace Edutone2022.Storage.Models
{
    public sealed class EmployeeContactDb : EntityDb
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string? Responsibility { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public FileDb? Image { get; set; }
    }
}
