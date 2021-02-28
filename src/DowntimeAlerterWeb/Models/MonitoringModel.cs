using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace DowntimeAlerterWeb.Models
{
    public class MonitoringModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public string Interval { get; set; }

        [NotMapped]
        public bool IsValid => !(string.IsNullOrEmpty(Interval) || string.IsNullOrEmpty(Url));
    }

    public class SprintTaskInformation
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }



    public class DashboardModel
    {
        public IEnumerable<MonitoringModel> Monitors { get; set; }
    }
}

