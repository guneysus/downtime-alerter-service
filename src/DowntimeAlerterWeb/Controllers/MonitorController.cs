using DowntimeAlerterWeb.Models;
using DowntimeAlerterWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerterWeb.Controllers
{

    [Authorize]
    public class MonitorController : Controller
    {
        private readonly IDowntimeAlertService _downtimeService;

        public MonitorController(IDowntimeAlertService downTimeService) => _downtimeService = downTimeService;


        [HttpGet] public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(MonitoringModel model)
        {
            var id = _downtimeService.AddMonitor(model);
            return RedirectToAction("Edit", new { id });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _downtimeService.GetMonitorById(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(MonitoringModel model)
        {
            model = _downtimeService.UpdateMonitor(model);
            return RedirectToAction("Edit", new { model.Id });
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _downtimeService.DeleteMonitorById(id);
        }


    }
}
