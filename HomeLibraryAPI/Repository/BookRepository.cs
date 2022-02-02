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
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(LibraryContext libraryContext) : base(libraryContext)
        {
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await FindAll()
                .OrderBy(b => b.Title)
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await FindByCondition(b => b.Id.Equals(id))
                .SingleOrDefaultAsync();
        }
    }
}
