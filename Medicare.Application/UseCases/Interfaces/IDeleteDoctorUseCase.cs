

namespace Medicare.Application.UseCases.Interfaces
{
    public interface IDeleteDoctorUseCase
    {
        Task<bool> ExecyteAsync(Guid id, CancellationToken cancellationToken);
    }
}
