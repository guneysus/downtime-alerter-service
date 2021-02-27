using DowntimeAlerterWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DowntimeAlerterWeb.Controllers
{
    public class MonitorController : Controller
    {
        private readonly IDowntimeAlertService downtimeService;

        public MonitorController(IDowntimeAlertService downTimeService)
        {
            this.downtimeService = downTimeService;
        }


        [Authorize, HttpGet] public IActionResult Add() => View();

        [Authorize, HttpPost]
        public IActionResult Add(Models.MonitoringModel model)
        {
            var id = downtimeService.AddMonitor(model);
            return RedirectToAction("Edit", new { id });
        }

        [Authorize, HttpGet]
        public IActionResult Edit(int id)
        {
            var model = downtimeService.GetMonitorById(id);
            return View(model);
        }

        [Authorize, HttpDelete]
        public void Delete(int id)
        {
            downtimeService.DeleteMonitorById(id);
        }


    }
}
