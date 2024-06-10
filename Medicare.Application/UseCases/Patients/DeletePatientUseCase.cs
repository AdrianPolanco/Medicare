

using Medicare.Application.Services.Interfaces;
using Medicare.Application.Services;
using Medicare.Domain.Entities;
using Medicare.Application.UseCases.Patients.Interfaces;

namespace Medicare.Application.UseCases.Patients
{
    public class DeletePatientUseCase: IDeletePatientUseCase
    {
        private readonly IFileService<Patient> _fileService;
        private readonly IPatientService _patientService;
        public DeletePatientUseCase(IFileService<Patient> fileService, IPatientService patientService)
        {
            _fileService = fileService;
            _patientService = patientService;
        }
        public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _patientService.DeleteAsync(id, cancellationToken);
            _fileService.DeleteFolder(id);
            return true;
        }
    }
}
