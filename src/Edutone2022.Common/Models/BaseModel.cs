namespace Edutone2022.Common.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
