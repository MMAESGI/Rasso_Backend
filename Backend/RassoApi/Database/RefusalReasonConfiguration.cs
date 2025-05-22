using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RassoApi.Models;

namespace RassoApi.Database
{
    public class RefusalReasonConfiguration : IEntityTypeConfiguration<RefusalReason>
    {
        public void Configure(EntityTypeBuilder<RefusalReason> builder)
        {
            builder.ToTable("refusal_reasons");

            builder.HasKey(rr => rr.Id);

            builder.Property(rr => rr.Id).HasColumnName("id");
            builder.Property(rr => rr.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            builder.Property(rr => rr.Label).HasColumnName("label").HasMaxLength(100).IsRequired();
        }
    }

}
