using System;
using System.Threading;

namespace Sync_Scopes
{
    public class ScopedSemaphoreSlim : ScopedPrimitive
    {
        private readonly SemaphoreSlim _semaphore;

        public ScopedSemaphoreSlim(int initialCount)
        {
            _semaphore = new SemaphoreSlim(initialCount);
        }

        public ScopedSemaphoreSlim(SemaphoreSlim semaphoreSlim)
        {
            _semaphore = semaphoreSlim;
        }

        public override SynchronizationScope CreateScope()
        {
            _semaphore.Wait();
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(int milisecondTimeout)
        {
            _semaphore.Wait(milisecondTimeout);
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(TimeSpan timeout)
        {
            _semaphore.Wait(timeout);
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(int milisecondTimeout, bool exitContext)
        {
            return CreateScope(milisecondTimeout);
        }

        public override SynchronizationScope CreateScope(TimeSpan timeout, bool exitContext)
        {
            return CreateScope(timeout);
        }

        public override void ReleaseLock()
        {
            _semaphore.Release();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                if (disposing)
                {
                    _semaphore.Dispose();
                }
                disposedValue = true;
            }
        }
    }
}
