

namespace Medicare.Application.Services
{
    public interface IFileUploader
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string folder, CancellationToken cancellationToken);
    }
}
