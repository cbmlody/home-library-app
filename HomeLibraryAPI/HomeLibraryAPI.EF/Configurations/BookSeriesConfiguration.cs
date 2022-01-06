using HomeLibraryAPI.EF.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeLibraryAPI.EF.Configurations
{
    public class BookSeriesConfiguration : IEntityTypeConfiguration<BookSeries>
{
        public void Configure(EntityTypeBuilder<BookSeries> builder)
    {
            builder.HasKey(b => b.Id);
            builder.HasMany(b => b.Books)
                .WithOne(b => b.BookSeries);
        }
    }
}
