

using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Domain.Repositories.Base;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories
{
    public class DoctorRepository: SelectableRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context, ISelectableReadOnlyRepository<Doctor> repository) : base(context, repository)
        {
        }

        public override async Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<Doctor, object>>[] includes)
        {
            try
            {
                includes = new Expression<Func<Doctor, object>>[]
                {
                   u => u.Office,
                };
                Doctor? doctor = await base.GetByIdAsync(id, cancellationToken, includes);

                return doctor;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override async Task<ICollection<Doctor>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<Doctor, bool>> filter = null, params Expression<Func<Doctor, object>>[] includes)
        {
            try
            {
                includes = new Expression<Func<Doctor, object>>[]
                {
                    u => u.Office
                };

                ICollection<Doctor> doctors = await base.GetByPagesAsync(page, cancellationToken, filter, includes);

                return doctors;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override async Task UpdateAsync(Doctor entity, CancellationToken cancellationToken)
        {
            using (var transaction = await BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    Doctor existingDoctor = await GetByIdAsync(entity.Id, cancellationToken);

                    if (existingDoctor == null)
                    {
                        throw new Exception($"El doctor con Id {entity.Id} no fue encontrado en la base de datos.");
                    }

                    existingDoctor.Name = entity.Name;
                    existingDoctor.Lastname = entity.Lastname;
                    existingDoctor.Email = entity.Email;
                    existingDoctor.Phone = entity.Phone;
                    existingDoctor.IdentityCard = entity.IdentityCard;
                    existingDoctor.ImageRoute = entity.ImageRoute;


                    await base.UpdateAsync(existingDoctor, cancellationToken);
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
