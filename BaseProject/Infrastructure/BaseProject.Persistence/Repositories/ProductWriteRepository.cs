using BaseProject.Application.Interfaces.Repositories;
using BaseProject.Domain.Entities;
using BaseProject.Persistence.Contexts;
using BaseProject.Persistence.Repositories.Common;

namespace BaseProject.Persistence.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(BaseProjectDbContext dbContext) : base(dbContext)
        {
        }
    }
}
