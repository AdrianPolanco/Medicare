using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;

namespace Medicare.Infrastructure.Repositories
{
    public class OfficeRepository : PartialRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(ApplicationDbContext context) : base(context)
        {
        }

		public override async Task UpdateAsync(Office entity, CancellationToken cancellationToken)
		{
			using (var transaction = await BeginTransactionAsync(cancellationToken))
			{
				try
				{
					Office existingOffice = await GetByIdAsync(entity.Id, cancellationToken);

					if (existingOffice == null)
					{
						// Manejar el caso en que el usuario no se encuentre en la base de datos
						throw new Exception($"La oficina con Id {entity.Id} no fue encontrado en la base de datos.");
					}

					existingOffice.OwnerId = entity.OwnerId;

					await base.UpdateAsync(existingOffice, cancellationToken);
					await SaveChangesAsync(cancellationToken);
					await CommitAsync(cancellationToken);
				}
				catch (Exception ex)
				{
					await RollbackAsync(cancellationToken);
					throw new Exception(ex.Message);
				}
			}
		}
	}
}
