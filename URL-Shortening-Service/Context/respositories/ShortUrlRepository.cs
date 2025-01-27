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
    }
}
