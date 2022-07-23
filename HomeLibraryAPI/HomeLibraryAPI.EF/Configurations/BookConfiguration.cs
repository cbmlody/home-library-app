using HomeLibraryAPI.EF.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Collections.Generic;

namespace HomeLibraryAPI.EF.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title)
                .IsRequired();
            builder.HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "Authors_Books",
                    x => x.HasOne<Author>().WithMany().HasConstraintName("Author_Books"),
                    x => x.HasOne<Book>().WithMany().HasConstraintName("Book_Authors"));
            builder.HasOne(b => b.BookSeries)
                .WithMany(b => b.Books);
        }
    }
}
