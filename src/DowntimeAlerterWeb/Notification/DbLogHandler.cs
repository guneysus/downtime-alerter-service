using DowntimeAlerterWeb.Entities;
using DowntimeAlerterWeb.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DowntimeAlerterWeb.Notification
{
    public class DbLogHandler : INotificationHandler<ResponseNotification>
    {
        private readonly IServiceProvider _services;

        public DbLogHandler(IServiceProvider services)
        {
            _services = services;
        }

        public Task Handle(ResponseNotification notification, CancellationToken cancellationToken)
        {
            using (var scope = _services.CreateScope())
            {
                var downtimeService = 
                    scope.ServiceProvider
                    .GetRequiredService<IDowntimeAlertService>();

                var statusLog = new StatusLog
                {
                    ExecutedOn = notification.ExecutedOn,
                    MonitorId = notification.TaskInformation.Id,
                    HttpStatusCode = (int)notification.HttpStatus
                };

                downtimeService.LogStatus(statusLog);
            }

            return Task.CompletedTask;
        }
    }

}
