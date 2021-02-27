using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DowntimeAlerterWeb.Services
{
    internal class BacklogProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger _logger;
        private readonly IDowntimeAlertService _downtimeAlertService;

        public BacklogProcessingService(ILogger<BacklogProcessingService> logger, IDowntimeAlertService downtimeAlertService)
        {
            _logger = logger;
            this._downtimeAlertService = downtimeAlertService;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var monitors = _downtimeAlertService.GetMonitorList();

                executionCount++;

                _logger.LogInformation("Scoped Processing Service is working. Count: {Count}", executionCount);

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
