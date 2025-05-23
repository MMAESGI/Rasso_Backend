
using Microsoft.EntityFrameworkCore;
using Identity.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Database
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnType("char(36)");
            builder.Property(u => u.Name).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(150).IsRequired();
            builder.Property(u => u.PasswordHash).HasColumnType("text").IsRequired();
            builder.Property(u => u.IsActive).HasDefaultValue(true);
            builder.Property(u => u.CreatedAt).HasColumnType("timestamp");
            builder.Property(u => u.AnonymizedAt).HasColumnType("timestamp");

            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .HasConstraintName("fk_users_role");
        }
    }
}
