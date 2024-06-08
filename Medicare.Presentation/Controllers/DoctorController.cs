using Medicare.Application.Enums;
using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Infrastructure.Helpers;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Helpers;
using Medicare.Presentation.Models.Doctors;
using Medicare.Presentation.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using System.Linq.Expressions;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public class DoctorController : Controller
    {
        private readonly IRegisterDoctorUseCase _registerDoctorUseCase;
        private readonly ISessionService _sessionService;
        private readonly IDoctorService _doctorService;
        public DoctorController(IRegisterDoctorUseCase registerDoctorUseCase, ISessionService sessionService, IDoctorService doctorService)
        {
            _registerDoctorUseCase = registerDoctorUseCase;
            _sessionService = sessionService;
            _doctorService = doctorService;
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
    }
}
