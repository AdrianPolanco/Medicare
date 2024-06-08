
using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Interfaces
{
    public interface IRegisterDoctorUseCase
    {
        Task<bool> ExecuteAsync(Doctor doctor, Stream? fileStream, string? fileName, CancellationToken cancellationToken);
    }
}
