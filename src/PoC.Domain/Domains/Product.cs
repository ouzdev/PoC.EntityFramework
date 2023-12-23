using Common.Entity;

namespace PoC.Domain.Domains
{
    public class Product(string name, string sku, string description, decimal price, Guid categoryId) : BaseEntity<Guid>
    {
        public string Name { get; set; } = name;
        public string Sku { get; set; } = sku;
        public string Description { get; set; } = description;
        public decimal Price { get; set; } = price;
        public Guid CategoryId { get; set; } = categoryId;
        public Category Category { get; set; }
    }
}
