using BaseProject.Application.Interfaces.Repositories;
using BaseProject.Domain.Entities;
using BaseProject.Persistence.Contexts;
using BaseProject.Persistence.Repositories.Common;

namespace BaseProject.Persistence.Repositories
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(BaseProjectDbContext dbContext) : base(dbContext)
        {
        }
    }
}
