using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Doctors.Interfaces
{
    public interface IRegisterDoctorUseCase
    {
        Task<bool> ExecuteAsync(Doctor doctor, Stream? fileStream, string? fileName, CancellationToken cancellationToken);
    }
}
