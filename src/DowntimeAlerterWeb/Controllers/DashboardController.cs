using DowntimeAlerterWeb.Models;
using DowntimeAlerterWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerterWeb.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDowntimeAlertService downtimeAlertService;

        public DashboardController(IDowntimeAlertService downtimeAlertService)
        {
            this.downtimeAlertService = downtimeAlertService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var monitors = downtimeAlertService.GetMonitorList();

            var model = new DashboardModel
            {
                Monitors = monitors
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Status()
        {
            var model = new DashboardStatusModel
            {
                Statuses = downtimeAlertService.GetStatusModels()
            };

            return View(model);
        }
    }
}
