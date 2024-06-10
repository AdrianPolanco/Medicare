namespace Medicare.Application.UseCases.Users.Interfaces
{
    public interface IDeleteDoctorUseCase
    {
        Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken);
    }
}
