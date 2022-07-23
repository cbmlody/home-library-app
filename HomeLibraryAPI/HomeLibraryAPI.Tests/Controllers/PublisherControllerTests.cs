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
    public class PublisherControllerTests
    {
        private readonly Mock<ILoggerManager> _logger;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapper;
        private readonly IMapper _mapper;

        private readonly PublisherController _controller;

        private readonly List<Publisher> _publishers = new()
        {
            new Publisher { Id = new Guid("dbf1673f-26da-4dae-a035-53077cb40504"), Name = "TestPublisher1" },
            new Publisher { Id = new Guid("ca492419-102f-49bc-b903-5ce85ceea7cf"), Name = "TestPublisher2" }
        };

        public PublisherControllerTests()
        {
            _logger = new Mock<ILoggerManager>();
            _repositoryWrapper = new Mock<IRepositoryWrapper>();
            _mapper = TestHelper.InitializeMapper();

            _controller = new PublisherController(_logger.Object, _repositoryWrapper.Object, _mapper);
        }

        [Fact]
        public async Task GetAllPublishers_WhenCalled_ReturnsOkResponse()
        {
            // Arrange
            _repositoryWrapper.Setup(m => m.Publisher.GetAllAsync()).ReturnsAsync(_publishers);

            // Act
            var result = await _controller.GetAllPublishers();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAllPublishers_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            _repositoryWrapper.Setup(m => m.Publisher.GetAllAsync()).ReturnsAsync(_publishers);

            // Act
            var result = await _controller.GetAllPublishers();
            var publishersResult = (result as OkObjectResult).Value;

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _repositoryWrapper.Verify(m => m.Publisher.GetAllAsync(), Times.Once());
            publishersResult.Should().BeOfType<List<PublisherDto>>();
            publishersResult.As<List<PublisherDto>>().Count.Should().Be(_publishers.Count);
        }

        [Fact]
        public async Task GetPublisherById_WhenCalled_ReturnsOkResponse()
        {
            // Arrange
            var id = Guid.Empty;
            _repositoryWrapper.Setup(m => m.Publisher.GetByIdAsync(id)).ReturnsAsync(new Publisher());

            // Act
            var result = await _controller.GetPublisherById(id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetPublisherById_WhenCalled_ReturnsItem()
        {
            // Arrange
            var id = new Guid("dbf1673f-26da-4dae-a035-53077cb40504");
            var publisher = new Publisher { Id = id, Name = "TestPublisher1" };
            _repositoryWrapper.Setup(m => m.Publisher.GetByIdAsync(id)).ReturnsAsync(publisher);
            var expected = new PublisherDto { Id = id, Name = "TestPublisher1" };

            // Act
            var result = await _controller.GetPublisherById(id);
            var publisherResult = (result as OkObjectResult).Value;

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _repositoryWrapper.Verify(m => m.Publisher.GetByIdAsync(id), Times.Once());
            publisherResult.Should().BeOfType<PublisherDto>();
            publisherResult.As<PublisherDto>().Id.Should().Be(expected.Id);
            publisherResult.As<PublisherDto>().Name.Should().Be(expected.Name);
        }

        [Fact]
        public async Task GetPublisherById_WhenCalled_ReturnsNotFoundResponse()
        {
            // Arrange
            var id = Guid.Empty;
            Publisher publisher = null;
            _repositoryWrapper.Setup(m => m.Publisher.GetByIdAsync(id)).ReturnsAsync(publisher);

            // Act
            var result = await _controller.GetPublisherById(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _repositoryWrapper.Verify(m => m.Publisher.GetByIdAsync(id), Times.Once());
        }
    }
}
