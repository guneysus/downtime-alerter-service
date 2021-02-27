using DowntimeAlerterWeb.Entities;
using DowntimeAlerterWeb.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DowntimeAlerterWeb.Services
{
    public class DowntimeAlertService : IDowntimeAlertService
    {
        private readonly DowntimeAlerterDataContext db;

        public DowntimeAlertService(DowntimeAlerterDataContext db)
        {
            this.db = db;
        }

        int IDowntimeAlertService.AddMonitor(MonitoringModel monitoringModel)
        {
            var entity = monitoringModel.Adapt<Monitor>();
            _ = db.Monitors.Add(entity);
            var result = db.SaveChanges();
            return entity.Id;
        }

        void IDowntimeAlertService.DeleteMonitorById(int id)
        {
            var entity = db.Monitors.Find(id);
            _  = db.Monitors.Remove(entity);
            _ = db.SaveChanges();
        }

        MonitoringModel IDowntimeAlertService.GetMonitorById(int id)
        {
            var entiy = db.Monitors.Find(id);
            var model = entiy.Adapt<MonitoringModel>();
            return model;
        }

        IEnumerable<MonitoringModel> IDowntimeAlertService.GetMonitorList()
        {
            return db
                .Monitors
                .AsNoTracking()
                .ProjectToType<MonitoringModel>();
        }
    }
}
