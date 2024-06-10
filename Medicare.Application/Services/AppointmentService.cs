
using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using System.Linq.Expressions;

namespace Medicare.Application.Services
{
    public class AppointmentService: Service<Appointment>, IAppointmentService
    {
        public AppointmentService(IAppointmentRepository repository) : base(repository)
        {
        }


        public async Task<ICollection<Appointment>> GetByPagesAsync(int page, CancellationToken cancellationToken, 
            Expression<Func<Appointment, bool>> filter = null, params Expression<Func<Appointment, object>>[] includes)
        {
            return await _repository.GetByPagesAsync(page, cancellationToken, filter, includes);
        }
    }
}
