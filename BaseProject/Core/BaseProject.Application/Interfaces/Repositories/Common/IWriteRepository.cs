using BaseProject.Domain.Entities.Common;
using System.Linq.Expressions;

namespace BaseProject.Application.Interfaces.Repositories.Common
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        //Create
        Task<int> AddAsync(T entity);
        Task<int> AddAsync(IEnumerable<T> entities);
        int Add(T entity);
        int Add(IEnumerable<T> entities);

        //Update
        Task<int> UpdateAsync(T entity);
        int Update(T entity);

        //Delete
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate);
        int Delete(T entity);
        int Delete(Guid id);
        bool Delete(Expression<Func<T, bool>> predicate);

        //Extra Functionalities
        Task<int> AddOrUpdateAsync(T entity);
        int AddOrUpdate(T entity);
        IQueryable<T> AsQueryable();

        //Bulks
        Task BulkDeleteById(IEnumerable<Guid> ids);
        Task BulkDelete(Expression<Func<T, bool>> predicate);
        Task BulkDelete(IEnumerable<T> entities);
        Task BulkUpdate(IEnumerable<T> entities);
        Task BulkAdd(IEnumerable<T> entities);
    }
}
