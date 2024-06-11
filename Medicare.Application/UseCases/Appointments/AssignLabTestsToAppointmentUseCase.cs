

using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Appointments.Interfaces;
using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Appointments
{
    public class AssignLabTestsToAppointmentUseCase : IAssignLabTestsToAppointmentUseCase
    {
        private readonly ISessionService _sessionService;
        private readonly ILabTestResultService _labTestResultService;

        public AssignLabTestsToAppointmentUseCase(
            ISessionService sessionService,
            ILabTestResultService labTestResultService)
        {
            _sessionService = sessionService;
            _labTestResultService = labTestResultService;
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
            return true;
        }
    }
}
