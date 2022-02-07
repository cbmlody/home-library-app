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
    public class AuthorControllerTests
    {
        private readonly Mock<IRepositoryWrapper> _repositoryWrapper;
        private readonly Mock<ILoggerManager> _logger;
        private readonly IMapper _mapper;
        private readonly AuthorController _controller;

        private readonly List<Author> _authors = new()
        {
            new Author { Id = new Guid("256a97f7-6e03-4b11-b74c-ce1b6f44d87e"), FirstName = "Author1Name", LastName = "Author1LastName" },
            new Author { Id = new Guid("81498d45-9695-4445-819a-58a85d2dab40"), FirstName = "Author2Name", LastName = "Author2LastName" },
            new Author { Id = new Guid("c0035b44-395d-439d-8c44-df9e83eecae9"), FirstName = "Author3Name", LastName = "Author3LastName" },
        };

        public AuthorControllerTests()
        {
            _logger = new Mock<ILoggerManager>();
            _repositoryWrapper = new Mock<IRepositoryWrapper>();
            _mapper = TestHelper.InitializeMapper();
            _controller = new AuthorController(_logger.Object, _repositoryWrapper.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAuthors_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _repositoryWrapper.Setup(m => m.Author.GetAllAsync()).ReturnsAsync(_authors);

            // Act
            var result = await _controller.GetAllAuthors();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAllAuthors_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            _repositoryWrapper.Setup(m => m.Author.GetAllAsync()).ReturnsAsync(_authors);

            // Act
            var result = await _controller.GetAllAuthors();
            var authorsResult = (result as OkObjectResult).Value;

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _repositoryWrapper.Verify(m => m.Author.GetAllAsync(), Times.Once());
            authorsResult.As<List<AuthorDto>>().Count.Should().Be(_authors.Count);
        }

        [Fact]
        public async Task GetAuthorById_WhenCalled_ReturnsOkResult()
        {

        }

        [Fact]
        public async Task GetAuthorById_WhenCalled_ReturnsItem()
        {
            // Arrange
            var id = new Guid("256a97f7-6e03-4b11-b74c-ce1b6f44d87e");
            var author = new Author { Id = id, FirstName = "Author1Name", LastName = "Author1LastName" };
            _repositoryWrapper.Setup(m => m.Author.GetByIdAsync(id)).ReturnsAsync(author);
            var expected = new AuthorDto { Id = id, FirstName = "Author1Name", LastName = "Author1LastName" };

            // Act
            var result = await _controller.GetAuthorById(id);
            var authorResult = (result as OkObjectResult).Value;

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            authorResult.Should().BeOfType<AuthorDto>();
            authorResult.As<AuthorDto>().Id.Should().Be(expected.Id);
            authorResult.As<AuthorDto>().FirstName.Should().Be(expected.FirstName);
            authorResult.As<AuthorDto>().LastName.Should().Be(expected.LastName);
            _repositoryWrapper.Verify(m => m.Author.GetByIdAsync(id), Times.Once());
        }

        [Fact]
        public async Task GetAuthorById_WhenCalledWithIncorrectId_ReturnsNotFoundResult()
        {
            // Arrange
            Guid authorId = Guid.Empty;
            Author expected = null;
            _repositoryWrapper.Setup(m => m.Author.GetByIdAsync(authorId)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetAuthorById(authorId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _repositoryWrapper.Verify(m => m.Author.GetByIdAsync(authorId), Times.Once());
        }
    }
}
