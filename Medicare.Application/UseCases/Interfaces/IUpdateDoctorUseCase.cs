using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Interfaces
{
    public interface IUpdateDoctorUseCase
    {
        Task<bool> ExecuteAsync(Doctor doctor, Stream? fileStream, string? fileName, CancellationToken cancellationToken);
    }
}
