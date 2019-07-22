using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace DiDemo.Api.Services
{
    public class BackgroundWorkQueue
    {
        private ConcurrentQueue<Func<CancellationToken, Task>> _queue = new ConcurrentQueue<Func<CancellationToken, Task>>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void Enqueue(Func<CancellationToken, Task> work)
        {
            if (work == null)
            {
                throw new ArgumentNullException(nameof(work));
            }

            _queue.Enqueue(work);
            _signal.Release();
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _queue.TryDequeue(out Func<CancellationToken, Task> work);

            return work;
        }
    }
}
