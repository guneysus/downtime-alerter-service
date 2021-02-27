using DowntimeAlerterWeb.Models;
using DowntimeAlerterWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DowntimeAlerterWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDowntimeAlertService downtimeAlertService;

        public DashboardController(IDowntimeAlertService downtimeAlertService)
        {
            this.downtimeAlertService = downtimeAlertService;
        }


        [Authorize, HttpGet]
        public IActionResult Index()
        {
            var monitors = downtimeAlertService.GetMonitorList();

            var model = new DashboardModel
            {
                Monitors = monitors
            };

            return View(model);
        }
    }
}
