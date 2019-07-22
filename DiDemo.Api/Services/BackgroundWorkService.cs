using DiDemo.Abstractions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiDemo.Api.Services
{
    public class BackgroundWorkService : BackgroundService
    {
        private readonly BackgroundWorkQueue _queue;
        private readonly ILogger _logger;
        private ConcurrentDictionary<Guid, Task> _executingWork = new ConcurrentDictionary<Guid, Task>();

        public BackgroundWorkService(BackgroundWorkQueue queue, ILogger logger)
        {
            _queue = queue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Func<CancellationToken, Task> work = await _queue.DequeueAsync(cancellationToken);
                    RunWork(work, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.ToString());
                }
            }

            var executingWork = _executingWork.ToArray().Select(wt => wt.Value);
            const int timeout = 3000;
            await Task.WhenAny(Task.WhenAll(executingWork), Task.Delay(timeout));
        }

        private void RunWork(Func<CancellationToken, Task> work, CancellationToken cancellationToken)
        {
            var workId = Guid.NewGuid();
            Task workTask = work(cancellationToken); // Start execution
            _executingWork.TryAdd(workId, workTask); // Save the task in the "list"
            workTask.ContinueWith(t => _executingWork.TryRemove(workId, out Task _)); // Remove the task when we are done
        }
    }
}
