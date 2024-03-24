using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vacation_Request_Tracker.Models;
using Vacation_Request_Tracker.Repositories.Vacation;

namespace Vacation_Request_Tracker.Controllers
{
    public class VacationController : Controller
    {
        private readonly IVacationRepository vacationRepositories;

        public VacationController(IVacationRepository vacationRepositories)
        {
            this.vacationRepositories = vacationRepositories;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(TbVacationRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var vacation = new TbVacationRequest
            {
                
                RequestId = request.RequestId,
                EmployeeName = request.EmployeeName,
                Department = request.Department,
                Title = request.Title,
                SubmissionDate = request.SubmissionDate,
                VacationDateFrom = request.VacationDateFrom,
                VacationDateTo = request.VacationDateTo,
                Notes = request.Notes
            };

            await vacationRepositories.AddAsync(vacation);

            TempData["ConfirmationMessage"] = "Your vacation request has been successfully submitted.";

            return RedirectToAction("Confirmation");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var vacation = await vacationRepositories.GetAllAsync();

            return View(vacation);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var request = await vacationRepositories.GetAsync(id);

            if (request != null)
            {
                var vacation = new TbVacationRequest
                {
                    RequestId = request.RequestId,
                    EmployeeName = request.EmployeeName,
                    Department = request.Department,
                    Title = request.Title,
                    SubmissionDate = request.SubmissionDate,
                    VacationDateFrom = request.VacationDateFrom,
                    VacationDateTo = request.VacationDateTo,
                    Notes = request.Notes
                };

                return View(vacation);

            }

            return View(null);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(TbVacationRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Edit");
            }
            var vacation = new TbVacationRequest
            {
                RequestId = request.RequestId,
                EmployeeName = request.EmployeeName,
                Department = request.Department,
                Title = request.Title,
                SubmissionDate = request.SubmissionDate,
                VacationDateFrom = request.VacationDateFrom,
                VacationDateTo = request.VacationDateTo,
                Notes = request.Notes
            };

            var updatedVac = await vacationRepositories.UpdateAsync(vacation);

            if (updatedVac != null)
            {
                return RedirectToAction("List");
            }

            else
            {
                return RedirectToAction("Edit");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(TbVacationRequest request)
        {
            var deletedVac = await vacationRepositories.DeleteAsync(request.RequestId);

            if (deletedVac != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = request.RequestId });

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(Guid id)
        {
            var request = await vacationRepositories.GetAsync(id);  
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
