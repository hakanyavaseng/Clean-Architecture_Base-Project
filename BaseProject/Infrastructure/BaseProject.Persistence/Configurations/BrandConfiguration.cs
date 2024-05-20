using BaseProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseProject.Persistence.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(
            new Brand()
            {
                Id = Guid.Parse("9C3EA609-D3E7-4B66-B326-E786F3CBE745"),
                Name = "Brand 1",
                CreatedDate = DateTime.Now
            },
            new Brand()
             {
                 Id = Guid.Parse("45CE42D0-4EE4-4ADE-902E-C2CE7EEF6B53"),
                 Name = "Brand 2",
                 CreatedDate = DateTime.Now
            });
        }
    }
}
