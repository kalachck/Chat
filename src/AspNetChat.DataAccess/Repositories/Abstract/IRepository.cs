using System.Linq.Expressions;
using AspNetChat.DataAccess.Entities.Abstract;

namespace AspNetChat.DataAccess.Repositories.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
