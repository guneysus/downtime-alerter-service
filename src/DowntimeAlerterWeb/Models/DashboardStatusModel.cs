using System.Collections.Generic;

namespace DowntimeAlerterWeb.Models
{
    public class DashboardStatusModel
    {
        public IEnumerable<StatusModel> Statuses { get; set; }
    }
}
