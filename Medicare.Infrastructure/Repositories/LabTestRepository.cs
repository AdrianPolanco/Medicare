using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Domain.Repositories.Base;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories
{
    public class LabTestRepository: SelectableRepository<LabTest>, ILabTestRepository
    {
        public LabTestRepository(ApplicationDbContext applicationDbContext, ISelectableReadOnlyRepository<LabTest> repository) : base(applicationDbContext, repository)
        {
            
        }

        public override async Task<LabTest?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<LabTest, object>>[] includes)
        {
            return await base.GetByIdAsync(id, cancellationToken, lt => lt.Office);
        }

        public override async Task<ICollection<LabTest>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<LabTest, bool>> filter, params Expression<Func<LabTest, object>>[] includes)
        {
            return await base.GetByPagesAsync(page, cancellationToken, filter, lt => lt.Office);
        }

        public override async Task UpdateAsync(LabTest entity, CancellationToken cancellationToken)
        {
            using (var transaction = await BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    LabTest existingLabTest = await GetByIdAsync(entity.Id, cancellationToken);

                    if (existingLabTest == null)
                    {
                        throw new Exception($"La prueba de laboratorio con Id {entity.Id} no fue encontrado en la base de datos.");
                    }

                    existingLabTest.Name = entity.Name;

                    await base.UpdateAsync(existingLabTest, cancellationToken);
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
