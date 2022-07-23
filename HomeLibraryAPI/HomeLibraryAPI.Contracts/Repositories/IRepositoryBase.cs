using System;
using System.Linq;
using System.Linq.Expressions;

namespace HomeLibraryAPI.Contracts.Repositories
{
    /// <summary>
    /// Generic repository interface.
    /// </summary>
    /// <typeparam name="TEntity">Generic entity type</typeparam>
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Method used to get all entities from db.
        /// </summary>
        /// <returns>Collection of entities.</returns>
        IQueryable<TEntity> FindAll();

        /// <summary>
        /// Method used to get entity by expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Collection of entities.</returns>
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Method used to create new entity in db.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        void Create(TEntity entity);

        /// <summary>
        /// Method used to update entity in db.
        /// </summary>
        /// <param name="entity">The modified entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Method used to delete entity from db.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(TEntity entity);
    }
}
