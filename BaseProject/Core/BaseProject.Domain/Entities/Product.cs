using BaseProject.Domain.Entities.Common;

namespace BaseProject.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

        //Constructors
        public Product()
        {
            
        }
        public Product(string title, string description, decimal price, decimal discount)
        {
            Title = title;
            Description = description;
            Price = price;
            Discount = discount;
        }
    }
}
