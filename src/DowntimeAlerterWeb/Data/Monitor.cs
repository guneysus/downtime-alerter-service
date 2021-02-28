using System;
using System.Collections.Generic;

namespace DowntimeAlerterWeb.Entities
{
    public class Monitor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Interval { get; set; }

        public virtual ICollection<StatusLog> Statuses { get; set; }
    }

    public class StatusLog
    {
        public int Id { get; set; }
        public int MonitorId { get; set; }
        public Monitor Monitor { get; set; }

        public int HttpStatusCode { get; set; }
        public DateTime ExecutedOn { get; set; }
    }

}
