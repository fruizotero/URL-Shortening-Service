using Microsoft.EntityFrameworkCore;
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

        public virtual async Task<ShortUrlEntity> AddOriginalUrl(string url)
        {
            var shortCode = Guid.NewGuid().ToString().Substring(0, 6);
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
    }
}
