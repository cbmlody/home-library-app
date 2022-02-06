using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeLibraryAPI.Contracts.Repositories
{
    public interface IRepositoryBaseAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
