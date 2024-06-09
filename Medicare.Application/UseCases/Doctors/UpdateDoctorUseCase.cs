using Medicare.Application.Services.Interfaces;
using Medicare.Application.Services;
using Medicare.Domain.Entities;
using Medicare.Application.UseCases.Doctors.Interfaces;

namespace Medicare.Application.UseCases.Doctors
{
    public class UpdateDoctorUseCase : IUpdateDoctorUseCase
    {
        private readonly IFileService<Doctor> _fileService;
        private readonly IDoctorService _doctorService;
        private readonly ISessionService _sessionService;
        public UpdateDoctorUseCase(IFileService<Doctor> fileService, IDoctorService doctorService, ISessionService sessionService)
        {
            _fileService = fileService;
            _doctorService = doctorService;
            _sessionService = sessionService;
        }
        public async Task<bool> ExecuteAsync(Doctor doctor, Stream? fileStream, string? fileName, CancellationToken cancellationToken)
        {
            //Validando si los campos requeridos estan vacios
            if (doctor is null
                  || doctor.Id == Guid.Empty
                  || string.IsNullOrEmpty(doctor.Name)
                  || string.IsNullOrEmpty(doctor.Lastname)
                  || string.IsNullOrEmpty(doctor.Phone)
                  || string.IsNullOrEmpty(doctor.Email)
                  || string.IsNullOrEmpty(doctor.IdentityCard)
                  || string.IsNullOrEmpty(doctor.ImageRoute)) return false;


            if (fileStream is null || string.IsNullOrEmpty(fileName)) return true;

            string profileImageRoute = await _fileService.UploadImageAsync(fileStream, fileName, doctor.Id, cancellationToken);

            doctor.ImageRoute = profileImageRoute;

            await _doctorService.UpdateAsync(doctor, cancellationToken);

            return true;
        }
    }
}
