using BaseProject.Domain.Entities.Common;

namespace BaseProject.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<ProductCategory> ProductCategories { get; set; } 
    }
}