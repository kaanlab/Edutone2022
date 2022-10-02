using System.ComponentModel.DataAnnotations;

namespace Edutone2022.Storage.Models
{
    public abstract class EntityDb
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
