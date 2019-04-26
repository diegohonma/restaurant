using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities
{
    public class Product
    {
        public int Id { get; }

        public string Description { get; }

        public string CookTime { get; }

        public ProductType Type { get; }

        public Product(int id, string description, string cookTime, ProductType type)
        {
            Id = id;
            Description = description;
            CookTime = cookTime;
            Type = type;
        }
    }
}
