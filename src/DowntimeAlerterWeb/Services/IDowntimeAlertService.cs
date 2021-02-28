using DowntimeAlerterWeb.Entities;
using DowntimeAlerterWeb.Models;
using System.Collections.Generic;

namespace DowntimeAlerterWeb.Services
{
    public interface IDowntimeAlertService
    {
        int AddMonitor(MonitoringModel monitoringModel);
        MonitoringModel UpdateMonitor(MonitoringModel model);
        MonitoringModel GetMonitorById(int id);
        void DeleteMonitorById(int id);
        IEnumerable<MonitoringModel> GetMonitorList();
        IEnumerable<StatusModel> GetStatusModels();
        void LogStatus(StatusLog statusLog);
    }
}
