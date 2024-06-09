using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Doctors.Interfaces
{
    public interface IUpdateDoctorUseCase
    {
        Task<bool> ExecuteAsync(Doctor doctor, Stream? fileStream, string? fileName, CancellationToken cancellationToken);
    }
}
