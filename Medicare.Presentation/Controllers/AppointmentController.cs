using Medicare.Application.UseCases.Appointments.Interfaces;
using Medicare.Domain;
using Medicare.Domain.Entities;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Models.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AssistantAuthorizationFilter))]
    public class AppointmentController : Controller
    {       
        private readonly ICreateAppointmentUseCase _createAppointmentUseCase;
        public AppointmentController(ICreateAppointmentUseCase createAppointmentUseCase)
        {
            _createAppointmentUseCase = createAppointmentUseCase;
        }
        public IActionResult Index()
        {
            return View();
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
