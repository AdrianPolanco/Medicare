using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Patients.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Infrastructure.Helpers;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Helpers;
using Medicare.Presentation.Models.Doctors;
using Medicare.Presentation.Models.Patients;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AssistantAuthorizationFilter))]
    public class PatientController : Controller
    {
        private readonly ICreatePatientUseCase _createPatientUseCase;
        private readonly ISessionService _sessionService;
        private readonly IPatientService _patientService;

        public PatientController(ICreatePatientUseCase createPatientUseCase, ISessionService sessionService, IPatientService patientService)
        {
            _createPatientUseCase = createPatientUseCase;
            _sessionService = sessionService;
            _patientService = patientService;
        }

        public async Task<IActionResult> Index(int? page, string search, CancellationToken cancellationToken)
        {
            page = page ?? 1;
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            Expression<Func<Patient, bool>> searchFilter = FilterHelper.GetPatientFilter(search, userSessionInfo.OfficeId);
            ICollection<Patient> recoveredDoctors = await _patientService.GetByPagesAsync((int)page, cancellationToken, searchFilter);
            List<Patient> patients = recoveredDoctors.ToList();
            int pages = await _patientService.GetRowsCountAsync(cancellationToken);
            PatientsMenuViewModel patientsMenuViewModel = new PatientsMenuViewModel
            {
                Patients = patients,
                Pages = pages,
                CurrentPage = (int)page
            };
            return View(patientsMenuViewModel);
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
