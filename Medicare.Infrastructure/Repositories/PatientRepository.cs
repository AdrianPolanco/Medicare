using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories
{
    public class PatientRepository : Repository<Patient> ,IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Patient> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<Patient, object>>[] includes)
        {
            try
            {
                includes = new Expression<Func<Patient, object>>[]
                {
                    u => u.Office,
                };
                Patient? patient = await base.GetByIdAsync(id, cancellationToken, includes);

                return patient;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override async Task<ICollection<Patient>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<Patient, bool>> filter = null, params Expression<Func<Patient, object>>[] includes)
        {
            try
            {
                includes = new Expression<Func<Patient, object>>[]
                {
                    u => u.Office,
                };

                ICollection<Patient> patients = await base.GetByPagesAsync(page, cancellationToken, filter, includes);

                return patients;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public override async Task UpdateAsync(Patient entity, CancellationToken cancellationToken)
        {
            using (var transaction = await BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    Patient patient = await GetByIdAsync(entity.Id, cancellationToken);
                    patient.Name = entity.Name;
                    patient.Lastname = entity.Lastname;
                    patient.PhoneNumber = entity.PhoneNumber;
                    patient.Address = entity.Address;
                    patient.IdentityCard = entity.IdentityCard;
                    patient.BirthDate = entity.BirthDate;
                    patient.IsSmoker = entity.IsSmoker;
                    patient.HasAllergy = entity.HasAllergy;
                    patient.Photo = entity.Photo;

                    await SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
