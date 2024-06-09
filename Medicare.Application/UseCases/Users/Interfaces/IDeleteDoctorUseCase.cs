namespace Medicare.Application.UseCases.Users.Interfaces
{
    public interface IDeleteDoctorUseCase
    {
        Task<bool> ExecyteAsync(Guid id, CancellationToken cancellationToken);
    }
}
