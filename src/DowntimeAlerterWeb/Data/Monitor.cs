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

        public string URL { get; set; }

        public string Interval { get; set; }
    }

}
