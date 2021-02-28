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
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Net.Http;

namespace DowntimeAlerterWeb.Services
{
    internal class BacklogProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger _logger;
        private readonly IDowntimeAlertService _downtimeAlertService;
        private readonly MonitorLoop _monitorLoop;

        public BacklogProcessingService(ILogger<BacklogProcessingService> logger, IDowntimeAlertService downtimeAlertService, MonitorLoop monitorLoop)
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

                var nextExecution = DateTime.Now
                    .getNextExecutionTimeTotalMinutes(TimeSpan.Zero);

                foreach (var item in monitors)
                {
                    var totalMinutes = parseInterval(item.Interval).TotalMinutes;
                    var inSprint = (nextExecution % totalMinutes) == 0;
                    if (inSprint)
                    {
                        _monitorLoop.AddTask(item.Adapt<SprintTask>());
                    }
                }

                executionCount++;

                _logger.LogInformation("Scoped Processing Service is working. Count: {Count}", executionCount);

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }

    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }

    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<Func<CancellationToken, Task>> _workItems = new ConcurrentQueue<Func<CancellationToken, Task>>();

        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }

    public class QueuedHostedService : BackgroundService
    {
        private readonly ILogger<QueuedHostedService> _logger;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue,
            ILogger<QueuedHostedService> logger)
        {
            TaskQueue = taskQueue;
            _logger = logger;
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing {WorkItem}.", nameof(workItem));
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }

    public class MonitorLoop
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly CancellationToken _cancellationToken;

        public MonitorLoop(IBackgroundTaskQueue taskQueue,
            ILogger<MonitorLoop> logger,
            IHostApplicationLifetime applicationLifetime, IHttpClientFactory clientFactory)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _clientFactory = clientFactory;
            _cancellationToken = applicationLifetime.ApplicationStopping;
        }

        public void StartMonitorLoop()
        {
            _logger.LogInformation("Monitor Loop is starting.");

            // Run a console user input loop in a background thread
            Task.Run(() => Monitor());
        }

        public void Monitor()
        {
            while (!_cancellationToken.IsCancellationRequested) ; // Application stopping, DO cleanup, teardown here.
        }


        public void AddTask(SprintTask task)
        {
            Func<CancellationToken, Task> httpGet = async token =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, task.Url);
                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
            };

            _logger.LogInformation("Adding task to the queue: Monitor#{Id} {Url}", task.Id, task.Url);
            _taskQueue.QueueBackgroundWorkItem(httpGet);
        }
    }
}
