using System.ComponentModel.DataAnnotations;

namespace Common.Entity
{
    public class Entity<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }

        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// Entity with Guid as Id for 
    /// </summary>
    public class Entity : Entity<Guid>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }

}
