using Medicare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Medicare.Infrastructure.Context.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Lastname).IsRequired().HasMaxLength(100);
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(300);
            builder.Property(p => p.IdentityCard).IsRequired().HasMaxLength(20);
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.IsSmoker).IsRequired();
            builder.Property(p => p.HasAllergy).IsRequired();
            builder.Property(p => p.Photo).HasMaxLength(300);
            builder.Property(p => p.Deleted).HasDefaultValue(false);

            /* ValueConverter<DateOnly, DateTime> dateOnlyConverter = new(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));

             builder.Property(p => p.BirthDate).HasConversion(dateOnlyConverter);*/

            builder.HasOne(p => p.Office)
                .WithMany(o => o.Patients)
                .HasForeignKey(p => p.OfficeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasQueryFilter(p => !p.Deleted);
        }
    }
}
