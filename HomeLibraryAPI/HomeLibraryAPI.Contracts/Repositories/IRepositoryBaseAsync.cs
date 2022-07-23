using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeLibraryAPI.Contracts.Repositories
{
    /// <summary>
    /// Asynchronous generic repository interface.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IRepositoryBaseAsync<TEntity> where TEntity : class
    {
        /// <summary>
        /// Asynchronously gets all entities from db.
        /// </summary>
        /// <returns>Collection of entites</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Asynchronously gets entity by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The matching entity</returns>
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
