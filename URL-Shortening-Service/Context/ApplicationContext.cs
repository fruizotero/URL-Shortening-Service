using System.Reflection;
using Microsoft.EntityFrameworkCore;
using URL_Shortening_Service.Models.entities;

namespace URL_Shortening_Service.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<ShortUrlEntity> ShortUrls { get; set; }
    }

}
