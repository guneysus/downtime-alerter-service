using DowntimeAlerterWeb.Services;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DowntimeAlerterWeb.Notification
{
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
            return _email.SendEmailAsync(_options.NotificationEmail, subject, msg);
        }
    }

}
