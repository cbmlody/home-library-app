using HomeLibraryAPI.EF.Models;
using HomeLibraryAPI.EF.Models.Enums;

using Microsoft.EntityFrameworkCore;

using System;

namespace HomeLibraryAPI.EF.ModelBuilderExtension
{
    internal static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Author>().HasData(
                new Author
                {
                    Id = new Guid("dcfad00e-0ea4-458a-b002-9ba54935c52c"),
                    FirstName = "Author",
                    LastName = "Test",
                },
                new Author
                {
                    Id = new Guid("08A61211-6BFD-4FFB-9C1B-395F6D1B17C4"),
                    FirstName = "AuthorName",
                    MiddleName = "Middle",
                    LastName = "Last"
                }
            );

            builder.Entity<Book>().HasData(
                new Book
                {
                    Id = new Guid("c80e88ef-455f-42fc-ad9f-0cec12d2363f"),
                    Title = "Test Book Title",
                    Pages = 100,
                    EAN = "1111111111111",
                    CoverType = CoverType.Soft,
                    Volume = 1
                },
                new Book
                {
                    Id = new Guid("69c10efb-0fc3-4b87-bb1b-a06263d19d63"),
                    Title = "Test Book",
                    Pages = 150,
                    EAN = "2222222222222",
                    CoverType = CoverType.Hard,
                    Volume = 1
                },
                new Book
                {
                    Id = new Guid("042ef9b5-c3e1-45ee-bf40-f1ff39510348"),
                    Title = "Dummy Book",
                    Pages = 620,
                    EAN = "1212111111111",
                    CoverType = CoverType.Soft,
                    Volume = 1
                },
                new Book
                {
                    Id = new Guid("f3a34a7e-fa46-4c25-bbeb-55b5eacd7f93"),
                    Title = "Test Book Title",
                    Pages = 400,
                    EAN = "1313131313133",
                    CoverType = CoverType.Soft,
                    Volume = 2
                }
                );

            builder.Entity<Publisher>().HasData(
                new Publisher
                {
                    Id = new Guid("e47b5498-596e-4aa9-aaf5-5778ce5170b8"),
                    Name = "Dummy Publisher"
                });

            builder.Entity<BookSeries>().HasData(
                new BookSeries
                {
                    Id = new Guid("216de672-375e-4412-9e5e-4fce850a0d7c"),
                    Name = "Dummy Book series"
                });
        }
    }
}
