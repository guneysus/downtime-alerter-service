using DowntimeAlerterWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerterWeb.Entities
{
    public class Monitor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Interval { get; set; }
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
