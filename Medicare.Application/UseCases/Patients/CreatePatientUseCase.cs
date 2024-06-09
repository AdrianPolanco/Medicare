using Medicare.Application.Services;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Patients.Interfaces;
using Medicare.Domain.Entities;
using System.IO;

namespace Medicare.Application.UseCases.Patients
{
    public class CreatePatientUseCase : ICreatePatientUseCase
    {
        private readonly IPatientService _patientService;
        private readonly ISessionService _sessionService;
        private readonly IOfficeService _officeService;
        private readonly IFileService<Patient> _fileService;

        public CreatePatientUseCase(IPatientService patientService, 
            ISessionService sessionService, 
            IOfficeService officeService,
            IFileService<Patient> fileService)
        {
            _patientService = patientService;
            _sessionService = sessionService;
            _officeService = officeService;
            _fileService = fileService;
        }

        public async Task<bool> ExecuteAsync(Patient patient, Stream? fileStream, string? fileName, CancellationToken cancellationToken)
        {
            //Validando las propiedades de la entidad
            if(patient is null
                || string.IsNullOrWhiteSpace(patient.Name)
                || string.IsNullOrWhiteSpace(patient.Lastname)
                || string.IsNullOrWhiteSpace(patient.PhoneNumber)
                || string.IsNullOrWhiteSpace(patient.Address)
                || string.IsNullOrWhiteSpace(patient.IdentityCard)
                || patient.BirthDate == default) return false;

            //Obteniendo la oficina del usuario autenticado
            var officeId = _sessionService.GetSession().OfficeId;

            //Validando que el consultorio exista
            var office = await _officeService.GetByIdAsync(officeId, cancellationToken);

            if(office is null) return false;

            //Creando el paciente en la base de datos
            var patientCreated = new Patient
            {
                Name = patient.Name,
                Lastname = patient.Lastname,
                PhoneNumber = patient.PhoneNumber,
                Address = patient.Address,
                IdentityCard = patient.IdentityCard,
                BirthDate = patient.BirthDate,
                IsSmoker = patient.IsSmoker,
                HasAllergy = patient.HasAllergy,
                Photo = patient.Photo,
                OfficeId = officeId
            };

            await _patientService.AddAsync(patientCreated, cancellationToken);

            if (fileStream is null || string.IsNullOrEmpty(fileName)) return true;
            
            //Subiendo la imagen de perfil del paciente en caso de que se haya proporcionado
            string profileImageRoute = await _fileService.UploadImageAsync(fileStream, fileName, patientCreated.Id, cancellationToken);
            patientCreated.Photo = profileImageRoute;

            await _patientService.UpdateAsync(patientCreated, cancellationToken);

            return true;

        }
    }
}
