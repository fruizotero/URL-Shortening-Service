using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Moq;
using URL_Shortening_Service.Context;
using URL_Shortening_Service.Context.respositories;
using URL_Shortening_Service.Exceptions;
using URL_Shortening_Service.Models.DTOs;
using URL_Shortening_Service.Models.entities;
using URL_Shortening_Service.Services;

namespace URL_Shortening_Service.Tests
{
    public class ShortUrlServiceTests
    {

        private readonly ShortUrlService _shortUrlService;
        private readonly Mock<ShortUrlRepository> _shortUrlRepositoryMock;
        private readonly ApplicationContext _context;


        public ShortUrlServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase(databaseName: "TestDatabase") // Nombre único para la base de datos en memoria
              .Options;

            _context = new ApplicationContext(options);
            _shortUrlRepositoryMock = new Mock<ShortUrlRepository>(_context);
            _shortUrlService = new ShortUrlService(_shortUrlRepositoryMock.Object);
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        [Fact]
        public async Task GetOriginalUrlByShortCode_ShouldReturnEntity_WhenShortCodeExists()
        {
            // Arrange
            var shortCode = "abc123";
            var shortUrlEntity = new ShortUrlEntity
            {
                Id = 1,
                Url = "https://www.google.com",
                ShortCode = "abc123"
            };
            _shortUrlRepositoryMock.Setup(x => x.GetOriginalUrlByShortCode(shortCode)).ReturnsAsync(shortUrlEntity);
            // Act
            var result = await _shortUrlService.GetShortUrlByShortCode(shortCode);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(shortUrlEntity.Url, result.Url);
            Assert.Equal(shortUrlEntity.ShortCode, result.ShortCode);

            _shortUrlRepositoryMock.Verify(x => x.GetOriginalUrlByShortCode(shortCode), Times.Once);
        }

        // devuelve exepcion shortUrlNotFoundException cuando no existe el shortCode
        [Fact]
        public async Task GetOriginalUrlByShortCode_ShouldThrowShortUrlNotFoundException_WhenShortCodeDoesNotExist()
        {
            // Arrange
            var shortCode = "abc123";
            _shortUrlRepositoryMock.Setup(x => x.GetOriginalUrlByShortCode(shortCode)).ReturnsAsync((ShortUrlEntity)null);
            // Act
            var exception = await Assert.ThrowsAsync<ShortUrlNotFoundException>(() => _shortUrlService.GetShortUrlByShortCode(shortCode));
            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Short URL not found", exception.Message);
            _shortUrlRepositoryMock.Verify(x => x.GetOriginalUrlByShortCode(shortCode), Times.Once);
        }

        // devuelve exepcion shortUrlCannotBeEmpty cuando la url es vacia
        [Fact]
        public async Task AddShortUrl_ShouldThrowShortUrlCannotBeEmpty_WhenUrlIsEmpty()
        {
            // Arrange
            var urlRequest = new ShortUrlRequestDTO {
                Url = ""
            };
            var shortCode = "abc123";
            // Act
            var exception = await Assert.ThrowsAsync<ShortUrlCannotBeEmpty>(() => _shortUrlService.AddShortUrl(urlRequest));
            // Assert
            Assert.NotNull(exception);
            Assert.Equal("URL cannot be empty", exception.Message);
            _shortUrlRepositoryMock.Verify(x => x.AddOriginalUrl(urlRequest.Url, shortCode), Times.Never);
        }


        // devuelve exepcion shortUrlIsNotValid cuando la url no es valida
        [Fact]
        public async Task AddShortUrl_ShouldThrowShortUrlIsNotValid_WhenUrlIsNotValid()
        {
            // Arrange
            var urlRequest = new ShortUrlRequestDTO
            {
                Url = "google.com"
            };
            var shortCode = "abc123";
            // Act
            var exception = await Assert.ThrowsAsync<ShortUrlIsNotValid>(() => _shortUrlService.AddShortUrl(urlRequest));
            // Assert
            Assert.NotNull(exception);
            Assert.Equal("URL is not valid", exception.Message);
            _shortUrlRepositoryMock.Verify(x => x.AddOriginalUrl(urlRequest.Url, shortCode), Times.Never);
        }


        // devuelve la entidad ShortUrlEntity cuando la url es valida
        [Fact]
        public async Task AddShortUrl_ShouldReturnDTO_WhenUrlIsValid()
        {
            // Arrange
            var urlRequest = new ShortUrlRequestDTO
            {
                Url = "https://www.google.com"
            };


            var shortUrlEntity = new ShortUrlEntity
            {
                Id = 1,
                Url = "https://www.google.com",
                ShortCode = "abc123"
            };

            _shortUrlRepositoryMock.Setup(x => x.AddOriginalUrl(urlRequest.Url, It.IsAny<string>())).ReturnsAsync(shortUrlEntity);

            // Act
            var shortUrlDTO = await _shortUrlService.AddShortUrl(urlRequest);
            // Assert
            Assert.NotNull(shortUrlDTO);
            Assert.Equal(shortUrlEntity.Url, shortUrlDTO.Url);
            Assert.Equal(shortUrlEntity.ShortCode, shortUrlDTO.ShortCode);
            _shortUrlRepositoryMock.Verify(x => x.AddOriginalUrl(urlRequest.Url, It.IsAny<string>()), Times.Once);
        }
    }
}
