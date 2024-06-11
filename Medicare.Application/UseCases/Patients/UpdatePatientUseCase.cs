
using Medicare.Application.Services;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Patients.Interfaces;
using Medicare.Domain.Entities;
using System.Numerics;

namespace Medicare.Application.UseCases.Patients
{
    public class UpdatePatientUseCase : IUpdatePatientUseCase
    {
        private readonly IFileService<Patient> _fileService;
        private readonly IPatientService _patientService;

        public UpdatePatientUseCase(IFileService<Patient> fileService, IPatientService patientService)
        {
            _fileService = fileService;
            _patientService = patientService;
        }

        public async Task<bool> ExecuteAsync(Patient patient, Stream? fileStream, string? fileName, CancellationToken cancellationToken)
        {
            if(patient is null
                || patient.Id == Guid.Empty
                || string.IsNullOrEmpty(patient.Name)
                || string.IsNullOrEmpty(patient.Lastname)
                || string.IsNullOrEmpty(patient.PhoneNumber)
                || string.IsNullOrEmpty(patient.Address)
                || string.IsNullOrEmpty(patient.IdentityCard)) return false;

            if(fileStream is not null && string.IsNullOrEmpty(fileName) || (fileStream is null && !string.IsNullOrEmpty(fileName))) return true;
            if (fileStream is null && string.IsNullOrEmpty(fileName)) return true;

            if (!string.IsNullOrEmpty(fileName))
            {
                string profileImageRoute = await _fileService.UploadImageAsync(fileStream, fileName, patient.Id, cancellationToken);
                patient.Photo = profileImageRoute;
            }

            await _patientService.UpdateAsync(patient, cancellationToken);

            return true;
        }
    }
}
