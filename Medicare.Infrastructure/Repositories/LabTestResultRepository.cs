
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories
{
    public class LabTestResultRepository : Repository<LabTestResult>, ILabTestResultRepository
    {
        public LabTestResultRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<LabTestResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<LabTestResult, object>>[] includes)
        {
            try
            {
                includes = new Expression<Func<LabTestResult, object>>[]
                {
                      lrt => lrt.LabTest,
                      lrt => lrt.Patient,
                      lrt => lrt.Office,
                      lrt => lrt.Appointment
                };
                LabTestResult? result = await base.GetByIdAsync(id, cancellationToken, includes);

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<LabTestResult>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<LabTestResult, bool>> filter = null, params Expression<Func<LabTestResult, object>>[] includes)
        {
            try
            {
                includes = new Expression<Func<LabTestResult, object>>[]
                {
                      lrt => lrt.LabTest,
                      lrt => lrt.Patient,
                      lrt => lrt.Office,
                      lrt => lrt.Appointment
                };

                ICollection<LabTestResult> results = await base.GetByPagesAsync(page, cancellationToken, filter, includes);

                return results;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(LabTestResult entity, CancellationToken cancellationToken)
        {
            using (var transaction = await BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    LabTestResult existingLabTestResult = await GetByIdAsync(entity.Id, cancellationToken);

                    if (existingLabTestResult == null)
                    {
                        // Manejar el caso en que el usuario no se encuentre en la base de datos
                        throw new Exception($"El resultado con Id {entity.Id} no fue encontrado en la base de datos.");
                    }

                    existingLabTestResult.Result = entity.Result;
                    existingLabTestResult.IsCompleted = true;
                    existingLabTestResult.CompletedAt = DateTime.Now;

                    await base.UpdateAsync(existingLabTestResult, cancellationToken);
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
