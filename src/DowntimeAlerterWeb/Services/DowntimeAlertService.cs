using DowntimeAlerterWeb.Entities;
using DowntimeAlerterWeb.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _ = db.Monitors.Remove(entity);
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

        IEnumerable<StatusModel> IDowntimeAlertService.GetStatusModels()
        {
            var query = (from m in db.Monitors.AsNoTracking()
                         select new StatusModel()
                         {
                             Domain = new Uri(m.Url).Host,
                             Status = (m.Statuses == null || m.Statuses.Count == 0) 
                                ? ServiceStatus.Unknown 
                                : m.Statuses
                                    .OrderByDescending(x => x.Id)
                                    .First().HttpStatusCode == 200 ? ServiceStatus.Up : ServiceStatus.Down
                         });

            return query;
        }

        void IDowntimeAlertService.LogStatus(StatusLog statusLog)
        {
            _ = db.StatusLogs.Add(statusLog);
            _ = db.SaveChanges();
        }

        MonitoringModel IDowntimeAlertService.UpdateMonitor(MonitoringModel model)
        {
            var entity = db.Monitors.Find(model.Id);
            db.Entry(entity).State = EntityState.Detached;

            entity = model.Adapt<Monitor>();
            db.Entry(entity).State = EntityState.Modified;

            db.SaveChanges();

            return entity.Adapt<MonitoringModel>();
        }
    }
}
