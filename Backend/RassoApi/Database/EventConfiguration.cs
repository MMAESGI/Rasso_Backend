using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RassoApi.Models.EventModels;

namespace RassoApi.Database
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
            builder.Property(e => e.Description).HasColumnName("description").IsRequired();
            builder.Property(e => e.Date).HasColumnName("date").IsRequired();
            builder.Property(e => e.Location).HasColumnName("location").HasMaxLength(255);
            builder.Property(e => e.Latitude).HasColumnName("latitude");
            builder.Property(e => e.Longitude).HasColumnName("longitude");
            builder.Property(e => e.Category).HasColumnName("category").HasMaxLength(50);
            builder.Property(e => e.StatusId).HasColumnName("status_id");
            builder.Property(e => e.OrganizerId).HasColumnName("organizer_id");
            builder.Property(e => e.ModeratedByUserId).HasColumnName("moderated_by_id");
            builder.Property(e => e.ModeratedAt).HasColumnName("moderated_at");
            builder.Property(e => e.RefusalReasonId).HasColumnName("refusal_reason_id");
            builder.Property(e => e.RefusalComment).HasColumnName("refusal_comment");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            builder.HasOne(e => e.Status).WithMany().HasForeignKey(e => e.StatusId);
            builder.HasOne(e => e.Organizer).WithMany().HasForeignKey(e => e.OrganizerId);
            builder.HasOne(e => e.ModeratedByUser).WithMany().HasForeignKey(e => e.ModeratedByUserId);
            builder.HasOne(e => e.RefusalReason).WithMany().HasForeignKey(e => e.RefusalReasonId);
        }
    } 
}
