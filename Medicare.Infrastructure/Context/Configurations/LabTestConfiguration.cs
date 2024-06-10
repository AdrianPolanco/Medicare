

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

            builder.HasOne(lt => lt.Office)
                .WithMany(o => o.LabTests)
                .HasForeignKey(lt => lt.OfficeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(lt => !lt.Deleted);
        }
    }
}
