using BaseProject.Application.Interfaces.Repositories.Common;
using BaseProject.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BaseProject.Persistence.Repositories.Common
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly DbContext dbContext;

        public WriteRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        protected DbSet<T> Table => dbContext.Set<T>();

        public IQueryable<T> AsQueryable() => Table.AsQueryable();

        #region Create
        public virtual async Task<int> AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual int Add(T entity)
        {
           Table.Add(entity);
           return dbContext.SaveChanges();
        }

        public virtual async Task<int> AddAsync(IEnumerable<T> entities)
        {
            if (entities is not null && !entities.Any())
                return 0;

            await Table.AddRangeAsync(entities);
            return await dbContext.SaveChangesAsync();
        }

        public int Add(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                return 0;

            Table.AddRange(entities);
            return dbContext.SaveChanges();
        }
        #endregion

        #region Update
        public virtual async Task<int> UpdateAsync(T entity)
        {
            Table.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return await dbContext.SaveChangesAsync();
        }

        public virtual int Update(T entity)
        {
            Table.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return dbContext.SaveChanges();
        }

        #endregion

        #region Delete
        public virtual async Task<int> DeleteAsync(T entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
                Table.Attach(entity);
            
            Table.Remove(entity);

            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = await Table.FindAsync(id);
            return await DeleteAsync(entity); 
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
           dbContext.RemoveRange(Table.Where(predicate));
           return await dbContext.SaveChangesAsync() > 0;
        }

        public virtual int Delete(T entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
                Table.Attach(entity);

            Table.Remove(entity);

            return dbContext.SaveChanges();
        }

        public virtual int Delete(Guid id)
        {
            var entity = Table.Find(id);
            return Delete(entity);
        }

        public virtual bool Delete(Expression<Func<T, bool>> predicate)
        {
            dbContext.RemoveRange(Table.Where(predicate));
            return dbContext.SaveChanges() > 0;
        }
        #endregion

        #region AddOrUpdate
        public virtual async Task<int> AddOrUpdateAsync(T entity)
        {
            if (!Table.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                dbContext.Update(entity);
            return await dbContext.SaveChangesAsync();
        }
        public int AddOrUpdate(T entity)
        {
            if (!Table.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                dbContext.Update(entity);
            return dbContext.SaveChanges();
        }
        #endregion

        #region Bulk Methods
        public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if (ids != null && !ids.Any())
                return Task.CompletedTask;

            dbContext.RemoveRange(Table.Where(i => ids.Contains(i.Id)));
            return dbContext.SaveChangesAsync();
        }

        public virtual Task BulkDelete(Expression<Func<T, bool>> predicate)
        {
            dbContext.RemoveRange(Table.Where(predicate));
            return dbContext.SaveChangesAsync();
        }

        public virtual Task BulkDelete(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                return Task.CompletedTask;

            Table.RemoveRange(entities);
            return dbContext.SaveChangesAsync();
        }

        public virtual Task BulkUpdate(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                return Task.CompletedTask;

            foreach (var entityItem in entities)
            {
                Table.Update(entityItem);
            }

            return dbContext.SaveChangesAsync();
        }

        public virtual async Task BulkAdd(IEnumerable<T> entities)
        {
            if (entities != null && !entities.Any())
                await Task.CompletedTask;

            await Table.AddRangeAsync(entities);

            await dbContext.SaveChangesAsync();
        }

        #endregion    
    }
}
