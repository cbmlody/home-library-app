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
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext libraryContext) : base(libraryContext)
        {
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await FindAll()
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .ToListAsync();
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            return await FindByCondition(a => a.Id.Equals(id))
                .SingleOrDefaultAsync();
        }
    }
}
