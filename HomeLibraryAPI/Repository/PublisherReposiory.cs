using Contracts.Repositories;

using Entities;
using Entities.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class PublisherReposiory : RepositoryBase<Publisher>, IPublisherRepository
    {
        public PublisherReposiory(LibraryContext libraryContext) : base(libraryContext)
        {
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await FindAll()
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Publisher> GetByIdAsync(Guid id)
        {
            return await FindByCondition(p => p.Id.Equals(id))
                .SingleOrDefaultAsync();
        }
    }
}
