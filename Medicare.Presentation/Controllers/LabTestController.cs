using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Helpers;
using Medicare.Presentation.Models.LabTests;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public class LabTestController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ILabTestService _labTestService;
        public LabTestController(
            ISessionService sessionService,
            ILabTestService labTestService)
        {
            _sessionService = sessionService;
            _labTestService = labTestService;
        }
        public async Task<IActionResult> Index(int? page, string search, CancellationToken cancellationToken)
        {
            page = page ?? 1;
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            Expression<Func<LabTest, bool>> searchFilter = FilterHelper.GetLabTestFilter(search, userSessionInfo.OfficeId);
            ICollection<LabTest> recoveredLabTests = await _labTestService.GetByPagesAsync((int)page, cancellationToken, searchFilter);
            List<LabTest> labTests = recoveredLabTests.ToList();
            int pages = await _labTestService.GetRowsCountAsync(cancellationToken);
            LabTestViewModel labTestViewModel = new LabTestViewModel
            {
                LabTests = labTests,
                Pages = pages,
                CurrentPage = (int)page
            };
            return View(labTestViewModel);
        }

        public IActionResult CreateNewLabTest()
        {
            return View();
        }

        public async Task<IActionResult> SaveNewLabTest(CreateLabTestViewModel labTestViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return View("CreateNewLabTest", labTestViewModel);

            LabTest labTest = new LabTest
            {
                Name = labTestViewModel.Name,
                OfficeId = labTestViewModel.OfficeId
            };
            await _labTestService.AddAsync(labTest, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditLabTest(Guid id, CancellationToken cancellationToken)
        {
            LabTest labTest = await _labTestService.GetByIdAsync(id, cancellationToken);
            UpdateLabTestViewModel editLabTestViewModel = new UpdateLabTestViewModel
            {
                Id = labTest.Id,
                Name = labTest.Name,
                OfficeId = labTest.OfficeId
            };
            return View(editLabTestViewModel);
        }

        public async Task<IActionResult> UpdateLabTest(UpdateLabTestViewModel labTestViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return View("EditLabTest", labTestViewModel);

            LabTest labTest = new LabTest
            {
                Id = labTestViewModel.Id,
                Name = labTestViewModel.Name,
                OfficeId = labTestViewModel.OfficeId
            };
            await _labTestService.UpdateAsync(labTest, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteLabTest(Guid labTestId, CancellationToken cancellationToken)
        {
            await _labTestService.DeleteAsync(labTestId, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
