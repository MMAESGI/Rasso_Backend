using Identity.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Identity.Database
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_roles");

            builder.HasKey(ur => ur.Id);

            builder.Property(ur => ur.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(ur => ur.Code)
                .HasColumnName("code")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(ur => ur.Code)
                .IsUnique();

            builder.Property(ur => ur.Label)
                .HasColumnName("label")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
