﻿using URL_Shortening_Service.Context.respositories;
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
                throw new ShortUrlNotFoundException("Short code not found");
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
       
            if(await CheckIfUrlExists(url))
            {
                throw new ShortUrlAlreadyExists("URL already exists");
            }

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

        public async Task<ShortUrlDTO> UpdateOriginalUrl(ShortUrlRequestDTO shortUrlRequestDTO, string shortCode)
        {
            var url = shortUrlRequestDTO.Url;

            var shortUrl = await _shortUrlRepository.GetOriginalUrlByShortCode(shortCode);

            if (shortUrl == null)
            {
                throw new ShortUrlNotFoundException("Short code not found");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ShortUrlCannotBeEmpty("URL cannot be empty");

            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                throw new ShortUrlIsNotValid("URL is not valid");
            }

            var updatedShortUrl = await _shortUrlRepository.UpdateShortUrl(url, shortCode);

            return new ShortUrlDTO
            {
                Id = updatedShortUrl.Id,
                Url = updatedShortUrl.Url,
                ShortCode = updatedShortUrl.ShortCode,
                CreatedAt = updatedShortUrl.CreatedAt,
                UpdatedAt = updatedShortUrl.UpdatedAt
            };



        }

        public async Task DeleteShortUrl(string shortCode)
        {
            var shortUrl = await _shortUrlRepository.GetOriginalUrlByShortCode(shortCode);
            if (shortUrl == null)
            {
                throw new ShortUrlNotFoundException("Short code not found");
            }
            await _shortUrlRepository.DeleteShortUrl(shortCode);
        }


        public async Task IncrementAccessAcount(string shortCode)
        {
            await _shortUrlRepository.IncrementAccessCount(shortCode);
        }

        public async Task<ShortUrlDtoWithAccessAcount> Stats(string shortCode)
        {
            var shortUrl = await _shortUrlRepository.GetOriginalUrlByShortCode(shortCode);

            if (shortUrl == null)
            {
                throw new ShortUrlNotFoundException("Short code not found");
            }


            return new ShortUrlDtoWithAccessAcount
            {
                Id = shortUrl.Id,
                Url = shortUrl.Url,
                ShortCode = shortUrl.ShortCode,
                CreatedAt = shortUrl.CreatedAt,
                UpdatedAt = shortUrl.UpdatedAt,
                AccessCount = shortUrl.AccessCount
            };
        }

        public async Task<bool> CheckIfUrlExists(string url)
        {
            var shortUrl = await _shortUrlRepository.GetOriginalUrlByUrl(url);
            return shortUrl != null;
        }
    }
}
