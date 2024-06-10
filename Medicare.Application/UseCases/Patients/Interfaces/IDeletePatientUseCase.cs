
namespace Medicare.Application.UseCases.Patients.Interfaces
{
    public interface IDeletePatientUseCase
    {
        Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken);
    }
}
