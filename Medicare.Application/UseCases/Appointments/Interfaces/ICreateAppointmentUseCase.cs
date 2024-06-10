

using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Appointments.Interfaces
{
    public interface ICreateAppointmentUseCase
    {
        Task<bool> ExecuteAsync(Appointment appointment, CancellationToken cancellationToken);
    }
}
