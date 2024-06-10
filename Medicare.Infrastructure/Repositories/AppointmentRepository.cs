using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<Appointment, object>>[] includes)
        {
            try
            {
                includes = new Expression<Func<Appointment, object>>[]
                {
                    a => a.Patient,
                    a => a.Doctor,
                    a => a.Office,
                    a => a.LabTestResults
                };
                Appointment? appointment = await base.GetByIdAsync(id, cancellationToken, includes);

                return appointment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Appointment>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<Appointment, bool>> filter = null, params Expression<Func<Appointment, object>>[] includes)
        {
            try
            {
                includes = new Expression<Func<Appointment, object>>[]
                {
                    a => a.Patient,
                    a => a.Doctor,
                    a => a.Office,
                    a => a.LabTestResults
                };

                ICollection<Appointment> appointments = await base.GetByPagesAsync(page, cancellationToken, filter, includes);

                return appointments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Appointment entity, CancellationToken cancellationToken)
        {
            using (var transaction = await BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    Appointment existingAppointment = await GetByIdAsync(entity.Id, cancellationToken);
                    existingAppointment.State = entity.State;

                    await base.UpdateAsync(entity, cancellationToken);
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
