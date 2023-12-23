using Common.Entity;

namespace PoC.Domain.Domains
{
    public class Category(string name, string description) : BaseEntity<Guid>
    {
        public string Name { get; set; } = name;

        public string Description { get; set; } = description;

        public ICollection<Product> Products { get; set; }

    }
}
