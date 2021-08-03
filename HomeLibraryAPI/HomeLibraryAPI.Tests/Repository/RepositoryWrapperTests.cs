using Entities;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Moq;

using Xunit;

namespace Repository.Tests
{
    public class RepositoryWrapperTests
    {
        [Fact]
        public void GetAuthorRepositoryTest_ProperRepositoryTypeReturned()
        {
            // Arrange
            var context = GetLibraryContextMock();

            // Act
            var repositoryWrapper = new RepositoryWrapper(context.Object);
            var result = repositoryWrapper.Author;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AuthorRepository>();
        }

        [Fact]
        public void GetBookRepositoryTest_ProperRepositoryTypeReturned()
        {
            // Arrange
            var context = GetLibraryContextMock();

            // Act
            var repositoryWrapper = new RepositoryWrapper(context.Object);
            var result = repositoryWrapper.Book;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BookRepository>();
        }

        [Fact]
        public void SaveTest_ProperMethodUsed()
        {
            // Arrange
            var context = GetLibraryContextMock();

            // Act
            var repositoryWrapper = new RepositoryWrapper(context.Object);
            repositoryWrapper.Save();

            // Assert
            context.Verify(x => x.SaveChanges());
        }

        private static Mock<LibraryContext> GetLibraryContextMock()
        {
            var dbContextOptionsBuidler = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("libraryTest");
            return new Mock<LibraryContext>(dbContextOptionsBuidler.Options);
        }
    }
}