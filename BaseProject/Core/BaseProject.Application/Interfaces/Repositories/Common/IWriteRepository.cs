using BaseProject.Domain.Entities.Common;
using System.Linq.Expressions;

namespace BaseProject.Application.Interfaces.Repositories.Common
{
    public interface IWriteRepository<T, TKey> where T : BaseEntity
    {
        // Create
        Task<bool> AddAsync(T entity);
        bool Add(T entity);

        // Update
        Task<bool> UpdateAsync(T entity);
        bool Update(T entity);

        // Delete
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(TKey id);
        bool Delete(T entity);
        bool Delete(TKey id);

        // Extra Functionalities
        IQueryable<T> AsQueryable();
    }
}
