using Medicare.Application.Services;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Doctors
{
    public class DeleteDoctorUseCase : IDeleteDoctorUseCase
    {
        private readonly IFileService<Doctor> _fileService;
        private readonly IDoctorService _doctorService;
        public DeleteDoctorUseCase(IFileService<Doctor> fileService, IDoctorService doctorService)
        {
            _fileService = fileService;
            _doctorService = doctorService;
        }
        public async Task<bool> ExecyteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _doctorService.DeleteAsync(id, cancellationToken);
            _fileService.DeleteFolder(id);
            return true;
        }
    }
}
