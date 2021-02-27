using DowntimeAlerterWeb.Models;
using System.Collections.Generic;

namespace DowntimeAlerterWeb.Services
{
    public interface IDowntimeAlertService
    {
        int AddMonitor(MonitoringModel monitoringModel);
        MonitoringModel GetMonitorById(int id);
        void DeleteMonitorById(int id);
        IEnumerable<MonitoringModel> GetMonitorList();
    }
}
