using Medicare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medicare.Infrastructure.Context.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(30).IsRequired();
            builder.Property(r => r.Deleted).HasDefaultValue(false);
            builder.HasData(new Role
            {
                Id = Guid.NewGuid(),
                Name = "Administrador"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Asistente"
            });
        }
    }
}
