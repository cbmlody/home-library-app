using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> FindAll();
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
