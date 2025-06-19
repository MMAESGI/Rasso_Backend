using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RassoApi.Models.EventModels;

namespace RassoApi.Database
{
    public class EventMediaConfiguration : IEntityTypeConfiguration<EventMedia>
    {
        public void Configure(EntityTypeBuilder<EventMedia> builder)
        {
            builder.ToTable("event_media");

            builder.HasKey(ei => ei.Id);

            builder.Property(ei => ei.Id).HasColumnName("id");
            builder.Property(ei => ei.EventId).HasColumnName("event_id");
            builder.Property(ei => ei.S3Url).HasColumnName("s3_url").HasMaxLength(500).IsRequired();
            builder.Property(ei => ei.Filename).HasColumnName("filename").HasMaxLength(255);
            builder.Property(ei => ei.Description).HasColumnName("description").HasMaxLength(255);
            builder.Property(ei => ei.UploadedAt).HasColumnName("uploaded_at");

            builder.HasOne(ei => ei.Event)
                .WithMany(e => e.Images)
                .HasForeignKey(ei => ei.EventId);
        }
    }

}
