
using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Appointments.Interfaces
{
    public interface IAssignLabTestsToAppointmentUseCase
    {
        Task<bool> ExecuteAsync(Appointment appointment, List<Guid> labTestIds, CancellationToken cancellationToken);
    }
}
