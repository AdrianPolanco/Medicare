

using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Appointments.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain;

namespace Medicare.Application.UseCases.Appointments
{
    public class AssignLabTestsToAppointmentUseCase : IAssignLabTestsToAppointmentUseCase
    {
        private readonly ISessionService _sessionService;
        private readonly ILabTestResultService _labTestResultService;
        private readonly IAppointmentService _appointmentService;

        public AssignLabTestsToAppointmentUseCase(
            ISessionService sessionService,
            ILabTestResultService labTestResultService,
            IAppointmentService appointmentService)
        {
            _sessionService = sessionService;
            _labTestResultService = labTestResultService;
            _appointmentService = appointmentService;
        }

        public async Task<bool> ExecuteAsync(Appointment appointment, List<Guid> labTestIds, CancellationToken cancellationToken)
        {
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            if (userSessionInfo.OfficeId != appointment.OfficeId) return false;

            foreach(var labTestId in labTestIds)
            {

                var labTestResult = new LabTestResult
                {
                    AppointmentId = appointment.Id,
                    LabTestId = labTestId,
                    PatientId = appointment.PatientId,
                    OfficeId = userSessionInfo.OfficeId,
                    IsCompleted = false,
                };

                await _labTestResultService.AddAsync(labTestResult, cancellationToken);
            }

            Appointment recoveredAppointment = await _appointmentService.GetByIdAsync(appointment.Id, cancellationToken);
            recoveredAppointment.State = AppointmentStates.PendingResult;
            await _appointmentService.UpdateAsync(recoveredAppointment, cancellationToken);
            return true;
        }
    }
}
