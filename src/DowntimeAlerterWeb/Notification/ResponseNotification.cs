using DowntimeAlerterWeb.Models;
using MediatR;
using System;
using System.Net;

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

    public class FailedResponseNotification : ResponseNotification
    {
        protected FailedResponseNotification(SprintTaskInformation taskInformation, HttpStatusCode httpStatus, Exception exception) : base(taskInformation, httpStatus, exception)
        {
        }
        public static FailedResponseNotification New(SprintTaskInformation taskInformation, HttpStatusCode httpStatus, Exception exception) => new FailedResponseNotification(taskInformation, httpStatus, exception);
    }
}
