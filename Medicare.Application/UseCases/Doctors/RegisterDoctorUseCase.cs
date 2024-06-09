using Medicare.Application.Services;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Doctors.Interfaces;
using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Doctors
{
    public class RegisterDoctorUseCase : IRegisterDoctorUseCase
    {
        private readonly IFileService<Doctor> _fileService;
        private readonly IDoctorService _doctorService;
        private readonly ISessionService _sessionService;
        public RegisterDoctorUseCase(IFileService<Doctor> fileService, IDoctorService doctorService, ISessionService sessionService)
        {
            _fileService = fileService;
            _doctorService = doctorService;
            _sessionService = sessionService;
        }
        public async Task<bool> ExecuteAsync(Doctor doctor, Stream? fileStream, string? fileName, CancellationToken cancellationToken)
        {
            //Validando si los campos requeridos estan vacios
            if (doctor is null
                  || string.IsNullOrEmpty(doctor.Name)
                  || string.IsNullOrEmpty(doctor.Lastname)
                  || string.IsNullOrEmpty(doctor.Phone)
                  || string.IsNullOrEmpty(doctor.Email)
                  || string.IsNullOrEmpty(doctor.IdentityCard)) return false;

            //Dandole la ruta de la imagen por defecto al doctor a crear
            doctor.ImageRoute = _fileService.GetDefaultImageRoute();
            //Dandole la oficina actual al doctor a crear
            doctor.OfficeId = _sessionService.GetSession().OfficeId;

            //Creando el doctor
            Doctor createdDoctor = await _doctorService.AddAsync(doctor, cancellationToken);

            if(fileStream is null || string.IsNullOrEmpty(fileName)) return true;

            string profileImageRoute = await _fileService.UploadImageAsync(fileStream, fileName, createdDoctor.Id, cancellationToken);

            createdDoctor.ImageRoute = profileImageRoute;

            await _doctorService.UpdateAsync(createdDoctor, cancellationToken);

            return true;
        }
    }
}
