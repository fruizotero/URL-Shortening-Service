using Microsoft.EntityFrameworkCore;
using URL_Shortening_Service.Models.DTOs;
using URL_Shortening_Service.Models.entities;

namespace URL_Shortening_Service.Context.respositories
{
    public class ShortUrlRepository
    {

        private readonly ApplicationContext _context;

        public ShortUrlRepository(ApplicationContext context)
        {
            _context = context;
        }


        public virtual async Task<ShortUrlEntity> GetOriginalUrlByShortCode(string shortCode)
        {
            return await _context.ShortUrls.FirstOrDefaultAsync(x => x.ShortCode == shortCode);
        }

        public virtual async Task<ShortUrlEntity> AddOriginalUrl(string url, string shortCode)
        {

            var shortUrl = new ShortUrlEntity
            {
                Url = url,
                ShortCode = shortCode,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();
            return shortUrl;
        }

        public virtual async Task<ShortUrlEntity> UpdateShortUrl(string url, string shortCode)
        {

            var shortUrl = await GetOriginalUrlByShortCode(shortCode);
            shortUrl.Url = url;
            shortUrl.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return shortUrl;
        }

      
    }
}
