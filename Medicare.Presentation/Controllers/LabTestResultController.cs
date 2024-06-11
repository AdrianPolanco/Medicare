using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Helpers;
using Medicare.Presentation.Models.LabTestsResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AssistantAuthorizationFilter))]
    public class LabTestResultController : Controller
    {
        private readonly ILabTestResultService _labTestResultService;
        private readonly ISessionService _sessionService;
        private readonly IAppointmentService _appointmentService;
        public LabTestResultController(ILabTestResultService labTestResultService, ISessionService sessionService, IAppointmentService appointment) {
            _labTestResultService = labTestResultService;
            _sessionService = sessionService;
            _appointmentService = appointment;
        }
        public async Task<IActionResult> Index(int? page, string search, CancellationToken cancellationToken)
        {   
            page = page ?? 1;
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            Expression<Func<LabTestResult, bool>> searchFilter = FilterHelper.GetLabTestResultFilter(search, userSessionInfo.OfficeId);
            ICollection<LabTestResult> recoveredTestResults = await _labTestResultService.GetByPagesAsync((int)page, cancellationToken, searchFilter);
            List<LabTestResult> results = recoveredTestResults.ToList();
            int pages = await _appointmentService.GetRowsCountAsync(cancellationToken);
            LabTestsMenuViewModel labTestsMenuViewModel = new LabTestsMenuViewModel
            {
                LabTestResults = results,
                Pages = pages,
                CurrentPage = (int)page
            };
            return View(labTestsMenuViewModel);
        }

        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            LabTestResult labTestResult = await _labTestResultService.GetByIdAsync(id, cancellationToken);
            LabTestResultViewModel labTestResultViewModel = new LabTestResultViewModel
            {
                Id = labTestResult.Id
            };
            return View(labTestResultViewModel);
        }   

        public async Task<IActionResult> PublishResults(LabTestResultViewModel labTestResultViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return View("Edit", labTestResultViewModel);

            LabTestResult labTestResult = await _labTestResultService.GetByIdAsync(labTestResultViewModel.Id, cancellationToken);
            labTestResult.Result = labTestResultViewModel.Result;
            await _labTestResultService.UpdateAsync(labTestResult, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
