using Medicare.Application.UseCases.Patients.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Infrastructure.Helpers;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Models.Doctors;
using Medicare.Presentation.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AssistantAuthorizationFilter))]
    public class PatientController : Controller
    {
        private readonly ICreatePatientUseCase _createPatientUseCase;

        public PatientController(ICreatePatientUseCase createPatientUseCase)
        {
            _createPatientUseCase = createPatientUseCase;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> SaveNewPatient(CreatePatientViewModel patientViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View("CreateNewDoctor", patientViewModel);

            Patient patient = new Patient
            {
                Name = patientViewModel.Name,
                Lastname = patientViewModel.Lastname,
                Address = patientViewModel.Address,
                PhoneNumber = patientViewModel.PhoneNumber,
                IdentityCard = patientViewModel.IdentityCard,
                BirthDate = patientViewModel.BirthDate,
                IsSmoker = patientViewModel.IsSmoker,
                HasAllergy = patientViewModel.HasAllergy,
            };

            Stream? _fileStream = null;
            string? _fileName = null;

            if (patientViewModel.Image is not null)
            {
                (var fileStream, var fileName) = await FileHelper.ConvertIFormFileToStreamAsync(patientViewModel.Image, cancellationToken);
                _fileStream = fileStream;
                _fileName = fileName;
            }

            await _createPatientUseCase.ExecuteAsync(patient, _fileStream, _fileName, cancellationToken);

            return RedirectToAction("Index");
        }
    }
}
