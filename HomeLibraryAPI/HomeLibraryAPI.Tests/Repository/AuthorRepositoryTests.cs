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

    public class AuthorRepositoryTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(0)]
        public void FindAll_ObjectsStored_CollectionsAreEqual(int numberOfAuthors)
        {
            // Arrange
            var authors = GetTestAuthors(numberOfAuthors);

            var repository = GetInMemoryAuthorRepository(authors);

            // Act
            var result = repository.FindAll().ToList();

            // Assert
            result.Should().HaveCount(numberOfAuthors);
            result.Should().BeEquivalentTo(authors);
        }

        [Fact]
        public void FindAllByCondition_ObjectsStored_CollectionsAreEqual()
        {
            // Arrange
            var testAuthor = GetTestAuthor();

            List<Author> authors = new() { testAuthor };
            Expression<Func<Author, bool>> expression = x => string.Equals(x.FirstName, "First Name");

            var repository = GetInMemoryAuthorRepository(authors);

            // Act
            var result = repository.FindAllByCondition(expression).ToList();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(authors.Count);
            result.Should().BeEquivalentTo(authors);
        }

        [Fact]
        public void Create_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testAuthor = GetTestAuthor();

            var dbSetMock = new Mock<DbSet<Author>>();

            var dbContextOptions = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("Library Test")
                .Options;
            var context = new Mock<LibraryContext>(dbContextOptions);
            context.Setup(m => m.Set<Author>()).Returns(dbSetMock.Object);

            // Act
            var repository = new AuthorRepository(context.Object);
            repository.Create(testAuthor);

            // Assert
            context.Verify(x => x.Set<Author>());
            dbSetMock.Verify(x => x.Add(It.Is<Author>(y => y == testAuthor)));
        }

        [Fact]
        public void Delete_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testAuthor = GetTestAuthor();

            var dbSetMock = new Mock<DbSet<Author>>();

            var dbContextOptions = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("Library Test")
                .Options;
            var context = new Mock<LibraryContext>(dbContextOptions);
            context.Setup(m => m.Set<Author>()).Returns(dbSetMock.Object);

            // Act
            var repository = new AuthorRepository(context.Object);
            repository.Delete(testAuthor);

            // Assert
            context.Verify(x => x.Set<Author>());
            dbSetMock.Verify(x => x.Remove(It.Is<Author>(y => y == testAuthor)));
        }

        [Fact]
        public void Update_TestObjectPassed_ProperMethodCalled()
        {
            // Arrange
            var testAuthor = GetTestAuthor();

            var dbSetMock = new Mock<DbSet<Author>>();

            var dbContextOptions = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("Library Test")
                .Options;
            var context = new Mock<LibraryContext>(dbContextOptions);
            context.Setup(m => m.Set<Author>()).Returns(dbSetMock.Object);

            // Act
            var repository = new AuthorRepository(context.Object);
            repository.Update(testAuthor);

            // Assert
            context.Verify(x => x.Set<Author>());
            dbSetMock.Verify(x => x.Update(It.Is<Author>(y => y == testAuthor)));
        }

        private IAuthorRepository GetInMemoryAuthorRepository(List<Author> authors = null)
        {
            if (authors is null)
                authors = new();

            var dbSetMock = new Mock<DbSet<Author>>();
            dbSetMock.As<IQueryable<Author>>().Setup(x => x.Provider).Returns(authors.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Author>>().Setup(x => x.Expression).Returns(authors.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Author>>().Setup(x => x.ElementType).Returns(authors.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Author>>().Setup(x => x.GetEnumerator()).Returns(authors.AsQueryable().GetEnumerator());

            var dbContextOptionsBuidler = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("libraryTest");
            var context = new Mock<LibraryContext>(dbContextOptionsBuidler.Options);
            context.Setup(x => x.Set<Author>()).Returns(dbSetMock.Object);

            return new AuthorRepository(context.Object);
        }

        private List<Author> GetTestAuthors(int numberOfAuthors)
        {
            var authorsFaker = new Faker<Author>()
                .RuleFor(a => a.Id, _ => Guid.NewGuid())
                .RuleFor(a => a.FirstName, f => f.Person.FirstName)
                .RuleFor(a => a.MiddleName, f => f.Person.FirstName)
                .RuleFor(a => a.LastName, f => f.Person.LastName)
                .RuleFor(a => a.Books, _ => null);

            return numberOfAuthors > 0 ? authorsFaker.Generate(numberOfAuthors) : new();
        }

        private Author GetTestAuthor()
        {
            return new Author
            {
                Id = Guid.Parse("7bd2336b-25ca-4e92-b096-16583b801bb0"),
                FirstName = "First Name",
                MiddleName = "Middle Name",
                LastName = "Last Name",
                Books = null
            };
        }
    }
}