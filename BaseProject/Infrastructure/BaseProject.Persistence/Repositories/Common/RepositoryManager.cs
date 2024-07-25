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
        public void Dispose() => context.Dispose();
        public async ValueTask DisposeAsync() => await context.DisposeAsync();
        public int Save() => context.SaveChanges();
        public async Task<int> SaveAsync() 
        {
            using(var transaction = await context.Database.BeginTransactionAsync())
            {
                try 
                {
                    var result = await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An error occurred while saving changes");
                }
            }    
        }
        IReadRepository<T> IRepositoryManager.GetReadRepository<T>() => new ReadRepository<T>(context);
        IWriteRepository<T> IRepositoryManager.GetWriteRepository<T>() => new WriteRepository<T>(context);
    }
}
