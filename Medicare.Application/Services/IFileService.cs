using Medicare.Domain.Entities.Base;

namespace Medicare.Application.Services
{
    public interface IFileService<T> where T : Entity
    {
        Task<string> UploadImageAsync(Stream fileStream, string fileName, Guid id, CancellationToken cancellationToken);
        string GetDefaultImageRoute();
        void DeleteFolder(Guid id);
    }
}
