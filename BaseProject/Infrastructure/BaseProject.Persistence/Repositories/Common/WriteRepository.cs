using BaseProject.Application.Interfaces.Repositories.Common;
using BaseProject.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace BaseProject.Persistence.Repositories.Common
{
    public class WriteRepository<T> : IWriteRepository<T, Guid> where T : BaseEntity
    {
        private readonly DbContext dbContext;

        public WriteRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        protected DbSet<T> Table => dbContext.Set<T>();

        public IQueryable<T> AsQueryable() => Table.AsQueryable();

        private async Task<bool> ExecuteSafelyAsync(Func<Task> operation)
        {
            try
            {
                await operation();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ExecuteSafely(Action operation)
        {
            try
            {
                operation();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Create
        public virtual async Task<bool> AddAsync(T entity)
        {
            return await ExecuteSafelyAsync(async () => {
                EntityEntry entry = await Table.AddAsync(entity);
                if (entry.State != EntityState.Added)
                    throw new Exception("Entity not added.");
            });
        }

        public virtual bool Add(T entity)
        {
            return ExecuteSafely(() => {
                EntityEntry entry = Table.Add(entity);
                if (entry.State != EntityState.Added)
                    throw new Exception("Entity not added.");
            });
        }
        #endregion

        #region Update
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            return await ExecuteSafelyAsync(async () => {
                Table.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
            });
        }

        public virtual bool Update(T entity)
        {
            return ExecuteSafely(() => {
                Table.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
            });
        }
        #endregion

        #region Delete
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            return await ExecuteSafelyAsync(async () => {
                if (dbContext.Entry(entity).State == EntityState.Detached)
                    Table.Attach(entity);

                await Task.Run(() => Table.Remove(entity));
            });
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            return await ExecuteSafelyAsync(async () => {
                var entity = await Table.FindAsync(id);
                if (entity == null)
                    throw new Exception("Entity not found.");

                await DeleteAsync(entity);
            });
        }

        public virtual bool Delete(T entity)
        {
            return ExecuteSafely(() => {
                if (dbContext.Entry(entity).State == EntityState.Detached)
                    Table.Attach(entity);

                Table.Remove(entity);
            });
        }

        public virtual bool Delete(Guid id)
        {
            return ExecuteSafely(() => {
                var entity = Table.Find(id);
                if (entity == null)
                    throw new Exception("Entity not found.");
                Delete(entity);
            });
        }
        #endregion
    }
}
