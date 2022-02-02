using Entities.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations
{
    public class BookshelveConfiguration : IEntityTypeConfiguration<Bookshelve>
    {
        public void Configure(EntityTypeBuilder<Bookshelve> builder)
        {
            builder.HasKey(g => g.Id);
            builder.HasMany(g => g.Books);
        }
    }
}
