

using Medicare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medicare.Infrastructure.Context.Configurations
{
    public class LabTestConfiguration : IEntityTypeConfiguration<LabTest>
    {
        public void Configure(EntityTypeBuilder<LabTest> builder)
        {
            builder.HasKey(lt => lt.Id);
            builder.Property(lt => lt.Name).HasMaxLength(100).IsRequired();

            builder.HasData(
                new LabTest { Id = Guid.NewGuid(), Name = "Blood Test"},
                new LabTest { Id = Guid.NewGuid(), Name = "Urine Test"},
                new LabTest { Id = Guid.NewGuid(), Name = "X-Ray"},
                new LabTest { Id = Guid.NewGuid(), Name = "MRI"},
                new LabTest { Id = Guid.NewGuid(), Name = "CT Scan"});

            builder.HasQueryFilter(lt => !lt.Deleted);
        }
    }
}
