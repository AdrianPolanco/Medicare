using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Appointments.Interfaces;
using Medicare.Domain;
using Medicare.Domain.Entities;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Helpers;
using Medicare.Presentation.Models.Appointments;
using Medicare.Presentation.Models.Patients;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AssistantAuthorizationFilter))]
    public class AppointmentController : Controller
    {       
        private readonly ICreateAppointmentUseCase _createAppointmentUseCase;
        private readonly ISessionService _sessionService;
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(ICreateAppointmentUseCase createAppointmentUseCase, ISessionService sessionService, IAppointmentService appointmentService)
        {
            _createAppointmentUseCase = createAppointmentUseCase;
            _sessionService = sessionService;
            _appointmentService = appointmentService;
        }
        public async Task<IActionResult> Index(int? page, string search, CancellationToken cancellationToken)
        {
            page = page ?? 1;
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            Expression<Func<Appointment, bool>> searchFilter = FilterHelper.GetAppointmentFilter(search, userSessionInfo.OfficeId);
            ICollection<Appointment> recoveredAppointments = await _appointmentService.GetByPagesAsync((int)page, cancellationToken, searchFilter);
            List<Appointment> appointments = recoveredAppointments.ToList();
            int pages = await _appointmentService.GetRowsCountAsync(cancellationToken);
            AppointmentsMenuViewModel appointmentsMenuViewModel = new AppointmentsMenuViewModel
            {
                Appointments = appointments,
                Pages = pages,
                CurrentPage = (int)page
            };
            return View(appointmentsMenuViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }   

        public async Task<IActionResult> Save(CreateAppointmentViewModel model, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid) return View(model);

            var appointment = new Appointment
            {
                Date = model.Date,
                Hour = model.Time,
                Reason = model.Reason,
                PatientId = model.SelectedPatientId,
                DoctorId = model.SelectedDoctorId,
                State = AppointmentStates.PendingAppointment,
                
            };

            await _createAppointmentUseCase.ExecuteAsync(appointment, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
