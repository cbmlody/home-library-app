using Entities.Configurations;
using Entities.Models;

using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}
