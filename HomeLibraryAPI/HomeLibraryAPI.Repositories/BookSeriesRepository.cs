using HomeLibraryAPI.Contracts.Repositories;
using HomeLibraryAPI.EF;
using HomeLibraryAPI.EF.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibraryAPI.Repositories
{
    public class BookSeriesRepository : RepositoryBase<BookSeries>, IBookSeriesRepository
    {
        public BookSeriesRepository(LibraryContext libraryContext) : base(libraryContext)
        {
        }

        public async Task<IEnumerable<BookSeries>> GetAllAsync()
        {
            return await FindAll()
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public async Task<BookSeries> GetByIdAsync(Guid id)
        {
            return await FindByCondition(b => b.Id.Equals(id))
                .SingleOrDefaultAsync();
        }
    }
}
