using BaseProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BaseProject.Persistence.Contexts
{
    public class BaseProjectDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public BaseProjectDbContext()
        {
            
        }
        public BaseProjectDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
