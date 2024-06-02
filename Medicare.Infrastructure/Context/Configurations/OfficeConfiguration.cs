using Medicare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Medicare.Infrastructure.Context.Configurations
{
    internal class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).HasMaxLength(50).IsRequired();
            builder.Property(o => o.Deleted).HasDefaultValue(false);

            builder
                .HasOne(o => o.Owner)
                .WithOne(u => u.OwnedOffice)
                .HasForeignKey<User>(o => o.OwnedOfficeId);

        }
    }
}
