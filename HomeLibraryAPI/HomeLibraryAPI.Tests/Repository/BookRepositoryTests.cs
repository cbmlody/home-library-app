using Bogus;

using Contracts.Repositories;

using Entities;
using Entities.Models;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Xunit;

namespace Repository.Tests
{
    public class BookRepositoryTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(2)]
        [InlineData(12)]
        [InlineData(0)]
        public void FindAll_ObjectsStored_CollectionsAreEqual(int numberOfBooks)
        {
            // Arrange
            var books = GetTestBooks(numberOfBooks);

            var repository = GetInMemoryBookRepository(books);

            // Act
            var result = repository.FindAll().ToList();

            // Assert
            result.Should().HaveCount(numberOfBooks);
            result.Should().BeEquivalentTo(books);
        }

        [Fact]
        public void FindAllByCondition_ObjectsStored_CollectionsAreEqual()
        {
            // Arrange
            var testBook = GetTestBook();

            List<Book> books = new() { testBook };
            Expression<Func<Book, bool>> expression = x => string.Equals(x.Title, "Book Title");

            var repository = GetInMemoryBookRepository(books);

            // Act
            var result = repository.FindAllByCondition(expression).ToList();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(books.Count);
            result.Should().BeEquivalentTo(books);
        }

        [Fact]
        public void Create_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testBook = GetTestBook();

            var dbSetMock = new Mock<DbSet<Book>>();

            var dbContextOptions = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("Library Test")
                .Options;
            var context = new Mock<LibraryContext>(dbContextOptions);
            context.Setup(m => m.Set<Book>()).Returns(dbSetMock.Object);

            // Act
            var repository = new BookRepository(context.Object);
            repository.Create(testBook);

            // Assert
            context.Verify(x => x.Set<Book>());
            dbSetMock.Verify(x => x.Add(It.Is<Book>(y => y == testBook)));
        }

        [Fact]
        public void Delete_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testBook = GetTestBook();

            var dbSetMock = new Mock<DbSet<Book>>();

            var dbContextOptions = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("Library Test")
                .Options;
            var context = new Mock<LibraryContext>(dbContextOptions);
            context.Setup(m => m.Set<Book>()).Returns(dbSetMock.Object);

            // Act
            var repository = new BookRepository(context.Object);
            repository.Delete(testBook);

            // Assert
            context.Verify(x => x.Set<Book>());
            dbSetMock.Verify(x => x.Remove(It.Is<Book>(y => y == testBook)));
        }

        [Fact]
        public void Update_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testBook = GetTestBook();

            var dbSetMock = new Mock<DbSet<Book>>();

            var dbContextOptions = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("Library Test")
                .Options;
            var context = new Mock<LibraryContext>(dbContextOptions);
            context.Setup(m => m.Set<Book>()).Returns(dbSetMock.Object);

            // Act
            var repository = new BookRepository(context.Object);
            repository.Update(testBook);

            // Assert
            context.Verify(x => x.Set<Book>());
            dbSetMock.Verify(x => x.Update(It.Is<Book>(y => y == testBook)));
        }

        private static IBookRepository GetInMemoryBookRepository(List<Book> books = null)
        {
            if (books is null)
                books = new();

            var dbSetMock = new Mock<DbSet<Book>>();
            dbSetMock.As<IQueryable<Book>>().Setup(x => x.Provider).Returns(books.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Book>>().Setup(x => x.Expression).Returns(books.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Book>>().Setup(x => x.ElementType).Returns(books.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Book>>().Setup(x => x.GetEnumerator()).Returns(books.AsQueryable().GetEnumerator());

            var dbContextOptionsBuidler = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("libraryTest");
            var context = new Mock<LibraryContext>(dbContextOptionsBuidler.Options);
            context.Setup(x => x.Set<Book>()).Returns(dbSetMock.Object);

            return new BookRepository(context.Object);
        }

        private static List<Book> GetTestBooks(int numberOfBooks)
        {
            var booksFaker = new Faker<Book>()
                .RuleFor(b => b.Id, _ => Guid.NewGuid())
                .RuleFor(b => b.Title, f => f.Hacker.Phrase())
                .RuleFor(b => b.EAN, f => f.Commerce.Ean13())
                .RuleFor(b => b.Authors, _ => null);

            return numberOfBooks > 0 ? booksFaker.Generate(numberOfBooks) : new();
        }

        private static Book GetTestBook()
        {
            return new Book
            {
                Id = Guid.Parse("7bd2336b-25ca-4e92-b096-16583b801bb0"),
                Title = "Book Title",
                EAN = "123-45-678-9012-3",
                Authors = null
            };
        }
    }
}