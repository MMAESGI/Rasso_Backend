using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RassoApi.Models.EventModels;

namespace RassoApi.Database
{
    public class EventStatusConfiguration : IEntityTypeConfiguration<EventStatus>
    {
        public void Configure(EntityTypeBuilder<EventStatus> builder)
        {
            builder.ToTable("event_statuses");

            builder.HasKey(es => es.Id);

            builder.Property(es => es.Id).HasColumnName("id");
            builder.Property(es => es.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        }
    }

}
