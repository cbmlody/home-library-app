using Bogus;

using Contracts.Repositories;

using Entities;
using Entities.Models;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Moq;

using Repository;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Repository.Tests
{
    public class BookSeriesRepositoryTest
    {
        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        [InlineData(0)]
        [InlineData(105)]
        public void FindAll_ObjectsStored_CollectionsAreEqual(int numberOfSeries)
        {
            // Arrange
            var series = GetTestBookSeries(numberOfSeries);

            var repository = GetInMemoryBookSeriesRepository(series);

            // Act
            var result = repository.FindAll().ToList();

            // Assert
            result.Should().HaveCount(numberOfSeries);
            result.Should().BeEquivalentTo(series);
        }

        private static IBookSeriesRepository GetInMemoryBookSeriesRepository(List<BookSeries> series = null)
        {
            if (series is null)
                series = new();

            var dbSetMock = new Mock<DbSet<BookSeries>>();
            dbSetMock.As<IQueryable<BookSeries>>().Setup(x => x.Provider).Returns(series.AsQueryable().Provider);
            dbSetMock.As<IQueryable<BookSeries>>().Setup(x => x.Expression).Returns(series.AsQueryable().Expression);
            dbSetMock.As<IQueryable<BookSeries>>().Setup(x => x.ElementType).Returns(series.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<BookSeries>>().Setup(x => x.GetEnumerator()).Returns(series.AsQueryable().GetEnumerator());

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("libraryTest");
            var context = new Mock<LibraryContext>(dbContextOptionsBuilder.Options);
            context.Setup(x => x.Set<BookSeries>()).Returns(dbSetMock.Object);

            return new BookSeriesRepository(context.Object);
        }

        private static List<BookSeries> GetTestBookSeries(int numberOfSeries)
        {
            var bookSeriesFaker = new Faker<BookSeries>()
                .RuleFor(bs => bs.Id, _ => Guid.NewGuid())
                .RuleFor(bs => bs.Name, f => f.Hacker.Phrase())
                .RuleFor(bs => bs.Books, _ => null);

            return numberOfSeries > 0 ? bookSeriesFaker.Generate(numberOfSeries) : new();
        }
    }
}
