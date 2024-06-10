using Medicare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medicare.Infrastructure.Context.Configurations
{
    internal class LabTestResultConfiguration : IEntityTypeConfiguration<LabTestResult>
    {
        public void Configure(EntityTypeBuilder<LabTestResult> builder)
        {
            builder.HasKey(ltr => ltr.Id);
            builder.Property(ltr => ltr.Result).HasColumnType("text");
            builder.Property(ltr => ltr.IsCompleted).HasDefaultValue(false);
            builder.HasOne(ltr => ltr.Patient).WithMany(p => p.LabTestResults).HasForeignKey(ltr => ltr.PatientId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ltr => ltr.LabTest).WithMany(lt => lt.LabTestResults).HasForeignKey(ltr => ltr.LabTestId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ltr => ltr.Office).WithMany(o => o.LabTestResults).HasForeignKey(ltr => ltr.OfficeId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(ltr => ltr.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.HasQueryFilter(ltr => !ltr.Deleted && !ltr.IsCompleted);  
        }
    }
}
