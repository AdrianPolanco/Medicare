using Medicare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medicare.Infrastructure.Context.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Lastname).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Username).HasMaxLength(35).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(300).IsRequired();
            builder.Property(u => u.Deleted).HasDefaultValue(false);


            builder
                .HasOne(u => u.Office)
                .WithMany(o => o.Users)
                .HasForeignKey(u => u.OfficeId);

            builder
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

			builder.HasQueryFilter(u => !u.Deleted);
        }
    }
}
