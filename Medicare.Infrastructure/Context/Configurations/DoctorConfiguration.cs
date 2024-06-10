

using Medicare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medicare.Infrastructure.Context.Configurations
{
    internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).HasMaxLength(50).IsRequired();
            builder.Property(d => d.Lastname).HasMaxLength(100).IsRequired();
            builder.Property(d => d.Email).HasMaxLength(100).IsRequired();
            builder.Property(d => d.Phone).HasMaxLength(25).IsRequired();
            builder.Property(d => d.IdentityCard).HasMaxLength(30);
            builder.Property(d => d.ImageRoute).HasMaxLength(500);

            builder.HasOne(d => d.Office)
                .WithMany(o => o.Doctors)
                .HasForeignKey(d => d.OfficeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(d => !d.Deleted);    
        }
    }
}
