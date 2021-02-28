using DowntimeAlerterWeb.Entities;
using DowntimeAlerterWeb.Models;
using DowntimeAlerterWeb.Services;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DowntimeAlerterWeb.Notification
{
    public class ResponseNotification : INotification
    {
        public HttpStatusCode HttpStatus { get; }
        public Exception Exception { get; }
        public SprintTaskInformation TaskInformation { get; }
        public DateTime ExecutedOn { get; }

        protected ResponseNotification(SprintTaskInformation taskInformation, HttpStatusCode httpStatus, Exception exception)
        {
            HttpStatus = httpStatus;
            Exception = exception;
            TaskInformation = taskInformation;
            ExecutedOn = DateTime.Now;
        }

        public static ResponseNotification New(SprintTaskInformation taskInformation, HttpStatusCode httpStatus, Exception exception) => new ResponseNotification(taskInformation, httpStatus, exception);
    }

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

    public class EmailNotificationHandler : INotificationHandler<ResponseNotification>
    {
        private readonly IEmailSender _email;
        private readonly AuthMessageSenderOptions _options;

        public EmailNotificationHandler(IEmailSender email, IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            _email = email;
            _options = optionsAccessor.Value;
        }

        public Task Handle(ResponseNotification notification, CancellationToken cancellationToken)
        {
            string domain = new Uri(notification.TaskInformation.Url).Host;
            string subject = $"{domain} is down";
            string msg = $@"
{domain} seems to be down at {notification.ExecutedOn}
";
            return _email.SendEmailAsync(_options.NoreplyEmail, subject, msg);
        }
    }

}
