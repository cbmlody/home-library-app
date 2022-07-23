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
using System.Threading.Tasks;

using Xunit;

namespace HomeLibraryAPI.Tests.Controllers
{
    public class BookControllerTests
    {
        private readonly Mock<ILoggerManager> _logger;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly BookController _controller;

        private readonly List<Book> _books = new()
        {
            new Book { Id = new Guid("8d858c61-946b-4a16-87a0-8db089fc978b"), Title = "TestBookTitle1", Pages = 100, Volume = 1 },
            new Book { Id = new Guid("89a9e5d4-0b1a-46bf-b4f1-b08243f5d9c4"), Title = "TestBookTitle2", Pages = 200, Volume = 2 },
            new Book { Id = new Guid("2723c616-e3cb-4821-a7d8-58f317edc145"), Title = "TestBookTitle3", Pages = 300, Volume = 1 },
            new Book { Id = new Guid("d57be722-480c-403e-9210-a45ca86716e7"), Title = "TestBookTitle4", Pages = 400, Volume = 2 },
        };

        public BookControllerTests()
        {
            _logger = new Mock<ILoggerManager>();
            _repositoryWrapper = new Mock<IRepositoryWrapper>();
            _mapper = TestHelper.InitializeMapper();

            _controller = new BookController(_logger.Object, _repositoryWrapper.Object, _mapper);
        }

        [Fact]
        public async Task GetAllBooks_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _repositoryWrapper.Setup(m => m.Book.GetAllAsync()).ReturnsAsync(_books);

            // Act
            var result = await _controller.GetAllBooks();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAllBooks_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            _repositoryWrapper.Setup(m => m.Book.GetAllAsync()).ReturnsAsync(_books);

            // Act
            var result = await _controller.GetAllBooks();
            var bookResult = (result as OkObjectResult).Value;

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _repositoryWrapper.Verify(m => m.Book.GetAllAsync(), Times.Once());
            bookResult.Should().BeOfType<List<BookDto>>();
            bookResult.As<List<BookDto>>().Count.Should().Be(_books.Count);
        }

        [Fact]
        public async Task GetBookById_WhenCallled_ReturnsOkResponse()
        {
            // Arrange
            var id = Guid.Empty;
            _repositoryWrapper.Setup(m => m.Book.GetByIdAsync(id)).ReturnsAsync(new Book());

            // Act
            var result = await _controller.GetBookById(id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBookById_WhenCallled_ReturnsItem()
        {
            // Arrange
            var id = new Guid("8d858c61-946b-4a16-87a0-8db089fc978b");
            var book = new Book { Id = id, Title = "TestBookTitle2", Pages = 200, Volume = 2 };
            _repositoryWrapper.Setup(m => m.Book.GetByIdAsync(id)).ReturnsAsync(book);
            var expected = new BookDto { Id = id, Title = "TestBookTitle2", Pages = 200, Volume = 2 };

            // Act
            var result = await _controller.GetBookById(id);
            var bookResult = (result as OkObjectResult).Value;

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            bookResult.Should().BeOfType<BookDto>();
            bookResult.As<BookDto>().Id.Should().Be(expected.Id);
            bookResult.As<BookDto>().Title.Should().Be(expected.Title);
            bookResult.As<BookDto>().Pages.Should().Be(expected.Pages);
            bookResult.As<BookDto>().Volume.Should().Be(expected.Volume);
            _repositoryWrapper.Verify(m => m.Book.GetByIdAsync(id), Times.Once());
        }

        [Fact]
        public async Task GetBookById_WhenCallled_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.Empty;
            Book expected = null;
            _repositoryWrapper.Setup(m => m.Book.GetByIdAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetBookById(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _repositoryWrapper.Verify(m => m.Book.GetByIdAsync(id), Times.Once());
        }
    }
}
