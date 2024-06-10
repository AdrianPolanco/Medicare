using Medicare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Medicare.Infrastructure.Context.Configurations
{
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Reason).HasColumnType("text");
            builder.Property(a => a.State).HasConversion<int>();

            builder.HasOne(a => a.Patient).WithMany(p => p.Appointments).HasForeignKey(a => a.PatientId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Doctor).WithMany(d => d.Appointments).HasForeignKey(a => a.DoctorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Office).WithMany(o => o.Appointments).HasForeignKey(a => a.OfficeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(a => a.LabTestResults).WithOne(ltr => ltr.Appointment).HasForeignKey(ltr => ltr.AppointmentId).OnDelete(DeleteBehavior.Restrict);

            //Configurar TimeOnly a TimeSpan para que sea compatible con time de SQL Server
            var converter = new ValueConverter<TimeOnly, TimeSpan>(v => v.ToTimeSpan(), v => TimeOnly.FromTimeSpan(v));

            builder.Property(a => a.Hour).HasConversion(converter).HasColumnType("time");

        }
    }
}
