using BaseProject.Domain.Entities.Common;

namespace BaseProject.Domain.Entities
{
    public class ProductCategory : IBaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}