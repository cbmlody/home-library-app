using HomeLibraryAPI.EF.Configurations;
using HomeLibraryAPI.EF.ModelBuilderExtension;
using HomeLibraryAPI.EF.Models;

using Microsoft.EntityFrameworkCore;

namespace HomeLibraryAPI.EF
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookSeries> BookSeries { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Bookshelve> Bookshelves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookSeriesConfiguration());
            modelBuilder.ApplyConfiguration(new BookshelveConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());

            modelBuilder.Seed();
        }
    }
}
