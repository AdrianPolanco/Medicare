using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using System.Linq.Expressions;

namespace Medicare.Application.Services
{
    public class PatientService : SelectableService<Patient>, IPatientService
    {
        public PatientService(IPatientRepository repository) : base(repository)
        {
        }

        public override async Task<Patient> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<Patient, object>>[] includes)
        {
            try
            {
                Patient? patient = await base.GetByIdAsync(id, cancellationToken, p => p.Office);

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
                ICollection<Patient> patients = await base.GetByPagesAsync(page, cancellationToken, filter, p => p.Office);

                return patients;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
