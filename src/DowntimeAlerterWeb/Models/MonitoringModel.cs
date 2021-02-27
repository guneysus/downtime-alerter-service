using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DowntimeAlerterWeb.Models
{
    public class MonitoringModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string URL { get; set; }

        public string Interval { get; set; }
    }

    public class DashboardModel
    {
        public IEnumerable<MonitoringModel> Monitors { get; set; }
    }
}

