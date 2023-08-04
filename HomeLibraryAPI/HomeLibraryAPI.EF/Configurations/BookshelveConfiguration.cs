using HomeLibraryAPI.EF.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Collections.Generic;

namespace HomeLibraryAPI.EF.Configurations
{
    public class BookshelveConfiguration : IEntityTypeConfiguration<Bookshelve>
    {
        public void Configure(EntityTypeBuilder<Bookshelve> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Books)
                .WithMany(b => b.Bookshelves)
                .UsingEntity<Dictionary<string, object>>(
                    "Bookshelves_Books",
                    x => x.HasOne<Book>().WithMany().HasConstraintName("Book_Bookshelves"),
                    x => x.HasOne<Bookshelve>().WithMany().HasConstraintName("Bookshelve_Books"));
        }
    }
}
