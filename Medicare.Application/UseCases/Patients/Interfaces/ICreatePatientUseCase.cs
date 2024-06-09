using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Patients.Interfaces
{
    public interface ICreatePatientUseCase
    {
        Task<bool> ExecuteAsync(Patient patient, Stream? fileStream, string? fileName, CancellationToken cancellationToken);
    }
}
