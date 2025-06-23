using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RassoApi.Models;

namespace RassoApi.Database
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("favorites");

            builder.HasKey(f => new { f.UserId, f.EventId });

            builder.Property(f => f.UserId).HasColumnName("user_id");
            builder.Property(f => f.EventId).HasColumnName("event_id");
            builder.Property(f => f.CreatedAt).HasColumnName("created_at");
            builder.HasOne(f => f.Event).WithMany().HasForeignKey(f => f.EventId);
        }
    }

}
