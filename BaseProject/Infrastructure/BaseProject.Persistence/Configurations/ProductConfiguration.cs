using BaseProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseProject.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product()
                {
                    Id = Guid.Parse("C1926F23-AA70-475B-95B0-EBE6D1600B7E"),
                    Title = "Product 1",
                    Description = "Description 1",
                    Discount = 0,
                    CreatedDate = DateTime.Now,
                    Price = 100,
                    BrandId = Guid.Parse("9C3EA609-D3E7-4B66-B326-E786F3CBE745")

                },
                new Product()
                {
                    Id = Guid.Parse("D4144813-0E1F-4312-B82D-CC56336EB6DA"),
                    Title = "Product 2",
                    Description = "Description 2",
                    Discount = 0,
                    CreatedDate = DateTime.Now,
                    Price = 200,
                    BrandId = Guid.Parse("45CE42D0-4EE4-4ADE-902E-C2CE7EEF6B53")

                });
        }
    }
}
