

using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Appointments.Interfaces;
using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Appointments
{
    public class CreateAppointmentUseCase : ICreateAppointmentUseCase
    {
        private readonly ISessionService _sessionService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly ILabTestService _labTestService;
        public CreateAppointmentUseCase(
            ISessionService sessionService,
            IAppointmentService appointmentService, 
            IPatientService patientService, 
            IDoctorService doctorService, 
            ILabTestService labTestService)
        {
            _sessionService = sessionService;
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
            _labTestService = labTestService;
        }
        public async Task<bool> ExecuteAsync(Appointment appointment, CancellationToken cancellationToken)
        {
            if (appointment is null
                || appointment.Date == default
                || appointment.PatientId == default
                || appointment.DoctorId == default
                || string.IsNullOrEmpty(appointment.Reason)) return false;

             var patient = await _patientService.GetByIdAsync(appointment.PatientId, cancellationToken);
            if (patient is null) return false;

            var doctor = await _doctorService.GetByIdAsync(appointment.DoctorId, cancellationToken);
            if (doctor is null) return false;

            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            appointment.OfficeId = userSessionInfo.OfficeId;

            await _appointmentService.AddAsync(appointment, cancellationToken);

            return true;
        }
    }
}
