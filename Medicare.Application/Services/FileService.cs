using Medicare.Domain.Entities.Base;

namespace Medicare.Application.Services
{
    public class FileService<T> : IFileService<T> where T : Entity
    {
        private readonly IFileUploader _fileUploader;
        private readonly string _folder;
        private readonly string _baseFolder;

        public FileService(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
            _baseFolder = $"/images";
            _folder = $"{_baseFolder}/{typeof(T).Name}s";
        }

        public string GetDefaultImageRoute()
        {
            string defaultImageRoute = $"{_baseFolder}/user-default.png";
            return defaultImageRoute;
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string fileName, Guid id, CancellationToken cancellationToken)
        {
            if (fileStream == null || fileStream.Length == 0)
            {
                throw new ArgumentException("File stream is empty");
            }

            string folder = $"{_folder}/{id}";
            return await _fileUploader.UploadFileAsync(fileStream, fileName, folder, cancellationToken);
        }
    }
}
