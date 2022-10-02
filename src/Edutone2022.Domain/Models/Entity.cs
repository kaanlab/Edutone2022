
namespace Edutone2022.Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTimeOffset CreationDate { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }

        public void InitFill()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTimeOffset.UtcNow;
            IsDeleted = false;
        }
        public virtual void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTimeOffset.UtcNow;
        }
    }
}
