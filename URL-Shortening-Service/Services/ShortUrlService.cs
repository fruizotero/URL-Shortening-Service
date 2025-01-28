using URL_Shortening_Service.Context.respositories;
using URL_Shortening_Service.Exceptions;
using URL_Shortening_Service.Models.DTOs;
using URL_Shortening_Service.Models.entities;

namespace URL_Shortening_Service.Services
{
    public class ShortUrlService
    {

        private readonly ShortUrlRepository _shortUrlRepository;

        public ShortUrlService(ShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<ShortUrlDTO> GetShortUrlByShortCode(string shortCode)
        {
            var shortUrl = await _shortUrlRepository.GetOriginalUrlByShortCode(shortCode);

            if (shortUrl == null)
            {
                throw new ShortUrlNotFoundException("Short URL not found");
            }

            return new ShortUrlDTO
            {
                Id = shortUrl.Id,
                Url = shortUrl.Url,
                ShortCode = shortUrl.ShortCode,
                CreatedAt = shortUrl.CreatedAt,
                UpdatedAt = shortUrl.UpdatedAt
            };
        }

        public async Task<ShortUrlDTO> AddShortUrl(ShortUrlRequestDTO shortUrlRequestDTO)
        {

            var url = shortUrlRequestDTO.Url;

            if (string.IsNullOrEmpty(url))
            {
                throw new ShortUrlCannotBeEmpty("URL cannot be empty");
            }

        
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                throw new ShortUrlIsNotValid("URL is not valid");
            }
            var shortCode = Guid.NewGuid().ToString().Substring(0, 6);
            var shortUrl = await _shortUrlRepository.AddOriginalUrl(url, shortCode);
            return new ShortUrlDTO
            {
                Id = shortUrl.Id,
                Url = shortUrl.Url,
                ShortCode = shortUrl.ShortCode,
                CreatedAt = shortUrl.CreatedAt,
                UpdatedAt = shortUrl.UpdatedAt
            };
        }

    }
}
