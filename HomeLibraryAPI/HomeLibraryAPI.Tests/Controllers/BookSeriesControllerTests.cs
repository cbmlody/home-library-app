using AutoMapper;

using FluentAssertions;

using HomeLibraryAPI.Contracts;
using HomeLibraryAPI.Controllers;
using HomeLibraryAPI.EF.DTO;
using HomeLibraryAPI.EF.Models;
using HomeLibraryAPI.Tests.Helpers;

using Microsoft.AspNetCore.Mvc;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace HomeLibraryAPI.Tests.Controllers
{
    public class BookSeriesControllerTests
    {
        private readonly Mock<IRepositoryWrapper> _repository;
        private readonly Mock<ILoggerManager> _logger;
        private readonly IMapper _mapper;
        private readonly BookSeriesController _controller;

        private readonly List<BookSeries> _bookSeries = new()
        {
            new BookSeries { Id = new Guid("97d38b2a-5061-420c-b7a1-9d58073b142a"), Name = "First Book Series" },
            new BookSeries { Id = new Guid("34993693-447d-4f69-82a2-baa254e1ab8f"), Name = "Second Book Series" },
            new BookSeries { Id = new Guid("f2486e7b-c434-4378-99cf-6f94cad10168"), Name = "Last Book Series" }
        };

        public BookSeriesControllerTests()
        {
            _logger = new Mock<ILoggerManager>();
            _repository = new Mock<IRepositoryWrapper>();
            _mapper = TestHelper.InitializeMapper();
            _controller = new BookSeriesController(_logger.Object, _repository.Object, _mapper);
        }

        [Fact]
        public async Task GetAllBookSeries_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _repository.Setup(e => e.BookSeries.GetAllAsync()).ReturnsAsync(_bookSeries);

            // Act
            var result = await _controller.GetAllBookSeries();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAllBookSeries_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            _repository.Setup(e => e.BookSeries.GetAllAsync()).ReturnsAsync(_bookSeries);

            // Act
            var result = await _controller.GetAllBookSeries();
            var bookSeriesResult = (result as OkObjectResult).Value;

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _repository.Verify(m => m.BookSeries.GetAllAsync(), Times.Once());
            bookSeriesResult.As<List<BookSeriesDto>>().Count.Should().Be(_bookSeries.Count);
        }

        [Fact]
        public async Task GetBookSeriesById_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            Guid id = Guid.Empty;
            _repository.Setup(e => e.BookSeries.GetByIdAsync(id)).ReturnsAsync(new BookSeries());

            // Act
            var result = await _controller.GetBookSeriesById(id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBookSeriesById_WhenCalled_ReturnsSingleItem()
        {
            // Arrange
            var expected = _bookSeries.Last();
            var id = expected.Id;
            _repository.Setup(e => e.BookSeries.GetByIdAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetBookSeriesById(id);
            var bookSeriesResult = (result as OkObjectResult).Value;

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            bookSeriesResult.Should().BeOfType<BookSeriesDto>();
            bookSeriesResult.As<BookSeriesDto>().Id.Should().Be(expected.Id);
            bookSeriesResult.As<BookSeriesDto>().Name.Should().Be(expected.Name);
            _repository.Verify(e => e.BookSeries.GetByIdAsync(id), Times.Once());
        }
    }
}
