using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DowntimeAlerterWeb.Services
{
    public class DowntimeAlertHostedService : BackgroundService
    {
        private readonly ILogger<DowntimeAlertHostedService> _logger;

        public DowntimeAlertHostedService(IServiceProvider services,
            ILogger<DowntimeAlertHostedService> logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service running.");

            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                BacklogProcessingService backlogService = scope.ServiceProvider.GetRequiredService<BacklogProcessingService>();

                await backlogService.DoWork(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}
