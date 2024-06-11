using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Patients.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Infrastructure.Helpers;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Helpers;
using Medicare.Presentation.Models.Patients;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AssistantAuthorizationFilter))]
    public class PatientController : Controller
    {
        private readonly ICreatePatientUseCase _createPatientUseCase;
        private readonly IUpdatePatientUseCase _updatePatientUseCase;
        private readonly IDeletePatientUseCase _deletePatientUseCase;
        private readonly ISessionService _sessionService;
        private readonly IPatientService _patientService;

        public PatientController(
            ICreatePatientUseCase createPatientUseCase,
            IUpdatePatientUseCase updatePatientUseCase,
            IDeletePatientUseCase deletePatientUseCase,
            ISessionService sessionService,
            IPatientService patientService          
            )
        {
            _createPatientUseCase = createPatientUseCase;
            _updatePatientUseCase = updatePatientUseCase;
            _deletePatientUseCase = deletePatientUseCase;
            _sessionService = sessionService;
            _patientService = patientService;
        }

        public async Task<IActionResult> Index(int? page, string search, CancellationToken cancellationToken)
        {
            page = page ?? 1;
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            Expression<Func<Patient, bool>> searchFilter = FilterHelper.GetPatientFilter(search, userSessionInfo.OfficeId);
            ICollection<Patient> recoveredPatients = await _patientService.GetByPagesAsync((int)page, cancellationToken, searchFilter);
            List<Patient> patients = recoveredPatients.ToList();
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
                return View("Create", patientViewModel);

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

        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            Patient patient = await _patientService.GetByIdAsync(id, cancellationToken);
            UpdatePatientViewModel updatePatientViewModel = new UpdatePatientViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                Lastname = patient.Lastname,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                IdentityCard = patient.IdentityCard,
                BirthDate = patient.BirthDate,
                IsSmoker = patient.IsSmoker,
                HasAllergy = patient.HasAllergy,
                Photo = patient.Photo
            };
            return View(updatePatientViewModel);
        }

        public async Task<IActionResult> Update(UpdatePatientViewModel updatePatientViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View("Edit", updatePatientViewModel);

            Patient patient = new Patient
            {
                Name = updatePatientViewModel.Name,
                Lastname = updatePatientViewModel.Lastname,
                Address = updatePatientViewModel.Address,
                PhoneNumber = updatePatientViewModel.PhoneNumber,
                IdentityCard = updatePatientViewModel.IdentityCard,
                BirthDate = updatePatientViewModel.BirthDate,
                IsSmoker = updatePatientViewModel.IsSmoker,
                HasAllergy = updatePatientViewModel.HasAllergy,
                Id = updatePatientViewModel.Id,
                Photo = updatePatientViewModel.Photo,
            };

            Stream? _fileStream = null;
            string? _fileName = null;

            if (updatePatientViewModel.Image is not null)
            {
                (var fileStream, var fileName) = await FileHelper.ConvertIFormFileToStreamAsync(updatePatientViewModel.Image, cancellationToken);
                _fileStream = fileStream;
                _fileName = fileName;
            }

            await _updatePatientUseCase.ExecuteAsync(patient, _fileStream, _fileName, cancellationToken);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid patientId, CancellationToken cancellationToken)
        {
            await _deletePatientUseCase.ExecuteAsync(patientId, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
