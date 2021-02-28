using DowntimeAlerterHelpers;
using DowntimeAlerterWeb.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static DowntimeAlerterHelpers.DateTimeHelpers;
using static IntervalParserLib.IntervalParser;
using Mapster;

namespace DowntimeAlerterWeb.Services
{
    internal class BacklogProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger _logger;
        private readonly IDowntimeAlertService _downtimeAlertService;
        private readonly MonitorLoop _monitorLoop;

        public BacklogProcessingService(ILogger<BacklogProcessingService> logger, 
            IDowntimeAlertService downtimeAlertService, 
            MonitorLoop monitorLoop)
        {
            _logger = logger;
            _downtimeAlertService = downtimeAlertService;
            _monitorLoop = monitorLoop;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                IEnumerable<MonitoringModel> monitors = _downtimeAlertService.GetMonitorList();

                var currentExecutionTotalMinutes = getCurrentExecutionTimeTotalMinutes();

                foreach (var item in monitors)
                {
                    if (item.IsValid)
                    {
                        var totalMinutes = parseInterval(item.Interval).TotalMinutes;
                        var inSprint = (currentExecutionTotalMinutes % totalMinutes) == 0;
                        if (inSprint)
                        {
                            _monitorLoop.AddTask(item.Adapt<SprintTaskInformation>());
                        }
                    }

                }

                executionCount++;

                _logger.LogInformation("Scoped Processing Service is working. Count: {Count}", executionCount);

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
