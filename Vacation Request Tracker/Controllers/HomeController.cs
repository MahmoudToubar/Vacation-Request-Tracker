using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vacation_Request_Tracker.Models;
using Vacation_Request_Tracker.Repositories.Vacation;

namespace Vacation_Request_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVacationRepository vacationRepositories;

        public HomeController(ILogger<HomeController> logger, IVacationRepository vacationRepositories)
        {
            _logger = logger;
            this.vacationRepositories = vacationRepositories;
        }

        public async Task <IActionResult> Index()
        {           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}