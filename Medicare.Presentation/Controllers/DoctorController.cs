using Medicare.Application.Enums;
using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Doctors.Interfaces;
using Medicare.Application.UseCases.Users.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Infrastructure.Helpers;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Helpers;
using Medicare.Presentation.Models.Doctors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public class DoctorController : Controller
    {
        private readonly IRegisterDoctorUseCase _registerDoctorUseCase;
        private readonly IUpdateDoctorUseCase _updateDoctorUseCase;
        private readonly IDeleteDoctorUseCase _deleteDoctorUseCase;
        private readonly ISessionService _sessionService;
        private readonly IDoctorService _doctorService;
        public DoctorController(
            IRegisterDoctorUseCase registerDoctorUseCase, 
            IUpdateDoctorUseCase updateDoctorUseCase,
            IDeleteDoctorUseCase deleteDoctorUseCase,
            ISessionService sessionService, 
            IDoctorService doctorService)
        {
            _registerDoctorUseCase = registerDoctorUseCase;
            _updateDoctorUseCase = updateDoctorUseCase;
            _sessionService = sessionService;
            _doctorService = doctorService;
            _deleteDoctorUseCase = deleteDoctorUseCase;
        }
        public async Task<IActionResult> Index(int? page, string search, CancellationToken cancellationToken)
        {
            page = page ?? 1;
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            Expression<Func<Doctor, bool>> searchFilter = FilterHelper.GetDoctorFilter(search, userSessionInfo.OfficeId);
            ICollection<Doctor> recoveredDoctors = await _doctorService.GetByPagesAsync((int)page, cancellationToken, searchFilter);
            List<Doctor> doctors = recoveredDoctors.ToList();
            int pages = await _doctorService.GetRowsCountAsync(cancellationToken);
            DoctorsMenuViewModel doctorsMenuViewModel = new DoctorsMenuViewModel
            {
                Doctors = doctors,
                Pages = pages,
                CurrentPage = (int)page
            };
            return View(doctorsMenuViewModel);
        }


        public IActionResult CreateNewDoctor()
        {
            return View();
        }

        public async Task<IActionResult> SaveNewDoctor(CreateDoctorViewModel doctorViewModel, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return View("CreateNewDoctor", doctorViewModel);

            Doctor doctor = new Doctor
            {
                Name = doctorViewModel.Name,
                Lastname = doctorViewModel.Lastname,
                Email = doctorViewModel.Email,
                Phone = doctorViewModel.Phone,
                IdentityCard = doctorViewModel.IdentityCard
            };

            Stream? _fileStream = null;
            string? _fileName = null;

            if(doctorViewModel.Image is not null)
            {
                (var fileStream, var fileName) = await FileHelper.ConvertIFormFileToStreamAsync(doctorViewModel.Image, cancellationToken);
                _fileStream = fileStream;
                _fileName = fileName;
            }

            await _registerDoctorUseCase.ExecuteAsync(doctor, _fileStream, _fileName, cancellationToken);

            return RedirectToAction("Index");
        }   

        public async Task<IActionResult> EditDoctor(Guid id, CancellationToken cancellationToken)
        {
            Doctor? doctor = await _doctorService.GetByIdAsync(id, cancellationToken);
            if(doctor is null)
                return RedirectToAction("Index");

            EditDoctorViewModel editDoctorViewModel = new EditDoctorViewModel
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Lastname = doctor.Lastname,
                Email = doctor.Email,
                Phone = doctor.Phone,
                IdentityCard = doctor.IdentityCard,
                OfficeId = doctor.OfficeId,
                ImageRoute = doctor.ImageRoute
            };

            return View(editDoctorViewModel);
        }

        public async Task<IActionResult> UpdateDoctor(EditDoctorViewModel doctorViewModel, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return View("EditDoctor", doctorViewModel);

            Doctor doctor = new Doctor
            {
                Id = doctorViewModel.Id,
                Name = doctorViewModel.Name,
                Lastname = doctorViewModel.Lastname,
                Email = doctorViewModel.Email,
                Phone = doctorViewModel.Phone,
                IdentityCard = doctorViewModel.IdentityCard,
                OfficeId = doctorViewModel.OfficeId,
                ImageRoute = doctorViewModel.ImageRoute,

            };

            Stream? _fileStream = null;
            string? _fileName = null;

            if(doctorViewModel.Image is not null)
            {
                (var fileStream, var fileName) = await FileHelper.ConvertIFormFileToStreamAsync(doctorViewModel.Image, cancellationToken);
                _fileStream = fileStream;
                _fileName = fileName;
            }

            await _updateDoctorUseCase.ExecuteAsync(doctor, _fileStream, _fileName, cancellationToken);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteDoctor(Guid doctorId, CancellationToken cancellationToken)
        {
            await _deleteDoctorUseCase.ExecuteAsync(doctorId, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
