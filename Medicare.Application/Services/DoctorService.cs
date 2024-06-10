
using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using System.Linq.Expressions;

namespace Medicare.Application.Services
{
    public class DoctorService : SelectableService<Doctor>, IDoctorService
    {
        public readonly IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository): base(doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<Doctor, object>>[] includes)
        {
            return await _doctorRepository.GetByIdAsync(id, cancellationToken, includes);
        }

        public async Task<ICollection<Doctor>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<Doctor, bool>> filter = null, params Expression<Func<Doctor, object>>[] includes)
        {
            return await _doctorRepository.GetByPagesAsync(page, cancellationToken, filter, includes);
        }
    }
}
