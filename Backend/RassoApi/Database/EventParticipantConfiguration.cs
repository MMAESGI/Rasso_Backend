using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RassoApi.Models.EventModels;

namespace RassoApi.Database
{
    public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.ToTable("event_participants");

            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id).HasColumnName("id");
            builder.Property(ep => ep.EventId).HasColumnName("event_id");
            builder.Property(ep => ep.UserId).HasColumnName("user_id");
            builder.Property(ep => ep.RegisteredAt).HasColumnName("registered_at");

            builder.HasOne(ep => ep.Event).WithMany().HasForeignKey(ep => ep.EventId);
        }
    }

}
