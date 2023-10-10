using System.Linq.Expressions;
using AspNetChat.DataAccess.Context;
using AspNetChat.DataAccess.Entities.Abstract;
using AspNetChat.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AspNetChat.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _databaseContext;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _databaseContext.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Add(entity);

            await _databaseContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Update(entity);

            await _databaseContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Remove(entity);

            await _databaseContext.SaveChangesAsync();
        }
    }
}
