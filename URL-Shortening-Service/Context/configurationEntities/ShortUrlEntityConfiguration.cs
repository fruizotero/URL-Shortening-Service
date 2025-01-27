using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URL_Shortening_Service.Models.entities;

namespace URL_Shortening_Service.Context.configurationEntities
{
    public class ShortUrlEntityConfiguration : IEntityTypeConfiguration<ShortUrlEntity>
    {
        public void Configure(EntityTypeBuilder<ShortUrlEntity> builder)
        {
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            // valor por defecto
            builder.Property(x => x.UpdatedAt).HasDefaultValueSql(null);
        }
    }
}
