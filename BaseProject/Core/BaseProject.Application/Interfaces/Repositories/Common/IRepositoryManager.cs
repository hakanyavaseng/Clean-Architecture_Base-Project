using BaseProject.Domain.Entities.Common;

namespace BaseProject.Application.Interfaces.Repositories.Common
{
    public interface IRepositoryManager : IDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity, new();
        IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity;
        Task<int> SaveAsync();
        int Save();
    }
}
