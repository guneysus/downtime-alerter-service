using System.Collections.Generic;

namespace DowntimeAlerterWeb.Models
{
    public class DashboardModel
    {
        public IEnumerable<MonitoringModel> Monitors { get; set; }
    }
}
