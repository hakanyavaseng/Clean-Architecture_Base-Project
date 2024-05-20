using BaseProject.Application.Interfaces.Repositories.Common;
using BaseProject.Persistence.Contexts;

namespace BaseProject.Persistence.Repositories.Common
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly BaseProjectDbContext context;
        public RepositoryManager(BaseProjectDbContext context)
        {
            this.context = context;
        }
        public async ValueTask DisposeAsync() => await context.DisposeAsync();
        public int Save() => context.SaveChanges();
        public async Task<int> SaveAsync() => await context.SaveChangesAsync();
        IReadRepository<T> IRepositoryManager.GetReadRepository<T>() => new ReadRepository<T>(context);
        IWriteRepository<T> IRepositoryManager.GetWriteRepository<T>() => new WriteRepository<T>(context);
    }
}
