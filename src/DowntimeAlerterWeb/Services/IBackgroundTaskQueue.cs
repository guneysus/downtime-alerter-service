﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace DowntimeAlerterWeb.Services
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
